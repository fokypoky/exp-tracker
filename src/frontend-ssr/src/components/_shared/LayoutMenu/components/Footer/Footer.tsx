'use client';

import classNames from 'classnames';
import { useContext, useEffect } from 'react';

import { AuthContext } from '@providers';

import styles from './Footer.module.css';

type Props = {
  className?: string;
}

export const Footer = ({ className }: Props) => {
  const { user } = useContext(AuthContext);

  return (
    // TODO: при клике вести на страницу редактирования профиля
    <div className={classNames(styles.footer, className)}>
      {user.login && (
        <>
          <div className={styles.avatar}>{user.login.charAt(0)?.toUpperCase() || ''}</div>
          <div className={styles['user_info']}>
            {user.login}
          </div>
        </>
      )}
    </div>
  );
};
