export const REQUIRED_MSG = 'Поле обязательно';

export const MAX_LOGIN_LEN = 10;
export const MAX_CATEGORY_NAME_LEN = 100;

export const MIN_LOGIN_LEN = 4;
export const MIN_PASSWORD_LEN = 8;
export const MIN_CATEGORY_NAME_LEN = 3;

export const MIN_TRANSACTION_COST = 0;

export const MIN_DAY_OF_MONTH = 1;
export const MAX_DAY_OF_MONTH = 31;

// messages

export const INVALID_ENUM_MSG = 'Неизвестный тип значения';

export const LOGIN_LEN_MORE_THEN_MAX_MSG = `Логин не может быть больше ${MAX_LOGIN_LEN} символа(-ов)`;
export const CATEGORY_NAME_LEN_MORE_THEN_MAX_MSG =
  `Название не может превышать ${MAX_CATEGORY_NAME_LEN} символа(-ов)`;

export const LOGIN_LEN_LESS_THEN_MIN_MSG = `Логин не может быть меньше ${MIN_LOGIN_LEN} символа(-ов)`;
export const PASSWORD_LEN_LESS_THEN_MIN_MSG = `Пароль не может быть меньше ${MIN_PASSWORD_LEN} символа(-ов)`;
export const CATEGORY_NAME_LEN_LESS_THEN_MIN_MSG = `Название не может быть меньше ${MIN_CATEGORY_NAME_LEN} символа(-ов)`;

export const TRANSACTION_COST_LESS_THEN_MIN_MSG = `Стоимость не может быть меньше ${MIN_TRANSACTION_COST}`;

export const LESS_THEN_MIN_DAY_OF_MONTH_MSG = `День не может быть меньше ${MIN_DAY_OF_MONTH}-го`;
export const MORE_THEN_MAX_DAY_OF_MONTH_MSG = `День не может быть больше ${MAX_DAY_OF_MONTH}-го`;
