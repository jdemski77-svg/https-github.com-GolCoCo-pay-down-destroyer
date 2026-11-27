'use client';

import { useCallback, useEffect, useRef, useState } from 'react';
import { savePlan, type PayDownPlan } from '@/lib/planStore';
import { setUnsaved } from '@/lib/unsaved';

type Patch = Partial<Omit<PayDownPlan, 'id'>>;

const AUTOSAVE_DELAY_MS = 1500;

/**
 * Autosave for a tab: call markDirty() on every edit, and the tab's changes are
 * saved 1.5 s after the last one. Pending changes are also saved when the tab is
 * left, and the browser warns before closing the page with anything unsaved.
 *
 * buildPatch must return the fields to save from the tab's current state.
 */
export function useAutosave(key: string, buildPatch: () => Patch, errorText: string) {
  const buildRef = useRef(buildPatch);
  buildRef.current = buildPatch;

  // Each edit bumps the revision; a save records which revision it covered.
  const revRef = useRef(0);
  const savedRevRef = useRef(0);
  const [rev, setRev] = useState(0);
  const [savedRev, setSavedRev] = useState(0);
  const [saving, setSaving] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [justSaved, setJustSaved] = useState(false);
  const dirty = rev !== savedRev;

  const saveNow = useCallback(async () => {
    const target = revRef.current;
    if (target === savedRevRef.current) return;
    setSaving(true);
    setError(null);
    try {
      await savePlan(buildRef.current());
      savedRevRef.current = Math.max(savedRevRef.current, target);
      setSavedRev(savedRevRef.current);
      if (savedRevRef.current === revRef.current) {
        setJustSaved(true);
        setTimeout(() => setJustSaved(false), 2500);
      }
    } catch {
      setError(errorText);
    } finally {
      setSaving(false);
    }
  }, [errorText]);

  const markDirty = useCallback(() => {
    revRef.current += 1;
    setRev(revRef.current);
    setJustSaved(false);
    setError(null);
  }, []);

  // Save a moment after the last edit. After a failed save, wait for the next
  // edit or a click on Save before trying again.
  useEffect(() => {
    if (!dirty || error) return;
    const t = setTimeout(saveNow, AUTOSAVE_DELAY_MS);
    return () => clearTimeout(t);
  }, [rev, dirty, error, saveNow]);

  // Warn before leaving the page while anything is unsaved or still saving.
  useEffect(() => {
    setUnsaved(key, dirty || saving);
  }, [key, dirty, saving]);

  // Leaving the tab: save whatever is pending right away.
  useEffect(() => {
    return () => {
      if (revRef.current !== savedRevRef.current) {
        setUnsaved(key, true);
        savePlan(buildRef.current())
          .then(() => setUnsaved(key, false))
          .catch(() => setUnsaved(key, false));
      } else {
        setUnsaved(key, false);
      }
    };
  }, [key]);

  return { markDirty, saveNow, dirty, saving, error, justSaved };
}
