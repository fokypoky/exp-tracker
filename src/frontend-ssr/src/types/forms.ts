import {
  TransactionCategory,
  TransactionIntervalStrategy,
  TransactionIntervalType,
  TransactionType,
} from '@api';

export type LoginFormType = {
  login: string;
  password: string;
}

export type RegisterFormType = {
  login: string;
  password: string;
  repeatPassword: string;
}

export type SearchFormType = {
  search: string;
}

export type CategoryFormType = {
  id?: string;
  name: string;
  description?: string;
}

export type TransactionFormType = {
  id?: string;
  cost: number;
  type: TransactionType;
  intervalType: TransactionIntervalType;
  intervalStrategy?: TransactionIntervalStrategy;
  date: string;
  dayOfMonth?: number;
  description?: string;
  category?: TransactionCategory;
}
