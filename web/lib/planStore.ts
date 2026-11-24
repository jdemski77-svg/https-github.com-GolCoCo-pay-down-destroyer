import { base44 } from '@/lib/base44Client';
import type { CreditEntry, IncomeEntry, LoanEntry, MonthlyBillEntry, UserSettings } from '@/lib/types';

/** One record per member holding everything they enter in the tool. */
export interface PayDownPlan extends Omit<UserSettings, 'user_id'> {
  id: string;
  income_entries: IncomeEntry[];
  monthly_bills: MonthlyBillEntry[];
  living_expenses: MonthlyBillEntry[];
  loans: LoanEntry[];
  credits: CreditEntry[];
}

const Plan = base44.entities.PayDownPlan;

/** The member's own plan, or null if they haven't saved anything yet. Access rules only return their own. */
export async function loadPlan(): Promise<Partial<PayDownPlan> | null> {
  const rows = await Plan.list('-created_date', 1);
  return (rows?.[0] as Partial<PayDownPlan>) ?? null;
}

/** Saves only the given fields, creating the plan on first save. */
export async function savePlan(patch: Partial<Omit<PayDownPlan, 'id'>>): Promise<void> {
  const existing = await loadPlan();
  if (existing?.id) await Plan.update(existing.id, patch);
  else await Plan.create(patch);
}
