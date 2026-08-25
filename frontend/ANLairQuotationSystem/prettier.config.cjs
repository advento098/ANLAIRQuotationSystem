/**
 * @see https://prettier.io/docs/configuration
 * @type {import("prettier").Config}
 */
const config = {
  plugins: ["prettier-plugin-tailwindcss"],
  tailwindStylesheet: "./src/index.css",
  semi: true,
  singleQuote: false,
  trailingComma: "all",
  printWidth: 100,
  tabWidth: 2,
};

module.exports = config;