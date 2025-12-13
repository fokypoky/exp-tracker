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
  search?: string;
  noLimit?: boolean;
  limit?: number;
  offset?: number;
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

// transactions

export type GetTransactionsRequest = {
  search?: string;
  limit?: number;
  offset?: number;
}

export type GetTransactionRequest = {
  id: string;
}

export enum TransactionType {
  withdraw = 'Withdraw',
  deposit = 'Deposit',
}

export enum TransactionIntervalType {
  single = 'Single',
  repeatable = 'Repeatable',
}

export enum TransactionIntervalStrategy {
  firstDayOfMonth = 'FirstDayOfMonth',
  lastDayOfMonth = 'LastDayOfMonth',
  specifiedDayOfMonth = 'SpecifiedDayOfMonth',
}

export type Transaction = {
  id?: string;
  cost: number;
  type: TransactionType;
  intervalType: TransactionIntervalType;
  intervalStrategy?: TransactionIntervalStrategy;
  date: string;
  dayOfMonth?: number;
  description?: string;
  category: TransactionCategory;
}
