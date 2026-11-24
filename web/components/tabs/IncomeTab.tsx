'use client';

import { useEffect, useMemo, useState } from 'react';
import { loadPlan, savePlan } from '@/lib/planStore';
import SaveBar from '@/components/SaveBar';
import { IncomeCategory, IncomeEntry } from '@/lib/types';
import {
  CTRL_INCOME_CHECK,
  CTRL_INCOME_GOVER,
  CTRL_INCOME_PAYCHECK,
  CTRL_INCOME_RETIRE,
  HINT_INCOME,
  HINT_INCOME_CHECK,
  HINT_INCOME_GOVER,
  HINT_INCOME_PAYCHECK,
  HINT_INCOME_RETIRE,
  INCOME_SLOTS,
} from '@/lib/constants';

const SECTIONS: { category: IncomeCategory; title: string; hint: string }[] = [
  { category: 'paycheck', title: CTRL_INCOME_PAYCHECK, hint: HINT_INCOME_PAYCHECK },
  { category: 'retirement', title: CTRL_INCOME_RETIRE, hint: HINT_INCOME_RETIRE },
  { category: 'governmental', title: CTRL_INCOME_GOVER, hint: HINT_INCOME_GOVER },
  { category: 'checking', title: CTRL_INCOME_CHECK, hint: HINT_INCOME_CHECK },
];

const CATEGORIES: IncomeCategory[] = ['paycheck', 'retirement', 'governmental', 'checking'];

function emptyEntries(category: IncomeCategory): IncomeEntry[] {
  return Array.from({ length: INCOME_SLOTS }, (_, i) => ({ category, slot: i + 1, source: '', amount: 0 }));
}

export default function IncomeTab() {
  const [entries, setEntries] = useState<Record<IncomeCategory, IncomeEntry[]>>({
    paycheck: emptyEntries('paycheck'),
    retirement: emptyEntries('retirement'),
    governmental: emptyEntries('governmental'),
    checking: emptyEntries('checking'),
  });
  const [incomeTotal, setIncomeTotal] = useState(0);
  const [totalTouched, setTotalTouched] = useState(false);
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
      const incomeRows = plan?.income_entries ?? [];
      const settings = plan;
      setEntries((prev) => {
        const next = { ...prev };
        CATEGORIES.forEach((cat) => {
          const base = emptyEntries(cat);
          (incomeRows || [])
            .filter((r) => r.category === cat)
            .forEach((r) => {
              base[r.slot - 1] = { ...base[r.slot - 1], source: r.source, amount: Number(r.amount) };
            });
          next[cat] = base;
        });
        return next;
      });
      if (settings?.income_total != null) {
        setIncomeTotal(Number(settings.income_total));
        setTotalTouched(true);
      }
      setLoading(false);
    })();
    return () => {
      active = false;
    };
  }, []);

  const subtotals = useMemo(() => {
    const result: Record<IncomeCategory, number> = { paycheck: 0, retirement: 0, governmental: 0, checking: 0 };
    CATEGORIES.forEach((cat) => {
      result[cat] = entries[cat].reduce((s, e) => s + (Number(e.amount) || 0), 0);
    });
    return result;
  }, [entries]);

  const computedTotal = subtotals.paycheck + subtotals.retirement + subtotals.governmental + subtotals.checking;

  useEffect(() => {
    if (!totalTouched && !loading) setIncomeTotal(computedTotal);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [computedTotal, loading]);

  function markDirty() {
    setDirty(true);
    setJustSaved(false);
    setError(null);
  }

  function updateEntry(category: IncomeCategory, slot: number, field: 'source' | 'amount', value: string) {
    setEntries((prev) => {
      const list = prev[category].slice();
      const idx = slot - 1;
      list[idx] = { ...list[idx], [field]: field === 'amount' ? Number(value) || 0 : value };
      return { ...prev, [category]: list };
    });
    markDirty();
  }

  async function handleSave() {
    setSaving(true);
    setError(null);

    const rows = CATEGORIES.flatMap((cat) =>
      entries[cat].map((e) => ({ category: cat, slot: e.slot, source: e.source, amount: e.amount }))
    );
    try {
      await savePlan({ income_entries: rows, income_total: incomeTotal });
    } catch {
      setError('Failed to save income. Please try again.');
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
      <div className="panel p-3 text-sm text-gray-700">
        <span className="font-bold">Dan: </span>
        {HINT_INCOME}
      </div>

      {SECTIONS.map(({ category, title, hint }) => (
        <div key={category} className="panel">
          <div className="section-header">{title}</div>
          <p className="border-b border-gray-200 bg-gray-50 p-3 text-xs text-gray-600">{hint}</p>
          <div className="grid grid-cols-1 gap-3 p-3 sm:grid-cols-2 lg:grid-cols-4">
            {entries[category].map((entry) => (
              <div key={entry.slot} className="flex gap-2">
                <input
                  className="field-input"
                  placeholder={`Source ${entry.slot}`}
                  value={entry.source}
                  onChange={(e) => updateEntry(category, entry.slot, 'source', e.target.value)}
                />
                <input
                  className="field-input w-28"
                  type="number"
                  step="0.01"
                  placeholder="Amount"
                  value={entry.amount || ''}
                  onChange={(e) => updateEntry(category, entry.slot, 'amount', e.target.value)}
                />
              </div>
            ))}
          </div>
          <div className="border-t border-gray-200 px-3 py-2 text-right text-sm font-semibold">
            Subtotal: ${subtotals[category].toLocaleString(undefined, { minimumFractionDigits: 2 })}
          </div>
        </div>
      ))}

      <div className="panel flex items-center justify-end gap-3 p-4">
        <label className="field-label m-0">Income Total</label>
        <input
          className="field-input w-40 text-right font-bold"
          type="number"
          step="0.01"
          value={incomeTotal}
          onChange={(e) => {
            setIncomeTotal(Number(e.target.value) || 0);
            setTotalTouched(true);
            markDirty();
          }}
        />
      </div>

      <SaveBar dirty={dirty} saving={saving} error={error} justSaved={justSaved} onSave={handleSave} />
    </div>
  );
}
