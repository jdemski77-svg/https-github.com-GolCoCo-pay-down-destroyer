'use client';

import { useState } from 'react';
import { base44, errorStatus } from '@/lib/base44Client';

/** Member's account page: shows their details and lets them change their password. */
export default function AccountPanel({ user }: { user: { id: string; email: string; full_name?: string | null } }) {
  const [current, setCurrent] = useState('');
  const [next, setNext] = useState('');
  const [confirm, setConfirm] = useState('');
  const [error, setError] = useState('');
  const [info, setInfo] = useState('');
  const [busy, setBusy] = useState(false);

  async function handleChange(e: React.FormEvent) {
    e.preventDefault();
    setError('');
    setInfo('');
    if (!current) return setError('Enter your current password.');
    if (next.length < 8) return setError('New password must be at least 8 characters.');
    if (next !== confirm) return setError('New passwords do not match.');
    setBusy(true);
    try {
      await base44.auth.changePassword({ userId: user.id, currentPassword: current, newPassword: next });
      setInfo('Your password has been changed.');
      setCurrent('');
      setNext('');
      setConfirm('');
    } catch (err) {
      const status = errorStatus(err);
      setError(
        status === 401 || status === 400
          ? 'Your current password is incorrect.'
          : status === 422
            ? 'That new password is not allowed. Try a different one.'
            : 'Could not change your password. Please try again.'
      );
    }
    setBusy(false);
  }

  return (
    <div className="mx-auto max-w-xl space-y-4">
      <div className="panel">
        <div className="section-header">My account</div>
        <dl className="grid grid-cols-[auto_1fr] gap-x-4 gap-y-2 p-4 text-sm">
          <dt className="font-semibold text-gray-600">Email</dt>
          <dd className="break-all">{user.email}</dd>
          {user.full_name && (
            <>
              <dt className="font-semibold text-gray-600">Name</dt>
              <dd>{user.full_name}</dd>
            </>
          )}
          <dt className="font-semibold text-gray-600">Membership</dt>
          <dd className="font-semibold text-brand-green">Active</dd>
        </dl>
      </div>

      <form onSubmit={handleChange} className="panel">
        <div className="section-header">Change password</div>
        <div className="flex flex-col gap-3 p-4">
          <div>
            <label className="field-label">Current password</label>
            <input className="field-input" type="password" autoComplete="current-password" value={current} onChange={(e) => setCurrent(e.target.value)} />
          </div>
          <div>
            <label className="field-label">New password</label>
            <input className="field-input" type="password" autoComplete="new-password" value={next} onChange={(e) => setNext(e.target.value)} />
          </div>
          <div>
            <label className="field-label">Confirm new password</label>
            <input className="field-input" type="password" autoComplete="new-password" value={confirm} onChange={(e) => setConfirm(e.target.value)} />
          </div>
          {error && <p className="text-sm text-brand-red">{error}</p>}
          {info && <p className="text-sm text-brand-green">{info}</p>}
          <button type="submit" disabled={busy} className="btn-primary self-start">
            {busy ? 'Saving…' : 'Change password'}
          </button>
          <p className="text-xs text-gray-500">
            Forgot your current password? Log out and use &ldquo;Forgot password?&rdquo; on the login screen.
          </p>
        </div>
      </form>
    </div>
  );
}
