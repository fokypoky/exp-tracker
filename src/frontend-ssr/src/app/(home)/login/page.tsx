'use client';

import { yupResolver } from '@hookform/resolvers/yup';
import Link from 'next/link';
import { useRouter } from 'next/navigation';
import { useEffect } from 'react';
import { useForm } from 'react-hook-form';

import { AuthRepository } from '@api';
import { Button, Card, Input, Page } from '@components';
import { ACCESS_TOKEN_KEY, APP_ROUTES, REFRESH_TOKEN_KEY } from '@constants';
import { useFetch, useNotifier } from '@hooks';
import { LoginFormType } from '@types';
import { LogInSchema, setStorageTokenPair } from '@utils';

import styles from './page.module.css';

export default function LoginPage () {
  const router = useRouter();

  const { control, handleSubmit } = useForm<LoginFormType>({
    defaultValues: {
      login: '',
      password: '',
    },
    resolver: yupResolver(LogInSchema) as any,
  });

  const { error: notifyError } = useNotifier();
  const { data, loading, dispatch, error } = useFetch(AuthRepository.logIn);

  const onSubmit = (data: LoginFormType) => dispatch(data);

  useEffect(() => {
    error && notifyError({ title: '', message: error });
  }, [error, notifyError]);

  useEffect(() => {
    if (!data || !router) return;

    setStorageTokenPair(data.accessToken, data.refreshToken);

    router.push(APP_ROUTES.overview);
  }, [data, router]);

  useEffect(() => {
    const accessToken = localStorage.getItem(ACCESS_TOKEN_KEY);
    const refreshToken = localStorage.getItem(REFRESH_TOKEN_KEY);

    if (accessToken && refreshToken) router.push(APP_ROUTES.overview);
  }, []);

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
            <Button
              type="submit"
              spinner={loading}
              disabled={loading}
            >
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
