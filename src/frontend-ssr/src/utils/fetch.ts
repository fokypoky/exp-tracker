import axios, { AxiosError, AxiosResponse, HttpStatusCode } from 'axios';

import { RefreshTokenRequest, TokenPair } from '@api';
import { ACCESS_TOKEN_KEY, REFRESH_TOKEN_KEY } from '@constants';

import { clearStorageTokenPair, setStorageTokenPair } from './localStorage';

type RequestMethod = 'GET' | 'POST';

const instance = axios.create({
  baseURL: process.env.NEXT_PUBLIC_BACKEND_URL,
});

export const fetchApi = async <TRequest, TResponse>(
  url: string,
  method: RequestMethod,
  body?: TRequest,
  headers?: Record<string, string>,
): Promise<AxiosResponse<TResponse>> => {
  return instance<TResponse>(url, { method, data: body, headers });
};

export const authFetchApi = async <TRequest, TResponse>(
  url: string,
  method: RequestMethod,
  body?: TRequest,
  headers?: Record<string, string>,
  errorCallback?: () => void,
): Promise<AxiosResponse<TResponse>> => {
  const newHeaders: Record<string, string> = {
    'Authorization': `Bearer ${localStorage.getItem(ACCESS_TOKEN_KEY)}`,
    ...(headers || {}),
  };

  try {
    const result = await fetchApi<TRequest, TResponse>(url, method, body, newHeaders);
    return result;
  } catch (e) {
    const axiosError = e as AxiosError;

    if (axiosError.status === HttpStatusCode.Unauthorized) {
      try {
        const refreshedTokenPair = await fetchApi<RefreshTokenRequest, TokenPair>(
          '/auth/refresh',
          'POST',
          { refreshToken: localStorage.getItem(REFRESH_TOKEN_KEY) || '' },
        );
        const { accessToken, refreshToken } = refreshedTokenPair.data;

        setStorageTokenPair(accessToken, refreshToken);

        const refreshedHeaders: Record<string, string> = { ...headers, 'Authorization': `Bearer ${accessToken}` };

        return fetchApi<TRequest, TResponse>(url, method, body, refreshedHeaders); 
      } catch (e) {
        console.error(e);
        clearStorageTokenPair();
        throw e;
      }
    }

    throw e;
  }
};
