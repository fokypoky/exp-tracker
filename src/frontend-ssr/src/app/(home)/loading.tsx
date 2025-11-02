import { Label, Spinner } from '@components';

import styles from './loading.module.css';

export default function Loading() {
  return (
    <div className={styles.loading_page}>
      <Spinner size="l" />
      <Label text="Загрузка" />
    </div>
  );
}
