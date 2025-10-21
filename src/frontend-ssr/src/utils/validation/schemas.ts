import * as yup from 'yup';

import { LoginFormType } from '@types';

import {
  LOGIN_LEN_LESS_THEN_MIN_MSG,
  LOGIN_LEN_MORE_THEN_MAX_MSG,
  MAX_LOGIN_LEN,
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
