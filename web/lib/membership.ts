import { base44 } from '@/lib/base44Client';

export type MembershipStatus = 'active' | 'revoked';

/** Asks the server to re-check this user's access (everyone is active unless revoked). */
export async function syncMembership(): Promise<{ status: MembershipStatus; admin: boolean }> {
  const res = await base44.functions.invoke('sync-membership', {});
  return res.data;
}
