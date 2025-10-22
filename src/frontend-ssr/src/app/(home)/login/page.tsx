'use client';

import { yupResolver } from '@hookform/resolvers/yup';
import Link from 'next/link';
import { useForm } from 'react-hook-form';

import { AuthRepository } from '@api';
import { Button, Card, Input, Page } from '@components';
import { APP_ROUTES } from '@constants';
import { useFetch } from '@hooks';
import { LoginFormType } from '@types';
import { LogInSchema } from '@utils';

import styles from './page.module.css';

export default function LoginPage () {
  const { control, handleSubmit } = useForm<LoginFormType>({
    defaultValues: {
      login: '',
      password: '',
    },
    resolver: yupResolver(LogInSchema) as any, // TODO: временное решение
  });

  const { data, loading, dispatch } = useFetch(AuthRepository.logIn);

  const onSubmit = (data: LoginFormType) => {
    const { login, password } = data;
    dispatch({ login, password });
  };

  return (
    <Page>
      <div className={styles.container}>
        <Card
          className={styles.form}
          header={<h2 className={styles.header}>Добро пожаловать</h2>}
        >
          <form className={styles.form__content} onSubmit={handleSubmit(onSubmit)}>
            <Input
              control={control}
              name="login"
              label="Логин или e-mail"
              placeholder="Введите логин или e-mail"
            />
            <Input
              control={control}
              name="password"
              label="Пароль"
              placeholder="Введите пароль"
              type="password"
            />
            <Link href="">Забыли пароль?</Link>
            <Button type="submit">
              Войти
            </Button>
            <Link href={APP_ROUTES.register} className={styles.register_link}>
              Нет аккаунта? Создайте его
            </Link>
          </form>
        </Card>
      </div>
    </Page>
  );
}
