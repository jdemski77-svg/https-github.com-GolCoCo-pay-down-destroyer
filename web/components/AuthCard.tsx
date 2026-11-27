'use client';

/* eslint-disable @next/next/no-img-element */

/** Dark branded page with a white card, used by the join, reset and paywall screens. */
export default function AuthCard({ title, children }: { title?: string; children: React.ReactNode }) {
  return (
    <div className="brand-bg flex min-h-screen flex-col items-center justify-center px-4 py-10">
      <img src="/blueprint-wordmark.png" alt="The Pay Down Blueprint" className="mb-6 w-full max-w-md" />
      <div className="w-full max-w-md overflow-hidden rounded-lg bg-white shadow-2xl">
        {title && (
          <div className="flex items-center gap-3 bg-brand-navy px-5 py-3">
            <img src="/logo-small.png" alt="" className="h-9 w-9 rounded-full bg-white" />
            <span className="font-display text-lg uppercase tracking-wide text-white">{title}</span>
          </div>
        )}
        <div className="flex flex-col gap-3 p-6">{children}</div>
      </div>
      <p className="mt-6 text-xs text-white/50">© Pay Down Debt Solutions</p>
    </div>
  );
}
