import { ArrowRight, LockKeyhole, Mail, ShieldCheck } from "lucide-react";
import { useEffect, useState } from "react";
import { type LoginPayload } from "../../../types/auth/LoginPayload";
import { useAuth } from "../../../contexts/AuthProvider";
import { useNavigate } from "react-router";

export default function LoginPage() {
  const { login, isAuthenticated } = useAuth();
  const navigate = useNavigate();

  const [loginPayload, setLoginPayload] = useState<LoginPayload>({
    UserIdentifier: "",
    Password: "",
  });

  const handleLogin = async () => {
    if (loginPayload.UserIdentifier === "" && loginPayload.Password === "") {
      console.log("Please fill out fields");
      return;
    }

    try {
      await login(loginPayload.UserIdentifier, loginPayload.Password);
    } catch (err) {
      console.log(err);
    }
  };

  useEffect(() => {
    if (isAuthenticated) {
      navigate("home");
    }
  }, []);

  return (
    <main className="flex min-h-dvh w-full bg-alabaster-grey-100 font-sans text-ink-black-900">
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
          <p className="mb-4 text-sm font-semibold tracking-[0.2em] text-prussian-blue-400 uppercase">
            Business Management
          </p>

          <h2 className="font-heading text-4xl leading-tight font-bold tracking-tight text-alabaster-grey-50 xl:text-5xl">
            Create professional quotations with confidence.
          </h2>

          <p className="mt-6 max-w-lg text-base leading-7 text-dusty-denim-300">
            A streamlined quotation management system designed to help you prepare, organize, and
            manage your business quotations efficiently.
          </p>
        </div>

        <div className="relative flex items-center gap-3 text-sm text-dusty-denim-400">
          <ShieldCheck className="h-4 w-4 text-prussian-blue-400" />
          <span>Secure business management</span>
        </div>
      </section>

      {/* Login panel */}
      <section className="flex w-full items-center justify-center p-6 sm:p-10 lg:w-1/2">
        <div className="w-full max-w-md">
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

          <div className="mb-8">
            <p className="mb-2 text-sm font-semibold text-prussian-blue-600">Welcome back</p>

            <h1 className="font-heading text-3xl font-bold tracking-tight text-ink-black-900">
              Sign in to your account
            </h1>

            <p className="mt-3 text-sm leading-6 text-dusty-denim-600">
              Enter your credentials to continue managing your quotations.
            </p>
          </div>

          <div className="space-y-5">
            {/* Email */}
            <div>
              <label
                htmlFor="email"
                className="mb-2 block text-sm font-semibold text-ink-black-800"
              >
                Username or Email address
              </label>

              <div className="relative">
                <Mail className="pointer-events-none absolute top-1/2 left-3.5 h-4 w-4 -translate-y-1/2 text-dusty-denim-400" />

                <input
                  id="email"
                  name="email"
                  type="email"
                  onChange={(e) =>
                    setLoginPayload((p) => ({ ...p, UserIdentifier: e.target.value }))
                  }
                  value={loginPayload.UserIdentifier}
                  autoComplete="email"
                  placeholder="you@company.com"
                  className="h-12 w-full rounded-lg border border-alabaster-grey-300 bg-alabaster-grey-50 pr-4 pl-10 text-sm text-ink-black-900 transition outline-none placeholder:text-dusty-denim-400 focus:border-prussian-blue-500 focus:ring-4 focus:ring-prussian-blue-500/10"
                />
              </div>
            </div>

            {/* Password */}
            <div>
              <div className="mb-2 flex items-center justify-between">
                <label
                  htmlFor="password"
                  className="block text-sm font-semibold text-ink-black-800"
                >
                  Password
                </label>

                <button
                  type="button"
                  className="cursor-pointer text-xs font-semibold text-prussian-blue-600 transition hover:text-prussian-blue-700"
                >
                  Forgot password?
                </button>
              </div>

              <div className="relative">
                <LockKeyhole className="pointer-events-none absolute top-1/2 left-3.5 h-4 w-4 -translate-y-1/2 text-dusty-denim-400" />

                <input
                  id="password"
                  name="password"
                  type="password"
                  onChange={(e) => setLoginPayload((p) => ({ ...p, Password: e.target.value }))}
                  value={loginPayload.Password}
                  autoComplete="current-password"
                  placeholder="Enter your password"
                  className="h-12 w-full rounded-lg border border-alabaster-grey-300 bg-alabaster-grey-50 pr-4 pl-10 text-sm text-ink-black-900 transition outline-none placeholder:text-dusty-denim-400 focus:border-prussian-blue-500 focus:ring-4 focus:ring-prussian-blue-500/10"
                />
              </div>
            </div>

            {/* Remember me */}
            <div className="flex">
              <label className="flex w-fit cursor-pointer items-center gap-3">
                <input
                  type="checkbox"
                  className="h-4 w-4 rounded border-alabaster-grey-300 accent-prussian-blue-600"
                />

                <span className="w-fit text-sm text-dusty-denim-600">Keep me signed in</span>
              </label>
              <button
                onClick={() => navigate("/auth/register")}
                className="ml-auto cursor-pointer text-sm font-semibold text-prussian-blue-600 transition hover:text-prussian-blue-700"
              >
                Register here
              </button>
            </div>
            {/* TODO: Continue working with login page logic */}
            {/* Submit */}
            <button
              onClick={handleLogin}
              className="group flex h-12 w-full cursor-pointer items-center justify-center gap-2 rounded-lg bg-prussian-blue-600 px-5 font-heading text-sm leading-0 font-semibold text-white shadow-sm transition hover:bg-prussian-blue-700 hover:shadow-md focus:ring-4 focus:ring-prussian-blue-500/20 focus:outline-none active:scale-[0.99]"
            >
              Sign in
              <ArrowRight className="h-4 w-4 transition-transform group-hover:translate-x-0.5" />
            </button>
          </div>

          {/* Footer */}
          <div className="mt-8 border-t border-alabaster-grey-300 pt-6 text-center">
            <p className="text-xs leading-5 text-dusty-denim-500">
              Authorized users only. Your account activity may be monitored for security purposes.
            </p>
          </div>
        </div>
      </section>
    </main>
  );
}
