'use client';

import { useEffect, useState } from 'react';
import AuthCard from '@/components/AuthCard';
import { base44, errorMessage, errorStatus } from '@/lib/base44Client';
import { syncMembership } from '@/lib/membership';

type Step = 'loading' | 'form' | 'verify' | 'finishing';

/**
 * Where buyers land after paying in GoHighLevel. They create their account here with the
 * email they paid with, confirm the emailed code, and go straight into the app.
 */
export default function JoinPage() {
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
      // Already logged in (an existing user who just paid): re-check and go in.
      if (await base44.auth.isAuthenticated().catch(() => false)) {
        await syncMembership().catch(() => null);
        window.location.replace('/');
        return;
      }
      // GoHighLevel's thank-you page can link here as /join/?email={{contact.email}}
      const fromLink = new URLSearchParams(window.location.search).get('email');
      if (fromLink) setEmail(fromLink.trim());
      setStep('form');
    })();
  }, []);

  async function finish() {
    setStep('finishing');
    try {
      await base44.auth.loginViaEmailPassword(email.trim(), password);
      if (fullName.trim()) await base44.auth.updateMe({ full_name: fullName.trim() }).catch(() => null);
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
    if (!email.trim()) return setError('Please enter the email you paid with.');
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
          ? 'An account with this email already exists. Log in from the home page and your membership will be applied.'
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
    <AuthCard title="Create your account">
      <p className="text-sm font-semibold text-brand-green">Thank you for joining!</p>
      <p className="text-sm text-gray-600">Set up your login for the Pay Down members area.</p>
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
          <p className="mt-1 text-xs text-gray-500">
            Use the same email you paid with — that&apos;s how we find your membership.
          </p>
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
          {busy ? 'Creating account…' : 'Create account'}
        </button>
        <a href="/" className="text-xs text-brand-blue underline">
          Already have an account? Log in
        </a>
      </form>
    </AuthCard>
  );
}
