'use client';

export default function SaveBar({
  dirty,
  saving,
  error,
  justSaved,
  onSave,
}: {
  dirty: boolean;
  saving: boolean;
  error: string | null;
  justSaved: boolean;
  onSave: () => void;
}) {
  if (!dirty && !saving && !error && !justSaved) return null;

  return (
    <div className="fixed bottom-0 left-0 right-0 z-20 flex items-center justify-between border-t border-gray-300 bg-white px-6 py-3 shadow-[0_-2px_8px_rgba(0,0,0,0.12)]">
      <span className="text-sm">
        {error ? (
          <span className="font-semibold text-brand-red">{error}</span>
        ) : saving ? (
          <span className="text-gray-600">Saving…</span>
        ) : dirty ? (
          <span className="font-semibold text-brand-red">You have unsaved changes</span>
        ) : (
          <span className="font-semibold text-brand-green">Saved</span>
        )}
      </span>
      {(dirty || error) && (
        <button
          onClick={onSave}
          disabled={saving}
          className="rounded bg-brand-green px-5 py-2 text-sm font-bold text-white hover:opacity-90 disabled:opacity-50"
        >
          {saving ? 'Saving…' : 'Save'}
        </button>
      )}
    </div>
  );
}
