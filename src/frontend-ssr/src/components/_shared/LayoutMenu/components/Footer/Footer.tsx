'use client';

import classNames from 'classnames';

import styles from './Footer.module.css';

type Props = {
  className?: string;
}

export const Footer = ({ className }: Props) => {
  const componentClassName = classNames(styles.footer, className);

  // TODO: получить пользователя из контекста и отрисовать его

  return (
    <div className={componentClassName}>
      <div></div>

    </div>
  );
};
