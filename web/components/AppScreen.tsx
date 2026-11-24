'use client';

import { useState } from 'react';
import TopBar from '@/components/TopBar';
import IncomeTab from '@/components/tabs/IncomeTab';
import DebtsTab from '@/components/tabs/DebtsTab';
import IdentifyImplementTab from '@/components/tabs/IdentifyImplementTab';
import AccountPanel from '@/components/AccountPanel';
import AdminMembers from '@/components/AdminMembers';

type TabKey = 'income' | 'debts' | 'implement' | 'account' | 'members';

export interface AppUser {
  id: string;
  email: string;
  full_name?: string | null;
}

export default function AppScreen({ user, isAdmin }: { user: AppUser; isAdmin: boolean }) {
  const [tab, setTab] = useState<TabKey>('income');

  const tabs: { key: TabKey; label: string; activeClass: string }[] = [
    { key: 'income', label: 'Organize Income', activeClass: 'bg-brand-green text-white' },
    { key: 'debts', label: 'Organize Debts', activeClass: 'bg-brand-red text-white' },
    { key: 'implement', label: 'Identify & Implement', activeClass: 'bg-brand-blue text-white' },
    { key: 'account', label: 'My Account', activeClass: 'bg-brand-navy text-white' },
    ...(isAdmin ? [{ key: 'members' as TabKey, label: 'Members', activeClass: 'bg-brand-ink text-white' }] : []),
  ];

  return (
    <div className="flex min-h-screen flex-col">
      <TopBar email={user.email} />
      <div className="flex overflow-x-auto border-b border-gray-300 bg-white">
        {tabs.map((t) => (
          <button
            key={t.key}
            onClick={() => setTab(t.key)}
            className={`shrink-0 px-5 py-3 font-display text-sm uppercase tracking-wide ${
              tab === t.key ? t.activeClass : 'text-gray-600 hover:bg-gray-100'
            } ${t.key === 'account' ? 'ml-auto' : ''}`}
          >
            {t.label}
          </button>
        ))}
      </div>
      <div className="flex-1 overflow-auto bg-gray-100 p-4">
        {tab === 'income' && <IncomeTab />}
        {tab === 'debts' && <DebtsTab />}
        {tab === 'implement' && <IdentifyImplementTab />}
        {tab === 'account' && <AccountPanel user={user} />}
        {tab === 'members' && isAdmin && <AdminMembers />}
      </div>
    </div>
  );
}
