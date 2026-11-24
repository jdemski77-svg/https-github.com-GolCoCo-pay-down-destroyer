// Admin-only member management.
//   { action: "list" }                                   → all users with membership info
//   { action: "revoke", userId, cancelSubscription? }    → block access now (optionally stop billing)
//   { action: "restore", userId }                        → undo a revoke; access follows their payments again
//   { action: "grant", userId }                          → free access without paying
import { createClientFromRequest } from "npm:@base44/sdk";
import { json, syncUser } from "../../shared/membership.ts";

export default async function (req: Request): Promise<Response> {
  const base44 = createClientFromRequest(req);
  let me;
  try {
    me = await base44.auth.me();
  } catch {
    return json({ error: "unauthenticated" }, 401);
  }
  if (me?.role !== "admin") return json({ error: "forbidden" }, 403);

  const body = await req.json().catch(() => ({}));
  const users = base44.asServiceRole.entities.User;

  if (body.action === "list") {
    const all = await users.list("-created_date", 1000);
    return json({
      // deno-lint-ignore no-explicit-any
      members: all.map((u: any) => ({
        id: u.id,
        email: u.email,
        full_name: u.full_name ?? null,
        role: u.role,
        created_date: u.created_date,
        membership_status: u.membership_status ?? "none",
        membership_source: u.membership_source ?? null,
        has_subscription: Boolean(u.membership_subscription_id),
      })),
    });
  }

  if (typeof body.userId !== "string" || !body.userId) return json({ error: "missing_user" }, 400);
  const target = await users.get(body.userId);
  if (!target) return json({ error: "not_found" }, 404);

  if (body.action === "revoke") {
    let subscription: string | null = null;
    if (body.cancelSubscription && target.membership_subscription_id) {
      try {
        const res = await base44.asServiceRole.payments.cancelSubscription(target.membership_subscription_id);
        subscription = res.status;
      } catch (err) {
        console.error("cancelSubscription failed", err);
        // deno-lint-ignore no-explicit-any
        subscription = `cancel_failed: ${(err as any)?.code ?? "unknown"}`;
      }
    }
    await users.update(target.id, { membership_status: "revoked" });
    return json({ ok: true, membership_status: "revoked", subscription });
  }

  if (body.action === "grant") {
    await users.update(target.id, { membership_status: "active", membership_source: "manual" });
    return json({ ok: true, membership_status: "active" });
  }

  if (body.action === "restore") {
    // Clear the block, then recompute from their payments right away.
    await users.update(target.id, { membership_status: "inactive", membership_source: "purchase" });
    const status = await syncUser(base44, { ...target, membership_status: "inactive", membership_source: "purchase" });
    return json({ ok: true, membership_status: status });
  }

  return json({ error: "unknown_action" }, 400);
}
