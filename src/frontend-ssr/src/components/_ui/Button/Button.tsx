import classNames from 'classnames';
import { MouseEvent } from 'react';

import styles from './Button.module.css';

type Props = {
  children: string | React.ReactNode;
  color?: ButtonColor;

  onClick?(event: MouseEvent<HTMLButtonElement>): void;
}

export enum ButtonColor {
  primary = 'primary',
  secondary = 'secondary',
}

export const Button = ({ children, color = ButtonColor.primary, onClick }: Props) => {
  const className = classNames(styles.button, {
    [styles['button__primary']]: color === ButtonColor.primary,
    [styles['button__secondary']]: color === ButtonColor.secondary,
  });
  
  return (
    <button
      className={className}
      onClick={onClick}
    >
      {children}
    </button>
  );
};