import { useFetch } from '@hooks';
import { ApiContracts } from '@api';
import { useEffect, useState } from 'react';
import type { Profile as ProfileType } from '@api/contracts.ts';

export const Profile = () => {
	const { authFetch } = useFetch();
	const [profile, setProfile] = useState<ApiContracts.Profile | null>(null);

	useEffect(() => {
		authFetch<ProfileType, unknown>('/profile', 'GET');
	}, []);

	return (
		<></>
	);
};
