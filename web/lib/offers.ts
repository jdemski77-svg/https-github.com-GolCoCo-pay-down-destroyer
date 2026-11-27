/**
 * The existing-customer sign-up link (/existing/) for people who bought the
 * downloadable course + app. It stops working after the end of Dec 25, 2026
 * (11:59 PM US Eastern = 05:00 UTC on Dec 26). The server enforces the same
 * cutoff in base44/shared/membership.ts — change both together.
 */
export const EXISTING_CUSTOMER_DEADLINE = new Date('2026-12-26T05:00:00Z');

export function existingCustomerLinkOpen(now = new Date()): boolean {
  return now < EXISTING_CUSTOMER_DEADLINE;
}
