'use client';

import moment from 'moment-timezone';
import { useEffect, useMemo } from 'react';

import { TransactionsRepository } from '@api';
import { Page, PageHeader, Pagination, Table } from '@components';
import { CREATE_PAGE_ID, TRANSACTION_TYPE_MAPPING } from '@constants';
import { useAppRouter, useFetch, useFilters } from '@hooks';
import { TableRow } from '@types';
import { toLocalDate } from '@utils';

import { HeaderCodes, HEADERS_TABLE } from './TransactionListPage.constants';

export const TransactionsListPage = () => {
  const navigate = useAppRouter();
  const { filters, setPage, page, setItemsPerPage, setFilters } = useFilters();
  const { data, error, dispatch, totalCount } = useFetch(TransactionsRepository.getList);
  
  useEffect(() => {
    dispatch({ ...filters });
  }, [filters]);

  const rows = useMemo(() => {
    if (!data) return [];

    const rows: TableRow[] = [];

    data.forEach((transaction) => {
      const row: TableRow = new Map();

      row.set(HeaderCodes.transactionType, TRANSACTION_TYPE_MAPPING[transaction.type]);
      row.set(HeaderCodes.date, toLocalDate(transaction.date));
      row.set(HeaderCodes.cost, transaction.cost.toString());
      row.set(HeaderCodes.category, transaction.category.name);
      row.set(HeaderCodes.description, transaction.description);

      rows.push(row);
    });

    return rows;
  }, [data]);

  return (
    <>
      <Page
        header={
          <PageHeader
            title="Транзакции"
            subtitle="Добавляй свои расходы и доходы, чтобы увидеть детальную статистику"
            onAdd={() => navigate(`/${CREATE_PAGE_ID}`)}
          />
        }
      >
        <Table
          headers={HEADERS_TABLE}
          rows={rows}
          emptyText="Список транзакций пуст"
        />
        <Pagination
          totalCount={totalCount}
          page={page}
          onItemsPerPageChanged={(itemsPerPage) => setPage(itemsPerPage)}
          onPageChanged={(page) => setPage(page)}
        />
      </Page>
    </>
  );
};
