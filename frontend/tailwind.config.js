/** @type {import('tailwindcss').Config} */
export default {
  content: ['./index.html', './src/**/*.{vue,js,ts,jsx,tsx}'],
  theme: {
    extend: {
      colors: {
        primary: '#1e3a8a',
        secondary: '#9333ea',
        accent: '#10b981',
      },
    },
  },
  plugins: [],
}

