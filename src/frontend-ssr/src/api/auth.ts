import { fetchApi } from '@utils';

import { AuthRequest, RefreshTokenRequest, TokenPair } from './contracts';

class authRepository {
  register(request: AuthRequest) {
    return fetchApi<AuthRequest, TokenPair>('/auth/register', 'POST', request);
  }

  logIn(request: AuthRequest) {
    return fetchApi<AuthRequest, TokenPair>('/auth/login', 'POST', request);
  }

  logOut(request: RefreshTokenRequest) {
    return fetchApi<RefreshTokenRequest, object>('/auth/logout', 'POST', request);
  }

  refresh(request: RefreshTokenRequest) {
    return fetchApi<RefreshTokenRequest, TokenPair>('/auth/refresh', 'POST', request);
  }
}

export const AuthRepository = new authRepository();
