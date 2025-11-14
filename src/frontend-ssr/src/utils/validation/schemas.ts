import * as yup from 'yup';

import { CategoryFormType, LoginFormType, RegisterFormType } from '@types';

import {
  CATEGORY_NAME_LEN_LESS_THEN_MIN_MSG, CATEGORY_NAME_LEN_MORE_THEN_MAX_MSG,
  LOGIN_LEN_LESS_THEN_MIN_MSG,
  LOGIN_LEN_MORE_THEN_MAX_MSG, MAX_CATEGORY_NAME_LEN,
  MAX_LOGIN_LEN, MIN_CATEGORY_NAME_LEN,
  MIN_LOGIN_LEN, MIN_PASSWORD_LEN, PASSWORD_LEN_LESS_THEN_MIN_MSG,
  REQUIRED_MSG,
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
