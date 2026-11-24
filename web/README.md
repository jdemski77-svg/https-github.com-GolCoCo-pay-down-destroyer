# The Pay Down Blueprint — Members Area (Base44)

A members-only web version of the Pay Down tool, running entirely on Base44:
Base44 login, Base44 database, Base44 payments, and Base44 hosting.

## How it works for a new member

1. They open the site and click **Join now**.
2. They pay on the secure checkout page (your connected payment provider, e.g. Stripe).
3. They land on **Create your account** — the email is locked to the one they paid with —
   pick a password, type the code Base44 emails them, and they're in the tool.

Returning members just log in. Members can change their password under **My Account**,
or use **Forgot password?** on the login screen.

## How access is controlled

- Each user has a `membership_status` (`active`, `inactive`, `revoked`, `none`) that **only the
  server** can change — members can't edit it themselves (`base44/entities/User.jsonc`).
- Every time the app opens, the server re-checks their payment (`sync-membership`). A canceled or
  refunded payment turns access off automatically.
- All saved tool data (`PayDownPlan`) is locked by Base44's access rules to its owner **and** only
  while they're active — so even someone poking at the API can't read or save without paying.

### Revoking access (admins)

Log in with your admin account and open the **Members** tab:

- **Revoke** — locks them out immediately, even if they keep paying. You'll be asked whether to
  also cancel their subscription so they aren't billed again.
- **Restore** — removes the block; access goes back to following their payments.
- **Grant free access** — lets someone in without paying (comp accounts, testers, staff).

You can also edit `membership_status` directly on a user in the Base44 dashboard (Data → User).

## Set your price

Edit `base44/shared/membership.ts`:

```ts
export const PLAN = {
  name: "Pay Down Membership",
  amountCents: 1999,        // $19.99 — change this
  currency: "usd",
  frequency: "month",       // "month", "year", or null for a one-time payment
};
```

## One-time setup

**Important:** deploying to an app *replaces* that app's site and deletes any data tables
(entities) it has that aren't in this folder. Use a **new, empty Base44 app** for this unless you're
sure the existing one has nothing you need.

```bash
cd web
npm install
npx base44 login
npx base44 link            # pick your Base44 app (or: npx base44 link --create --name "Pay Down Blueprint")
cp .env.local.example .env.local   # paste your app id (it's in base44/.app.jsonc after linking)
```

In the Base44 dashboard for the app:

1. **Payments** → connect Stripe (recommended — supports cancel/refund) or Base44 Payments.
   PayPal is not supported by this setup.
2. **Users / Settings** → make sure you are an **admin** of the app, so you see the Members tab.
3. Leave the app **public** (visitors must reach the Join page before they have an account).

Then deploy everything (data rules, server functions, and the site):

```bash
npx base44 deploy --build
```

Test it end to end with a Stripe **test-mode** payment before going live.

## Day-to-day

```bash
npm run dev                 # run locally (uses your real Base44 app)
npx base44 deploy --build   # publish changes
```

## Project layout

| Path | What it is |
|---|---|
| `app/page.tsx` | The gate: landing page → paywall → tool |
| `app/join/` | Post-payment "create your account" page |
| `app/reset-password/` | Target page for password-reset emails |
| `components/LoginScreen.tsx` | Landing page with Join + member login |
| `components/AdminMembers.tsx` | Admin Members tab (revoke / restore / grant) |
| `components/AccountPanel.tsx` | My Account (change password) |
| `base44/entities/` | Data model + access rules |
| `base44/functions/` | Server code: checkout, payment check, admin actions |
| `base44/shared/membership.ts` | Price and membership rules |
