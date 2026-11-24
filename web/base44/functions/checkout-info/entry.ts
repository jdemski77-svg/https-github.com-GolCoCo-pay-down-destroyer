// Called by the /join page right after payment, before the buyer has an account.
// Given the checkout id from the return URL, says whether it's paid and which email
// paid, so the sign-up form can be prefilled with the email that must be used.
import { createClientFromRequest } from "npm:@base44/sdk";
import { isMembershipPurchase, json } from "../../shared/membership.ts";

export default async function (req: Request): Promise<Response> {
  const base44 = createClientFromRequest(req);
  const { checkout_id } = await req.json().catch(() => ({}));
  if (typeof checkout_id !== "string" || !checkout_id.trim()) {
    return json({ error: "missing_checkout_id" }, 400);
  }

  const { items } = await base44.asServiceRole.payments.listPurchases({ checkout_id: checkout_id.trim() });
  const purchase = items?.[0];
  if (!purchase || !isMembershipPurchase(purchase)) return json({ error: "not_found" }, 404);

  return json({ status: purchase.status, email: purchase.buyer_email ?? null });
}
