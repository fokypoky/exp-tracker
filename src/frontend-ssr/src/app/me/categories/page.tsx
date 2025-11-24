'use client';

import { useEffect, useMemo, useState } from 'react';

import { CategoriesRepository, TransactionCategory } from '@api';
import { ApproveModal, CardList, Page, PageHeader, Pagination, SearchInput } from '@components';
import { CREATE_PAGE_ID } from '@constants';
import { useAppRouter, useFetch, useFilters } from '@hooks';
import { CardListItem, ModalType } from '@types';

const defaultModalState: ModalType<TransactionCategory> = {
  show: false,
};

export default function CategoriesPage() {
  const { data, dispatch, totalCount } = useFetch(CategoriesRepository.getList);
  const { dispatch: dispatchDelete, error } = useFetch(CategoriesRepository.delete);

  const [modalState, setModalState] = useState<ModalType<TransactionCategory>>(defaultModalState);

  const { filters, setPage, page, setItemsPerPage, setFilters } = useFilters();
  const navigate = useAppRouter();

  useEffect(() => {
    dispatch({
      limit: filters.limit!,
      offset: filters.offset!,
    });
  }, [filters]);

  const cardList: CardListItem[] = useMemo(() => {
    if (!data) return [];

    return data.map((item) => ({
      title: item.name,
      description: item.description || '',
      onOpen: () => navigate(`/${item.id}`),
      onDelete: () => setModalState({ show: true, value: item }),
    }));
  }, [data, navigate, filters]);

  const onDeleteItem = (item: TransactionCategory) => {
    setModalState(defaultModalState);

    dispatchDelete(item.id).then(() => {
      dispatch({ limit: filters.limit!, offset: filters.offset! });
    });
  };

  return (
    <>
      {modalState.show && (
        <ApproveModal
          title="Удаление категории"
          message={`Вы действительно хотите удалить категорию ${modalState.value!.name}? Действие нельзя будет отменить`}
          onApprove={() => onDeleteItem(modalState.value!)}
          onCancel={() => setModalState(defaultModalState)}
        />
      )}
      <Page
        header={
          <PageHeader
            title="Категории"
            subtitle="Управляй категориями своих расходов и доходов"
            onAdd={() => navigate(`/${CREATE_PAGE_ID}`)}
          />
        }
      >
        <SearchInput
          onSearch={(value) => setFilters({ ...filters, searchString: value })}
        />
        <CardList items={cardList}/>
        <Pagination
          totalCount={totalCount}
          onItemsPerPageChanged={setItemsPerPage}
          onPageChanged={setPage}
          page={page}
        />
      </Page>
    </>
  );
}
