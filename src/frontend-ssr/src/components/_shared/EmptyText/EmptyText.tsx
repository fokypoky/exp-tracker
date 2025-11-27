import classNames from 'classnames';

import styles from './EmptyText.module.css';

type Props = {
  text: string;
  padded?: boolean;
}

export const EmptyText = ({ text, padded }: Props) => {
  const className = classNames(styles.empty_text, {
    [styles.empty_text__padded]: padded,
  });

  return (
    <div className={className}>
      {text}
    </div>
  );
};
