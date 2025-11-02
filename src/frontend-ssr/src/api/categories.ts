import { AxiosResponse } from 'axios';

import { authFetchApi } from '@utils';

import { GetCategoriesRequest, TransactionCategory } from './contracts';

class categoriesRepository {
  get(request: GetCategoriesRequest): Promise<AxiosResponse<TransactionCategory[]>> {
    return authFetchApi<GetCategoriesRequest, TransactionCategory[]>('/categories', 'GET');
  }
}

export const CategoriesRepository = new categoriesRepository();
