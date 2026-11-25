// Admin-only member management.
//   { action: "list" }            → everyone who has signed up, with their status
//   { action: "revoke", email }   → lock them out now
//   { action: "restore", email }  → let them back in
import { createClientFromRequest } from "npm:@base44/sdk";
import { json, normalizeEmail, setStatus } from "../../shared/membership.ts";

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
    const revoked = new Set((access as any[]).filter((a) => a.status === "revoked").map((a) => a.email));
    return json({
      // deno-lint-ignore no-explicit-any
      members: (users as any[]).map((u) => ({
        email: String(u.email).toLowerCase(),
        full_name: u.full_name ?? null,
        role: u.role,
        joined: u.created_date,
        status: revoked.has(String(u.email).toLowerCase()) ? "revoked" : "active",
      })),
    });
  }

  const email = normalizeEmail(body.email);
  if (!email) return json({ error: "Enter a valid email address." }, 400);

  if (body.action === "revoke" || body.action === "restore") {
    const status = body.action === "revoke" ? "revoked" : "active";
    await setStatus(base44, email, status, me.email);
    return json({ ok: true, email, status });
  }

  return json({ error: "unknown_action" }, 400);
}
