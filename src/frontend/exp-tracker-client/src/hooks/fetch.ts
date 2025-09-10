import { useState } from 'react';

import { type AxiosError, type AxiosResponse, HttpStatusCode } from 'axios';
import { useNavigate } from 'react-router-dom';
import { ApiContracts } from '@api';
import { AppRoutes, LocalStorageKey } from '@constants';
import { fetchApi, type RequestMethodType } from '@utils';

export const useFetch = () => {
	const [loading, setLoading] = useState<boolean>(false);
	const navigate = useNavigate();

	const fetch = () => {

	};

	const authFetch = async <TResponse, TRequest>(
		url: string,
		method: RequestMethodType,
		body?: TRequest,
		headers?: Record<string, string>,
	) => {
		setLoading(true);

		const requestHeaders: Record<string, string> = {
			...headers,
			['Authorization']: `Bearer ${localStorage.getItem(LocalStorageKey.AccessToken)}`,
		};

		try {
			const result = await fetchApi<TResponse, TRequest>(url, method, body, requestHeaders);
			return result;
			// @ts-ignore
		} catch (e: AxiosError<any>) {
			console.log('e', e);
			if (e.status === HttpStatusCode.Unauthorized) {
				console.log('unauthorized, refreshing token');
				try {
					const refreshRequest: ApiContracts.RefreshTokenRequest = {
						refreshToken: localStorage.getItem(LocalStorageKey.RefreshToken)!,
					};

					const refreshResponse = await fetchApi<ApiContracts.JwtTokenPair, ApiContracts.RefreshTokenRequest>(
						'/auth/refresh',
						'POST',
						refreshRequest,
					);

					const { accessToken, refreshToken } = refreshResponse.data;

					localStorage.setItem(LocalStorageKey.AccessToken, accessToken);
					localStorage.setItem(LocalStorageKey.RefreshToken, refreshToken);
				} catch (e) {
					console.error(e);

					localStorage.clear();
					navigate(AppRoutes.Login);
				}
			}
		}
	};

	return { loading, fetch, authFetch };
};
