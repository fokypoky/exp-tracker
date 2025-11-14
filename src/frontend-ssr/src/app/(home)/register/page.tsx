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
import { RegisterFormType } from '@types';
import { RegisterSchema, setStorageTokenPair } from '@utils';

import styles from './page.module.css';

export default function RegisterPage () {
  const router = useRouter();

  const { control, handleSubmit } = useForm<RegisterFormType>({
    defaultValues: {
      login: '',
      password: '',
      repeatPassword: '',
    },
    resolver: yupResolver(RegisterSchema) as any,
  });

  const { error: notifyError } = useNotifier();
  const { data, loading, dispatch, error } = useFetch(AuthRepository.register);

  const onSubmit = (data: RegisterFormType) => dispatch(data);

  useEffect(() => {
    error && notifyError({ title: '', message: error });
  }, [error]);

  useEffect(() => {
    if (!data) return;

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
          header={<h2 className={styles.header}>Регистрация</h2>}
        >
          <form className={styles.form__content} onSubmit={handleSubmit(onSubmit)}>
            <Input
              name="login"
              control={control}
              label="Логин или e-mail"
              placeholder="Введите логин или e-mail"
            />
            <Input
              name="password"
              control={control}
              label="Пароль"
              placeholder="Введите пароль"
              type="password"
            />
            <Input
              name="repeatPassword"
              control={control}
              label="Подтвердите пароль"
              placeholder="Введите пароль еще раз"
              type="password"
            />
            <Button
              type="submit"
              spinner={loading}
              disabled={loading}
            >
              Зарегистрироваться
            </Button>
            <Link href={APP_ROUTES.login} className={styles.login_link}>
              Уже есть аккаунт? Войдите
            </Link>
          </form>
        </Card>
      </div>
    </Page>
  );
}
