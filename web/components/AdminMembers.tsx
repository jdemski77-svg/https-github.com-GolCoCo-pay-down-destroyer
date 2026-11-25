'use client';

import { useCallback, useEffect, useMemo, useState } from 'react';
import { base44, errorMessage } from '@/lib/base44Client';

interface Member {
  email: string;
  full_name: string | null;
  role: string;
  joined: string;
  existing_customer?: boolean;
  status: 'active' | 'revoked';
}

/** Admin-only: everyone who has signed up, with a Revoke / Restore button. */
export default function AdminMembers() {
  const [members, setMembers] = useState<Member[] | null>(null);
  const [query, setQuery] = useState('');
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
    return (members ?? []).filter((m) => !q || m.email.includes(q) || (m.full_name ?? '').toLowerCase().includes(q));
  }, [members, query]);

  const revokedCount = (members ?? []).filter((m) => m.status === 'revoked').length;

  async function act(email: string, action: 'revoke' | 'restore') {
    if (action === 'revoke' && !window.confirm(`Revoke access for ${email}? They will be locked out immediately.`)) return;
    setBusy(email);
    setError('');
    setNotice('');
    try {
      await base44.functions.invoke('admin-members', { action, email });
      setNotice(action === 'revoke' ? `${email} has been revoked.` : `${email} has access again.`);
      await load();
    } catch (err) {
      setError(errorMessage(err, 'That change did not go through.'));
    }
    setBusy(null);
  }

  return (
    <div className="mx-auto max-w-6xl space-y-4">
      <div className="panel">
        <div className="section-header">Members</div>
        <div className="flex flex-wrap items-center gap-x-5 gap-y-2 p-4 text-sm">
          <span>
            <b>{members?.length ?? 0}</b> members
          </span>
          <span>
            <b>{(members ?? []).filter((m) => m.existing_customer).length}</b> existing customers
          </span>
          <span>
            <b>{revokedCount}</b> revoked
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
              <th className="px-4 py-2">Access</th>
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
            {shown.map((m) => {
              const isAdmin = m.role === 'admin';
              return (
                <tr key={m.email}>
                  <td className="px-4 py-2">
                    <div className="font-semibold">{m.full_name || m.email}</div>
                    {m.full_name && <div className="text-xs text-gray-500">{m.email}</div>}
                    {m.existing_customer && (
                      <div className="text-xs font-semibold text-brand-blue">Existing customer</div>
                    )}
                  </td>
                  <td className="px-4 py-2">
                    {isAdmin ? (
                      <span className="rounded bg-blue-100 px-2 py-0.5 text-xs font-semibold text-brand-blue">Admin</span>
                    ) : m.status === 'revoked' ? (
                      <span className="rounded bg-red-100 px-2 py-0.5 text-xs font-semibold text-red-800">Revoked</span>
                    ) : (
                      <span className="rounded bg-green-100 px-2 py-0.5 text-xs font-semibold text-green-800">Active</span>
                    )}
                  </td>
                  <td className="px-4 py-2 text-gray-600">{new Date(m.joined).toLocaleDateString()}</td>
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
                    {!isAdmin && m.status === 'revoked' && (
                      <button
                        disabled={busy === m.email}
                        onClick={() => act(m.email, 'restore')}
                        className="rounded bg-brand-green px-3 py-1 text-xs font-bold text-white hover:opacity-90 disabled:opacity-50"
                      >
                        Restore
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
      <p className="text-xs text-gray-500">Revoke locks someone out right away. Restore lets them back in.</p>
    </div>
  );
}
