import { useEffect, useState } from 'react';

import { Button, Form, Input } from 'antd';
import { useForm } from 'react-hook-form';
import { FormItem } from 'react-hook-form-antd';

import { useNavigate } from 'react-router-dom';
import { yupResolver } from '@hookform/resolvers/yup';
import { logInSchema } from '@utils';
import { AuthRepository } from '@api';
import { AppRoutes } from '@constants';
import { useAuth } from '@hooks';
import type { AuthFormType } from '@types';

import styles from './LogIn.module.css';

type AuthType = 'login' | 'register';

const defaultValues: AuthFormType = {
	login: '',
	password: '',
};

export const LogIn = () => {
	const [authType, setAuthType] = useState<AuthType | null>(null);

	const { handleSubmit, control } = useForm({
		defaultValues: defaultValues,
		resolver: yupResolver(logInSchema)
	});

	const { setTokenPair, isAuthorized } = useAuth();
	const navigate = useNavigate();

	useEffect(() => {
		const authorized = isAuthorized();
		authorized && navigate(AppRoutes.Profile);
	}, []);

	const onSubmit = (data: AuthFormType) => {
		if (!authType) return;

		const request = authType === 'register'
			? AuthRepository.register(data)
			: AuthRepository.logIn(data);

		request.then((res) => {
			const { accessToken, refreshToken } = res.data;
			setTokenPair(accessToken, refreshToken);
			navigate(AppRoutes.Profile);
		});
	}

	return (
		<div className={styles.container}>
			<Form
				className={styles.form}
				onFinish={handleSubmit(onSubmit)}
				layout="vertical"
			>
				<FormItem control={control} name="login" label="Логин">
					<Input placeholder="Введите логин" />
				</FormItem>
				<FormItem control={control} name="password" label="Пароль">
					<Input.Password placeholder="Введите пароль" />
				</FormItem>
				<div className={styles.buttons}>
					<Button
						color="default"
						variant="solid"
						htmlType="submit"
						onClick={() => setAuthType('register')}
					>
						Регистрация
					</Button>
					<Button
						type="primary"
						htmlType="submit"
						onClick={() => setAuthType('login')}
					>
						Вход
					</Button>
				</div>
			</Form>
		</div>
	);
};