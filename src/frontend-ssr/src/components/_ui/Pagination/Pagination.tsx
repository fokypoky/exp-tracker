'use client';

import classNames from 'classnames';
import { useEffect, useState } from 'react';

import { Select } from '@components';
import { PAGINATION_ITEMS_PER_PAGE_OPTIONS } from '@constants';
import { Option } from '@types';

import { MoveButtons, Separator } from './components';
import styles from './Pagination.module.css';
import { getPages, getTotalPages, showLeftSeparator, showRightSeparator } from './Pagination.utils';

type Props = {
  totalCount: number;
  page: number;

  onItemsPerPageChanged(itemsPerPage: number): void;
  onPageChanged(page: number): void;
}

export const Pagination = ({ totalCount, page, onItemsPerPageChanged, onPageChanged }: Props) => {
  const [itemsPerPage, setItemsPerPage] = useState<Option<number>>(PAGINATION_ITEMS_PER_PAGE_OPTIONS[0]);
  const [pages, setPages] = useState<number[]>([]);

  useEffect(() => {
    setPages(getPages(totalCount, itemsPerPage.value, page));
  }, [totalCount, page, itemsPerPage]);

  return (
    <div className={styles.pagination}>
      <div className={styles['pages_container']}>
        {pages.length > 0 && (
          <MoveButtons
            position="left"
            disabled={page === 1}
            onSingleMove={() => onPageChanged(page - 1)}
            onFullMove={() => onPageChanged(1)}
          />
        )}
        <div className={styles['pages_container__items']}>
          {showLeftSeparator(totalCount, itemsPerPage.value, page) && <Separator position="left" />}
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
          {showRightSeparator(totalCount, itemsPerPage.value, page) && <Separator position="right" />}
        </div>
        {pages.length > 0 && (
          <MoveButtons
            position="right"
            disabled={page === getTotalPages(totalCount, itemsPerPage.value)}
            onSingleMove={() => onPageChanged(page + 1)}
            onFullMove={() => onPageChanged(getTotalPages(totalCount, itemsPerPage.value))}
          />
        )}
      </div>
      <div className={styles.selector}>
        <Select
          menuPlacement="top"
          options={PAGINATION_ITEMS_PER_PAGE_OPTIONS}
          onChange={(opt) => setItemsPerPage(opt)}
          value={itemsPerPage}
          label="Количество элементов"
          labelGray
          size="s"
          disabled={pages.length === 0}
        />
      </div>
    </div>
  );
};
