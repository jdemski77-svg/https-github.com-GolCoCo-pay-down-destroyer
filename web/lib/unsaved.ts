/**
 * Tracks screens with changes that aren't saved yet, and makes the browser ask
 * "Leave site? Changes you made may not be saved" if someone closes or reloads
 * the page while any are pending.
 */
const pending = new Set<string>();
let installed = false;

function install() {
  if (installed || typeof window === 'undefined') return;
  installed = true;
  window.addEventListener('beforeunload', (e) => {
    if (pending.size === 0) return;
    e.preventDefault();
    // Older browsers need returnValue set to show the prompt.
    e.returnValue = '';
  });
}

export function setUnsaved(key: string, unsaved: boolean) {
  install();
  if (unsaved) pending.add(key);
  else pending.delete(key);
}

export function hasUnsaved(): boolean {
  return pending.size > 0;
}
