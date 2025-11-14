import classNames from 'classnames';

import styles from './Separator.module.css';

type Props = {
  position: 'left' | 'right';
}

export const Separator = ({ position }: Props) => {
  const className = classNames(styles.separator, {
    [styles['separator__left']]: position === 'left',
    [styles['separator__right']]: position === 'right',
  });

  return (
    <div className={className}>...</div>
  );
};
