// Webhook for GoHighLevel workflows. Point a GoHighLevel "Webhook" action at:
//
//   https://<your-app-domain>/functions/ghl-webhook?key=<GHL_WEBHOOK_SECRET>&action=grant
//
//   action=grant   → payment received: give this contact access
//   action=revoke  → subscription canceled / refunded: take access away
//
// GoHighLevel sends the contact's details in the body; we use the email
// (body.email, body.contact.email, or ?email=...). The key must match the
// GHL_WEBHOOK_SECRET secret in the Base44 dashboard, so nobody else can call this.
import { createClientFromRequest } from "npm:@base44/sdk";
import { secrets } from "base44:runtime";
import { findAccess, json, normalizeEmail, safeEqual, setAccessAndUser } from "../../shared/membership.ts";

export default async function (req: Request): Promise<Response> {
  if (req.method !== "POST") return json({ error: "method_not_allowed" }, 405);

  const url = new URL(req.url);
  const expected = secrets.get("GHL_WEBHOOK_SECRET");
  const key = url.searchParams.get("key") ?? req.headers.get("x-webhook-key") ?? "";
  if (!expected || !safeEqual(key, expected)) return json({ error: "unauthorized" }, 401);

  // deno-lint-ignore no-explicit-any
  const body: any = await req.json().catch(() => ({}));
  const action = (url.searchParams.get("action") ?? body.action ?? "").toLowerCase();
  if (action !== "grant" && action !== "revoke") return json({ error: "action must be grant or revoke" }, 400);

  const email = normalizeEmail(body.email ?? body.contact?.email ?? url.searchParams.get("email"));
  if (!email) return json({ error: "missing or invalid email" }, 400);

  const fullName =
    body.full_name ?? body.contact?.full_name ?? ([body.first_name, body.last_name].filter(Boolean).join(" ") || undefined);
  const contactId = body.contact_id ?? body.contact?.id ?? undefined;

  const base44 = createClientFromRequest(req);

  // An admin's revoke wins: a later payment from GoHighLevel doesn't undo it.
  // Use Restore in the Members tab to let them back in.
  const existing = await findAccess(base44, email);
  if (action === "grant" && existing?.status === "revoked") {
    return json({ ok: true, email, status: "revoked", note: "revoked by admin; not changed" });
  }

  const record = await setAccessAndUser(base44, email, {
    status: action === "grant" ? "active" : "inactive",
    source: "ghl",
    last_event: `ghl:${action} ${new Date().toISOString()}`,
    ...(fullName ? { full_name: String(fullName).slice(0, 200) } : {}),
    ...(contactId ? { ghl_contact_id: String(contactId).slice(0, 100) } : {}),
  });

  return json({ ok: true, email, status: record.status });
}
