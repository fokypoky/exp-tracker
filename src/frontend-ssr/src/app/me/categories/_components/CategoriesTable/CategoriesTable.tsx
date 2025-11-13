'use client';

import { useEffect, useMemo, useState } from 'react';

import { CategoriesRepository } from '@api';
import { Table } from '@components';
import { useFetch } from '@hooks';
import { TableRow } from '@types';

import { HEADER_CODES, HEADERS_TABLE } from './CategoriesTable.constants';
import styles from './CategoriesTable.module.css';

type PaginatedState = {
  limit: number;
  offset: number;
}

export const CategoriesTable = () => {
  const { data, error, dispatch } = useFetch(CategoriesRepository.getList);
  const [paginatedState, setPaginatedState] = useState<PaginatedState>({ limit: 10, offset: 0 });

  const rows: TableRow[] = useMemo(() => {
    if (!data) return [];

    const rows: TableRow[] = [];

    data.forEach((category) => {
      const row: TableRow = new Map();

      row.set(HEADER_CODES.name, category.name);
      row.set(HEADER_CODES.description, category.id);

      rows.push(row);
    });

    return rows;
  }, [data]);

  useEffect(() => {
    dispatch({ limit: paginatedState.limit, offset: paginatedState.offset });
  }, [paginatedState]);
  
  return (
    <div className={styles.table}>
      <Table
        headers={HEADERS_TABLE}
        rows={rows}
      />
    </div>
  );
};
