import classNames from 'classnames';

import { FONT_SIZES_MAPPING } from '@constants';
import { ComponentSize } from '@types';

import styles from './Label.module.css';

type Props = {
  text: string;
  tooltip?: string;
  bold?: boolean;
  className?: string;
  gray?: boolean;
  size?: ComponentSize;
}

export const Label = ({ text, tooltip, bold, gray, className, size = 'm' }: Props) => {
  const componentClassName = classNames(styles.label, {
    [styles.label__bold]: bold,
    [styles.label__gray]: gray,
  });

  return (
    <span className={componentClassName} style={{ fontSize: FONT_SIZES_MAPPING[size] }}>
      {text}
    </span>
  );
};
