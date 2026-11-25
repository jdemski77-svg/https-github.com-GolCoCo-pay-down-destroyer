'use client';

import { useEffect, useState } from 'react';
import AuthCard from '@/components/AuthCard';
import { base44, errorMessage } from '@/lib/base44Client';

/** Landing page for the link in the "reset your password" email. */
export default function ResetPasswordScreen() {
  const [token, setToken] = useState<string | null>(null);
  const [password, setPassword] = useState('');
  const [confirm, setConfirm] = useState('');
  const [error, setError] = useState('');
  const [done, setDone] = useState(false);
  const [busy, setBusy] = useState(false);

  useEffect(() => {
    const p = new URLSearchParams(window.location.search);
    setToken(p.get('token') || p.get('reset_token') || p.get('resetToken'));
  }, []);

  async function handleSubmit(e: React.FormEvent) {
    e.preventDefault();
    setError('');
    if (!token) return setError('This reset link is missing its code. Request a new link from the login page.');
    if (password.length < 8) return setError('Password must be at least 8 characters.');
    if (password !== confirm) return setError('Passwords do not match.');
    setBusy(true);
    try {
      await base44.auth.resetPassword({ resetToken: token, newPassword: password });
      setDone(true);
    } catch (err) {
      setError(errorMessage(err, 'This reset link is invalid or has expired. Request a new one.'));
    }
    setBusy(false);
  }

  return (
    <AuthCard title="Reset password">
      {done ? (
        <>
          <p className="text-sm font-semibold text-brand-green">Your password has been changed.</p>
          <a href="/" className="btn-primary text-center">
            Go to login
          </a>
        </>
      ) : (
        <form onSubmit={handleSubmit} className="flex flex-col gap-3">
          <div>
            <label className="field-label">New password</label>
            <input className="field-input" type="password" autoComplete="new-password" value={password} onChange={(e) => setPassword(e.target.value)} />
          </div>
          <div>
            <label className="field-label">Confirm new password</label>
            <input className="field-input" type="password" autoComplete="new-password" value={confirm} onChange={(e) => setConfirm(e.target.value)} />
          </div>
          {error && <p className="text-sm text-brand-red">{error}</p>}
          <button type="submit" disabled={busy} className="btn-primary">
            {busy ? 'Saving…' : 'Set new password'}
          </button>
          <a href="/" className="text-xs text-brand-blue underline">
            Back to login
          </a>
        </form>
      )}
    </AuthCard>
  );
}
