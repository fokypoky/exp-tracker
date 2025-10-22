import { AxiosError, AxiosResponse } from 'axios';
import { useState } from 'react';

import { ErrorResponse } from '@api';
import { FAILED_FETCH_MSG } from '@constants';

type FetchFn<TRequest, TResponse> = (request: TRequest) => Promise<AxiosResponse<TResponse>>;

export const useFetch = <TRequest, TResponse>(fetchFn: FetchFn<TRequest, TResponse>) => {
  const [data, setData] = useState<TResponse | null>(null);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  const dispatch = async (request: TRequest) => {
    setLoading(true);
    setData(null);
    setError(null);

    try {
      const response = await fetchFn(request);

      setData(response.data);
      setLoading(false);
    } catch (e) {
      const error = e as AxiosError<ErrorResponse>;

      setError(error.response?.data.message || FAILED_FETCH_MSG);
      setData(null);
      setLoading(false);
    }
  };

  return { data, loading, error, dispatch };
};
