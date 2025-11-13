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
