import { AxiosResponse } from 'axios';
import qs from 'qs';

import { authFetchApi } from '@utils';

import { GetCategoriesRequest, TransactionCategory } from './contracts';

class categoriesRepository {
  get(request: GetCategoriesRequest): Promise<AxiosResponse<TransactionCategory[]>> {
    const query = qs.stringify(request);
    return authFetchApi<GetCategoriesRequest, TransactionCategory[]>(`/categories?${query}`, 'GET');
  }
}

export const CategoriesRepository = new categoriesRepository();
