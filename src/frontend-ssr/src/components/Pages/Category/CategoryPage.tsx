'use client';

import { yupResolver } from '@hookform/resolvers/yup';
import { useEffect, useMemo, useState } from 'react';
import { useForm } from 'react-hook-form';

import { CategoriesRepository } from '@api';
import { Page, PageActions, PageHeader } from '@components';
import { APP_WORKSPACE_ROUTES, CREATE_PAGE_ID } from '@constants';
import { useAppRouter, useFetch } from '@hooks';
import { CategoryFormType } from '@types';
import { CategorySchema } from '@utils';

import { Properties } from './components/Properties';

type Props = {
  id: string;
}

export const CategoryPage = ({ id }: Props) => {
  const navigate = useAppRouter();

  const { error: getError, data, loading, dispatch: dispatchGet } = useFetch(CategoriesRepository.get);
  const { error: createError, dispatch: dispatchCreate } = useFetch(CategoriesRepository.create);
  const { error: updateError, dispatch: dispatchUpdate } = useFetch(CategoriesRepository.update);

  const form = useForm<CategoryFormType>({
    defaultValues: {
      name: '',
    },
    resolver: yupResolver(CategorySchema) as any,
  });

  const createMode = useMemo(() => id === CREATE_PAGE_ID, [id]);
  const [editMode, setEditMode] = useState<boolean>(createMode);

  useEffect(() => {
    !createMode && dispatchGet({ id });
  }, [id, createMode]);

  useEffect(() => {
    console.log('DATA', data);
    if (!data) return;
    form.reset(data);
  }, [data]);

  const onSave = () => {
    form.trigger().then((valid) => {
      if (!valid) return;

      const formData = form.getValues();

      if (createMode) {
        dispatchCreate({ name: formData.name, description: formData.description })
          .then((res) => {
            navigate(`${APP_WORKSPACE_ROUTES.categories}/${res?.id}`, true);
          });
      } else {
        dispatchUpdate({ ...formData, id: formData.id! })
          .then((res) => {
            res && dispatchGet({ id: res.id }).then(() => setEditMode(false));
          });
      }
    });
  };

  return (
    <Page
      loading={loading}
      header={
        <PageHeader
          title={createMode ? 'Создать новую категорию' : (data?.name || '')}
          subtitle={!createMode && data ? 'Категория расходов и доходов' : undefined}
          actions={
            <PageActions
              editMode={editMode}
              onApply={onSave}
              onCancel={() => setEditMode(false)}
              onChangeMode={() => setEditMode((prev) => !prev)}
            />
          }
        />
      }
    >
      <Properties form={form} editMode={editMode}/>
    </Page>
  );
};
