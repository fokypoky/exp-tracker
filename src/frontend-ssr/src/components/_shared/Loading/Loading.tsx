import { Label, Spinner } from '@components';

import styles from './Loading.module.css';

export const Loading = () => {
  return (
    <div className={styles.loading_page}>
      <Spinner />
      <Label text="Загрузка" />
    </div>
  );
};
