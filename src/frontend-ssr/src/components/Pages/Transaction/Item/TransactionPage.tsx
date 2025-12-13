'use client';

import { yupResolver } from '@hookform/resolvers/yup';
import { useEffect, useState } from 'react';
import { useForm } from 'react-hook-form';

import { CategoriesRepository, TransactionsRepository, TransactionType } from '@api';
import { Loading, Page, PageActions, PageHeader } from '@components';
import { APP_WORKSPACE_ROUTES, CREATE_PAGE_ID, TRANSACTION_FORM_DEFAULTS } from '@constants';
import { useAppRouter, useFetch } from '@hooks';
import { TransactionFormType } from '@types';
import { TransactionSchema } from '@utils';

import { Properties } from './components/Properties/Properties';

type Props = {
  id: string;
}

export const TransactionPage = ({ id }: Props) => {
  const navigate = useAppRouter();
  const form = useForm<TransactionFormType>({
    defaultValues: TRANSACTION_FORM_DEFAULTS(),
    resolver: yupResolver(TransactionSchema) as any,
  });

  const {
    dispatch: dispatchCategories,
    data: dataCategories,
    error: errorCategories,
    loading: loadingCategories,
  } = useFetch(CategoriesRepository.getList);

  const {
    dispatch:spatchGET,
    data: dataGET,
    error: errorGET,
    loading: loadingGET,
  } = useFetch(TransactionsRepository.get);

  const loading = loadingCategories || loadingGET;

  const createMode = id === CREATE_PAGE_ID;
  const [editMode, setEditMode] = useState<boolean>(createMode);

  useEffect(() => {
    dispatchCategories({ noLimit: true });

    !editMode && dispatchGET({ id }).then((res) => {
      res && form.reset(res);
    });
  }, []);

  const onSave = () => {
    form.trigger().then((valid) => {

    });
  };

  return (
    <Page
      header={
        <PageHeader
          title={createMode ? 'Добавить транзакцию' : ''}
          actions={
            <PageActions
              editMode={editMode}
              onApply={onSave}
              onCancel={() => {
                createMode ? navigate(APP_WORKSPACE_ROUTES.transactions, true) : setEditMode(false);
              }}
              onChangeMode={() => setEditMode((prev) => !prev)}
            />
          }
        />
      }
    >
      {loading ? (
        <Loading />
      ) : (
        <Properties form={form} editMode={editMode} />
      )}
    </Page>
  );
};
