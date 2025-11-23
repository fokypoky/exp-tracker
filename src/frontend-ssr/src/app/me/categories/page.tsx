'use client';

import { useEffect, useMemo } from 'react';

import { CategoriesRepository } from '@api';
import { CardList, Page, PageHeader, Pagination, SearchInput } from '@components';
import { CREATE_PAGE_ID } from '@constants';
import { useAppRouter, useFetch, useFilters } from '@hooks';
import { CardListItem } from '@types';

export default function CategoriesPage() {
  const { data, dispatch, totalCount } = useFetch(CategoriesRepository.getList);
  const { dispatch: dispatchDelete } = useFetch(CategoriesRepository.delete);

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
      onDelete: () => {
        dispatchDelete(item.id).then(() => {
          dispatch({ limit: filters.limit!, offset: filters.offset! });
        });
      },
    }));
  }, [data, navigate, filters]);

  return (
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
  );
}
