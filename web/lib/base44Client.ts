import { createClient } from '@base44/sdk';

// Your Base44 app id (not a secret). Set it in .env.local — see README.
const appId = process.env.NEXT_PUBLIC_BASE44_APP_ID as string;

export const base44 = createClient({ appId });

/** Pulls a readable message out of a Base44 / axios error. */
export function errorMessage(err: unknown, fallback = 'Something went wrong. Please try again.'): string {
  const e = err as { message?: string; response?: { data?: { message?: string; detail?: string; error?: string } } };
  return e?.response?.data?.message || e?.response?.data?.detail || e?.message || fallback;
}

export function errorStatus(err: unknown): number | undefined {
  const e = err as { status?: number; response?: { status?: number } };
  return e?.status ?? e?.response?.status;
}
