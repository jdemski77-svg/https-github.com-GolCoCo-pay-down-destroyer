// Called by the app every time a logged-in user opens it.
// Re-checks their payments and returns their membership status.
import { createClientFromRequest } from "npm:@base44/sdk";
import { json, syncUser } from "../../shared/membership.ts";

export default async function (req: Request): Promise<Response> {
  const base44 = createClientFromRequest(req);
  let user;
  try {
    user = await base44.auth.me();
  } catch {
    return json({ error: "unauthenticated" }, 401);
  }
  if (!user) return json({ error: "unauthenticated" }, 401);

  if (user.role === "admin") return json({ status: "active", admin: true });

  try {
    const status = await syncUser(base44, user);
    return json({ status, admin: false });
  } catch (err) {
    console.error("sync failed", err);
    // Fall back to the last saved status rather than locking a paying member out.
    return json({ status: user.membership_status ?? "none", admin: false, stale: true });
  }
}
