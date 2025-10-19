'use client';

import Link from 'next/link';

import { Button, Logo } from '@components';
import { FEATURES_CONTAINER_ID } from "@constants";

import styles from './Navbar.module.css';

export const Navbar = () => {
  return (
    <nav className={styles.nav}>
      <Logo />
      <div className={styles.actions}>
        <Link href={`#${FEATURES_CONTAINER_ID}`}>Возможности</Link>
        <Link href="">Цены</Link>
        <Link href="">Поддержка</Link>
      </div>
      <Button>
        <Link href="" className={styles.link}>Войти</Link>
      </Button>
    </nav>
  );
};