'use client';

import { Button, PlusIcon } from '@components';

import styles from './PageHeader.module.css';

type Props = {
  title: string | React.ReactNode;
  subtitle?: string | React.ReactNode;
  actions?: React.ReactNode;

  onAdd?(): void;
}

export const PageHeader = ({ title, subtitle, onAdd, actions }: Props) => {
  return (
    <div className={styles.page_header}>
      <div className={styles['page_header__title']}>
        <h1>{title}</h1>
        <div>
          {actions && <>{actions}</>}
          {onAdd && (
            <Button
              size="m"
              onClick={() => onAdd()}
              icon={<PlusIcon size="s" fill="white" />}
            >
              Добавить
            </Button>
          )}
        </div>
      </div>
      {subtitle && (
        <div className={styles['page_header__subtitle']}>
          <h3>{subtitle}</h3>
        </div>
      )}
    </div>
  );
};
