'use client';

import classNames from 'classnames';
import { HTMLInputTypeAttribute, useEffect } from 'react';
import { Control, Controller } from 'react-hook-form';

import { Label } from '@components';

import styles from './Input.module.css';

type Props = {
  control?: Control<any>;
  name?: string;
  label?: string;
  error?: string;
  placeholder?: string;
  type?: HTMLInputTypeAttribute;

  onChange?(val: string): void;
}

export const Input = (props: Props) => {
  const controlled = 'control' in props && 'name' in props;

  return controlled ? <ControlledInput {...props} /> : <UnControlledInput />;
};

const ControlledInput = (props: Props) => {
  const { error } = props.control!.getFieldState(props.name!);

  const className = classNames(styles.input, {
    [styles.input__error]: !!error,
  });

  return (
    <Controller
      name={props.name!}
      control={props.control}
      render={({ field }) => (
        <div className={styles.container}>
          {props.label && (
            <Label text={props.label} />
          )}
          <input
            {...field}
            className={className}
            placeholder={props.placeholder || 'Введите значение'}
            type={props.type}
            onChange={(e) => {
              field.onChange(e);
              props.onChange && props.onChange(e.target.value);
            }}
          />
          {!!error && (
            <span className={styles.error}>{error.message}</span>
          )}
        </div>
      )}
    />
  );
};

const UnControlledInput = () => {
  return (
    <input />
  );
};
