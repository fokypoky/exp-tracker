import { 
  LOGIN_LESS_THEN_MIN,
  LOGIN_MAX_LEN,
  LOGIN_MIN_LEN,
  LOGIN_MORE_THEN_MAX,
  LOGIN_NOT_MATCHES_REGEX,
  LOGIN_REGEX,
  PASSWORD_LESS_THEN_MIN,
  PASSWORD_MIN_LEN,
  REQUIRED_MSG
} from './constants';

import * as yup from 'yup';

export const logInSchema = yup.object({
  login: yup.string()
    .required(REQUIRED_MSG)
    .matches(LOGIN_REGEX, LOGIN_NOT_MATCHES_REGEX)
    .min(LOGIN_MIN_LEN, LOGIN_LESS_THEN_MIN)
    .max(LOGIN_MAX_LEN, LOGIN_MORE_THEN_MAX),

  password: yup.string()
    .required(REQUIRED_MSG)
    .min(PASSWORD_MIN_LEN, PASSWORD_LESS_THEN_MIN),
});