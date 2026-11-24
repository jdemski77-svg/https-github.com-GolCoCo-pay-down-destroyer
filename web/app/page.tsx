'use client';

import { useCallback, useEffect, useState } from 'react';
import { base44 } from '@/lib/base44Client';
import { syncMembership, type MembershipStatus } from '@/lib/membership';
import LoginScreen from '@/components/LoginScreen';
import AppScreen, { type AppUser } from '@/components/AppScreen';
import Paywall from '@/components/Paywall';

type Gate =
  | { kind: 'loading' }
  | { kind: 'loggedOut' }
  | { kind: 'member'; user: AppUser; isAdmin: boolean }
  | { kind: 'notMember'; user: AppUser; status: MembershipStatus };

/**
 * The members-area gate:
 *  - logged out          → landing page (Join + login)
 *  - logged in, unpaid   → paywall
 *  - logged in, member   → the Pay Down tool
 * Membership is re-checked on the server each time the app opens; the data itself is
 * also locked to active members on the server, so this screen is not the only guard.
 */
export default function Home() {
  const [gate, setGate] = useState<Gate>({ kind: 'loading' });

  const refresh = useCallback(async () => {
    setGate({ kind: 'loading' });
    if (!(await base44.auth.isAuthenticated().catch(() => false))) {
      setGate({ kind: 'loggedOut' });
      return;
    }
    try {
      const me = await base44.auth.me();
      const user: AppUser = { id: me.id, email: me.email, full_name: me.full_name };
      const { status, admin } = await syncMembership().catch(() => ({
        status: (me.membership_status ?? 'none') as MembershipStatus,
        admin: me.role === 'admin',
      }));
      if (status === 'active' || admin) setGate({ kind: 'member', user, isAdmin: admin });
      else setGate({ kind: 'notMember', user, status });
    } catch {
      setGate({ kind: 'loggedOut' });
    }
  }, []);

  useEffect(() => {
    refresh();
  }, [refresh]);

  if (gate.kind === 'loading') {
    return <div className="brand-bg flex h-screen items-center justify-center text-white/70">Loading…</div>;
  }
  if (gate.kind === 'loggedOut') return <LoginScreen onLoggedIn={refresh} />;
  if (gate.kind === 'notMember') return <Paywall status={gate.status} email={gate.user.email} />;
  return <AppScreen user={gate.user} isAdmin={gate.isAdmin} />;
}
