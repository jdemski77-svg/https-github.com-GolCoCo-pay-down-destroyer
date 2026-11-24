'use client';

import { useEffect, useState } from 'react';
/* eslint-disable @next/next/no-img-element */
import { base44, errorStatus } from '@/lib/base44Client';
import { JOIN_URL } from '@/lib/membership';

/** What logged-out visitors see: a Join button for new members and a login form for existing ones. */
export default function LoginScreen({ onLoggedIn }: { onLoggedIn: () => void }) {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [remember, setRemember] = useState(true);
  const [error, setError] = useState('');
  const [info, setInfo] = useState('');
  const [busy, setBusy] = useState(false);
  const [mode, setMode] = useState<'login' | 'reset'>('login');

  useEffect(() => {
    try {
      const saved = localStorage.getItem('paydown_remember_email');
      if (saved) setEmail(saved);
    } catch {
      // ignore storage failures (private browsing, etc.)
    }
  }, []);

  async function handleLogin(e: React.FormEvent) {
    e.preventDefault();
    setError('');
    setInfo('');
    if (!email || !password) {
      setError('Please enter your email and password.');
      return;
    }
    setBusy(true);
    try {
      await base44.auth.loginViaEmailPassword(email.trim(), password);
    } catch (err) {
      setBusy(false);
      setError(
        errorStatus(err) === 429
          ? 'Too many attempts. Please wait a minute and try again.'
          : 'Incorrect email or password. Please check again.'
      );
      return;
    }
    try {
      if (remember) localStorage.setItem('paydown_remember_email', email.trim());
      else localStorage.removeItem('paydown_remember_email');
    } catch {
      // ignore storage failures (private browsing, etc.)
    }
    onLoggedIn();
  }

  async function handleReset(e: React.FormEvent) {
    e.preventDefault();
    setError('');
    setInfo('');
    if (!email) {
      setError('Please enter your email first.');
      return;
    }
    setBusy(true);
    try {
      await base44.auth.resetPasswordRequest(email.trim());
    } catch {
      // Don't reveal whether the email has an account.
    }
    setBusy(false);
    setInfo('If that email has an account, a password reset link has been sent.');
  }

  const features = [
    ['Step-by-step training', 'Proven strategies to reduce interest and pay off debt faster.'],
    ['Track your progress', 'Easy-to-use worksheets and tracking tools.'],
    ['Powerful app access', 'Plan and stay motivated anytime, anywhere.'],
    ['Support & community', 'Answers and accountability every step of the way.'],
  ];

  return (
    <div className="brand-bg min-h-screen text-white">
      <header className="mx-auto flex max-w-6xl items-center gap-3 px-4 py-4">
        <img src="/logo-small.png" alt="Pay Down Debt Solutions" className="h-12 w-12 rounded-full bg-white" />
        <span className="font-display text-lg uppercase tracking-wide">Pay Down Debt Solutions</span>
      </header>

      <main className="mx-auto grid max-w-6xl gap-8 px-4 pb-12 lg:grid-cols-[1.4fr_1fr] lg:items-start">
        <section className="lg:col-start-1 lg:row-start-1">
          <img
            src="/blueprint-hero.webp"
            alt="The Pay Down Blueprint: your plan, your freedom, your future"
            className="w-full rounded-lg shadow-2xl ring-1 ring-white/10"
          />
        </section>

        <section className="flex flex-col gap-5 lg:col-start-2 lg:row-span-2 lg:row-start-1">
          <div className="rounded-lg bg-white p-6 text-gray-800 shadow-2xl">
            <h1 className="font-display text-2xl uppercase leading-tight text-brand-navy">
              Eliminate debt. Reduce interest. <span className="text-brand-blue">Build freedom.</span>
            </h1>
            <p className="mt-2 text-sm text-gray-600">
              Join the Pay Down Blueprint to unlock the members area and the Pay Down app.
            </p>
            {JOIN_URL && (
              <a href={JOIN_URL} className="btn-primary mt-4 block w-full py-3 text-center text-base">
                Join now
              </a>
            )}
            <p className="mt-2 text-xs text-gray-500">
              Already paid?{' '}
              <a href="/join/" className="font-semibold text-brand-blue underline">
                Create your account
              </a>{' '}
              with the email you paid with.
            </p>
          </div>

          <form
            onSubmit={mode === 'login' ? handleLogin : handleReset}
            className="flex flex-col gap-3 rounded-lg bg-white p-6 text-gray-800 shadow-2xl"
          >
            <p className="font-display text-lg uppercase tracking-wide text-brand-navy">
              {mode === 'login' ? 'Member login' : 'Reset your password'}
            </p>
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
            {mode === 'login' && (
              <div>
                <label className="field-label">Password</label>
                <input
                  className="field-input"
                  type="password"
                  autoComplete="current-password"
                  value={password}
                  onChange={(e) => setPassword(e.target.value)}
                />
              </div>
            )}
            {mode === 'login' && (
              <label className="flex items-center gap-2 text-xs font-semibold uppercase text-gray-600">
                <input type="checkbox" checked={remember} onChange={(e) => setRemember(e.target.checked)} />
                Remember me
              </label>
            )}
            {error && <p className="text-sm text-brand-red">{error}</p>}
            {info && <p className="text-sm text-brand-green">{info}</p>}
            <button type="submit" disabled={busy} className="btn-secondary">
              {mode === 'login' ? 'Log in' : 'Send reset link'}
            </button>
            <button
              type="button"
              className="text-left text-xs text-brand-blue underline"
              onClick={() => {
                setMode(mode === 'login' ? 'reset' : 'login');
                setError('');
                setInfo('');
              }}
            >
              {mode === 'login' ? 'Forgot password?' : 'Back to login'}
            </button>
          </form>
        </section>

        <section className="lg:col-start-1 lg:row-start-2">
          <ul className="grid gap-4 sm:grid-cols-2">
            {features.map(([title, text]) => (
              <li key={title} className="rounded-lg border border-white/10 bg-white/5 p-4">
                <p className="font-display uppercase tracking-wide text-brand-blue">{title}</p>
                <p className="mt-1 text-sm text-white/70">{text}</p>
              </li>
            ))}
          </ul>
        </section>
      </main>
    </div>
  );
}
