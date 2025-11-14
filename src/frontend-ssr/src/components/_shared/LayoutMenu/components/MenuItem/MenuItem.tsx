'use client';

import classNames from 'classnames';
import Link from 'next/link';
import { usePathname } from 'next/navigation';

import { APP_ROUTES } from '@constants';

import styles from './MenuItem.module.css';
import { getRouteIcon } from './MenuItem.utils';

type Props = {
  link: string;
  text: string;
}

export const MenuItem = ({ link, text }: Props) => {
  const path = usePathname();
  const active = link === APP_ROUTES.overview ? path.endsWith(link) : path.includes(link);

  const className = classNames(styles['menu_item'], {
    [styles['menu_item__active']]: active,
  });

  return (
    <Link href={link} className={className}>
      {getRouteIcon(link, active)}
      {text}
    </Link>
  );
};
