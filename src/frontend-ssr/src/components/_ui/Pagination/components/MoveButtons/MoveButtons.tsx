import classNames from 'classnames';

import { KeyboardArrowIcon, KeyboardDoubleArrowIcon } from '@components';

import styles from './MoveButtons.module.css';

type Props = {
  position: 'left' | 'right';
  disabled: boolean;

  onSingleMove(): void;
  onFullMove(): void;
}

export const MoveButtons = ({ position, disabled, onSingleMove, onFullMove }: Props) => {
  const className = classNames(styles.move_buttons, {
    [styles.move_buttons__disabled]: disabled,
  });

  return (
    <div className={className}>
      <div
        className={styles.icon}
        onClick={() => {
          !disabled && onSingleMove();
        }}
      >
        <KeyboardArrowIcon direction={position} size={20} color="currentColor" />
      </div>
      <div
        className={styles.icon}
        onClick={() => {
          !disabled && onFullMove();
        }}
      >
        <KeyboardDoubleArrowIcon direction={position} size={20} color="currentColor" />
      </div>
    </div>
  );
};
