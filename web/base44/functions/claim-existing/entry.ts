// Called right after someone signs up (or logs in) through the existing-customer
// link. Tags their account as an existing customer, but only before the deadline,
// so the tag can't be claimed after the link has expired.
import { createClientFromRequest } from "npm:@base44/sdk";
import { EXISTING_CUSTOMER_DEADLINE, json } from "../../shared/membership.ts";

export default async function (req: Request): Promise<Response> {
  const base44 = createClientFromRequest(req);
  let user;
  try {
    user = await base44.auth.me();
  } catch {
    return json({ error: "unauthenticated" }, 401);
  }
  if (!user) return json({ error: "unauthenticated" }, 401);

  if (new Date() >= EXISTING_CUSTOMER_DEADLINE) return json({ error: "expired" }, 410);

  if (user.signup_source !== "existing_customer") {
    await base44.asServiceRole.entities.User.update(user.id, { signup_source: "existing_customer" });
  }
  return json({ ok: true });
}
