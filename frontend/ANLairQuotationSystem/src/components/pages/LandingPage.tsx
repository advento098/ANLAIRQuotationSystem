import { ArrowRight, Check, FileText, Menu, ShieldCheck, X } from "lucide-react";
import { useState } from "react";
import { useNavigate } from "react-router";

function LandingPage() {
  const navigate = useNavigate();
  const [mobileMenuOpen, setMobileMenuOpen] = useState(false);

  function handleLogin() {
    navigate("auth/login");
  }

  return (
    <main className="min-h-screen overflow-hidden bg-alabaster-grey-50 font-sans text-prussian-blue-950">
      {/* =====================================================
          BACKGROUND
      ====================================================== */}

      <div className="pointer-events-none fixed inset-0 -z-10">
        {/* Fine grid */}
        <div
          className="absolute inset-0 opacity-[0.32]"
          style={{
            backgroundImage: `
              linear-gradient(
                to right,
                rgba(16, 23, 35, 0.035) 1px,
                transparent 1px
              ),
              linear-gradient(
                to bottom,
                rgba(16, 23, 35, 0.035) 1px,
                transparent 1px
              )
            `,
            backgroundSize: "48px 48px",
          }}
        />

        {/* Architectural circles */}
        <div className="absolute -top-72 -right-72 h-[800px] w-[800px] rounded-full border border-prussian-blue-200/40" />

        <div className="absolute -top-48 -right-48 h-[560px] w-[560px] rounded-full border border-prussian-blue-200/30" />

        <div className="absolute -bottom-96 -left-96 h-[800px] w-[800px] rounded-full border border-dusty-denim-200/30" />
      </div>

      {/* =====================================================
          NAVIGATION
      ====================================================== */}

      <header className="border-b border-alabaster-grey-200/70 bg-alabaster-grey-50/80 backdrop-blur-md">
        <div className="mx-auto flex h-20 max-w-7xl items-center justify-between px-6 sm:px-8 lg:px-10">
          {/* Brand */}
          <button
            type="button"
            onClick={() => window.scrollTo({ top: 0, behavior: "smooth" })}
            className="flex items-center gap-3"
          >
            <div className="flex h-9 w-9 items-center justify-center rounded-md bg-prussian-blue-900 font-heading text-sm font-bold text-white shadow-sm">
              A
            </div>

            <div className="text-left">
              <div className="font-heading text-sm font-bold tracking-[0.16em] text-prussian-blue-900">
                ANLAIR
              </div>

              <div className="mt-0.5 text-[9px] font-medium tracking-[0.16em] text-dusty-denim-400 uppercase">
                Quotation System
              </div>
            </div>
          </button>

          {/* Desktop navigation */}
          <nav className="hidden items-center gap-8 md:flex">
            <a
              href="#features"
              className="text-sm font-medium text-dusty-denim-600 transition hover:text-prussian-blue-900"
            >
              Features
            </a>

            <a
              href="#workflow"
              className="text-sm font-medium text-dusty-denim-600 transition hover:text-prussian-blue-900"
            >
              How it works
            </a>

            <a
              href="#security"
              className="text-sm font-medium text-dusty-denim-600 transition hover:text-prussian-blue-900"
            >
              Security
            </a>
          </nav>

          {/* Desktop actions */}
          <div className="hidden items-center gap-3 md:flex">
            <button
              type="button"
              onClick={handleLogin}
              className="rounded-md px-4 py-2.5 text-sm font-medium text-dusty-denim-700 transition hover:bg-white hover:text-prussian-blue-900"
            >
              Sign in
            </button>

            <button
              type="button"
              onClick={handleLogin}
              className="flex items-center gap-2 rounded-md bg-prussian-blue-900 px-4 py-2.5 text-sm font-medium text-white shadow-sm transition hover:bg-prussian-blue-800 focus:ring-4 focus:ring-prussian-blue-400/20 focus:outline-none"
            >
              Get started
              <ArrowRight className="h-4 w-4" />
            </button>
          </div>

          {/* Mobile menu button */}
          <button
            type="button"
            onClick={() => setMobileMenuOpen((current) => !current)}
            className="rounded-md p-2 text-dusty-denim-600 transition hover:bg-white hover:text-prussian-blue-900 md:hidden"
            aria-label="Toggle navigation"
          >
            {mobileMenuOpen ? <X className="h-5 w-5" /> : <Menu className="h-5 w-5" />}
          </button>
        </div>

        {/* Mobile navigation */}
        {mobileMenuOpen && (
          <div className="border-t border-alabaster-grey-200/70 bg-white/90 px-6 py-5 backdrop-blur-md md:hidden">
            <nav className="flex flex-col gap-1">
              <a
                href="#features"
                onClick={() => setMobileMenuOpen(false)}
                className="rounded-md px-3 py-3 text-sm font-medium text-dusty-denim-700 hover:bg-alabaster-grey-50"
              >
                Features
              </a>

              <a
                href="#workflow"
                onClick={() => setMobileMenuOpen(false)}
                className="rounded-md px-3 py-3 text-sm font-medium text-dusty-denim-700 hover:bg-alabaster-grey-50"
              >
                How it works
              </a>

              <a
                href="#security"
                onClick={() => setMobileMenuOpen(false)}
                className="rounded-md px-3 py-3 text-sm font-medium text-dusty-denim-700 hover:bg-alabaster-grey-50"
              >
                Security
              </a>

              <div className="mt-3 border-t border-alabaster-grey-200 pt-4">
                <button
                  type="button"
                  onClick={handleLogin}
                  className="w-full rounded-md bg-prussian-blue-900 px-4 py-3 text-sm font-medium text-white"
                >
                  Sign in
                </button>
              </div>
            </nav>
          </div>
        )}
      </header>

      {/* =====================================================
          HERO
      ====================================================== */}

      <section className="relative">
        <div className="mx-auto grid min-h-[680px] max-w-7xl items-center gap-16 px-6 py-24 sm:px-8 lg:grid-cols-[1.05fr_0.95fr] lg:px-10 lg:py-28">
          {/* Hero copy */}
          <div className="max-w-2xl">
            <div className="mb-7 inline-flex items-center gap-2 rounded-full border border-prussian-blue-100 bg-white/70 px-3 py-1.5 text-xs font-medium text-prussian-blue-700 shadow-sm backdrop-blur">
              <span className="h-1.5 w-1.5 rounded-full bg-prussian-blue-500" />
              Professional quotation management
            </div>

            <h1 className="font-heading text-4xl leading-[1.08] font-bold tracking-[-0.035em] text-prussian-blue-950 sm:text-5xl lg:text-6xl">
              Create better quotations.
              <span className="block text-dusty-denim-500">Close business faster.</span>
            </h1>

            <p className="mt-7 max-w-xl text-base leading-7 text-dusty-denim-600 sm:text-lg">
              ANLAIR gives your business a focused workspace for creating, organizing, and managing
              professional quotations without the clutter of spreadsheets and scattered documents.
            </p>

            <div className="mt-9 flex flex-col gap-3 sm:flex-row">
              <button
                type="button"
                onClick={handleLogin}
                className="group flex h-12 items-center justify-center gap-2 rounded-md bg-prussian-blue-900 px-6 text-sm font-medium text-white shadow-[0_8px_24px_rgba(16,23,35,0.16)] transition hover:bg-prussian-blue-800 focus:ring-4 focus:ring-prussian-blue-400/20 focus:outline-none"
              >
                Access your workspace
                <ArrowRight className="h-4 w-4 transition-transform group-hover:translate-x-0.5" />
              </button>

              <a
                href="#features"
                className="flex h-12 items-center justify-center rounded-md border border-alabaster-grey-300 bg-white/70 px-6 text-sm font-medium text-prussian-blue-800 transition hover:border-dusty-denim-300 hover:bg-white"
              >
                Explore features
              </a>
            </div>

            {/* Trust indicators */}
            <div className="mt-10 flex flex-wrap gap-x-6 gap-y-3 text-xs text-dusty-denim-400">
              <div className="flex items-center gap-2">
                <Check className="h-3.5 w-3.5 text-prussian-blue-500" />
                Centralized quotations
              </div>

              <div className="flex items-center gap-2">
                <Check className="h-3.5 w-3.5 text-prussian-blue-500" />
                Organized workflow
              </div>

              <div className="flex items-center gap-2">
                <Check className="h-3.5 w-3.5 text-prussian-blue-500" />
                Secure access
              </div>
            </div>
          </div>

          {/* Hero visual */}
          <div className="relative mx-auto w-full max-w-[520px]">
            {/* Decorative frame */}
            <div className="absolute -inset-5 rounded-2xl border border-prussian-blue-100/70" />

            <div className="absolute -inset-10 rounded-[28px] border border-dusty-denim-200/30" />

            {/* Main application preview */}
            <div className="relative overflow-hidden rounded-xl border border-alabaster-grey-200 bg-white shadow-[0_30px_80px_rgba(12,25,39,0.14)]">
              {/* Window header */}
              <div className="flex items-center justify-between border-b border-alabaster-grey-200 bg-alabaster-grey-50 px-5 py-4">
                <div className="flex items-center gap-2">
                  <div className="h-2.5 w-2.5 rounded-full bg-prussian-blue-300" />
                  <div className="h-2.5 w-2.5 rounded-full bg-dusty-denim-300" />
                  <div className="h-2.5 w-2.5 rounded-full bg-alabaster-grey-300" />
                </div>

                <span className="font-mono text-[9px] tracking-wider text-dusty-denim-400 uppercase">
                  quotation.workspace
                </span>
              </div>

              <div className="p-6">
                {/* Preview heading */}
                <div className="flex items-start justify-between">
                  <div>
                    <div className="font-mono text-[9px] tracking-wider text-dusty-denim-400 uppercase">
                      Quotation
                    </div>

                    <div className="mt-1 font-heading text-xl font-bold text-prussian-blue-950">
                      QTN-2026-0042
                    </div>
                  </div>

                  <div className="rounded-full bg-prussian-blue-50 px-3 py-1 text-[10px] font-medium text-prussian-blue-700">
                    Draft
                  </div>
                </div>

                {/* Client */}
                <div className="mt-7 rounded-lg border border-alabaster-grey-200 bg-alabaster-grey-50 p-4">
                  <div className="text-[9px] font-medium tracking-wider text-dusty-denim-400 uppercase">
                    Prepared for
                  </div>

                  <div className="mt-1 text-sm font-semibold text-prussian-blue-900">
                    Client Company
                  </div>

                  <div className="mt-1 text-xs text-dusty-denim-500">client@example.com</div>
                </div>

                {/* Items */}
                <div className="mt-5 space-y-3">
                  <div className="flex items-center justify-between border-b border-alabaster-grey-100 pb-3 text-xs">
                    <div>
                      <div className="font-medium text-prussian-blue-800">Professional Service</div>
                      <div className="mt-0.5 text-[10px] text-dusty-denim-400">1 × service</div>
                    </div>

                    <span className="font-medium text-prussian-blue-900">₱25,000</span>
                  </div>

                  <div className="flex items-center justify-between border-b border-alabaster-grey-100 pb-3 text-xs">
                    <div>
                      <div className="font-medium text-prussian-blue-800">Implementation</div>
                      <div className="mt-0.5 text-[10px] text-dusty-denim-400">1 × service</div>
                    </div>

                    <span className="font-medium text-prussian-blue-900">₱15,000</span>
                  </div>
                </div>

                {/* Total */}
                <div className="mt-6 flex items-end justify-between">
                  <span className="text-xs font-medium text-dusty-denim-500">Total quotation</span>

                  <span className="font-heading text-2xl font-bold text-prussian-blue-950">
                    ₱40,000
                  </span>
                </div>

                {/* Fake action */}
                <div className="mt-6 flex items-center gap-2">
                  <div className="flex h-9 flex-1 items-center justify-center rounded-md bg-prussian-blue-900 text-[10px] font-medium text-white">
                    Generate quotation
                  </div>

                  <div className="flex h-9 w-9 items-center justify-center rounded-md border border-alabaster-grey-200">
                    <FileText className="h-4 w-4 text-dusty-denim-500" />
                  </div>
                </div>
              </div>
            </div>

            {/* Floating security badge */}
            <div className="absolute -right-5 -bottom-6 flex items-center gap-3 rounded-lg border border-alabaster-grey-200 bg-white px-4 py-3 shadow-[0_12px_30px_rgba(12,25,39,0.10)] sm:-right-8">
              <div className="flex h-8 w-8 items-center justify-center rounded-md bg-prussian-blue-50">
                <ShieldCheck className="h-4 w-4 text-prussian-blue-700" />
              </div>

              <div>
                <div className="text-xs font-semibold text-prussian-blue-900">Secure workspace</div>

                <div className="mt-0.5 text-[10px] text-dusty-denim-400">
                  Authorized access only
                </div>
              </div>
            </div>
          </div>
        </div>
      </section>

      {/* =====================================================
          FEATURES
      ====================================================== */}

      <section id="features" className="border-y border-alabaster-grey-200/70 bg-white/60">
        <div className="mx-auto max-w-7xl px-6 py-24 sm:px-8 lg:px-10">
          <div className="max-w-2xl">
            <div className="font-mono text-[10px] font-medium tracking-[0.18em] text-dusty-denim-400 uppercase">
              Built for clarity
            </div>

            <h2 className="mt-3 font-heading text-3xl font-bold tracking-tight text-prussian-blue-950 sm:text-4xl">
              Everything you need to manage quotations.
            </h2>

            <p className="mt-4 text-sm leading-6 text-dusty-denim-600 sm:text-base">
              Keep your quotation process organized from the first draft to the final document.
            </p>
          </div>

          <div className="mt-14 grid gap-px overflow-hidden rounded-xl border border-alabaster-grey-200 bg-alabaster-grey-200 md:grid-cols-3">
            {[
              {
                number: "01",
                title: "Create",
                description:
                  "Build clear and professional quotations from a centralized workspace.",
              },
              {
                number: "02",
                title: "Organize",
                description:
                  "Keep clients, quotation records, and documents structured and easy to find.",
              },
              {
                number: "03",
                title: "Manage",
                description:
                  "Track quotation activity and keep your team working from a consistent source of information.",
              },
            ].map((feature) => (
              <div
                key={feature.number}
                className="bg-white p-7 transition hover:bg-alabaster-grey-50 sm:p-9"
              >
                <span className="font-mono text-[10px] text-dusty-denim-400">{feature.number}</span>

                <h3 className="mt-8 font-heading text-lg font-bold text-prussian-blue-900">
                  {feature.title}
                </h3>

                <p className="mt-3 text-sm leading-6 text-dusty-denim-500">{feature.description}</p>
              </div>
            ))}
          </div>
        </div>
      </section>

      {/* =====================================================
          WORKFLOW
      ====================================================== */}

      <section id="workflow">
        <div className="mx-auto max-w-7xl px-6 py-24 sm:px-8 lg:px-10">
          <div className="grid gap-16 lg:grid-cols-2 lg:items-center">
            <div>
              <div className="font-mono text-[10px] font-medium tracking-[0.18em] text-dusty-denim-400 uppercase">
                Simple workflow
              </div>

              <h2 className="mt-3 font-heading text-3xl font-bold tracking-tight text-prussian-blue-950 sm:text-4xl">
                Less time formatting.
                <span className="block text-dusty-denim-500">More time doing business.</span>
              </h2>

              <p className="mt-5 max-w-lg text-sm leading-6 text-dusty-denim-600 sm:text-base">
                ANLAIR brings the quotation process into one focused workspace, giving your team a
                consistent way to prepare and manage business proposals.
              </p>

              <button
                type="button"
                onClick={handleLogin}
                className="group mt-8 flex items-center gap-2 text-sm font-semibold text-prussian-blue-800 transition hover:text-prussian-blue-600"
              >
                Enter your workspace
                <ArrowRight className="h-4 w-4 transition-transform group-hover:translate-x-1" />
              </button>
            </div>

            {/* Steps */}
            <div className="space-y-0 rounded-xl border border-alabaster-grey-200 bg-white">
              {[
                ["01", "Prepare", "Enter your client and quotation details."],
                ["02", "Build", "Add services, products, pricing, and terms."],
                ["03", "Review", "Check the quotation before sending it."],
                ["04", "Manage", "Keep the final record organized for future reference."],
              ].map(([number, title, description], index) => (
                <div
                  key={number}
                  className={`flex gap-5 p-6 sm:p-7 ${
                    index !== 3 ? "border-b border-alabaster-grey-200" : ""
                  }`}
                >
                  <span className="font-mono text-xs text-dusty-denim-400">{number}</span>

                  <div>
                    <h3 className="font-heading text-sm font-bold text-prussian-blue-900">
                      {title}
                    </h3>

                    <p className="mt-1.5 text-sm leading-5 text-dusty-denim-500">{description}</p>
                  </div>
                </div>
              ))}
            </div>
          </div>
        </div>
      </section>

      {/* =====================================================
          SECURITY
      ====================================================== */}

      <section
        id="security"
        className="border-y border-alabaster-grey-200 bg-prussian-blue-900 text-white"
      >
        <div className="mx-auto flex max-w-7xl flex-col gap-8 px-6 py-20 sm:px-8 lg:flex-row lg:items-center lg:justify-between lg:px-10">
          <div className="max-w-2xl">
            <div className="font-mono text-[10px] tracking-[0.18em] text-prussian-blue-300 uppercase">
              Secure by design
            </div>

            <h2 className="mt-3 font-heading text-2xl font-bold tracking-tight sm:text-3xl">
              Your business information stays in a controlled workspace.
            </h2>

            <p className="mt-4 text-sm leading-6 text-prussian-blue-200">
              ANLAIR is designed around authenticated access and clear separation between authorized
              users and your business data.
            </p>
          </div>

          <button
            type="button"
            onClick={handleLogin}
            className="flex shrink-0 items-center justify-center gap-2 rounded-md bg-white px-5 py-3 text-sm font-semibold text-prussian-blue-900 transition hover:bg-prussian-blue-50"
          >
            Sign in to ANLAIR
            <ArrowRight className="h-4 w-4" />
          </button>
        </div>
      </section>

      {/* =====================================================
          FOOTER
      ====================================================== */}

      <footer>
        <div className="mx-auto flex max-w-7xl flex-col gap-4 px-6 py-8 sm:px-8 md:flex-row md:items-center md:justify-between lg:px-10">
          <div className="flex items-center gap-3">
            <div className="flex h-7 w-7 items-center justify-center rounded bg-prussian-blue-900 font-heading text-[10px] font-bold text-white">
              A
            </div>

            <span className="font-heading text-xs font-bold tracking-[0.14em] text-prussian-blue-900">
              ANLAIR
            </span>
          </div>

          <p className="text-[10px] tracking-wide text-dusty-denim-400 uppercase">
            Quotation Management System
          </p>

          <p className="text-[10px] text-dusty-denim-400">© 2026 ANLAIR. All rights reserved.</p>
        </div>
      </footer>
    </main>
  );
}

export default LandingPage;
