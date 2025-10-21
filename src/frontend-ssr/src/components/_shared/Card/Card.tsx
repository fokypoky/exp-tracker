import classNames from 'classnames';

import styles from './Card.module.css';

type Props = {
  children: React.ReactNode;

  header?: string | React.ReactNode;
  className?: string;
  headIcon?: React.ReactNode;
}

export const Card = ({ children, header, className, headIcon }: Props) => {
  return (
    <div className={classNames(styles.card, className)}>
      {headIcon && (
        <div>
          {headIcon}
        </div>
      )}
      {header && (
        <div className={styles.header}>
          {header}
        </div>
      )}
      <div className={styles.content}>
        {children}
      </div>
    </div>
  );
};
