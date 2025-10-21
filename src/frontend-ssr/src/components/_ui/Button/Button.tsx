import classNames from 'classnames';
import { ButtonHTMLAttributes, MouseEvent } from 'react';

import { ComponentSize } from '@types';

import styles from './Button.module.css';

type Props = {
  children: string | React.ReactNode;
  color?: ButtonColor;
  size?: ComponentSize;
  type?: 'button' | 'submit' | 'reset';
  disabled?: boolean;

  onClick?(event: MouseEvent<HTMLButtonElement>): void;
}

export enum ButtonColor {
  primary = 'primary',
  secondary = 'secondary',
}

export const Button = ({ children, color = ButtonColor.primary, size = 'm', disabled, onClick, type }: Props) => {
  const className = classNames(styles.button, styles[`button__size_${size}`], {
    [styles['button__primary']]: color === ButtonColor.primary,
    [styles['button__secondary']]: color === ButtonColor.secondary,
    [styles['button__primary_disabled']]: color === ButtonColor.primary && disabled,
    [styles['button__secondary_disabled']]: color === ButtonColor.secondary && disabled,
  });
  
  return (
    <button
      className={className}
      onClick={onClick}
      type={type}
      disabled={disabled}
    >
      {children}
    </button>
  );
};
