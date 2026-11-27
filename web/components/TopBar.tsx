'use client';

/* eslint-disable @next/next/no-img-element */
import { base44 } from '@/lib/base44Client';

export default function TopBar({ email }: { email: string }) {
  async function handleLogout() {
    await base44.auth.logout('/');
  }

  return (
    <div className="flex items-center justify-between gap-3 bg-brand-ink px-4 py-2 text-white">
      <div className="flex min-w-0 items-center gap-3">
        <img src="/logo-small.png" alt="Pay Down Debt Solutions" className="h-9 w-9 shrink-0 rounded-full bg-white" />
        <span className="truncate font-display text-base uppercase tracking-wide">
          The Pay Down <span className="text-brand-blue">Blueprint</span>
        </span>
      </div>
      <div className="flex items-center gap-3 text-xs">
        <span className="hidden text-white/60 sm:inline">{email}</span>
        <button onClick={handleLogout} className="rounded bg-white/10 px-3 py-1 font-semibold hover:bg-white/20">
          Log out
        </button>
      </div>
    </div>
  );
}
