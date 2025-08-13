import { useState } from 'react';

import { useNavigate } from 'react-router-dom';
import { Books, Coins, SignOut, User } from 'phosphor-react';
import { Tooltip } from 'antd';
import classNames from 'classnames';

import { AppRoutes } from '@constants';
import { ApproveModal } from '@components';
import { useAuth } from '@hooks';
import { AuthRepository } from '@api';

import { ICON_SIZE } from './PageActions.constants';
import styles from './PageActions.module.css';

export const PageActions = () => {
	const navigate = useNavigate();
	const { clearTokenPair, refreshToken } = useAuth();

  const [showModal, setShowModal] = useState<boolean>(false);

	const onActionClick = (route: AppRoutes) => {
		navigate(route);
	};

	const onLogOutAction = () => {
		setShowModal(false);

		AuthRepository
			.logOut({ refreshToken: refreshToken! })
			.catch((e) => console.error(e))
			.finally(() => {
				clearTokenPair();
				navigate(AppRoutes.Login);
			})
	};

	return (
		<>
			<ApproveModal
				title="Выход из профиля"
				warning="Вы действительно хотите выйти?"
				isOpen={showModal}
				onApprove={onLogOutAction}
				onClose={() => setShowModal(false)}
			/>
			<div className={styles.container}>
				<div className={styles.action} onClick={() => onActionClick(AppRoutes.Profile)}>
					<Tooltip title="Мой профиль" placement="right">
						<User size={24} />
					</Tooltip>
				</div>

				<div className={styles.action} onClick={() => onActionClick(AppRoutes.Expenses)}>
					<Tooltip title="Мои затраты" placement="right">
						<Coins size={ICON_SIZE} />
					</Tooltip>
				</div>

				<div className={styles.action} onClick={() => onActionClick(AppRoutes.Categories)}>
					<Tooltip title="Мои категории" placement="right">
						<Books size={ICON_SIZE} />
					</Tooltip>
				</div>

				<div
					className={classNames(styles.action, styles['action__logout'])}
					onClick={() => setShowModal(true)}
				>
					<Tooltip title="Выйти" placement="right">
						<SignOut size={ICON_SIZE} />
					</Tooltip>
				</div>
			</div>
		</>
	);
};
