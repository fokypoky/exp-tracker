import { useEffect, useState } from 'react';
import { Pencil } from 'phosphor-react';

import { parseDateWithTZ } from '@utils';
import { ApiContracts } from '@api';
import { Block, IconButton, InfoField } from '@components';

import styles from './UserInfo.module.css';

type Props = {
	profile: ApiContracts.Profile;
};

export const UserInfo = ({ profile }: Props) => {
	const [profileData, setProfileData] = useState<ApiContracts.Profile>(profile);
	const [editMode, setEditMode] = useState<boolean>(false);

	useEffect(() => {
		setProfileData(profile);
	}, [profile]);

	return (
		<Block title="Информация о пользователе" actions={
			<IconButton
				tooltip="Редактировать"
				icon={<Pencil size={24} />}
				onClick={() => setEditMode(!editMode)}
			/>
		}>
			<InfoField label="Пользователь" text={profileData.login} />
			<InfoField label="Дата регистрации" text={parseDateWithTZ(profile.registered)} />
		</Block>
	);
};