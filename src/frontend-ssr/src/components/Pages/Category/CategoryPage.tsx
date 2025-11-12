'use client';

import { useMemo } from 'react';

import { Page } from '@components';
import { APP_ROUTES } from '@constants';

type Props = {
	id: string;
}

export const CategoryPage = ({ id }: Props) => {
	const createMode = useMemo(() => id === APP_ROUTES.create, [id]);

	return (
		<Page>
			<>{id}</>
		</Page>
	);
};
