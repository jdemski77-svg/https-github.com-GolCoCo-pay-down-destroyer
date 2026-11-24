// Membership rules shared by every backend function.
// Payments happen in GoHighLevel. GoHighLevel tells us who paid (ghl-webhook), which is
// stored in MemberAccess by email. A user's access always follows the MemberAccess
// record for their email, copied onto their User record so data access rules can check it.

export type AccessStatus = "active" | "inactive" | "revoked";
export type MembershipStatus = "none" | AccessStatus;

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

/** Creates or updates the access record for an email. */
export async function setAccess(base44: Client, email: string, patch: Row): Promise<Row> {
  const existing = await findAccess(base44, email);
  const Access = base44.asServiceRole.entities.MemberAccess;
  return existing ? await Access.update(existing.id, patch) : await Access.create({ email, ...patch });
}

/** Finds the app user with this email, if they've created an account. */
export async function findUserByEmail(base44: Client, email: string): Promise<Row | null> {
  const Users = base44.asServiceRole.entities.User;
  let rows = await Users.filter({ email });
  if (!rows?.length) {
    // Accounts may have been created with different capitalization.
    const all = await Users.list("-created_date", 5000);
    rows = all.filter((u: Row) => String(u.email).toLowerCase() === email);
  }
  return rows?.[0] ?? null;
}

/** Copies the access status for the user's email onto their User record. */
export async function applyToUser(base44: Client, user: Row, status: MembershipStatus): Promise<void> {
  if (user.membership_status === status && user.membership_checked_at) return;
  await base44.asServiceRole.entities.User.update(user.id, {
    membership_status: status,
    membership_checked_at: new Date().toISOString(),
  });
}

/** Re-reads this user's access from MemberAccess and saves it on the user. */
export async function syncUser(base44: Client, user: Row): Promise<MembershipStatus> {
  const email = normalizeEmail(user.email);
  const access = email ? await findAccess(base44, email) : null;
  const status: MembershipStatus = (access?.status as AccessStatus) ?? "none";
  await applyToUser(base44, user, status);
  return status;
}

/** Sets access for an email and, if they already have an account, updates it right away. */
export async function setAccessAndUser(base44: Client, email: string, patch: Row): Promise<Row> {
  const record = await setAccess(base44, email, patch);
  const user = await findUserByEmail(base44, email);
  if (user) await applyToUser(base44, user, record.status);
  return record;
}

export function json(body: unknown, status = 200): Response {
  return Response.json(body, { status });
}

/** Constant-time string comparison for the webhook key. */
export function safeEqual(a: string, b: string): boolean {
  const ea = new TextEncoder().encode(a);
  const eb = new TextEncoder().encode(b);
  let diff = ea.length ^ eb.length;
  for (let i = 0; i < Math.max(ea.length, eb.length); i++) diff |= (ea[i] ?? 0) ^ (eb[i] ?? 0);
  return diff === 0;
}
