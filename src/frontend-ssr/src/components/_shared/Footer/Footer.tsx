import Link from 'next/link';

import styles from './Footer.module.css';

export const Footer = () => {
  return (
    <footer className={styles.footer}>
      <div className={styles['terms__container']}>
        <Link href="">Политика конфиденциальности</Link>
        <Link href="">Связаться с нами</Link>
      </div>
      <div>
        © 2025 ExpTracker. Все права сохранены
      </div>
    </footer>
  );
};
