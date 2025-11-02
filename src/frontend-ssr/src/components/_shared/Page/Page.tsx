import styles from './Page.module.css';

type Props = {
  children: React.ReactNode | React.ReactNode[];
  header?: string | React.ReactNode;
}

export const Page = ({ children, header }: Props) => {
  return (
    <div className={styles.page}>
      {header && (
        <div>{header}</div>
      )}
      {children}
    </div>
  );
};
