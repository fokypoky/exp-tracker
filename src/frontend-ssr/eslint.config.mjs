import { dirname } from 'path';
import { fileURLToPath } from 'url';

import { FlatCompat } from '@eslint/eslintrc';
import eslintPluginImport from 'eslint-plugin-import';

const __filename = fileURLToPath(import.meta.url);
const __dirname = dirname(__filename);

const compat = new FlatCompat({
  baseDirectory: __dirname,
});

const eslintConfig = [
  ...compat.extends('next/core-web-vitals', 'next/typescript'),
  {
    plugins: {
      import: eslintPluginImport,
    },
  },
  {
    rules: {
      // Точки с запятой
      'semi': ['error', 'always'],
      
      // Сортировка импортов
      'import/order': [
        'error',
        {
          'groups': [
            'builtin',
            'external',
            'internal',
            'parent',
            'sibling',
            'index',
            'object',
            'type',
          ],
          'pathGroups': [
            {
              'pattern': '@/**',
              'group': 'internal',
              'position': 'before',
            },
          ],
          'pathGroupsExcludedImportTypes': ['builtin'],
          'newlines-between': 'always',
          'alphabetize': {
            'order': 'asc',
            'caseInsensitive': true,
          },
        },
      ],
      // Пробелы вокруг фигурных скобок
      'object-curly-spacing': ['error', 'always'],
      'eol-last': ['warn', 'always'],
      'quotes': ['error', 'single', {
        'avoidEscape': true,
        'allowTemplateLiterals': true,
      }],
      'jsx-quotes': ['error', 'prefer-double'],
      'comma-dangle': ['error', 'always-multiline'],
    },
  },
  {
    ignores: [
      'node_modules/**',
      '.next/**',
      'out/**',
      'build/**',
      'next-env.d.ts',
    ],
  },
];

export default eslintConfig;
