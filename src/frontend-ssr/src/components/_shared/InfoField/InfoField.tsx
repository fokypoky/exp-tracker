import styles from './InfoField.module.css';

type Props = {
  children: string | React.ReactNode;
}

export const InfoField = ({ children }: Props) => {
  return (
    <span className={styles['info_field']}>{children}</span>
  );
};