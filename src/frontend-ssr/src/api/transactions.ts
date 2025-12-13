import { AxiosResponse } from 'axios';
import qs from 'qs';

import { GetTransactionRequest, GetTransactionsRequest, Transaction } from '@api';
import { authFetchApi } from '@utils';


class transactionsRepository {
  getList(request: GetTransactionsRequest): Promise<AxiosResponse<Transaction[]>> {
    const query = qs.stringify(request);
    return authFetchApi<GetTransactionsRequest, Transaction[]>(`/transactions?${query}`, 'GET');
  }

  get(request: GetTransactionRequest): Promise<AxiosResponse<Transaction>> {
    return authFetchApi<GetTransactionRequest, Transaction>(`/transactions/${request.id}`, 'GET');
  }
}

export const TransactionsRepository = new transactionsRepository();
