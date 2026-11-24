'use client';

import { useCallback, useEffect, useMemo, useState } from 'react';
import { base44, errorMessage } from '@/lib/base44Client';

interface Member {
  id: string;
  email: string;
  full_name: string | null;
  role: string;
  created_date: string;
  membership_status: 'none' | 'active' | 'inactive' | 'revoked';
  membership_source: 'purchase' | 'manual' | null;
  has_subscription: boolean;
}

const STATUS_STYLE: Record<Member['membership_status'], string> = {
  active: 'bg-green-100 text-green-800',
  inactive: 'bg-gray-100 text-gray-700',
  none: 'bg-gray-100 text-gray-500',
  revoked: 'bg-red-100 text-red-800',
};

/** Admin-only: see every member and revoke, restore, or grant access. */
export default function AdminMembers() {
  const [members, setMembers] = useState<Member[] | null>(null);
  const [query, setQuery] = useState('');
  const [busyId, setBusyId] = useState<string | null>(null);
  const [error, setError] = useState('');
  const [notice, setNotice] = useState('');

  const load = useCallback(async () => {
    setError('');
    try {
      const res = await base44.functions.invoke('admin-members', { action: 'list' });
      setMembers(res.data.members);
    } catch (err) {
      setError(errorMessage(err, 'Could not load members.'));
    }
  }, []);

  useEffect(() => {
    load();
  }, [load]);

  const shown = useMemo(() => {
    const q = query.trim().toLowerCase();
    return (members ?? []).filter(
      (m) => !q || m.email.toLowerCase().includes(q) || (m.full_name ?? '').toLowerCase().includes(q)
    );
  }, [members, query]);

  async function act(member: Member, action: 'revoke' | 'restore' | 'grant') {
    let cancelSubscription = false;
    if (action === 'revoke') {
      if (!window.confirm(`Revoke access for ${member.email}? They will be locked out immediately.`)) return;
      if (member.has_subscription) {
        cancelSubscription = window.confirm(
          'Also cancel their subscription so they are not billed again?\n\nOK = cancel billing too\nCancel = keep billing, just block access'
        );
      }
    }
    if (action === 'grant' && !window.confirm(`Give ${member.email} free access without paying?`)) return;

    setBusyId(member.id);
    setError('');
    setNotice('');
    try {
      const res = await base44.functions.invoke('admin-members', { action, userId: member.id, cancelSubscription });
      const sub = res.data.subscription ? ` Subscription: ${res.data.subscription}.` : '';
      setNotice(`${member.email} is now ${res.data.membership_status}.${sub}`);
      await load();
    } catch (err) {
      setError(errorMessage(err, 'That change did not go through.'));
    }
    setBusyId(null);
  }

  const counts = useMemo(() => {
    const c = { active: 0, revoked: 0, other: 0 };
    (members ?? []).forEach((m) => {
      if (m.membership_status === 'active') c.active++;
      else if (m.membership_status === 'revoked') c.revoked++;
      else c.other++;
    });
    return c;
  }, [members]);

  return (
    <div className="mx-auto max-w-6xl space-y-4">
      <div className="panel">
        <div className="section-header">Members</div>
        <div className="flex flex-wrap items-center gap-3 p-4 text-sm">
          <span>
            <b>{counts.active}</b> active
          </span>
          <span>
            <b>{counts.revoked}</b> revoked
          </span>
          <span>
            <b>{counts.other}</b> not paying
          </span>
          <input
            className="field-input ml-auto max-w-xs"
            placeholder="Search email or name"
            value={query}
            onChange={(e) => setQuery(e.target.value)}
          />
        </div>
        {error && <p className="px-4 pb-3 text-sm text-brand-red">{error}</p>}
        {notice && <p className="px-4 pb-3 text-sm text-brand-green">{notice}</p>}
      </div>

      <div className="panel overflow-x-auto">
        <table className="w-full text-left text-sm">
          <thead className="bg-gray-50 text-xs uppercase text-gray-500">
            <tr>
              <th className="px-4 py-2">Member</th>
              <th className="px-4 py-2">Status</th>
              <th className="px-4 py-2">Joined</th>
              <th className="px-4 py-2 text-right">Actions</th>
            </tr>
          </thead>
          <tbody className="divide-y divide-gray-100">
            {members === null && (
              <tr>
                <td colSpan={4} className="px-4 py-6 text-center text-gray-500">
                  Loading…
                </td>
              </tr>
            )}
            {shown.map((m) => (
              <tr key={m.id}>
                <td className="px-4 py-2">
                  <div className="font-semibold">{m.full_name || m.email}</div>
                  {m.full_name && <div className="text-xs text-gray-500">{m.email}</div>}
                  {m.role === 'admin' && <div className="text-xs font-semibold text-brand-blue">Admin</div>}
                </td>
                <td className="px-4 py-2">
                  <span className={`rounded px-2 py-0.5 text-xs font-semibold capitalize ${STATUS_STYLE[m.membership_status]}`}>
                    {m.membership_status === 'none' ? 'not paid' : m.membership_status}
                  </span>
                  {m.membership_source === 'manual' && m.membership_status === 'active' && (
                    <span className="ml-1 text-xs text-gray-500">(free)</span>
                  )}
                </td>
                <td className="px-4 py-2 text-gray-600">{new Date(m.created_date).toLocaleDateString()}</td>
                <td className="space-x-2 whitespace-nowrap px-4 py-2 text-right">
                  {m.role !== 'admin' && m.membership_status !== 'revoked' && (
                    <button
                      disabled={busyId === m.id}
                      onClick={() => act(m, 'revoke')}
                      className="rounded bg-brand-red px-3 py-1 text-xs font-bold text-white hover:opacity-90 disabled:opacity-50"
                    >
                      Revoke
                    </button>
                  )}
                  {m.membership_status === 'revoked' && (
                    <button
                      disabled={busyId === m.id}
                      onClick={() => act(m, 'restore')}
                      className="rounded bg-brand-green px-3 py-1 text-xs font-bold text-white hover:opacity-90 disabled:opacity-50"
                    >
                      Restore
                    </button>
                  )}
                  {m.role !== 'admin' && m.membership_status !== 'active' && (
                    <button
                      disabled={busyId === m.id}
                      onClick={() => act(m, 'grant')}
                      className="rounded border border-brand-blue px-3 py-1 text-xs font-bold text-brand-blue hover:bg-blue-50 disabled:opacity-50"
                    >
                      Grant free access
                    </button>
                  )}
                </td>
              </tr>
            ))}
            {members !== null && shown.length === 0 && (
              <tr>
                <td colSpan={4} className="px-4 py-6 text-center text-gray-500">
                  No members found.
                </td>
              </tr>
            )}
          </tbody>
        </table>
      </div>
      <p className="text-xs text-gray-500">
        Revoke blocks access right away, even if they keep paying. Restore removes the block and access then follows their
        payments again. Members also lose access automatically when their payment is canceled or refunded.
      </p>
    </div>
  );
}
