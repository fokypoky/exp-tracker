// routes

import { ComponentSize } from '@types';

export enum APP_ROUTES {
  base = '/',
  login = '/login',
  register = '/register',
  overview = '/me',
  transactions = '/transactions',
  categories = '/categories',
  groups = '/groups',
}

// containers

export const FEATURES_CONTAINER_ID = 'features_container';

// local storage

export const ACCESS_TOKEN_KEY = 'ACCESS_TOKEN';
export const REFRESH_TOKEN_KEY = 'REFRESH_TOKEN';

// mappings

export const SIZES_MAPPING: Record<ComponentSize, number> = {
  ['s']: 18,
  ['m']: 24,
  ['l']: 32,
};

// http

export const TOTAL_COUNT_HEADER = 'x-total-count';


export const PAGINATION_ITEMS_PER_PAGE = [10, 20, 50];
