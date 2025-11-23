import { AxiosResponse } from 'axios';
import qs from 'qs';

import { authFetchApi } from '@utils';

import { CreateCategoryRequest, GetCategoriesRequest, GetCategoryRequest, TransactionCategory } from './contracts';

class categoriesRepository {
  getList(request: GetCategoriesRequest): Promise<AxiosResponse<TransactionCategory[]>> {
    const query = qs.stringify(request);
    return authFetchApi<GetCategoriesRequest, TransactionCategory[]>(`/categories?${query}`, 'GET');
  }

  get(request: GetCategoryRequest): Promise<AxiosResponse<TransactionCategory>> {
    return authFetchApi<GetCategoryRequest, TransactionCategory>(`/categories/${request.id}`, 'GET');
  }

  create(request: CreateCategoryRequest): Promise<AxiosResponse<TransactionCategory>> {
    return authFetchApi<CreateCategoryRequest, TransactionCategory>('/categories', 'POST', request);
  }

  update(request: TransactionCategory): Promise<AxiosResponse<TransactionCategory>> {
    return authFetchApi<TransactionCategory, TransactionCategory>('/categories', 'PUT', request);
  }
}

export const CategoriesRepository = new categoriesRepository();
