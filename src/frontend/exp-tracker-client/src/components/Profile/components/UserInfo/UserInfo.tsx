import { useEffect, useState } from 'react';

import { parseDateWithTZ } from '@utils';
import { ApiContracts } from '@api';
import { Block, InfoField } from '@components';
import styles from './UserInfo.module.css';

type Props = {
	profile: ApiContracts.Profile;
};

export const UserInfo = ({ profile }: Props) => {
	const [profileData, setProfileData] = useState<ApiContracts.Profile>(profile);

	useEffect(() => {
		setProfileData(profile);
	}, [profile]);

	return (
		<Block title="Информация о пользователе">
			<InfoField label="Пользователь" text={profileData.login} />
			<InfoField label="Дата регистрации" text={parseDateWithTZ(profile.registered)} />
		</Block>
	);
};
