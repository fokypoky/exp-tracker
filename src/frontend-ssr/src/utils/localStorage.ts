import { ACCESS_TOKEN_KEY, REFRESH_TOKEN_KEY } from '@constants';

export const setStorageTokenPair = (accessToken: string, refreshToken: string) => {
  localStorage.setItem(ACCESS_TOKEN_KEY, accessToken);
  localStorage.setItem(REFRESH_TOKEN_KEY, refreshToken);

  window.dispatchEvent(new StorageEvent('storage', { key: ACCESS_TOKEN_KEY, newValue: accessToken }));
  window.dispatchEvent(new StorageEvent('storage', { key: REFRESH_TOKEN_KEY, newValue: refreshToken }));
};

export const clearStorageTokenPair = () => {
  localStorage.removeItem(ACCESS_TOKEN_KEY);
  localStorage.removeItem(REFRESH_TOKEN_KEY);

  window.dispatchEvent(new StorageEvent('storage', { key: ACCESS_TOKEN_KEY, newValue: null }));
  window.dispatchEvent(new StorageEvent('storage', { key: REFRESH_TOKEN_KEY, newValue: null }));
};
