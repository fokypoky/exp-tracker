import styles from './Page.module.css';

type Props = {
  children: React.ReactNode | React.ReactNode[];
}

export const Page = ({ children }: Props) => {
  return (
    <div className={styles.page}>
      {children}
    </div>
  );
};
