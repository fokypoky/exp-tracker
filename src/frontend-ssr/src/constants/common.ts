// routes

import { ComponentSize, Filters, Option } from '@types';

export enum APP_ROUTES {
  base = '/',
  login = '/login',
  register = '/register',
  overview = '/me',
  transactions = '/transactions',
  categories = '/categories',
  groups = '/groups',
  create = '/create',
}

// containers

export const FEATURES_CONTAINER_ID = 'features_container';

// local storage

export const ACCESS_TOKEN_KEY = 'ACCESS_TOKEN';
export const REFRESH_TOKEN_KEY = 'REFRESH_TOKEN';

// mappings

export const SIZES_MAPPING: Record<ComponentSize, number> = {
  ['s']: 16,
  ['m']: 18,
  ['l']: 24,
};

export const FONT_SIZES_MAPPING: Record<ComponentSize, string> = {
  ['s']: '0.85rem',
  ['m']: '1rem',
  ['l']: '1.25rem',
};

// http

export const TOTAL_COUNT_HEADER = 'x-total-count';

// pagination
export const PAGINATION_ITEMS_PER_PAGE = [10, 20, 50];
export const PAGINATION_ITEMS_PER_PAGE_OPTIONS: Option<number>[] = [
  { label: '10', value: 10 },
  { label: '20', value: 20 },
  { label: '50', value: 50 },
];
export const PAGINATION_PAGES_COUNT = 7;

// filters

export const DEFAULT_FILTERS: Filters = {
  searchString: '',
  limit: 10,
  offset: 0,
};
