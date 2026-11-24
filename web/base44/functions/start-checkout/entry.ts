// Starts a membership checkout. Works for anonymous visitors (the "Join" button)
// and for logged-in users who don't have an active membership yet.
// Returns { url } — the page sends the buyer there to pay.
import { createClientFromRequest, type CheckoutItem } from "npm:@base44/sdk";
import { json, PLAN, PRODUCT_TAG } from "../../shared/membership.ts";

export default async function (req: Request): Promise<Response> {
  const base44 = createClientFromRequest(req);

  // If someone is logged in, prefill their email so the payment matches their account.
  let email: string | undefined;
  try {
    const user = await base44.auth.me();
    if (user?.membership_status === "active") return json({ error: "already_member" }, 409);
    email = user?.email;
  } catch {
    // Anonymous visitor — that's fine.
  }

  const item: CheckoutItem = {
    name: PLAN.name,
    amount: PLAN.amountCents,
    ...(PLAN.frequency ? { recurring: { frequency: PLAN.frequency } } : {}),
  };

  try {
    const checkout = await base44.asServiceRole.payments.createCheckout({
      items: [item],
      currency: PLAN.currency,
      success_path: "/join/",
      cancel_path: "/",
      metadata: { product: PRODUCT_TAG },
      presentation: "redirect",
      ...(email ? { buyer: { email } } : {}),
    });
    return json({ url: checkout.redirect_url });
  } catch (err) {
    console.error("createCheckout failed", err);
    // deno-lint-ignore no-explicit-any
    return json({ error: (err as any)?.code ?? "checkout_failed" }, 502);
  }
}
