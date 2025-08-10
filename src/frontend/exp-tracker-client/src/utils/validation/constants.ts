// AUTH CONSTANTS
export const LOGIN_REGEX =
  /^[a-zA-Z0-9]+$/

export const LOGIN_MIN_LEN = 4;
export const LOGIN_MAX_LEN = 10;
export const PASSWORD_MIN_LEN = 8;

// COMMON MESSAGES
export const REQUIRED_MSG = 'Поле обязательно';

// AUTH MESSAGES
export const LOGIN_NOT_MATCHES_REGEX =
  'Логин должен состоять только из латинских букв и цифр';

export const LOGIN_LESS_THEN_MIN =
  `Минимальная длина логина ${LOGIN_MIN_LEN} символа(-ов)`;
export const LOGIN_MORE_THEN_MAX =
  `Максимальная длина логина ${LOGIN_MAX_LEN} символа(-ов)`;
export const PASSWORD_LESS_THEN_MIN =
  `Минимальная длина пароля ${PASSWORD_MIN_LEN} символа(-ов)`;