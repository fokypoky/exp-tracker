'use client';

import classNames from 'classnames';

import { useNotifier } from '@hooks';
import { ComponentPlacement } from '@types';

import { NotificationCard } from './components/NotificationCard';
import styles from './Notification.module.css';

type Props = {
  placement: ComponentPlacement;
}

export const Notification = ({ placement }: Props) => {
  const { notifications } = useNotifier();

  const className = classNames(styles.container, styles[`container_${placement}`]);

  return notifications.length > 0 ? (
    <div className={className}>
      {notifications.map((notification, index) => (
        <NotificationCard
          key={notification.id}
          id={notification.id || ''}
          index={index + 1}
          type={notification.type}
          title={notification.title}
          message={notification.message}
        />
      ))}
    </div>
  ) : <div></div>;
};
