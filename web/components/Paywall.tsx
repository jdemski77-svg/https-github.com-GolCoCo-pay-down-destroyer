'use client';

import { useState } from 'react';
import AuthCard from '@/components/AuthCard';
import { base44 } from '@/lib/base44Client';
import { JOIN_URL, type MembershipStatus } from '@/lib/membership';

/** Shown to logged-in users without active access. */
export default function Paywall({
  status,
  email,
  onCheckAgain,
}: {
  status: MembershipStatus;
  email: string;
  onCheckAgain: () => void;
}) {
  const [checking, setChecking] = useState(false);

  async function handleLogout() {
    await base44.auth.logout('/');
  }

  return (
    <AuthCard title="Membership">
      {status === 'revoked' ? (
        <>
          <p className="text-sm font-semibold text-brand-red">Your access to the members area has been turned off.</p>
          <p className="text-sm text-gray-600">If you think this is a mistake, please contact us.</p>
        </>
      ) : (
        <>
          <p className="text-sm font-semibold text-gray-700">
            {status === 'inactive'
              ? 'Your membership is no longer active.'
              : `We don't see a membership for ${email} yet.`}
          </p>
          {status === 'none' && (
            <p className="text-xs text-gray-500">
              Just paid? It can take a minute to come through — click <b>Check again</b>. Make sure you paid with this same
              email address.
            </p>
          )}
          <button
            onClick={() => {
              setChecking(true);
              onCheckAgain();
            }}
            disabled={checking}
            className="btn-primary"
          >
            {checking ? 'Checking…' : 'Check again'}
          </button>
          {JOIN_URL && (
            <a href={JOIN_URL} className="btn-secondary text-center">
              {status === 'inactive' ? 'Renew membership' : 'Join now'}
            </a>
          )}
        </>
      )}
      <button onClick={handleLogout} className="text-left text-xs text-brand-blue underline">
        Log out
      </button>
    </AuthCard>
  );
}
