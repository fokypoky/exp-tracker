import { useState } from 'react';
import { jwtDecode } from 'jwt-decode';

import type { JwtPayload } from '@types';
import { LocalStorageKey } from '@constants';

export const useAuth = () => {
  const [accessToken, setAccessToken] = useState<string | null>(
    localStorage.getItem(LocalStorageKey.AccessToken)
  );
  const [refreshToken, setRefreshToken] = useState<string | null>(
    localStorage.getItem(LocalStorageKey.RefreshToken)
  );

  const setTokenPair = (access: string, refresh: string) => {
    setAccessToken(access);
    setRefreshToken(refresh);

    localStorage.setItem(LocalStorageKey.AccessToken, access);
    localStorage.setItem(LocalStorageKey.RefreshToken, refresh);
  }

  const isAuthorized = (): boolean => {

    const tokensExists = !!accessToken && !!refreshToken;

    if (!tokensExists) return false;

    try {
      const { exp } = jwtDecode<JwtPayload>(refreshToken!);
      const current = Math.floor(Date.now() / 1000);

      return exp > current;
    } catch(e) {
      console.error(e);
      return false;
    }
  }

  return { accessToken, refreshToken, setTokenPair, isAuthorized };
}