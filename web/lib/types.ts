export type IncomeCategory = 'paycheck' | 'retirement' | 'governmental' | 'checking';

export interface IncomeEntry {
  id?: string;
  user_id?: string;
  category: IncomeCategory;
  slot: number;
  source: string;
  amount: number;
}

export interface MonthlyBillEntry {
  id?: string;
  user_id?: string;
  slot: number;
  chargeable: boolean;
  source: string;
  amount: number;
}

export interface LivingExpenseEntry {
  id?: string;
  user_id?: string;
  slot: number;
  chargeable: boolean;
  source: string;
  amount: number;
}

export interface LoanEntry {
  id?: string;
  user_id?: string;
  slot: number;
  source: string;
  payment: number;
  balance: number;
  interest_rate: number;
  outstanding_interest: number;
}

export type CreditType = 'CC' | 'LOC';

export interface CreditEntry {
  id?: string;
  user_id?: string;
  slot: number;
  source: string;
  payment: number;
  balance: number;
  interest_rate: number;
  credit_limit: number;
  type: CreditType;
}

export interface UserSettings {
  user_id: string;
  income_total: number;
  mortgage_rent_payment: number;
  total_monthly_exp: number;
  total_debts: number;
  chargeable_expenses: number;
  non_chargeable_expenses: number;
  credit_score_1: string;
  credit_score_2: string;
}

export interface PayDownResultRow {
  monthsToPayoff: number;
  monthlyStartingBalance: number;
  availableIncome: number;
  newBalance: number;
  chargeableExp: number;
  interest: number;
  monthlyEndingBalance: number;
}

export interface TraditionalResult {
  monthsToPayCurrent: number;
  totalInterestPaidCurrent: number;
}

export interface PayDownResult {
  rows: PayDownResultRow[];
  totalInterestPaid: number;
  monthsToPay: number;
}
