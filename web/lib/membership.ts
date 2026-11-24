import { base44 } from '@/lib/base44Client';

export type MembershipStatus = 'none' | 'active' | 'inactive' | 'revoked';

/** Your GoHighLevel sales/checkout page. Set NEXT_PUBLIC_JOIN_URL in .env.local. */
export const JOIN_URL = process.env.NEXT_PUBLIC_JOIN_URL || '';

/** Asks the server to re-check this user's access. */
export async function syncMembership(): Promise<{ status: MembershipStatus; admin: boolean }> {
  const res = await base44.functions.invoke('sync-membership', {});
  return res.data;
}
