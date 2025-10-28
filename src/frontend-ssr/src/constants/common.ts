// routes

import { ComponentSize } from '@types';

export enum APP_ROUTES {
  base = '/',
  login = '/login',
  register = '/register',
  profile = '/me',
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
