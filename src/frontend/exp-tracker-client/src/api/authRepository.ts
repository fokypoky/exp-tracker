import type { AxiosResponse } from 'axios';

import { ApiContracts } from '@api';
import { fetchApi } from '@utils';

type AuthRequest = ApiContracts.AuthRequest;
type RefreshTokenRequest = ApiContracts.RefreshTokenRequest;
type JwtTokenPair = ApiContracts.JwtTokenPair;

class Repository {
  register(request: AuthRequest): Promise<AxiosResponse<JwtTokenPair>> {
    return fetchApi<JwtTokenPair, AuthRequest>('/auth/register', 'POST', request);
  }

  logIn(request: AuthRequest): Promise<AxiosResponse<JwtTokenPair>> {
    return fetchApi<JwtTokenPair, AuthRequest>('/auth/login', 'POST', request);
  }

  logOut(request: RefreshTokenRequest): Promise<AxiosResponse> {
    return fetchApi<unknown, RefreshTokenRequest>('/auth/logout', 'POST', request);
  }
}

export const AuthRepository = new Repository();