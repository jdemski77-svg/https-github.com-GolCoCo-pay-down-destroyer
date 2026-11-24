'use client';

import { useCallback, useEffect, useMemo, useState } from 'react';
import { base44, errorMessage } from '@/lib/base44Client';

interface Member {
  email: string;
  full_name: string | null;
  status: 'none' | 'active' | 'inactive' | 'revoked';
  source: 'ghl' | 'admin' | null;
  has_account: boolean;
  role: string | null;
  joined: string | null;
  updated: string | null;
}

const STATUS_LABEL: Record<Member['status'], string> = {
  active: 'Active',
  inactive: 'Canceled',
  revoked: 'Revoked',
  none: 'No access',
};
const STATUS_STYLE: Record<Member['status'], string> = {
  active: 'bg-green-100 text-green-800',
  inactive: 'bg-gray-100 text-gray-700',
  none: 'bg-gray-100 text-gray-500',
  revoked: 'bg-red-100 text-red-800',
};

/** Admin-only: see every member and turn access on or off. */
export default function AdminMembers() {
  const [members, setMembers] = useState<Member[] | null>(null);
  const [query, setQuery] = useState('');
  const [newEmail, setNewEmail] = useState('');
  const [busy, setBusy] = useState<string | null>(null);
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
    return (members ?? [])
      .filter((m) => !q || m.email.includes(q) || (m.full_name ?? '').toLowerCase().includes(q))
      .sort((a, b) => (b.updated ?? b.joined ?? '').localeCompare(a.updated ?? a.joined ?? ''));
  }, [members, query]);

  const counts = useMemo(() => {
    const c = { active: 0, revoked: 0, waiting: 0 };
    (members ?? []).forEach((m) => {
      if (m.status === 'active') {
        c.active++;
        if (!m.has_account) c.waiting++;
      } else if (m.status === 'revoked') c.revoked++;
    });
    return c;
  }, [members]);

  async function act(email: string, action: 'revoke' | 'grant') {
    if (action === 'revoke' && !window.confirm(`Revoke access for ${email}? They will be locked out immediately.`)) return;
    setBusy(email);
    setError('');
    setNotice('');
    try {
      await base44.functions.invoke('admin-members', { action, email });
      setNotice(action === 'grant' ? `${email} now has access.` : `${email} has been revoked.`);
      await load();
    } catch (err) {
      setError(errorMessage(err, 'That change did not go through.'));
    }
    setBusy(null);
  }

  async function handleAdd(e: React.FormEvent) {
    e.preventDefault();
    if (!newEmail.trim()) return;
    await act(newEmail.trim().toLowerCase(), 'grant');
    setNewEmail('');
  }

  return (
    <div className="mx-auto max-w-6xl space-y-4">
      <div className="panel">
        <div className="section-header">Members</div>
        <div className="flex flex-wrap items-center gap-x-5 gap-y-2 p-4 text-sm">
          <span>
            <b>{counts.active}</b> active
          </span>
          <span>
            <b>{counts.waiting}</b> paid, no account yet
          </span>
          <span>
            <b>{counts.revoked}</b> revoked
          </span>
          <input
            className="field-input ml-auto max-w-xs"
            placeholder="Search email or name"
            value={query}
            onChange={(e) => setQuery(e.target.value)}
          />
        </div>
        <form onSubmit={handleAdd} className="flex flex-wrap items-center gap-2 border-t border-gray-200 p-4">
          <label className="field-label m-0 w-full sm:w-auto">Give someone access by email</label>
          <input
            className="field-input max-w-sm flex-1"
            type="email"
            placeholder="name@example.com"
            value={newEmail}
            onChange={(e) => setNewEmail(e.target.value)}
          />
          <button type="submit" disabled={!!busy} className="btn-primary py-1.5">
            Give access
          </button>
        </form>
        {error && <p className="px-4 pb-3 text-sm text-brand-red">{error}</p>}
        {notice && <p className="px-4 pb-3 text-sm text-brand-green">{notice}</p>}
      </div>

      <div className="panel overflow-x-auto">
        <table className="w-full text-left text-sm">
          <thead className="bg-gray-50 text-xs uppercase text-gray-500">
            <tr>
              <th className="px-4 py-2">Member</th>
              <th className="px-4 py-2">Access</th>
              <th className="px-4 py-2">Account</th>
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
            {shown.map((m) => {
              const isAdmin = m.role === 'admin';
              return (
                <tr key={m.email}>
                  <td className="px-4 py-2">
                    <div className="font-semibold">{m.full_name || m.email}</div>
                    {m.full_name && <div className="text-xs text-gray-500">{m.email}</div>}
                    {isAdmin && <div className="text-xs font-semibold text-brand-blue">Admin</div>}
                  </td>
                  <td className="px-4 py-2">
                    <span className={`rounded px-2 py-0.5 text-xs font-semibold ${STATUS_STYLE[isAdmin ? 'active' : m.status]}`}>
                      {isAdmin ? 'Admin' : STATUS_LABEL[m.status]}
                    </span>
                    {m.source && !isAdmin && (
                      <span className="ml-1 text-xs text-gray-500">{m.source === 'ghl' ? 'via GoHighLevel' : 'set by admin'}</span>
                    )}
                  </td>
                  <td className="px-4 py-2 text-gray-600">
                    {m.has_account ? `Joined ${new Date(m.joined as string).toLocaleDateString()}` : 'Not created yet'}
                  </td>
                  <td className="whitespace-nowrap px-4 py-2 text-right">
                    {!isAdmin && m.status === 'active' && (
                      <button
                        disabled={busy === m.email}
                        onClick={() => act(m.email, 'revoke')}
                        className="rounded bg-brand-red px-3 py-1 text-xs font-bold text-white hover:opacity-90 disabled:opacity-50"
                      >
                        Revoke
                      </button>
                    )}
                    {!isAdmin && m.status !== 'active' && (
                      <button
                        disabled={busy === m.email}
                        onClick={() => act(m.email, 'grant')}
                        className="rounded bg-brand-green px-3 py-1 text-xs font-bold text-white hover:opacity-90 disabled:opacity-50"
                      >
                        {m.status === 'revoked' ? 'Restore' : 'Give access'}
                      </button>
                    )}
                  </td>
                </tr>
              );
            })}
            {members !== null && shown.length === 0 && (
              <tr>
                <td colSpan={4} className="px-4 py-6 text-center text-gray-500">
                  No members yet.
                </td>
              </tr>
            )}
          </tbody>
        </table>
      </div>
      <p className="text-xs text-gray-500">
        Revoke locks someone out right away, even if GoHighLevel later sends a payment for them — use Restore to let them back
        in. Canceling or refunding in GoHighLevel turns access off automatically once that workflow is set up. Revoking here
        does not cancel their billing in GoHighLevel.
      </p>
    </div>
  );
}
