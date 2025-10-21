import classNames from 'classnames';

import styles from './Label.module.css';

type Props = {
  text: string;
  tooltip?: string;
  bold?: boolean;
  className?: string;
}

export const Label = ({ text, tooltip, bold, className }: Props) => {
  const componentClassName = classNames(styles.label, {
    [styles.label__bold]: bold,
  });

  return (
    <span className={componentClassName}>
      {text}
    </span>
  );
};
