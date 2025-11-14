import { Spinner } from '@components';

import styles from './Page.module.css';

type Props = {
  children: React.ReactNode | React.ReactNode[];
  header?: string | React.ReactNode;
  loading?: boolean;
}

export const Page = ({ children, header, loading }: Props) => {
  return (
    <div className={styles.page}>
      {header && (
        <div>{header}</div>
      )}
      {loading ? <Spinner size="l" /> : children}
    </div>
  );
};
