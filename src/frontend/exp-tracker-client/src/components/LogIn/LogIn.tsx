import { Button, Form, Input } from 'antd';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import { fetchApi, logInSchema } from '@utils';
import { FormItem } from 'react-hook-form-antd';

import styles from './LogIn.module.css';
import { useState } from 'react';
import type { AuthFormType } from '@types';

type AuthType = 'login' | 'register';

export const LogIn = () => {
	const [authType, setAuthType] = useState<AuthType | null>(null);

	const { handleSubmit, control } = useForm({
		defaultValues: { login: '', password: '' },
		resolver: yupResolver(logInSchema)
	});

	const onSubmit = (data: AuthFormType) => {
		if (!authType) return;
		fetchApi('/auth/register', 'POST', data).then((r) => console.log(r.data)).catch((e) => console.log(e))
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