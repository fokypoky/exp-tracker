'use client';

import { Page, PageHeader, Table } from '@components';
import { CREATE_PAGE_ID } from '@constants';
import { useAppRouter } from '@hooks';

import { HEADERS_TABLE } from './TransactionListPage.constants';

export const TransactionsListPage = () => {
  const navigate = useAppRouter();


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
          rows={[]}
          emptyText="Список транзакций пуст"
        />
      </Page>
    </>
  );
};
