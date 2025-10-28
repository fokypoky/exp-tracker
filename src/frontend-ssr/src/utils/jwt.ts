import { jwtDecode } from 'jwt-decode';

import { JwtPayload } from '@api';

export const parseJwtPayload = (jwt: string): JwtPayload | null => {
  try {
    const decoded = jwtDecode<JwtPayload>(jwt);
    return decoded;
  } catch (e) {
    console.error(e);
    return null;
  }
};

export const tokenExpired = (payload: JwtPayload): boolean => {
  const { exp } = payload;
  const current = Math.floor(new Date().getTime() / 1_000);
  
  return current > exp;
};
