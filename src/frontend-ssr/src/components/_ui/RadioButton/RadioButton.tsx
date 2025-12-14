import classNames from 'classnames';
import { Control, Controller } from 'react-hook-form';

import { ComponentSize } from '@types';

import { Label } from '../Label/Label';

import styles from './RadioButton.module.css';

type Props = {
  label: string;
  control?: Control<any>;
  name?: string;
  error?: string;
  value?: unknown;
  selected?: boolean;
  size?: ComponentSize;

  onChange?(value: unknown): void;
}

export const RadioButton = (props: Props) => {
  const controlled = !!props.control && !!props.name;
  return controlled ? <ControlledRadio {...props} /> : <RadioButton {...props} />;
};

const ControlledRadio = ({ name, control, ...props }: Props) => {
  return (
    <Controller
      name={name!}
      control={control!}
      render={({ field, fieldState }) => (
        <RadioComponent
          error={fieldState.error?.message}
          {...props}
          onChange={(value) => {
            field.onChange(value);
            props.onChange && props.onChange(value);
          }}
          selected={field.value === props.value}
        />
      )}
    />
  );
};

const RadioComponent = ({ size = 'm', ...props }: Props) => {
  const outerCircleClassName = classNames(styles.circle_outer, {
    [styles.circle_outer__s]: size === 's',
    [styles.circle_outer__m]: size === 'm',
    [styles.circle_outer__l]: size === 'l',
    [styles.circle_outer__error]: !!props.error,
    [styles.circle_outer__selected]: props.selected,
  });

  const innerCircleClassName = classNames(styles.circle_inner, {
    [styles.circle_inner__s]: size === 's',
    [styles.circle_inner__m]: size === 'm',
    [styles.circle_inner__l]: size === 'l',

  });

  return (
    <div
      className={styles.radio_container}
      onClick={() => props.onChange!(props.value)}
    >
      <div className={outerCircleClassName}>
        <div className={innerCircleClassName} />
      </div>
      <Label text={props.label} />
    </div>
  );
};
