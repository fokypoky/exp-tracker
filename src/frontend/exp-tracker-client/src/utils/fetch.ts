/* eslint-disable  @typescript-eslint/no-explicit-any */
import type { AxiosResponse } from "axios";
import axios from 'axios';

import { LocalStorageKey } from "@constants";

type RequestMethodType = 'GET' | 'POST';

const instance = axios.create({
  baseURL: 'http://localhost:5170',
});

export const fetchApi = <T, U>(
  url: string,
  method: RequestMethodType,
  body?: U,
  headers?: Record<string, any>
): Promise<AxiosResponse<T>> => {
  switch(method) {
    case 'GET':
      return instance<T>(url, { method: 'get', headers });
    case 'POST':
      return instance<T>(url, { method: 'post', headers, data: body });
  }
}

export const authFetch = <T, U>(
  url: string,
  method: RequestMethodType,
  body?: U,
  headers?: Record<string, any>
): Promise<AxiosResponse<T>> => {
  const token = localStorage.getItem(LocalStorageKey.AccessToken) || '';

  const requestHeaders: Record<string, any> = {
    ...headers,
    ['Authorization']: `Bearer ${token}`,
  };

  return fetchApi(url, method, body, requestHeaders);
}