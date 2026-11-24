'use client';

import { useState } from 'react';
import AuthCard from '@/components/AuthCard';
import { base44 } from '@/lib/base44Client';
import { goToCheckout, type MembershipStatus } from '@/lib/membership';

/** Shown to logged-in users without an active membership. */
export default function Paywall({ status, email }: { status: MembershipStatus; email: string }) {
  const [busy, setBusy] = useState(false);
  const [error, setError] = useState('');

  async function handleJoin() {
    setError('');
    setBusy(true);
    try {
      await goToCheckout();
    } catch {
      setError('Could not open the payment page. Please try again in a moment.');
      setBusy(false);
    }
  }

  async function handleLogout() {
    await base44.auth.logout('/');
  }

  return (
    <AuthCard title="Membership">
      {status === 'revoked' ? (
        <>
          <p className="text-sm font-semibold text-brand-red">Your access to Pay Down has been turned off.</p>
          <p className="text-sm text-gray-600">If you think this is a mistake, please contact us.</p>
        </>
      ) : (
        <>
          <p className="text-sm font-semibold text-gray-700">
            {status === 'inactive' ? 'Your membership is no longer active.' : 'You need a membership to use Pay Down.'}
          </p>
          <p className="text-xs text-gray-500">
            Pay with the email <span className="font-semibold">{email}</span> so it&apos;s linked to this account.
          </p>
          {error && <p className="text-sm text-brand-red">{error}</p>}
          <button
            onClick={handleJoin}
            disabled={busy}
            className="btn-primary"
          >
            {busy ? 'Opening secure checkout…' : status === 'inactive' ? 'Renew membership' : 'Join now'}
          </button>
        </>
      )}
      <button onClick={handleLogout} className="text-left text-xs text-brand-blue underline">
        Log out
      </button>
    </AuthCard>
  );
}
