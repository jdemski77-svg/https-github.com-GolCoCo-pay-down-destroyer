import type { Config } from 'tailwindcss';

const config: Config = {
  content: ['./app/**/*.{ts,tsx}', './components/**/*.{ts,tsx}'],
  theme: {
    extend: {
      colors: {
        brand: {
          blue: '#1463FF',
          navy: '#0A1F5C',
          ink: '#050B1A',
          steel: '#6B7280',
          green: '#006400',
          red: '#8B0000',
          magenta: '#8B008B',
        },
      },
      fontFamily: {
        display: ['Oswald', '"Arial Black"', 'Arial', 'sans-serif'],
        login: ['Oswald', '"Arial Black"', 'Arial', 'sans-serif'],
        sans: ['Inter', 'system-ui', 'Arial', 'sans-serif'],
      },
    },
  },
  plugins: [],
};

export default config;
