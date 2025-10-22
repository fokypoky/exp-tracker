import axios, { AxiosResponse } from 'axios';

import { ACCESS_TOKEN_KEY } from '@constants';

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

export const authFetchApi = <TRequest, TResponse>(
  url: string,
  method: RequestMethod,
  body?: TRequest,
  headers?: Record<string, string>,
): Promise<AxiosResponse<TResponse>> => {
  const newHeaders: Record<string, string> = {
    'Authorization': `Bearer ${localStorage.getItem(ACCESS_TOKEN_KEY)}`,
    ...(headers || {}),
  };

  return fetchApi<TRequest, TResponse>(url, method, body, newHeaders);
};
