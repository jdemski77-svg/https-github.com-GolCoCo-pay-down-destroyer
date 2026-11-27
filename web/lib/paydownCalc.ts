import { PayDownResult, PayDownResultRow, TraditionalResult } from './types';

/**
 * Faithful port of CtrlPDImplement.xaml.cs: UpdateSavingsResultTable.
 * `rate` is a decimal (annual %/100), applied per-iteration as-is (NOT /12) —
 * this matches the original app's baseline exactly, quirk included.
 */
export function calcTraditional(balance: number, payment: number, rate: number): TraditionalResult {
  let totalinterest = 0;
  let interest = balance * rate;
  let principal = payment - interest;
  if (principal <= 0) {
    return { monthsToPayCurrent: -1, totalInterestPaidCurrent: -1 };
  }
  totalinterest += interest;
  balance -= principal;
  if (payment >= balance) {
    return { monthsToPayCurrent: 1, totalInterestPaidCurrent: interest };
  }
  let months = 0;
  while (payment < balance && months < 1200) {
    interest = balance * rate;
    principal = payment - interest;
    if (principal <= 0) {
      return { monthsToPayCurrent: -1, totalInterestPaidCurrent: -1 };
    }
    balance -= principal;
    totalinterest += interest;
    months += 1;
  }
  if (months >= 1200) {
    return { monthsToPayCurrent: -1, totalInterestPaidCurrent: -1 };
  }
  interest = balance * rate;
  totalinterest += interest;
  return {
    monthsToPayCurrent: months + 2,
    totalInterestPaidCurrent: Math.round(totalinterest * 100) / 100,
  };
}

/**
 * Faithful port of CtrlPDImplement.xaml.cs: GetResultModels (the "Pay Down" strategy).
 * sb = initial balance, ai = available income, ir = annual interest rate as decimal
 * (e.g. 0.18 for 18%), ce = chargeable expenses.
 */
export function calcPayDown(sb: number, ai: number, ir: number, ce: number): PayDownResult {
  const rows: PayDownResultRow[] = [];
  let row = 1;
  let balance = sb;

  while (balance >= ai) {
    const sb1 = balance - ai;
    const interest = Math.floor(((balance - ai + balance) / 2) * (ir / 12)) + 1;
    const ending = sb1 + ce + interest;
    if (balance < ending) break;
    rows.push({
      monthsToPayoff: row,
      monthlyStartingBalance: balance,
      availableIncome: ai,
      newBalance: sb1,
      chargeableExp: ce,
      interest,
      monthlyEndingBalance: ending,
    });
    balance = ending;
    row += 1;
  }

  const totalInterestPaid = rows.reduce((sum, r) => sum + r.interest, 0);
  return { rows, totalInterestPaid, monthsToPay: rows.length };
}
