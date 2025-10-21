import Link from 'next/link';

import { DatabaseIcon } from '@components';
import { APP_ROUTES } from '@constants';

import styles from './Logo.module.css';

type Props = {
  url?: string;
}

export const Logo = ({ url = APP_ROUTES.base }: Props) => {
  return (
    <div className={styles.logo}>
      <DatabaseIcon />
      {/*<span className={styles['logo__text']}>ExpTracker</span>*/}
      <Link href={url} className={styles.logo__text}>ExpTracker</Link>
    </div>
  );
};
