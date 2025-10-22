import { AxiosError, AxiosResponse } from 'axios';
import { useState } from 'react';

import { ErrorResponse } from '@api';

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
      if (e instanceof AxiosError) {
        const error = e.response!.data as ErrorResponse;

        setError(error.message);
      } else {
        setError(JSON.stringify(e));
      }

      setData(null);
      setLoading(false);
    }
  };

  return { data, loading, error, dispatch };
};
