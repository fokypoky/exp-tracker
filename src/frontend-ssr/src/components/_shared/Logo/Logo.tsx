import { DatabaseIcon } from '@components';

import styles from './Logo.module.css';

export const Logo = () => {
  return (
    <div className={styles.logo}>
      <DatabaseIcon />
      <span className={styles['logo__text']}>ExpTracker</span>
    </div>
  );
};