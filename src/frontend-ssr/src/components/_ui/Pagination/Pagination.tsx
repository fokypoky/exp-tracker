'use client';

import classNames from 'classnames';
import { useEffect, useState } from 'react';

import { PAGINATION_ITEMS_PER_PAGE } from '@constants';

import { MoveButtons, Separator } from './components';
import styles from './Pagination.module.css';
import { getPages, getTotalPages, showLeftSeparator, showRightSeparator } from './Pagination.utils';

type Props = {
  totalCount: number;
  page: number;

  onItemsPerPageChanged(itemsPerPage: number): void;
  onPageChanged(page: number);
}

export const Pagination = ({ totalCount, page, onItemsPerPageChanged, onPageChanged }: Props) => {
  const [itemsPerPage, setItemsPerPage] = useState<number>(PAGINATION_ITEMS_PER_PAGE[0]);
  const [pages, setPages] = useState<number[]>([]);

  useEffect(() => {
    setPages(getPages(totalCount, itemsPerPage, page));
  }, [totalCount, page, itemsPerPage]);

  return (
    <div className={styles.pagination}>
      <div className={styles['pages_container']}>
        <MoveButtons
          position="left"
          disabled={page === 1}
          onSingleMove={() => onPageChanged(page - 1)}
          onFullMove={() => onPageChanged(1)}
        />
        <div className={styles['pages_container__items']}>
          {showLeftSeparator(totalCount, itemsPerPage, page) && <Separator position="left" />}
          {pages.map((item, index) => {
            const selected = page === item;
            const className = classNames(styles.page, {
              [styles['page__selected']]: selected,
            });

            return (
              <div
                key={index}
                className={className}
                onClick={() => {
                  !selected && onPageChanged(item);
                }}
              >
                {item}
              </div>
            );
          })}
          {showRightSeparator(totalCount, itemsPerPage, page) && <Separator position="right" />}
        </div>
        <MoveButtons
          position="right"
          disabled={page === getTotalPages(totalCount, itemsPerPage)}
          onSingleMove={() => onPageChanged(page + 1)}
          onFullMove={() => onPageChanged(getTotalPages(totalCount, itemsPerPage))}
        />
      </div>
      <div>SELECTOR</div>
    </div>
  );
};
