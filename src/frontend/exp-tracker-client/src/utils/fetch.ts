/* eslint-disable  @typescript-eslint/no-explicit-any */
import type { AxiosResponse } from 'axios';
import axios from 'axios';

export type RequestMethodType = 'GET' | 'POST';

export type FetchFn = <TRequest, TResponse>(
	url: string,
	method: RequestMethodType,
	body?: TRequest,
	headers?: Record<string, string>,
) => Promise<AxiosResponse<TResponse>>;

const instance = axios.create({
	baseURL: 'https://localhost:7166',
});

export const fetchApi = <TResponse, TRequest>(
	url: string,
	method: RequestMethodType,
	body?: TRequest,
	headers?: Record<string, string>,
): Promise<AxiosResponse<TResponse>> => {
	switch (method) {
		case 'GET':
			return instance<TResponse>(url, { method: 'get', headers });
		case 'POST':
			return instance<TResponse>(url, { method: 'post', headers, data: body });
	}
};
