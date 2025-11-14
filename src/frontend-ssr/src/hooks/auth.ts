import { useMemo, useState } from 'react';

export const useAuth = () => {
  const [accessToken, setAccessToken] = useState<string | null>();
  const [refreshToken, setRefreshToken] = useState<string | null>();

  const authorized = useMemo(() => true, [accessToken, refreshToken]);

  return { authorized };
};
