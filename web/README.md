# The Pay Down Blueprint — Members Area (Base44)

A members-only web version of the Pay Down tool, running entirely on Base44:
Base44 login, Base44 database, Base44 payments, and Base44 hosting.

## How it works for a new member

1. They buy in your **GoHighLevel** funnel.
2. A GoHighLevel workflow tells the members area that this email has paid.
3. Your thank-you page sends them to **/join/** on the members site, where they create a
   password (using the email they paid with), type the code Base44 emails them, and they're in.

Returning members just log in. Members can change their password under **My Account**,
or use **Forgot password?** on the login screen.

## How access is controlled

- `MemberAccess` holds one record per paying email (`active`, `inactive` = canceled, `revoked`).
  GoHighLevel fills it through the `ghl-webhook` function; admins change it in the Members tab.
  Members can't read or change it.
- Each time someone opens the app, the server copies their email's access onto their account
  (`membership_status`, which members can't edit — `base44/entities/User.jsonc`).
- All saved tool data (`PayDownPlan`) is locked by Base44's access rules to its owner **and** only
  while they're active, so nobody can read or save without access, even by calling the API directly.

### Revoking access (admins)

Log in with your admin account and open the **Members** tab:

- **Revoke** — locks them out immediately. A later GoHighLevel payment won't undo it.
- **Restore** / **Give access** — turns access on. Also works for an email that hasn't
  created an account yet (comps, staff, testers).
- Revoking here does **not** stop billing in GoHighLevel — cancel the subscription there too.

## GoHighLevel setup

1. In the Base44 dashboard → **Secrets**, add `GHL_WEBHOOK_SECRET` with a long random value
   (e.g. a password-manager-generated 32+ character string). Keep it private.
2. Find your webhook address: Base44 dashboard → Code → Functions → `ghl-webhook`. It looks like
   `https://<your-app-domain>/functions/ghl-webhook`.
3. In GoHighLevel, create a workflow:
   - **Trigger:** your payment trigger (e.g. *Order Submitted* / *Payment Received* for the
     Blueprint product).
   - **Action:** *Webhook* → method **POST** → URL
     `https://<your-app-domain>/functions/ghl-webhook?key=<GHL_WEBHOOK_SECRET>&action=grant`
4. Create a second workflow for cancellations/refunds (e.g. *Subscription Canceled*, *Refund*)
   with the same webhook but `&action=revoke`.
5. On your thank-you page, add a button linking to
   `https://<your-app-domain>/join/?email={{contact.email}}` so the email is prefilled.

The webhook reads the contact's email from GoHighLevel's standard webhook body
(`email`), so no custom data is needed.

## One-time setup

**Important:** deploying to an app *replaces* that app's site and deletes any data tables
(entities) it has that aren't in this folder. Use a **new, empty Base44 app** for this unless you're
sure the existing one has nothing you need.

```bash
cd web
npm install
npx base44 login
npx base44 link            # pick your Base44 app (or: npx base44 link --create --name "Pay Down Blueprint")
cp .env.local.example .env.local   # paste your app id and your GoHighLevel sales page URL
npx base44 deploy --build
```

In the Base44 dashboard: make sure you're an **admin** of the app (to see the Members tab),
leave the app **public** (buyers must reach /join before they have an account), and add the
`GHL_WEBHOOK_SECRET` secret.

Test end to end with a test purchase in GoHighLevel (use a different email from your admin
account — admins always have access).

## Day-to-day

```bash
npm run dev                 # run locally (uses your real Base44 app)
npx base44 deploy --build   # publish changes
```

## Project layout

| Path | What it is |
|---|---|
| `app/page.tsx` | The gate: landing page → "no membership" screen → tool |
| `app/join/` | Post-purchase "create your account" page |
| `app/reset-password/` | Target page for password-reset emails |
| `components/LoginScreen.tsx` | Landing page with Join (to GoHighLevel) + member login |
| `components/AdminMembers.tsx` | Admin Members tab (revoke / restore / give access) |
| `components/AccountPanel.tsx` | My Account (change password) |
| `base44/entities/` | Data model + access rules |
| `base44/functions/ghl-webhook/` | Receives GoHighLevel purchase / cancel events |
| `base44/functions/sync-membership/` | Re-checks a member's access when they open the app |
| `base44/functions/admin-members/` | Admin actions |
| `base44/shared/membership.ts` | Membership rules |
