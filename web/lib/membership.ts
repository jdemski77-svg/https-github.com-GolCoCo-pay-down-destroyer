import { base44 } from '@/lib/base44Client';

export type MembershipStatus = 'none' | 'active' | 'inactive' | 'revoked';

/** Asks the backend to re-check this user's payment. */
export async function syncMembership(): Promise<{ status: MembershipStatus; admin: boolean }> {
  const res = await base44.functions.invoke('sync-membership', {});
  return res.data;
}

/** Creates a checkout and sends the browser to the payment page. */
export async function goToCheckout(): Promise<void> {
  const res = await base44.functions.invoke('start-checkout', {});
  if (!res.data?.url) throw new Error('Could not start checkout.');
  window.location.assign(res.data.url);
}
