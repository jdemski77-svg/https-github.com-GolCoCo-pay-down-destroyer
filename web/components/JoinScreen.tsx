'use client';

import { useEffect, useState } from 'react';
import AuthCard from '@/components/AuthCard';
import { base44, errorMessage, errorStatus } from '@/lib/base44Client';
import { syncMembership } from '@/lib/membership';
import { existingCustomerLinkOpen } from '@/lib/offers';

type Step = 'loading' | 'form' | 'verify' | 'finishing' | 'expired';

/** Tags the account as an existing customer (server re-checks the deadline). */
async function claimExisting() {
  await base44.functions.invoke('claim-existing', {}).catch(() => null);
}

/**
 * Where clients create their members-area account: email + password, confirm the
 * emailed code, and they go straight into the app.
 *
 * variant "existing" is the separate, expiring link for people who already bought
 * the downloadable course + app (/existing/).
 */
export default function JoinScreen({ variant = 'new' }: { variant?: 'new' | 'existing' }) {
  const existing = variant === 'existing';
  const [step, setStep] = useState<Step>('loading');
  const [email, setEmail] = useState('');
  const [fullName, setFullName] = useState('');
  const [password, setPassword] = useState('');
  const [confirm, setConfirm] = useState('');
  const [code, setCode] = useState('');
  const [error, setError] = useState('');
  const [info, setInfo] = useState('');
  const [busy, setBusy] = useState(false);

  useEffect(() => {
    (async () => {
      if (existing && !existingCustomerLinkOpen()) {
        setStep('expired');
        return;
      }
      // Already logged in: go straight in.
      if (await base44.auth.isAuthenticated().catch(() => false)) {
        if (existing) await claimExisting();
        await syncMembership().catch(() => null);
        window.location.replace('/');
        return;
      }
      // Links can prefill the email, e.g. /join/?email=client@example.com
      const fromLink = new URLSearchParams(window.location.search).get('email');
      if (fromLink) setEmail(fromLink.trim());
      setStep('form');
    })();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  async function finish() {
    setStep('finishing');
    try {
      await base44.auth.loginViaEmailPassword(email.trim(), password);
      if (fullName.trim()) await base44.auth.updateMe({ full_name: fullName.trim() }).catch(() => null);
      if (existing) await claimExisting();
      await syncMembership().catch(() => null);
      window.location.replace('/');
    } catch (err) {
      setStep('verify');
      setError(errorMessage(err, 'Your account is ready, but we could not log you in. Please log in from the home page.'));
    }
  }

  async function handleRegister(e: React.FormEvent) {
    e.preventDefault();
    setError('');
    setInfo('');
    if (!email.trim()) return setError('Please enter your email.');
    if (password.length < 8) return setError('Password must be at least 8 characters.');
    if (password !== confirm) return setError('Passwords do not match.');
    setBusy(true);
    try {
      await base44.auth.register({ email: email.trim(), password });
      setStep('verify');
      setInfo(`We emailed a verification code to ${email.trim()}.`);
    } catch (err) {
      const status = errorStatus(err);
      setError(
        status === 409 || /exist/i.test(errorMessage(err, ''))
          ? 'An account with this email already exists. Log in from the home page instead.'
          : errorMessage(err, 'Could not create your account. Please try again.')
      );
    }
    setBusy(false);
  }

  async function handleVerify(e: React.FormEvent) {
    e.preventDefault();
    setError('');
    if (!code.trim()) return setError('Enter the code from your email.');
    setBusy(true);
    try {
      await base44.auth.verifyOtp({ email: email.trim(), otpCode: code.trim() });
    } catch (err) {
      setBusy(false);
      return setError(errorMessage(err, 'That code did not work. Check it and try again.'));
    }
    setBusy(false);
    await finish();
  }

  async function handleResend() {
    setError('');
    try {
      await base44.auth.resendOtp(email.trim());
      setInfo('A new code is on its way.');
    } catch (err) {
      setError(errorMessage(err, 'Could not resend the code. Please wait a minute and try again.'));
    }
  }

  if (step === 'expired') {
    return (
      <AuthCard title="Link expired">
        <p className="text-sm font-semibold text-gray-700">This link has expired.</p>
        <p className="text-sm text-gray-600">
          The free account offer for existing Blueprint customers ended on December 25. If you already created your
          account, you can still log in.
        </p>
        <a href="/" className="btn-primary text-center">
          Go to member login
        </a>
      </AuthCard>
    );
  }

  if (step === 'loading' || step === 'finishing') {
    return (
      <AuthCard title="Welcome">
        <p className="text-sm text-gray-600">{step === 'loading' ? 'Loading…' : 'Opening the members area…'}</p>
      </AuthCard>
    );
  }

  if (step === 'verify') {
    return (
      <AuthCard title="Verify your email">
        <form onSubmit={handleVerify} className="flex flex-col gap-3">
          {info && <p className="text-sm text-brand-green">{info}</p>}
          <div>
            <label className="field-label">Verification code</label>
            <input
              className="field-input text-center text-lg tracking-[0.4em]"
              inputMode="numeric"
              autoComplete="one-time-code"
              value={code}
              onChange={(e) => setCode(e.target.value)}
            />
          </div>
          {error && <p className="text-sm text-brand-red">{error}</p>}
          <button type="submit" disabled={busy} className="btn-primary">
            {busy ? 'Checking…' : 'Verify & enter'}
          </button>
          <button type="button" onClick={handleResend} className="text-left text-xs text-brand-blue underline">
            Resend code
          </button>
        </form>
      </AuthCard>
    );
  }

  return (
    <AuthCard title={existing ? 'Create your free account' : 'Create your account'}>
      {existing ? (
        <>
          <h1 className="font-display text-xl uppercase leading-tight text-brand-navy">
            Create your Free Account to Access the <span className="text-brand-blue">Blueprint App</span>
          </h1>
          <p className="text-sm text-gray-600">
            For customers who already purchased The Pay Down Blueprint course and downloadable app. Your new account gives
            you easy online access — no download needed.
          </p>
          <p className="text-xs font-semibold text-brand-red">This link expires December 25, 2026.</p>
        </>
      ) : (
        <p className="text-sm text-gray-600">Set up your login for the Pay Down Blueprint members area.</p>
      )}
      <form onSubmit={handleRegister} className="flex flex-col gap-3">
        <div>
          <label className="field-label">Email</label>
          <input
            className="field-input"
            type="email"
            autoComplete="username"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />
        </div>
        <div>
          <label className="field-label">Your name (optional)</label>
          <input className="field-input" autoComplete="name" value={fullName} onChange={(e) => setFullName(e.target.value)} />
        </div>
        <div>
          <label className="field-label">Password</label>
          <input
            className="field-input"
            type="password"
            autoComplete="new-password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
        </div>
        <div>
          <label className="field-label">Confirm password</label>
          <input
            className="field-input"
            type="password"
            autoComplete="new-password"
            value={confirm}
            onChange={(e) => setConfirm(e.target.value)}
          />
        </div>
        {error && <p className="text-sm text-brand-red">{error}</p>}
        <button type="submit" disabled={busy} className="btn-primary">
          {busy ? 'Creating account…' : existing ? 'Create my free account' : 'Create account'}
        </button>
        <a href="/" className="text-xs text-brand-blue underline">
          Already have an account? Log in
        </a>
      </form>
    </AuthCard>
  );
}
