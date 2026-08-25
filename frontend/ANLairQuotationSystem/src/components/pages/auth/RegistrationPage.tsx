import { ArrowRight, Check, CircleAlert, LockKeyhole, Mail, ShieldCheck, User } from "lucide-react";
import { useMemo, useState } from "react";
import { useNavigate } from "react-router";
import type { RegistrationPayload } from "../../../types/auth/RegistrationPayload";
import { Register } from "../../../services/AuthApiServices";
import type { ErrorResponse } from "../../../types/common/ErrorResponse";

const EMPTY_REGISTRATION_PAYLOAD = {
  Username: "",
  Password: "",
  ConfirmPassword: "",
  Firstname: "",
  Middlename: "",
  Surname: "",
  ContactNumber: "",
  Email: "",
  ExtensionName: "",
};

export default function RegistrationPage() {
  const navigate = useNavigate();

  const [registrationPayload, setRegistrationPayload] = useState<RegistrationPayload>(
    EMPTY_REGISTRATION_PAYLOAD,
  );

  const passwordRequirements = useMemo(
    () => ({
      length: registrationPayload.Password.length >= 8,
      uppercase: /[A-Z]/.test(registrationPayload.Password),
      lowercase: /[a-z]/.test(registrationPayload.Password),
      number: /[0-9]/.test(registrationPayload.Password),
      special: /[^A-Za-z0-9]/.test(registrationPayload.Password),
    }),
    [registrationPayload.Password],
  );

  const isPasswordValid = Object.values(passwordRequirements).every(Boolean);

  const isFormValid =
    registrationPayload.Firstname.trim() !== "" &&
    registrationPayload.Surname.trim() !== "" &&
    registrationPayload.Email.trim() !== "" &&
    registrationPayload.Username.trim() !== "" &&
    isPasswordValid &&
    registrationPayload.Password === registrationPayload.ConfirmPassword;

  const handleRegistration = async () => {
    if (!isFormValid) {
      return;
    }
    try {
      const res = await Register(registrationPayload);
      if (res.status === 400) {
        const err: ErrorResponse = res.data as any;
        alert(err.Message);
        return;
      }

      const data = res.data;

      if (!data.IsSuccess) {
        alert(data.Message);
        return;
      }

      alert("Successful registration");
      setRegistrationPayload(EMPTY_REGISTRATION_PAYLOAD);

      navigate("auth/login");
    } catch (err) {
      console.log(err);
    }
  };

  return (
    <main className="flex h-dvh w-full bg-alabaster-grey-100 font-sans text-ink-black-900">
      {/* Brand panel */}
      <section className="relative hidden w-1/2 overflow-hidden bg-ink-black-950 p-12 lg:flex lg:flex-col lg:justify-between">
        <div className="absolute -top-32 -right-32 h-96 w-96 rounded-full bg-prussian-blue-800/30 blur-3xl" />
        <div className="absolute -bottom-40 -left-20 h-96 w-96 rounded-full bg-dusk-blue-800/20 blur-3xl" />

        <div className="relative">
          <div className="flex items-center gap-3">
            <div className="flex h-11 w-11 items-center justify-center rounded-xl bg-prussian-blue-600 font-heading text-lg font-bold text-white shadow-lg shadow-prussian-blue-950/30">
              A
            </div>

            <div>
              <p className="font-heading text-lg font-bold tracking-tight text-alabaster-grey-50">
                ANLair
              </p>

              <p className="text-xs font-medium tracking-wide text-dusty-denim-300">
                QUOTATION SYSTEM
              </p>
            </div>
          </div>
        </div>

        <div className="relative max-w-xl">
          <h2 className="font-heading text-4xl leading-tight font-bold tracking-tight text-alabaster-grey-50 xl:text-5xl">
            Build a better way to manage your quotations.
          </h2>

          <p className="mt-6 max-w-lg text-base leading-7 text-dusty-denim-300">
            Create your ANLair account and gain access to a centralized workspace for preparing,
            organizing, and managing professional business quotations.
          </p>

          <div className="mt-8 space-y-4">
            <div className="flex items-center gap-3 text-sm text-dusty-denim-300">
              <div className="flex h-7 w-7 items-center justify-center rounded-full bg-prussian-blue-900/60">
                <Check className="h-3.5 w-3.5 text-prussian-blue-400" />
              </div>

              <span>Organize quotations in one place</span>
            </div>

            <div className="flex items-center gap-3 text-sm text-dusty-denim-300">
              <div className="flex h-7 w-7 items-center justify-center rounded-full bg-prussian-blue-900/60">
                <Check className="h-3.5 w-3.5 text-prussian-blue-400" />
              </div>

              <span>Maintain a professional quotation workflow</span>
            </div>

            <div className="flex items-center gap-3 text-sm text-dusty-denim-300">
              <div className="flex h-7 w-7 items-center justify-center rounded-full bg-prussian-blue-900/60">
                <Check className="h-3.5 w-3.5 text-prussian-blue-400" />
              </div>

              <span>Keep your business information secure</span>
            </div>
          </div>
        </div>

        <div className="relative flex items-center gap-3 text-sm text-dusty-denim-400">
          <ShieldCheck className="h-4 w-4 text-prussian-blue-400" />
          <span>Secure business management</span>
        </div>
      </section>

      {/* Registration panel */}
      <section className="flex w-full items-center-safe justify-center-safe overflow-y-auto p-6 sm:p-10 lg:w-1/2">
        <div className="w-full max-w-xl">
          {/* Mobile branding */}
          <div className="mb-10 flex items-center gap-3 lg:hidden">
            <div className="flex h-10 w-10 items-center justify-center rounded-lg bg-prussian-blue-600 font-heading font-bold text-white">
              A
            </div>

            <div>
              <p className="font-heading font-bold text-ink-black-900">ANLair</p>

              <p className="text-[10px] font-semibold tracking-wider text-dusty-denim-500">
                QUOTATION SYSTEM
              </p>
            </div>
          </div>

          {/* Header */}
          <div className="mb-8">
            <p className="mb-2 text-sm font-semibold text-prussian-blue-600">Create your account</p>

            <h1 className="font-heading text-3xl font-bold tracking-tight text-ink-black-900">
              Get started with ANLair
            </h1>

            <p className="mt-3 text-sm leading-6 text-dusty-denim-600">
              Set up your account to start managing your business quotations.
            </p>
          </div>

          <div className="space-y-5">
            {/* Name */}
            <div className="grid grid-cols-1 gap-5 sm:grid-cols-2">
              {/* First Name */}
              <div>
                <label
                  htmlFor="firstName"
                  className="mb-2 block text-sm font-semibold text-ink-black-800"
                >
                  First name
                </label>

                <div className="relative">
                  <User className="pointer-events-none absolute top-1/2 left-3.5 h-4 w-4 -translate-y-1/2 text-dusty-denim-400" />

                  <input
                    id="firstName"
                    name="firstName"
                    type="text"
                    value={registrationPayload.Firstname}
                    onChange={(e) =>
                      setRegistrationPayload((p) => ({
                        ...p,
                        Firstname: e.target.value,
                      }))
                    }
                    autoComplete="given-name"
                    placeholder="Juan"
                    className="h-12 w-full rounded-lg border border-alabaster-grey-300 bg-alabaster-grey-50 pr-4 pl-10 text-sm text-ink-black-900 transition outline-none placeholder:text-dusty-denim-400 focus:border-prussian-blue-500 focus:ring-4 focus:ring-prussian-blue-500/10"
                  />
                </div>
              </div>

              {/* Middle Name */}
              <div>
                <label
                  htmlFor="lastName"
                  className="mb-2 block text-sm font-semibold text-ink-black-800"
                >
                  Middle name
                </label>

                <div className="relative">
                  <User className="pointer-events-none absolute top-1/2 left-3.5 h-4 w-4 -translate-y-1/2 text-dusty-denim-400" />

                  <input
                    id="lastName"
                    name="lastName"
                    type="text"
                    value={registrationPayload.Middlename}
                    onChange={(e) =>
                      setRegistrationPayload((p) => ({
                        ...p,
                        Middlename: e.target.value,
                      }))
                    }
                    autoComplete="family-name"
                    placeholder="Dela Cruz"
                    className="h-12 w-full rounded-lg border border-alabaster-grey-300 bg-alabaster-grey-50 pr-4 pl-10 text-sm text-ink-black-900 transition outline-none placeholder:text-dusty-denim-400 focus:border-prussian-blue-500 focus:ring-4 focus:ring-prussian-blue-500/10"
                  />
                </div>
              </div>

              {/* Last Name */}
              <div>
                <label
                  htmlFor="lastName"
                  className="mb-2 block text-sm font-semibold text-ink-black-800"
                >
                  Last Name
                </label>

                <div className="relative">
                  <User className="pointer-events-none absolute top-1/2 left-3.5 h-4 w-4 -translate-y-1/2 text-dusty-denim-400" />

                  <input
                    id="lastName"
                    name="lastName"
                    type="text"
                    value={registrationPayload.Surname}
                    onChange={(e) =>
                      setRegistrationPayload((p) => ({
                        ...p,
                        Surname: e.target.value,
                      }))
                    }
                    autoComplete="family-name"
                    placeholder="Dela Cruz"
                    className="h-12 w-full rounded-lg border border-alabaster-grey-300 bg-alabaster-grey-50 pr-4 pl-10 text-sm text-ink-black-900 transition outline-none placeholder:text-dusty-denim-400 focus:border-prussian-blue-500 focus:ring-4 focus:ring-prussian-blue-500/10"
                  />
                </div>
              </div>

              {/* Extension Name */}
              <div>
                <label
                  htmlFor="lastName"
                  className="mb-2 block text-sm font-semibold text-ink-black-800"
                >
                  Name Extension
                </label>

                <div className="relative">
                  <User className="pointer-events-none absolute top-1/2 left-3.5 h-4 w-4 -translate-y-1/2 text-dusty-denim-400" />

                  <input
                    id="lastName"
                    name="lastName"
                    type="text"
                    value={registrationPayload.ExtensionName}
                    onChange={(e) =>
                      setRegistrationPayload((p) => ({
                        ...p,
                        ExtensionName: e.target.value,
                      }))
                    }
                    autoComplete="family-name"
                    placeholder="Dela Cruz"
                    className="h-12 w-full rounded-lg border border-alabaster-grey-300 bg-alabaster-grey-50 pr-4 pl-10 text-sm text-ink-black-900 transition outline-none placeholder:text-dusty-denim-400 focus:border-prussian-blue-500 focus:ring-4 focus:ring-prussian-blue-500/10"
                  />
                </div>
              </div>
            </div>

            {/* Email */}
            <div>
              <label
                htmlFor="email"
                className="mb-2 block text-sm font-semibold text-ink-black-800"
              >
                Email address
              </label>

              <div className="relative">
                <Mail className="pointer-events-none absolute top-1/2 left-3.5 h-4 w-4 -translate-y-1/2 text-dusty-denim-400" />

                <input
                  id="email"
                  name="email"
                  type="email"
                  value={registrationPayload.Email}
                  onChange={(e) =>
                    setRegistrationPayload((p) => ({
                      ...p,
                      Email: e.target.value,
                    }))
                  }
                  autoComplete="email"
                  placeholder="you@company.com"
                  className="h-12 w-full rounded-lg border border-alabaster-grey-300 bg-alabaster-grey-50 pr-4 pl-10 text-sm text-ink-black-900 transition outline-none placeholder:text-dusty-denim-400 focus:border-prussian-blue-500 focus:ring-4 focus:ring-prussian-blue-500/10"
                />
              </div>
            </div>

            {/* Username */}
            <div>
              <label
                htmlFor="username"
                className="mb-2 block text-sm font-semibold text-ink-black-800"
              >
                Username
              </label>

              <div className="relative">
                <User className="pointer-events-none absolute top-1/2 left-3.5 h-4 w-4 -translate-y-1/2 text-dusty-denim-400" />

                <input
                  id="username"
                  name="username"
                  type="text"
                  value={registrationPayload.Username}
                  onChange={(e) =>
                    setRegistrationPayload((p) => ({
                      ...p,
                      Username: e.target.value,
                    }))
                  }
                  autoComplete="username"
                  placeholder="juan.delacruz"
                  className="h-12 w-full rounded-lg border border-alabaster-grey-300 bg-alabaster-grey-50 pr-4 pl-10 text-sm text-ink-black-900 transition outline-none placeholder:text-dusty-denim-400 focus:border-prussian-blue-500 focus:ring-4 focus:ring-prussian-blue-500/10"
                />
              </div>
            </div>

            {/* Passwords */}
            <div className="grid grid-cols-1 gap-5 sm:grid-cols-2">
              {/* Password */}
              <div>
                <label
                  htmlFor="password"
                  className="mb-2 block text-sm font-semibold text-ink-black-800"
                >
                  Password
                </label>

                <div className="relative">
                  <LockKeyhole className="pointer-events-none absolute top-1/2 left-3.5 h-4 w-4 -translate-y-1/2 text-dusty-denim-400" />

                  <input
                    id="password"
                    name="password"
                    type="password"
                    value={registrationPayload.Password}
                    onChange={(e) =>
                      setRegistrationPayload((p) => ({
                        ...p,
                        Password: e.target.value,
                      }))
                    }
                    autoComplete="new-password"
                    placeholder="Create a password"
                    className="h-12 w-full rounded-lg border border-alabaster-grey-300 bg-alabaster-grey-50 pr-4 pl-10 text-sm text-ink-black-900 transition outline-none placeholder:text-dusty-denim-400 focus:border-prussian-blue-500 focus:ring-4 focus:ring-prussian-blue-500/10"
                  />
                </div>
              </div>

              {/* Confirm Password */}
              <div>
                <label
                  htmlFor="confirmPassword"
                  className="mb-2 block text-sm font-semibold text-ink-black-800"
                >
                  Confirm password
                </label>

                <div className="relative">
                  <LockKeyhole className="pointer-events-none absolute top-1/2 left-3.5 h-4 w-4 -translate-y-1/2 text-dusty-denim-400" />

                  <input
                    id="confirmPassword"
                    name="confirmPassword"
                    type="password"
                    value={registrationPayload.ConfirmPassword}
                    onChange={(e) =>
                      setRegistrationPayload((p) => ({
                        ...p,
                        ConfirmPassword: e.target.value,
                      }))
                    }
                    autoComplete="new-password"
                    placeholder="Repeat your password"
                    className="h-12 w-full rounded-lg border border-alabaster-grey-300 bg-alabaster-grey-50 pr-4 pl-10 text-sm text-ink-black-900 transition outline-none placeholder:text-dusty-denim-400 focus:border-prussian-blue-500 focus:ring-4 focus:ring-prussian-blue-500/10"
                  />
                </div>
              </div>
            </div>

            {/* Password requirements */}
            <div className="rounded-lg border border-alabaster-grey-300 bg-alabaster-grey-50 p-4">
              <p className="mb-3 text-xs font-semibold text-ink-black-800">Password requirements</p>

              <div className="grid grid-cols-1 gap-2 text-xs sm:grid-cols-2">
                <PasswordRequirement
                  valid={passwordRequirements.length}
                  label="At least 8 characters"
                />

                <PasswordRequirement
                  valid={passwordRequirements.uppercase}
                  label="One uppercase letter"
                />

                <PasswordRequirement
                  valid={passwordRequirements.lowercase}
                  label="One lowercase letter"
                />

                <PasswordRequirement valid={passwordRequirements.number} label="One number" />

                <PasswordRequirement
                  valid={passwordRequirements.special}
                  label="One special character"
                />

                <PasswordRequirement
                  valid={
                    registrationPayload.ConfirmPassword !== "" &&
                    registrationPayload.Password === registrationPayload.ConfirmPassword
                  }
                  label="Passwords match"
                />
              </div>
            </div>

            {/* Terms */}
            <label className="flex cursor-pointer items-start gap-3">
              <input
                type="checkbox"
                className="mt-0.5 h-4 w-4 shrink-0 rounded border-alabaster-grey-300 accent-prussian-blue-600"
              />

              <span className="text-xs leading-5 text-dusty-denim-600">
                I agree to the ANLair terms of service and acknowledge the privacy policy.
              </span>
            </label>

            {/* Submit */}
            <button
              onClick={handleRegistration}
              disabled={!isFormValid}
              className="group flex h-12 w-full cursor-pointer items-center justify-center gap-2 rounded-lg bg-prussian-blue-600 px-5 font-heading text-sm leading-0 font-semibold text-white shadow-sm transition hover:bg-prussian-blue-700 hover:shadow-md focus:ring-4 focus:ring-prussian-blue-500/20 focus:outline-none active:scale-[0.99] disabled:cursor-not-allowed disabled:opacity-50 disabled:hover:bg-prussian-blue-600 disabled:hover:shadow-sm"
            >
              Create account
              <ArrowRight className="h-4 w-4 transition-transform group-hover:translate-x-0.5" />
            </button>
          </div>

          {/* Sign in */}
          <div className="mt-6 text-center">
            <p className="text-sm text-dusty-denim-600">
              Already have an account?{" "}
              <button
                type="button"
                onClick={() => navigate("/auth/login")}
                className="cursor-pointer font-semibold text-prussian-blue-600 transition hover:text-prussian-blue-700"
              >
                Sign in
              </button>
            </p>
          </div>

          {/* Footer */}
          <div className="mt-6 border-t border-alabaster-grey-300 pt-6 text-center">
            <div className="flex items-center justify-center gap-2 text-xs text-dusty-denim-500">
              <ShieldCheck className="h-3.5 w-3.5 text-prussian-blue-500" />
              <span>Your account information is protected.</span>
            </div>
          </div>
        </div>
      </section>
    </main>
  );
}

function PasswordRequirement({ valid, label }: { valid: boolean; label: string }) {
  return (
    <div
      className={`flex items-center gap-2 ${
        valid ? "text-prussian-blue-600" : "text-dusty-denim-500"
      }`}
    >
      {valid ? (
        <Check className="h-3.5 w-3.5 shrink-0" />
      ) : (
        <CircleAlert className="h-3.5 w-3.5 shrink-0" />
      )}

      <span>{label}</span>
    </div>
  );
}
