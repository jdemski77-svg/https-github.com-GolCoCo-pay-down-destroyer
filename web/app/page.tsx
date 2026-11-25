'use client';

import { useCallback, useEffect, useState } from 'react';
import { base44 } from '@/lib/base44Client';
import { syncMembership, type MembershipStatus } from '@/lib/membership';
import LoginScreen from '@/components/LoginScreen';
import AppScreen, { type AppUser } from '@/components/AppScreen';
import Paywall from '@/components/Paywall';
import JoinScreen from '@/components/JoinScreen';
import ResetPasswordScreen from '@/components/ResetPasswordScreen';

type Gate =
  | { kind: 'loading' }
  | { kind: 'loggedOut' }
  | { kind: 'member'; user: AppUser; isAdmin: boolean }
  | { kind: 'notMember'; user: AppUser; status: MembershipStatus };

/**
 * The members-area gate:
 *  - logged out   → landing page (create account + login)
 *  - revoked      → "access turned off" screen
 *  - otherwise    → the Pay Down tool
 * Access is re-checked on the server each time the app opens; saved data is also
 * locked on the server, so this screen is not the only guard.
 */
/**
 * Base44 hosting serves this one page for every address, so the sign-up and
 * password-reset screens are picked here from the URL path.
 */
function screenForPath(): 'join' | 'reset' | 'home' {
  const path = window.location.pathname.replace(/\/+$/, '');
  if (path === '/join') return 'join';
  if (path === '/reset-password') return 'reset';
  return 'home';
}

export default function Home() {
  const [screen, setScreen] = useState<'join' | 'reset' | 'home' | null>(null);
  useEffect(() => setScreen(screenForPath()), []);
  if (screen === null) {
    return <div className="brand-bg flex h-screen items-center justify-center text-white/70">Loading…</div>;
  }
  if (screen === 'join') return <JoinScreen />;
  if (screen === 'reset') return <ResetPasswordScreen />;
  return <MembersGate />;
}

function MembersGate() {
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
        status: (me.membership_status === 'revoked' ? 'revoked' : 'active') as MembershipStatus,
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
  if (gate.kind === 'notMember') return <Paywall onCheckAgain={refresh} />;
  return <AppScreen user={gate.user} isAdmin={gate.isAdmin} />;
}
