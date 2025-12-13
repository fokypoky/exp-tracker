import * as yup from 'yup';

import { TransactionIntervalStrategy, TransactionIntervalType, TransactionType } from '@api';
import { CategoryFormType, LoginFormType, RegisterFormType, TransactionFormType } from '@types';

import {
  CATEGORY_NAME_LEN_LESS_THEN_MIN_MSG, CATEGORY_NAME_LEN_MORE_THEN_MAX_MSG,
  INVALID_ENUM_MSG,
  LESS_THEN_MIN_DAY_OF_MONTH_MSG,
  LOGIN_LEN_LESS_THEN_MIN_MSG,
  LOGIN_LEN_MORE_THEN_MAX_MSG, MAX_CATEGORY_NAME_LEN,
  MAX_DAY_OF_MONTH,
  MAX_LOGIN_LEN, MIN_CATEGORY_NAME_LEN,
  MIN_DAY_OF_MONTH,
  MIN_LOGIN_LEN, MIN_PASSWORD_LEN, MIN_TRANSACTION_COST, MORE_THEN_MAX_DAY_OF_MONTH_MSG, PASSWORD_LEN_LESS_THEN_MIN_MSG,
  REQUIRED_MSG,
  TRANSACTION_COST_LESS_THEN_MIN_MSG,
} from './constants';

export const LogInSchema = yup.object<LoginFormType>({
  login: yup.string()
    .required(REQUIRED_MSG)
    .min(MIN_LOGIN_LEN, LOGIN_LEN_LESS_THEN_MIN_MSG)
    .max(MAX_LOGIN_LEN, LOGIN_LEN_MORE_THEN_MAX_MSG),
  password: yup.string()
    .required(REQUIRED_MSG)
    .min(MIN_PASSWORD_LEN, PASSWORD_LEN_LESS_THEN_MIN_MSG),
});

export const RegisterSchema = yup.object<RegisterFormType>({
  login: yup.string()
    .required(REQUIRED_MSG)
    .min(MIN_LOGIN_LEN, LOGIN_LEN_LESS_THEN_MIN_MSG)
    .max(MAX_LOGIN_LEN, LOGIN_LEN_MORE_THEN_MAX_MSG),
  password: yup.string()
    .required(REQUIRED_MSG)
    .min(MIN_PASSWORD_LEN, PASSWORD_LEN_LESS_THEN_MIN_MSG),
  repeatPassword: yup.string()
    .required(REQUIRED_MSG)
    .oneOf([yup.ref('password')], 'Пароли должны совпадать'),
});

export const CategorySchema = yup.object<CategoryFormType>({
  name: yup.string()
    .required(REQUIRED_MSG)
    .min(MIN_CATEGORY_NAME_LEN, CATEGORY_NAME_LEN_LESS_THEN_MIN_MSG)
    .max(MAX_CATEGORY_NAME_LEN, CATEGORY_NAME_LEN_MORE_THEN_MAX_MSG),
  description: yup.string(),
});

export const TransactionSchema = yup.object<TransactionFormType>({
  cost: yup.number()
    .required(REQUIRED_MSG)
    .min(MIN_TRANSACTION_COST, TRANSACTION_COST_LESS_THEN_MIN_MSG),
  type: yup.string()
    .required()
    .oneOf(Object.values(TransactionType), INVALID_ENUM_MSG),
  intervalType: yup.string()
    .required(REQUIRED_MSG)
    .oneOf(Object.values(TransactionIntervalType), INVALID_ENUM_MSG),
  intervalStrategy: yup.string()
    .when('intervalType', {
      is: (it: string) => it === TransactionIntervalType.repeatable,
      then: (schema) =>
        schema.required(REQUIRED_MSG).oneOf(
          Object.values(TransactionIntervalStrategy), INVALID_ENUM_MSG,
        ),
      otherwise: (schema) =>
        schema.notRequired().nullable(),
    }),
  date: yup.string()
    .required(REQUIRED_MSG),
  dayOfMonth: yup.number()
    .when('intervalStratedy', {
      is: (strategy: string | undefined | null) => strategy !== undefined && strategy !== null,
      then: (schema) =>
        schema.required(REQUIRED_MSG)
          .min(MIN_DAY_OF_MONTH, LESS_THEN_MIN_DAY_OF_MONTH_MSG)
          .max(MAX_DAY_OF_MONTH, MORE_THEN_MAX_DAY_OF_MONTH_MSG),
      otherwise: (schema) => schema.notRequired().nullable(),
    }),
  description: yup.string(),
  category: yup.object(),
});

