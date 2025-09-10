import { Spin } from 'antd';
import { useEffect, useState } from 'react';

import { useFetch } from '@hooks';
import { ApiContracts } from '@api';
import type { Profile as ProfileType } from '@api/contracts.ts';

import { UserInfo } from './components';
import styles from './Profile.module.css';
import classNames from 'classnames';

export const Profile = () => {
	const { authFetch, loading } = useFetch();
	const [profile, setProfile] = useState<ApiContracts.Profile | null>(null);

	useEffect(() => {
		authFetch<ProfileType, unknown>('/profile', 'GET').then((res) => setProfile(res?.data || null));
	}, []);

	useEffect(() => {
		if (!profile) return;
	}, [profile]);

	const className = classNames(styles.container, {
		[styles['container__loading']]: !profile
	});

	return (
		<div className={className}>
			{profile ? <UserInfo profile={profile} /> : <Spin tip="Загрузка" />}
		</div>
	);
};
