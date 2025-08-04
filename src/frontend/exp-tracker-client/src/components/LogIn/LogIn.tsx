import { Button, Form, Input } from 'antd';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import { logInSchema } from '@utils';
import { FormItem } from 'react-hook-form-antd';

import styles from './LogIn.module.css';

export const LogIn = () => {
	const { handleSubmit, control } = useForm({
		defaultValues: { login: '', password: '' },
		resolver: yupResolver(logInSchema)
	});

	const onSubmit = (data: { login: string, password: string }) => {
		// TODO: реализация авторизации
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
					<Button color="default" variant="solid">Регистрация</Button>
					<Button type="primary" htmlType="submit">Вход</Button>
				</div>
			</Form>
		</div>
	);
};