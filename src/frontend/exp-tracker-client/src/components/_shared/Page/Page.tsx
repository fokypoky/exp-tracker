import { useEffect } from 'react';
import classNames from 'classnames';
import { useNavigate } from 'react-router-dom';

import { PageActions } from '@components';
import { useAuth } from '@hooks';
import { AppRoutes } from '@constants';

import styles from './Page.module.css';

type Props = {
	children: React.ReactNode;
	actions?: boolean;
	protectedMode?: boolean;
}

export const Page = ({ children, actions, protectedMode }: Props) => {
	const navigate = useNavigate();
	const { isAuthorized } = useAuth();

	const className = classNames(styles.page, {
		[styles['actions-page']]: !!actions,
	});

	useEffect(() => {
		protectedMode && !isAuthorized() && navigate(AppRoutes.Login);
	}, []);

	return (
		<div className={className}>
			{actions && <PageActions />}
			<div className={styles.page__content}>
				{children}
			</div>
		</div>
	);
};
