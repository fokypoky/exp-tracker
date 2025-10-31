'use client';

import { useRouter } from 'next/navigation';
import { createContext, useCallback, useEffect, useMemo, useState } from 'react';

import { ACCESS_TOKEN_KEY, APP_ROUTES, REFRESH_TOKEN_KEY } from '@constants';
import { User } from '@types';
import { parseJwtPayload, tokenExpired } from '@utils';

export type AuthContextType = {
  user: User;
  authorized: boolean;
  setTokenPair(accessToken: string, refreshToken: string): void;
}

export const AuthContext = createContext<AuthContextType>({
  user: {
    login: '',
    guid: '',
  },
  authorized: false,
  setTokenPair: (_, __) => {},
});

type Props = {
  children: React.ReactNode;
}

export const AuthProvider = ({ children }: Props) => {
  const router = useRouter();

  const [user, setUser] = useState<User>({ login: '', guid: '' });

  const [accessToken, setAccessToken] = useState<string | null>(null);
  const [refreshToken, setRefreshToken] = useState<string | null>(null);
  const [init, setInit] = useState<boolean>(false);

  const authorized = useMemo(() => {
    if (!init) return true;

    if (!accessToken || !refreshToken) return false;

    const payload = parseJwtPayload(refreshToken);
    
    if (!payload) return false;
    if (tokenExpired(payload)) return false;

    return true;
  }, [accessToken, refreshToken, init]);

  useEffect(() => {
    if (!accessToken) return;

    const { login, guid } = parseJwtPayload(accessToken)!;

    setUser({ login, guid });
  }, [accessToken]);

  const setTokenPair = useCallback((accessToken: string, refreshToken: string) => {
    setAccessToken(accessToken);
    setRefreshToken(refreshToken);
  }, []);

  useEffect(() => {
    setAccessToken(localStorage.getItem(ACCESS_TOKEN_KEY));
    setRefreshToken(localStorage.getItem(REFRESH_TOKEN_KEY));

    const handle = (event: StorageEvent) => {
      const { key } = event;

      key === ACCESS_TOKEN_KEY && setAccessToken(localStorage.getItem(ACCESS_TOKEN_KEY));
      key === REFRESH_TOKEN_KEY && setRefreshToken(localStorage.getItem(REFRESH_TOKEN_KEY));
    };

    window.addEventListener('storage', handle);

    setInit(true);

    return () => window.removeEventListener('storage', handle);
  }, []);

  useEffect(() => {
    if (!router) return;

    !authorized && router.push(APP_ROUTES.login);
  }, [authorized, router]);

  return (
    <AuthContext.Provider value={{ user, authorized, setTokenPair }}>
      {children}
    </AuthContext.Provider>
  );
};
