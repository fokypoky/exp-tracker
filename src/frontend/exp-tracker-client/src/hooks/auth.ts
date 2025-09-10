import { useEffect, useState } from 'react';
import { jwtDecode } from 'jwt-decode';

import type { JwtPayload } from '@types';
import { LocalStorageKey } from '@constants';

export const useAuth = () => {
	const [accessToken, setAccessToken] = useState<string | null>(
		localStorage.getItem(LocalStorageKey.AccessToken),
	);
	const [refreshToken, setRefreshToken] = useState<string | null>(
		localStorage.getItem(LocalStorageKey.RefreshToken),
	);

	useEffect(() => {
		window.addEventListener('storage', () => {
			const storageAccess = localStorage.getItem(LocalStorageKey.AccessToken);
			const storageRefresh = localStorage.getItem(LocalStorageKey.RefreshToken);

			storageAccess !== accessToken && setAccessToken(storageAccess);
			storageRefresh !== refreshToken && setRefreshToken(storageRefresh);
		});
	}, []);

	const setTokenPair = (access: string, refresh: string) => {
		setAccessToken(access);
		setRefreshToken(refresh);

		localStorage.setItem(LocalStorageKey.AccessToken, access);
		localStorage.setItem(LocalStorageKey.RefreshToken, refresh);
	};

	const clearTokenPair = () => {
		localStorage.removeItem(LocalStorageKey.AccessToken);
		localStorage.removeItem(LocalStorageKey.RefreshToken);
	};

	const isAuthorized = (): boolean => {
		const tokensExists = !!accessToken && !!refreshToken;

		if (!tokensExists) return false;

		try {
			const { exp } = jwtDecode<JwtPayload>(refreshToken!);
			const current = Math.floor(Date.now() / 1000);

			return exp > current;
		} catch (e) {
			console.error(e);
			return false;
		}
	};

	return { accessToken, refreshToken, clearTokenPair, setTokenPair, isAuthorized };
};
