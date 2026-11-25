'use client';

import { useEffect, useMemo, useState } from 'react';
import { loadPlan } from '@/lib/planStore';
import { calcPayDown, calcTraditional } from '@/lib/paydownCalc';
import { CreditEntry, PayDownResult, TraditionalResult, UserSettings } from '@/lib/types';
import { HINT_IDENTIFY } from '@/lib/constants';

const currency = (n: number) => `$${(n || 0).toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 })}`;

export default function IdentifyImplementTab() {
  const [credits, setCredits] = useState<CreditEntry[]>([]);
  const [settings, setSettings] = useState<UserSettings | null>(null);
  const [selectedSlot, setSelectedSlot] = useState<number | null>(null);
  const [initialBalance, setInitialBalance] = useState(0);
  const [availableIncome, setAvailableIncome] = useState(0);
  const [chargeableExpenses, setChargeableExpenses] = useState(0);
  const [interestRate, setInterestRate] = useState(0);
  const [traditional, setTraditional] = useState<TraditionalResult | null>(null);
  const [payDown, setPayDown] = useState<PayDownResult | null>(null);
  const [calculating, setCalculating] = useState(false);
  const [message, setMessage] = useState('');

  useEffect(() => {
    let active = true;
    (async () => {
      const plan = await loadPlan().catch(() => null);
      if (!active) return;
      const saved = ((plan?.credits as CreditEntry[]) || []).slice().sort((a, b) => a.slot - b.slot);
      setCredits(saved.filter((c) => c.source));
      if (plan) {
        setSettings({
          user_id: '',
          income_total: Number(plan.income_total) || 0,
          mortgage_rent_payment: Number(plan.mortgage_rent_payment) || 0,
          total_monthly_exp: Number(plan.total_monthly_exp) || 0,
          total_debts: Number(plan.total_debts) || 0,
          chargeable_expenses: Number(plan.chargeable_expenses) || 0,
          non_chargeable_expenses: Number(plan.non_chargeable_expenses) || 0,
          credit_score_1: plan.credit_score_1 ?? '',
          credit_score_2: plan.credit_score_2 ?? '',
        });
      }
    })();
    return () => {
      active = false;
    };
  }, []);

  const selected = useMemo(() => credits.find((c) => c.slot === selectedSlot) || null, [credits, selectedSlot]);

  function selectDestroyer(entry: CreditEntry) {
    setSelectedSlot(entry.slot);
    const s = settings;
    if (!s) return;
    if (entry.type === 'CC') {
      setAvailableIncome(s.income_total - s.non_chargeable_expenses + entry.payment);
      setChargeableExpenses(s.chargeable_expenses);
    } else {
      setAvailableIncome(s.income_total);
      setChargeableExpenses(s.total_monthly_exp - entry.payment);
    }
    setInitialBalance(entry.balance);
    setInterestRate(entry.interest_rate);
    setTraditional(null);
    setPayDown(null);
    setMessage('');
  }

  function implement() {
    if (!selected) return;
    setCalculating(true);
    setMessage('');
    const rateDecimal = interestRate / 100;
    const trad = calcTraditional(initialBalance, selected.payment, rateDecimal);
    const pd = calcPayDown(initialBalance, availableIncome, rateDecimal, chargeableExpenses);
    setTraditional(trad);
    setPayDown(pd);
    if (pd.rows.length === 0) {
      setMessage('Your selections lead to increasing debt. Consult the instruction video for alternatives.');
    }
    setCalculating(false);
  }

  const monthsSavings = traditional && payDown && traditional.monthsToPayCurrent > 0 ? traditional.monthsToPayCurrent - payDown.monthsToPay : null;
  const interestSavings =
    traditional && payDown && traditional.totalInterestPaidCurrent >= 0 ? traditional.totalInterestPaidCurrent - payDown.totalInterestPaid : null;

  return (
    <div className="mx-auto grid max-w-7xl grid-cols-1 gap-4 lg:grid-cols-[680px_1fr]">
      <div className="space-y-4">
        <div className="panel">
          <div className="section-header">Identify The Debt Destroyer</div>
          <p className="border-b border-gray-200 bg-gray-50 p-3 text-xs text-gray-600 whitespace-pre-line">{HINT_IDENTIFY}</p>
          <div className="max-h-64 overflow-y-auto">
            {credits.length === 0 && <div className="p-3 text-sm text-gray-500">No credit cards or lines of credit entered yet.</div>}
            {credits.map((c, i) => (
              <button
                key={c.slot}
                onClick={() => selectDestroyer(c)}
                className={`flex w-full items-center justify-between border-b border-gray-100 px-3 py-2 text-sm ${
                  selectedSlot === c.slot ? 'bg-brand-magenta text-white' : 'hover:bg-gray-50'
                }`}
              >
                <span className="w-8 text-xs opacity-70">{i + 1}.</span>
                <span className="flex-1 text-left">{c.source}</span>
                <span
                  className={`mr-3 rounded px-2 py-0.5 text-xs font-bold text-white ${c.type === 'CC' ? 'bg-brand-green' : 'bg-brand-red'}`}
                >
                  {c.type}
                </span>
                <span className="tabular-nums">{currency(c.payment)}</span>
              </button>
            ))}
          </div>
        </div>

        <div className="panel">
          <div className="section-header">Pay Down Debt Destroyer</div>
          <div className="grid grid-cols-2 gap-3 p-3">
            <Field label="Initial Balance" value={initialBalance} onChange={setInitialBalance} />
            <Field label="Available Income" value={availableIncome} onChange={setAvailableIncome} />
            <Field label="Chargeable Expenses" value={chargeableExpenses} onChange={setChargeableExpenses} />
            <Field label="Interest Rate (%)" value={interestRate} onChange={setInterestRate} />
          </div>
          <div className="flex items-center gap-3 border-t border-gray-200 p-3">
            <button
              disabled={!selected || calculating}
              onClick={implement}
              className="flex items-center gap-2 rounded bg-brand-green px-4 py-2 text-sm font-bold text-white hover:opacity-90 disabled:opacity-50 disabled:cursor-not-allowed"
            >
              <img src="/calculate.png" alt="" className="h-4 w-4" /> IMPLEMENT
            </button>
            {!selected && <span className="text-sm text-brand-red">Select a Debt Destroyer above first.</span>}
            {selected && message && <span className="text-sm text-brand-red">{message}</span>}
          </div>
        </div>

        <div className="panel">
          <div className="section-header">Savings Result</div>
          <div className="grid grid-cols-3 divide-x divide-gray-200 p-3 text-center text-sm">
            <ResultColumn label="Traditional" color="text-brand-red" months={traditional?.monthsToPayCurrent ?? null} amount={traditional?.totalInterestPaidCurrent ?? null} />
            <ResultColumn label="Pay Down" color="text-brand-red" months={payDown?.monthsToPay ?? null} amount={payDown?.totalInterestPaid ?? null} />
            <ResultColumn label="Savings" color="text-brand-green" months={monthsSavings} amount={interestSavings} />
          </div>
        </div>
      </div>

      <div className="panel overflow-hidden">
        <div className="section-header">Results</div>
        <div className="overflow-auto">
          <table className="w-full text-sm">
            <thead>
              <tr className="bg-brand-magenta text-white">
                <th className="p-2 text-left">Months to Payoff</th>
                <th className="p-2 text-right">Monthly Starting Balance</th>
                <th className="p-2 text-right">Available Income</th>
                <th className="p-2 text-right">New Balance</th>
                <th className="p-2 text-right">Chargeable Expenses</th>
                <th className="p-2 text-right">Interest</th>
                <th className="p-2 text-right">Monthly Ending Balance</th>
              </tr>
            </thead>
            <tbody>
              {payDown?.rows.map((r) => (
                <tr key={r.monthsToPayoff} className="odd:bg-gray-50">
                  <td className="p-2">{r.monthsToPayoff}</td>
                  <td className="p-2 text-right">${Math.round(r.monthlyStartingBalance).toLocaleString()}</td>
                  <td className="p-2 text-right">${Math.round(r.availableIncome).toLocaleString()}</td>
                  <td className="p-2 text-right">${Math.round(r.newBalance).toLocaleString()}</td>
                  <td className="p-2 text-right">${Math.round(r.chargeableExp).toLocaleString()}</td>
                  <td className="p-2 text-right">${Math.round(r.interest).toLocaleString()}</td>
                  <td className="p-2 text-right">${Math.round(r.monthlyEndingBalance).toLocaleString()}</td>
                </tr>
              ))}
            </tbody>
          </table>
          {!payDown && <div className="p-4 text-sm text-gray-500">Select a Debt Destroyer and click Implement to see results.</div>}
        </div>
      </div>
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

function ResultColumn({
  label,
  color,
  months,
  amount,
}: {
  label: string;
  color: string;
  months: number | null;
  amount: number | null;
}) {
  return (
    <div className="px-2">
      <div className={`font-display font-bold ${color}`}>{label}</div>
      <div className="mt-1 text-xs text-gray-500">Months</div>
      <div className="font-semibold">{months === null ? '—' : months < 0 ? 'Never' : months}</div>
      <div className="mt-1 text-xs text-gray-500">Interest</div>
      <div className="font-semibold">{amount === null ? '—' : amount < 0 ? '—' : currency(amount)}</div>
    </div>
  );
}
