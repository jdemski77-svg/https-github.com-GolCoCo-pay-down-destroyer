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

// The plan's id once known, so saves update it instead of creating a second plan.
let planId: string | null = null;
// Saves run one at a time, so two tabs saving at once can't both create a plan.
let queue: Promise<unknown> = Promise.resolve();

/** The member's own plan, or null if they haven't saved anything yet. Access rules only return their own. */
export async function loadPlan(): Promise<Partial<PayDownPlan> | null> {
  const rows = await Plan.list('-created_date', 1);
  const plan = (rows?.[0] as Partial<PayDownPlan>) ?? null;
  if (plan?.id) planId = plan.id;
  return plan;
}

/** Saves only the given fields, creating the plan on first save. */
export function savePlan(patch: Partial<Omit<PayDownPlan, 'id'>>): Promise<void> {
  const run = queue.then(async () => {
    if (!planId) await loadPlan();
    if (planId) {
      await Plan.update(planId, patch);
    } else {
      const created = (await Plan.create(patch)) as { id?: string };
      if (created?.id) planId = created.id;
    }
  });
  queue = run.catch(() => undefined);
  return run;
}
