import { Label, RadioButton } from '@components';
import { ComponentSize, RadioButtonGroupConfig } from '@types';

import styles from './RadioButtonGroup.module.css';

type Props = {
  config: RadioButtonGroupConfig;
  label?: string;
  size?: ComponentSize;

  onChange?(value: unknown): void;
}

export const RadioButtonGroup = ({ label, size = 'm', config, onChange }: Props) => {
  return (
    <div className={styles.radio_group}>
      {label && (
        <Label text={label} size="m" />
      )}
      <div className={styles.buttons}>
        {config.items?.map((item, index) => (
          <RadioButton
            key={index}
            label={item.label}
            value={item.value}
            control={config.form.control}
            name={config.fieldName}
            size={size}
            onChange={onChange}
          />
        ))}
      </div>
    </div>
  );
};
