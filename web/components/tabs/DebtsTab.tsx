'use client';

import { useEffect, useMemo, useState } from 'react';
import { loadPlan, savePlan } from '@/lib/planStore';
import SaveBar from '@/components/SaveBar';
import { CreditEntry, CreditType, LoanEntry, MonthlyBillEntry, UserSettings } from '@/lib/types';
import {
  CTRL_DEBTS_CREDITCARDS,
  CTRL_DEBTS_LIVING_EXP,
  CTRL_DEBTS_LOANS,
  CTRL_DEBTS_MONTHLY_BILLING,
  DEBTS_CREDITS_SLOTS,
  DEBTS_LIVING_SLOTS,
  DEBTS_LOANS_SLOTS,
  DEBTS_MONTHLY_SLOTS,
  HINT_DEBTS,
  HINT_DEBTS_LIVING,
  HINT_DEBTS_LOANS,
  HINT_DEBTS_MONTHLY,
} from '@/lib/constants';

function emptyBills(n: number): MonthlyBillEntry[] {
  return Array.from({ length: n }, (_, i) => ({ slot: i + 1, chargeable: false, source: '', amount: 0 }));
}
function emptyLoans(n: number): LoanEntry[] {
  return Array.from({ length: n }, (_, i) => ({
    slot: i + 1,
    source: '',
    payment: 0,
    balance: 0,
    interest_rate: 0,
    outstanding_interest: 0,
  }));
}
function emptyCredits(n: number): CreditEntry[] {
  return Array.from({ length: n }, (_, i) => ({
    slot: i + 1,
    source: '',
    payment: 0,
    balance: 0,
    interest_rate: 0,
    credit_limit: 0,
    type: 'CC' as CreditType,
  }));
}

const defaultSettings: UserSettings = {
  user_id: '',
  income_total: 0,
  mortgage_rent_payment: 0,
  total_monthly_exp: 0,
  total_debts: 0,
  chargeable_expenses: 0,
  non_chargeable_expenses: 0,
  credit_score_1: '',
  credit_score_2: '',
};

/** Copies the settings fields off a saved plan, filling any gaps with defaults. */
function pickSettings(plan: Partial<UserSettings>): Partial<UserSettings> {
  const out: Partial<UserSettings> = {};
  (Object.keys(defaultSettings) as (keyof UserSettings)[]).forEach((k) => {
    if (k !== 'user_id' && plan[k] != null) (out as Record<string, unknown>)[k] = plan[k];
  });
  return out;
}

export default function DebtsTab() {
  const [bills, setBills] = useState<MonthlyBillEntry[]>(emptyBills(DEBTS_MONTHLY_SLOTS));
  const [living, setLiving] = useState<MonthlyBillEntry[]>(emptyBills(DEBTS_LIVING_SLOTS));
  const [loans, setLoans] = useState<LoanEntry[]>(emptyLoans(DEBTS_LOANS_SLOTS));
  const [credits, setCredits] = useState<CreditEntry[]>(emptyCredits(DEBTS_CREDITS_SLOTS));
  const [settings, setSettings] = useState<UserSettings>({ ...defaultSettings });
  const [expTouched, setExpTouched] = useState(false);
  const [loading, setLoading] = useState(true);

  const [dirty, setDirty] = useState(false);
  const [saving, setSaving] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [justSaved, setJustSaved] = useState(false);

  useEffect(() => {
    let active = true;
    (async () => {
      const plan = await loadPlan().catch(() => null);
      if (!active) return;
      const billsRes = { data: plan?.monthly_bills ?? [] };
      const livingRes = { data: plan?.living_expenses ?? [] };
      const loansRes = { data: plan?.loans ?? [] };
      const creditsRes = { data: plan?.credits ?? [] };

      const base = emptyBills(DEBTS_MONTHLY_SLOTS);
      (billsRes.data || []).forEach((r) => (base[r.slot - 1] = { slot: r.slot, chargeable: r.chargeable, source: r.source, amount: Number(r.amount) }));
      setBills(base);

      const baseLiving = emptyBills(DEBTS_LIVING_SLOTS);
      (livingRes.data || []).forEach((r) => (baseLiving[r.slot - 1] = { slot: r.slot, chargeable: r.chargeable, source: r.source, amount: Number(r.amount) }));
      setLiving(baseLiving);

      const baseLoans = emptyLoans(DEBTS_LOANS_SLOTS);
      (loansRes.data || []).forEach(
        (r) =>
          (baseLoans[r.slot - 1] = {
            slot: r.slot,
            source: r.source,
            payment: Number(r.payment),
            balance: Number(r.balance),
            interest_rate: Number(r.interest_rate),
            outstanding_interest: Number(r.outstanding_interest),
          })
      );
      setLoans(baseLoans);

      const baseCredits = emptyCredits(DEBTS_CREDITS_SLOTS);
      (creditsRes.data || []).forEach(
        (r) =>
          (baseCredits[r.slot - 1] = {
            slot: r.slot,
            source: r.source,
            payment: Number(r.payment),
            balance: Number(r.balance),
            interest_rate: Number(r.interest_rate),
            credit_limit: Number(r.credit_limit),
            type: r.type,
          })
      );
      setCredits(baseCredits);

      if (plan) {
        setSettings((s) => ({ ...s, ...pickSettings(plan) }));
        setExpTouched(true);
      }
      setLoading(false);
    })();
    return () => {
      active = false;
    };
  }, []);

  const sums = useMemo(() => {
    const billsTotal = bills.reduce((s, b) => s + (b.amount || 0), 0);
    const livingTotal = living.reduce((s, b) => s + (b.amount || 0), 0);
    const loansPayments = loans.reduce((s, l) => s + (l.payment || 0), 0);
    const creditsPayments = credits.reduce((s, c) => s + (c.payment || 0), 0);
    const chargeable =
      bills.filter((b) => b.chargeable).reduce((s, b) => s + b.amount, 0) +
      living.filter((b) => b.chargeable).reduce((s, b) => s + b.amount, 0);
    const nonChargeable =
      bills.filter((b) => !b.chargeable).reduce((s, b) => s + b.amount, 0) +
      living.filter((b) => !b.chargeable).reduce((s, b) => s + b.amount, 0);
    return { billsTotal, livingTotal, loansPayments, creditsPayments, chargeable, nonChargeable };
  }, [bills, living, loans, credits]);

  const computedMonthlyExp =
    sums.loansPayments + sums.creditsPayments + (settings.mortgage_rent_payment || 0) + sums.billsTotal + sums.livingTotal;

  useEffect(() => {
    if (!expTouched && !loading) {
      setSettings((s) => ({ ...s, total_monthly_exp: computedMonthlyExp }));
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [computedMonthlyExp, loading]);

  function markDirty() {
    setDirty(true);
    setJustSaved(false);
    setError(null);
  }

  function updateSettings(patch: Partial<UserSettings>) {
    setSettings((s) => ({ ...s, ...patch }));
    markDirty();
  }

  function updateBillLike(
    setter: React.Dispatch<React.SetStateAction<MonthlyBillEntry[]>>,
    slot: number,
    patch: Partial<MonthlyBillEntry>
  ) {
    setter((prev) => {
      const list = prev.slice();
      list[slot - 1] = { ...list[slot - 1], ...patch };
      return list;
    });
    markDirty();
  }

  function updateLoan(slot: number, patch: Partial<LoanEntry>) {
    setLoans((prev) => {
      const list = prev.slice();
      list[slot - 1] = { ...list[slot - 1], ...patch };
      return list;
    });
    markDirty();
  }

  function updateCredit(slot: number, patch: Partial<CreditEntry>) {
    setCredits((prev) => {
      const list = prev.slice();
      list[slot - 1] = { ...list[slot - 1], ...patch };
      return list;
    });
    markDirty();
  }

  async function handleSave() {
    setSaving(true);
    setError(null);

    // income_total belongs to the Income tab, so it is not written here.
    const { user_id: _u, income_total: _i, ...debtSettings } = settings;
    try {
      await savePlan({ monthly_bills: bills, living_expenses: living, loans, credits, ...debtSettings });
    } catch {
      setError('Failed to save debts. Please try again.');
      setSaving(false);
      return;
    }

    setSaving(false);
    setDirty(false);
    setJustSaved(true);
    setTimeout(() => setJustSaved(false), 2500);
  }

  return (
    <div className="mx-auto max-w-6xl space-y-4 pb-20">
      <div className="panel p-3 text-sm text-gray-700">{HINT_DEBTS}</div>

      <BillSection
        title={CTRL_DEBTS_MONTHLY_BILLING}
        hint={HINT_DEBTS_MONTHLY}
        entries={bills}
        onChange={(slot, patch) => updateBillLike(setBills, slot, patch)}
      />
      <BillSection
        title={CTRL_DEBTS_LIVING_EXP}
        hint={HINT_DEBTS_LIVING}
        entries={living}
        onChange={(slot, patch) => updateBillLike(setLiving, slot, patch)}
      />

      <div className="panel">
        <div className="section-header">{CTRL_DEBTS_LOANS}</div>
        <p className="border-b border-gray-200 bg-gray-50 p-3 text-xs text-gray-600">{HINT_DEBTS_LOANS}</p>
        <div className="overflow-x-auto p-3">
          <table className="w-full text-sm">
            <thead>
              <tr className="text-left text-xs text-gray-500">
                <th className="p-1">Source</th>
                <th className="p-1">Payment</th>
                <th className="p-1">Balance</th>
                <th className="p-1">Rate %</th>
                <th className="p-1">Outstanding Interest</th>
              </tr>
            </thead>
            <tbody>
              {loans.map((l) => (
                <tr key={l.slot}>
                  <td className="p-1">
                    <input className="field-input" value={l.source} onChange={(e) => updateLoan(l.slot, { source: e.target.value })} />
                  </td>
                  <td className="p-1">
                    <input className="field-input w-28" type="number" step="0.01" value={l.payment || ''} onChange={(e) => updateLoan(l.slot, { payment: Number(e.target.value) || 0 })} />
                  </td>
                  <td className="p-1">
                    <input className="field-input w-28" type="number" step="0.01" value={l.balance || ''} onChange={(e) => updateLoan(l.slot, { balance: Number(e.target.value) || 0 })} />
                  </td>
                  <td className="p-1">
                    <input className="field-input w-20" type="number" step="0.01" value={l.interest_rate || ''} onChange={(e) => updateLoan(l.slot, { interest_rate: Number(e.target.value) || 0 })} />
                  </td>
                  <td className="p-1">
                    <input className="field-input w-28" type="number" step="0.01" value={l.outstanding_interest || ''} onChange={(e) => updateLoan(l.slot, { outstanding_interest: Number(e.target.value) || 0 })} />
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </div>

      <div className="panel">
        <div className="section-header">{CTRL_DEBTS_CREDITCARDS}</div>
        <div className="overflow-x-auto p-3">
          <table className="w-full text-sm">
            <thead>
              <tr className="text-left text-xs text-gray-500">
                <th className="p-1">Source</th>
                <th className="p-1">Payment</th>
                <th className="p-1">Balance</th>
                <th className="p-1">Rate %</th>
                <th className="p-1">Limit</th>
                <th className="p-1">Type</th>
              </tr>
            </thead>
            <tbody>
              {credits.map((c) => (
                <tr key={c.slot}>
                  <td className="p-1">
                    <input className="field-input" value={c.source} onChange={(e) => updateCredit(c.slot, { source: e.target.value })} />
                  </td>
                  <td className="p-1">
                    <input className="field-input w-28" type="number" step="0.01" value={c.payment || ''} onChange={(e) => updateCredit(c.slot, { payment: Number(e.target.value) || 0 })} />
                  </td>
                  <td className="p-1">
                    <input className="field-input w-28" type="number" step="0.01" value={c.balance || ''} onChange={(e) => updateCredit(c.slot, { balance: Number(e.target.value) || 0 })} />
                  </td>
                  <td className="p-1">
                    <input className="field-input w-20" type="number" step="0.01" value={c.interest_rate || ''} onChange={(e) => updateCredit(c.slot, { interest_rate: Number(e.target.value) || 0 })} />
                  </td>
                  <td className="p-1">
                    <input className="field-input w-28" type="number" step="0.01" value={c.credit_limit || ''} onChange={(e) => updateCredit(c.slot, { credit_limit: Number(e.target.value) || 0 })} />
                  </td>
                  <td className="p-1">
                    <select
                      className="field-input"
                      value={c.type}
                      onChange={(e) => updateCredit(c.slot, { type: e.target.value as CreditType })}
                    >
                      <option value="CC">CC</option>
                      <option value="LOC">LOC</option>
                    </select>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </div>

      <div className="panel grid grid-cols-1 gap-3 p-4 sm:grid-cols-3">
        <Field label="Mortgage/Rent Payment" value={settings.mortgage_rent_payment} onChange={(v) => updateSettings({ mortgage_rent_payment: v })} />
        <Field
          label="Total Monthly Expense"
          value={settings.total_monthly_exp}
          onChange={(v) => {
            setExpTouched(true);
            updateSettings({ total_monthly_exp: v });
          }}
        />
        <Field label="Total Debt" value={settings.total_debts} onChange={(v) => updateSettings({ total_debts: v })} />
        <Field label="Chargeable Expenses" value={settings.chargeable_expenses} onChange={(v) => updateSettings({ chargeable_expenses: v })} />
        <Field label="Non-Chargeable Expenses" value={settings.non_chargeable_expenses} onChange={(v) => updateSettings({ non_chargeable_expenses: v })} />
        <div>
          <label className="field-label">Credit Score 1</label>
          <input className="field-input" value={settings.credit_score_1} onChange={(e) => updateSettings({ credit_score_1: e.target.value })} />
        </div>
        <div>
          <label className="field-label">Credit Score 2</label>
          <input className="field-input" value={settings.credit_score_2} onChange={(e) => updateSettings({ credit_score_2: e.target.value })} />
        </div>
      </div>

      <SaveBar dirty={dirty} saving={saving} error={error} justSaved={justSaved} onSave={handleSave} />
    </div>
  );
}

function Field({ label, value, onChange }: { label: string; value: number; onChange: (v: number) => void }) {
  return (
    <div>
      <label className="field-label">{label}</label>
      <input className="field-input" type="number" step="0.01" value={value || ''} onChange={(e) => onChange(Number(e.target.value) || 0)} />
    </div>
  );
}

function BillSection({
  title,
  hint,
  entries,
  onChange,
}: {
  title: string;
  hint: string;
  entries: MonthlyBillEntry[];
  onChange: (slot: number, patch: Partial<MonthlyBillEntry>) => void;
}) {
  return (
    <div className="panel">
      <div className="section-header">{title}</div>
      <p className="border-b border-gray-200 bg-gray-50 p-3 text-xs text-gray-600 whitespace-pre-line">{hint}</p>
      <div className="grid grid-cols-1 gap-3 p-3 sm:grid-cols-2 lg:grid-cols-3">
        {entries.map((entry) => (
          <div key={entry.slot} className="flex items-center gap-2">
            <select
              className="field-input w-20"
              value={entry.chargeable ? 'YES' : 'NO'}
              onChange={(e) => onChange(entry.slot, { chargeable: e.target.value === 'YES' })}
            >
              <option value="NO">NO</option>
              <option value="YES">YES</option>
            </select>
            <input
              className="field-input"
              placeholder={`Source ${entry.slot}`}
              value={entry.source}
              onChange={(e) => onChange(entry.slot, { source: e.target.value })}
            />
            <input
              className="field-input w-24"
              type="number"
              step="0.01"
              placeholder="Amount"
              value={entry.amount || ''}
              onChange={(e) => onChange(entry.slot, { amount: Number(e.target.value) || 0 })}
            />
          </div>
        ))}
      </div>
    </div>
  );
}
