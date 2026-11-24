// Membership settings and helpers shared by every backend function.

// ─── EDIT THESE to set what members pay ─────────────────────────────────────
export const PLAN = {
  name: "Pay Down Membership",
  amountCents: 1999, // $19.99 — PLACEHOLDER, set your real price
  currency: "usd",
  // "month" or "year" for a subscription, or null for a single one-time payment.
  frequency: "month" as "month" | "year" | null,
};
// ────────────────────────────────────────────────────────────────────────────

// Tag stored on every checkout so we only ever count purchases of this membership.
export const PRODUCT_TAG = "paydown-membership";

export type MembershipStatus = "none" | "active" | "inactive" | "revoked";

// deno-lint-ignore no-explicit-any
type Client = any;
// deno-lint-ignore no-explicit-any
type Purchase = any;

/** Lists purchases (service role), following cursors, up to `maxPages` pages. */
export async function listAll(base44: Client, params: Record<string, string>, maxPages = 5): Promise<Purchase[]> {
  const out: Purchase[] = [];
  let cursor: string | undefined;
  for (let i = 0; i < maxPages; i++) {
    const page = await base44.asServiceRole.payments.listPurchases({ ...params, limit: 100, cursor });
    out.push(...(page.items ?? []));
    if (!page.next_cursor) break;
    cursor = page.next_cursor;
  }
  return out;
}

export function isMembershipPurchase(p: Purchase): boolean {
  return p?.metadata?.product === PRODUCT_TAG;
}

/**
 * Re-checks a user's payments and saves the result on their User record.
 * - "revoked" sticks until an admin restores the user.
 * - Free access granted by an admin (source "manual") is kept as-is.
 * - Otherwise: active while a membership purchase is paid, else inactive.
 */
export async function syncUser(base44: Client, user: Record<string, any>): Promise<MembershipStatus> {
  const current = (user.membership_status ?? "none") as MembershipStatus;
  if (current === "revoked") return "revoked";
  if (current === "active" && user.membership_source === "manual") return "active";

  const purchases: Purchase[] = [];
  if (user.membership_subscription_id) {
    purchases.push(...(await listAll(base44, { subscription_id: user.membership_subscription_id }, 1)));
  }
  purchases.push(...(await listAll(base44, { buyer_email: user.email })));

  const mine = purchases.filter(isMembershipPurchase);
  const paid = mine.find((p) => p.status === "paid");
  const status: MembershipStatus = paid ? "active" : mine.length || current !== "none" ? "inactive" : "none";

  const patch: Record<string, unknown> = {
    membership_status: status,
    membership_checked_at: new Date().toISOString(),
  };
  if (paid) {
    patch.membership_source = "purchase";
    if (paid.subscription_id) patch.membership_subscription_id = paid.subscription_id;
  }
  await base44.asServiceRole.entities.User.update(user.id, patch);
  return status;
}

export function json(body: unknown, status = 200): Response {
  return Response.json(body, { status });
}
