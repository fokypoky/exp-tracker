'use client';

import { useEffect, useMemo } from 'react';

import { CategoriesRepository } from '@api';
import { Page, PageHeader, Pagination, SearchInput, Table } from '@components';
import { CREATE_PAGE_ID, TABLE_CLICK_KEY } from '@constants';
import { useAppRouter, useFetch, useFilters } from '@hooks';
import { TableRow } from '@types';

import { HEADER_CODES, HEADERS_TABLE } from './page.constants';

export default function CategoriesPage() {
  const { data, dispatch, totalCount } = useFetch(CategoriesRepository.getList);
  const { filters, setPage, page, setItemsPerPage, setFilters } = useFilters();
  const navigate = useAppRouter();

  useEffect(() => {
    dispatch({
      limit: filters.limit!,
      offset: filters.offset!,
    });
  }, [filters]);

  const rowsTable = useMemo(() => {
    if (!data) return [];

    const rows: TableRow[] = [];

    data.forEach((category) => {
      const row: TableRow = new Map();

      row.set(HEADER_CODES.name, category.name);
      row.set(HEADER_CODES.description, category.id);

      row.set(TABLE_CLICK_KEY, () => navigate(category.id));

      rows.push(row);
    });

    return rows;
  }, [data, navigate]);

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
      <Table
        headers={HEADERS_TABLE}
        rows={rowsTable}
      />
      <Pagination
        totalCount={totalCount}
        onItemsPerPageChanged={setItemsPerPage}
        onPageChanged={setPage}
        page={page}
      />
    </Page>
  );
}
