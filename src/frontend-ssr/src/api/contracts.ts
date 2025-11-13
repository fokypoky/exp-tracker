// requests

export type AuthRequest = {
  login: string;
  password: string;
}

export type RefreshTokenRequest = {
  refreshToken: string;
}

// categories

export type GetCategoriesRequest = {
  limit: number;
  offset: number;
}

export type GetCategoryRequest = {
  id: string;
}

export type CreateCategoryRequest = {
  name: string;
  description?: string;
}

// entities

export type ErrorResponse = {
  code: number;
  message: string;
}

export type TokenPair = {
  accessToken: string;
  refreshToken: string;
}

export type JwtPayload = {
  login: string;
  guid: string;
  exp: number;
  iss: string;
}

export type TransactionCategory = {
  id: string;
  name: string;
  description?: string;
}
