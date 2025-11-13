'use client';

import { yupResolver } from '@hookform/resolvers/yup';
import { useEffect, useMemo } from 'react';
import { useForm } from 'react-hook-form';

import { CategoriesRepository } from '@api';
import { Page, PageActions, PageHeader } from '@components';
import { APP_WORKSPACE_ROUTES, CREATE_PAGE_ID } from '@constants';
import { useAppRouter, useFetch, useNotifier } from '@hooks';
import { CategoryFormType } from '@types';
import { CategorySchema } from '@utils';

import { Properties } from './components/Properties';

type Props = {
	id: string;
}

export const CategoryPage = ({ id }: Props) => {
	const navigate = useAppRouter();

	const { data, loading, dispatch: dispatchGet } = useFetch(CategoriesRepository.get);
	const { error: createError, dispatch: dispatchCreate } = useFetch(CategoriesRepository.create);

	const { error: notifyError } = useNotifier();

	const form = useForm<CategoryFormType>({
		defaultValues: {
			name: '',
		},
		resolver: yupResolver(CategorySchema) as any,
	});

	const createMode = useMemo(() => id === CREATE_PAGE_ID, [id]);

	useEffect(() => {
		!createMode && dispatchGet({ id });
	}, [id, createMode]);

	useEffect(() => {
		createError && notifyError({ message: createError, title: '' });
	}, [createError]);

	useEffect(() => {
		if (!data) return;
		form.reset(data);
	}, [data]);

	const onSave = () => {
		form.trigger().then((valid) => {
			if (!valid) return;

			const { name, description } = form.getValues();
			// TODO: добавить переход к созданной категории. нужно добавить в useAppRouter метод replaceCurrent
			dispatchCreate({ name, description });
		});
	};
	const onCancel = () => {
		navigate(APP_WORKSPACE_ROUTES.categories, true);
	};

	return (
		<Page
			loading={loading}
			header={
				<PageHeader
					title={createMode ? 'Добавить новую категорию' : (data?.name || '')}
					subtitle={!createMode && data ? 'Категория расходов и доходов' : undefined}
					actions={
						<PageActions
							editMode={createMode}
							onApply={onSave}
							onCancel={onCancel}
						/>
 					}
				/>
			}
		>
			<Properties form={form} editMode={createMode} />
		</Page>
	);
};
