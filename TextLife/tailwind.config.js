const { hairlineWidth } = require('nativewind/theme');

/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ['./src/**/*.{ts,tsx}'],
  presets: [require('nativewind/preset')],
  theme: {
    extend: {
      fontFamily: {
        outfit: ['Outfit'],
        'outfit-medium': ['Outfit-Medium'],
        'outfit-semibold': ['Outfit-SemiBold'],
        'outfit-bold': ['Outfit-Bold'],
        'outfit-extrabold': ['Outfit-ExtraBold'],
        'outfit-black': ['Outfit-Black'],
        mono: ['JetBrainsMono'],
        'mono-medium': ['JetBrainsMono-Medium'],
        'mono-bold': ['JetBrainsMono-Bold'],
      },
      colors: {
        bg: {
          DEFAULT: '#f7f9fc',
          elevated: '#ffffff',
          sunken: '#eef1f6',
        },
        brand: {
          primary: '#3b82f6',
          secondary: '#60a5fa',
          subtle: '#dbeafe',
        },
        success: {
          DEFAULT: '#22c55e',
          subtle: '#dcfce7',
          on: '#166534',
        },
        warning: {
          DEFAULT: '#f59e0b',
          subtle: '#fef3c7',
          on: '#92400e',
        },
        danger: {
          DEFAULT: '#ef4444',
          subtle: '#fee2e2',
          on: '#991b1b',
        },
        stat: {
          health: '#22c55e',
          happiness: '#e8a308',
          smarts: '#2563eb',
          looks: '#a855f7',
        },
        text: {
          primary: '#0f172a',
          secondary: '#475569',
          tertiary: '#94a3b8',
          disabled: '#cbd5e1',
          inverse: '#ffffff',
        },
        border: {
          DEFAULT: '#e2e8f0',
        },
      },
      spacing: {
        xs: '4px',
        sm: '8px',
        md: '12px',
        lg: '16px',
        xl: '24px',
        '2xl': '32px',
        '3xl': '48px',
        '4xl': '64px',
      },
      borderRadius: {
        sm: '8px',
        md: '12px',
        lg: '16px',
        xl: '20px',
      },
      fontSize: {
        xs: '11px',
        sm: '13px',
        base: '15px',
        lg: '17px',
        xl: '20px',
        '2xl': '24px',
        '3xl': '32px',
        '4xl': '40px',
      },
      borderWidth: {
        hairline: hairlineWidth(),
      },
    },
  },
  plugins: [],
};
