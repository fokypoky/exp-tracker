'use client';

import { yupResolver } from '@hookform/resolvers/yup';
import Link from 'next/link';
import { useForm } from 'react-hook-form';

import { AuthRepository } from '@api';
import { Button, Card, Input, Page } from '@components';
import { APP_ROUTES } from '@constants';
import { useFetch } from '@hooks';
import { RegisterFormType } from '@types';
import { RegisterSchema } from '@utils';

import styles from './page.module.css';

export default function RegisterPage () {
  const { control, handleSubmit } = useForm<RegisterFormType>({
    defaultValues: {
      login: '',
      password: '',
      repeatPassword: '',
    },
    resolver: yupResolver(RegisterSchema) as any,
  });

  // TODO: implement
  const { data, loading, dispatch, error } = useFetch(AuthRepository.register);

  const onSubmit = (data: RegisterFormType) => {
    dispatch(data);
  };

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
            <Button type="submit">
              Зарегестрироваться
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
