'use client';

import Link from 'next/link';
import { usePathname } from 'next/navigation';

import { Button, Logo } from '@components';
import { APP_ROUTES, FEATURES_CONTAINER_ID } from '@constants';

import styles from './Navbar.module.css';

export const Navbar = () => {
  const path = usePathname();

  const isRootPath = path === APP_ROUTES.base;
  const isAuthPath = path === APP_ROUTES.login || path === APP_ROUTES.register;

  return (
    <nav className={styles.nav}>
      <Logo />
      {isRootPath && (
        <div className={styles.actions}>
          <Link href={`#${FEATURES_CONTAINER_ID}`}>Возможности</Link>
          <Link href="">Цены</Link>
          <Link href="">Поддержка</Link>
        </div>
      )}
      {!isAuthPath && (
        <Button size="s">
          <Link href={APP_ROUTES.login} className={styles.link}>Начать</Link>
        </Button>
      )}
    </nav>
  );
};
