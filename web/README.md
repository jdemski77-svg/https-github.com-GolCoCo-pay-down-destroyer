# The Pay Down Blueprint — Members Area (Base44)

A members area for your clients, running on Base44: login, database and hosting.
No payments — you send clients the link and they sign up.

**Live at:** https://members.paydowndebtsolutions.com

## For clients

1. Open https://members.paydowndebtsolutions.com and click **Create your account**
   (or go straight to `/join/`; you can prefill the email with `/join/?email=client@example.com`).
2. Enter email + password, type the code Base44 emails them, and they're in the Pay Down tool.
3. Returning clients log in. **My Account** lets them change their password; **Forgot password?**
   is on the login screen.

## Existing-customer link (expires)

For people who already bought the downloadable course + app:
**https://members.paydowndebtsolutions.com/existing/**

- Says "Create your Free Account to Access the Blueprint App".
- Works until the end of **Dec 25, 2026 (11:59 PM US Eastern)**, then shows "This link has expired".
  Accounts already created keep working.
- Sign-ups through it are tagged **Existing customer** in the Members tab (the server refuses the
  tag after the deadline).
- To change the date, edit `EXISTING_CUSTOMER_DEADLINE` in both `web/lib/offers.ts` and
  `web/base44/shared/membership.ts`, then redeploy.

## For admins

Log in with an admin account (`info@paydowndebtsolutions.com`) and open the **Members** tab:

- **Revoke** — locks that client out immediately. Their saved data is locked too.
- **Restore** — lets them back in.

Anyone with the link can create an account.

## How it's protected

- Each client's saved data (`PayDownPlan`) can only be read or changed by that client, and only
  while they're not revoked — enforced by Base44's server rules, not just the screens.
- Revokes live in `MemberAccess` (admins only) and are copied onto the user's locked
  `membership_status` field, which clients can't edit.

## Making changes

```bash
cd web
npm install
npx base44 login
cp .env.local.example .env.local   # app id: 6b04fe2f4817010412dc98bb
npm run dev                         # run locally (uses the real Base44 app)
npx base44 deploy --build           # publish
```

## Project layout

| Path | What it is |
|---|---|
| `app/page.tsx` | Landing/login → Pay Down tool (or "access turned off" if revoked) |
| `components/JoinScreen.tsx` | Create-your-account page (`/join/`, and `/existing/` for existing customers) |
| `components/ResetPasswordScreen.tsx` | Password-reset page (`/reset-password/`) |
| `components/AdminMembers.tsx` | Admin Members tab (revoke / restore) |
| `components/AccountPanel.tsx` | My Account (change password) |
| `components/tabs/` | The Pay Down tool itself |
| `base44/entities/` | Data model + access rules |
| `base44/functions/` | Server code: access check and admin actions |
