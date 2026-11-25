// Membership rules shared by every backend function.
// The members area is open: anyone who creates an account gets in, unless an
// admin has revoked them. Revokes are stored in MemberAccess by email and copied
// onto the user's account so the data access rules can enforce them.

export type MembershipStatus = "active" | "revoked";

// deno-lint-ignore no-explicit-any
type Client = any;
// deno-lint-ignore no-explicit-any
type Row = Record<string, any>;

export function normalizeEmail(email: unknown): string | null {
  if (typeof email !== "string") return null;
  const e = email.trim().toLowerCase();
  return /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(e) ? e : null;
}

export async function findAccess(base44: Client, email: string): Promise<Row | null> {
  const rows = await base44.asServiceRole.entities.MemberAccess.filter({ email });
  return rows?.[0] ?? null;
}

/** Finds the app user with this email, if they've created an account. */
export async function findUserByEmail(base44: Client, email: string): Promise<Row | null> {
  const Users = base44.asServiceRole.entities.User;
  let rows = await Users.filter({ email });
  if (!rows?.length) {
    const all = await Users.list("-created_date", 5000);
    rows = all.filter((u: Row) => String(u.email).toLowerCase() === email);
  }
  return rows?.[0] ?? null;
}

async function applyToUser(base44: Client, user: Row, status: MembershipStatus): Promise<void> {
  if (user.membership_status === status && user.membership_checked_at) return;
  await base44.asServiceRole.entities.User.update(user.id, {
    membership_status: status,
    membership_checked_at: new Date().toISOString(),
  });
}

/** Everyone is active unless an admin revoked their email. Saves the result on the user. */
export async function syncUser(base44: Client, user: Row): Promise<MembershipStatus> {
  const email = normalizeEmail(user.email);
  const access = email ? await findAccess(base44, email) : null;
  const status: MembershipStatus = access?.status === "revoked" ? "revoked" : "active";
  await applyToUser(base44, user, status);
  return status;
}

/** Revokes or restores an email, and updates their account right away if they have one. */
export async function setStatus(base44: Client, email: string, status: MembershipStatus, by: string): Promise<void> {
  const Access = base44.asServiceRole.entities.MemberAccess;
  const existing = await findAccess(base44, email);
  const patch = { status, source: "admin", last_event: `admin:${status} by ${by} ${new Date().toISOString()}` };
  if (existing) await Access.update(existing.id, patch);
  else await Access.create({ email, ...patch });
  const user = await findUserByEmail(base44, email);
  if (user) await applyToUser(base44, user, status);
}

export function json(body: unknown, status = 200): Response {
  return Response.json(body, { status });
}
