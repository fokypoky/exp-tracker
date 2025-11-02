export type TokenPair = {
  accessToken: string;
  refreshToken: string;
}

export type AuthRequest = {
  login: string;
  password: string;
}

export type RefreshTokenRequest = {
  refreshToken: string;
}

export type ErrorResponse = {
  code: number;
  message: string;
}

export type GetCategoriesRequest = {
  limit: number;
  offset: number;
}

export type JwtPayload = {
  login: string;
  guid: string;
  exp: number;
  iss: string;
}

export type TransactionCategory = {
  guid: string;
  name: string;
}
