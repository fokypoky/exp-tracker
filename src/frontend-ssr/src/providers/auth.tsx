'use client'

import {createContext, useEffect, useMemo, useState} from "react";
import {User} from "@types";
import {ACCESS_TOKEN_KEY, REFRESH_TOKEN_KEY} from "@constants";

export type AuthContextType = {
  user: User | null;
  authorized: boolean;
}

export const AuthContext = createContext<AuthContextType>({
  user: null,
  authorized: false,
});

type Props = {
  children: React.ReactNode;
}

export const AuthProvider = ({ children }: Props) => {
  const [user, setUser] = useState<User | null>(null);

  const [accessToken, setAccessToken] = useState<string | null>(
    localStorage.getItem(ACCESS_TOKEN_KEY)
  );
  const [refreshToken, setRefreshToken] = useState<string | null>(
    localStorage.getItem(REFRESH_TOKEN_KEY)
  );



  const authorized = useMemo(() => {
    if (!accessToken || !refreshToken) return false;

    return true; // TODO: валидация refresh token
  }, [accessToken, refreshToken]);

  useEffect(() => {

  }, [authorized]);

  useEffect(() => {
    const handle = (event: StorageEvent) => {
      const { key } = event;

      key === ACCESS_TOKEN_KEY && setAccessToken(localStorage.getItem(ACCESS_TOKEN_KEY));
      key === REFRESH_TOKEN_KEY && setRefreshToken(localStorage.getItem(REFRESH_TOKEN_KEY));
    };

    window.addEventListener('storage', handle);

    return () => window.removeEventListener('storage', handle);
  }, []);

  return (
    <AuthContext.Provider value={{ user, authorized }}>
      {children}
    </AuthContext.Provider>
  )
}