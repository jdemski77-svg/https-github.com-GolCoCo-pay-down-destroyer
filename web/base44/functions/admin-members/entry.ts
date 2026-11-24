// Admin-only member management.
//   { action: "list" }               → everyone with access records and/or accounts
//   { action: "revoke", email }      → turn access off now
//   { action: "grant", email }       → turn access on (also adds someone by email)
import { createClientFromRequest } from "npm:@base44/sdk";
import { json, normalizeEmail, setAccessAndUser } from "../../shared/membership.ts";

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

  if (body.action === "list") {
    const [users, access] = await Promise.all([
      base44.asServiceRole.entities.User.list("-created_date", 5000),
      base44.asServiceRole.entities.MemberAccess.list("-updated_date", 5000),
    ]);
    // deno-lint-ignore no-explicit-any
    const byEmail = new Map<string, any>();
    // deno-lint-ignore no-explicit-any
    for (const a of access as any[]) {
      byEmail.set(a.email, {
        email: a.email,
        full_name: a.full_name ?? null,
        status: a.status,
        source: a.source ?? null,
        has_account: false,
        role: null,
        joined: null,
        updated: a.updated_date ?? a.created_date,
      });
    }
    // deno-lint-ignore no-explicit-any
    for (const u of users as any[]) {
      const email = String(u.email).toLowerCase();
      const row = byEmail.get(email) ?? { email, status: "none", source: null, updated: null };
      byEmail.set(email, {
        ...row,
        full_name: u.full_name || row.full_name || null,
        has_account: true,
        role: u.role,
        joined: u.created_date,
      });
    }
    return json({ members: [...byEmail.values()] });
  }

  const email = normalizeEmail(body.email);
  if (!email) return json({ error: "Enter a valid email address." }, 400);

  if (body.action === "revoke" || body.action === "grant") {
    const status = body.action === "grant" ? "active" : "revoked";
    const record = await setAccessAndUser(base44, email, {
      status,
      source: "admin",
      last_event: `admin:${body.action} by ${me.email} ${new Date().toISOString()}`,
    });
    return json({ ok: true, email, status: record.status });
  }

  return json({ error: "unknown_action" }, 400);
}
