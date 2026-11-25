'use client';

import AuthCard from '@/components/AuthCard';
import { base44 } from '@/lib/base44Client';

/** Shown to a member whose access an admin has turned off. */
export default function Paywall({ onCheckAgain }: { onCheckAgain: () => void }) {
  return (
    <AuthCard title="Pay Down Blueprint App">
      <p className="text-sm font-semibold text-brand-red">Your access to the Pay Down Blueprint App has been turned off.</p>
      <p className="text-sm text-gray-600">If you think this is a mistake, please contact us.</p>
      <button onClick={onCheckAgain} className="btn-secondary">
        Check again
      </button>
      <button onClick={() => base44.auth.logout('/')} className="text-left text-xs text-brand-blue underline">
        Log out
      </button>
    </AuthCard>
  );
}
