'use client';

import React, { useLayoutEffect, useRef, useState } from 'react';

import { useNotifier } from '@hooks';
import { ComponentPlacement } from '@types';

import { NotificationCard } from './components/NotificationCard';
import styles from './Notification.module.css';
import { Position } from './Notification.types';
import { calculatePosition } from './Notification.utils';



type Props = {
  placement: ComponentPlacement;
}

export const Notification = ({ placement }: Props) => {
  const ref = useRef<HTMLDivElement>(null);

  const [position, setPosition] = useState<Position>({ top: 0, left: 0 });

  const { notifications } = useNotifier();

  useLayoutEffect(() => {
    setPosition(calculatePosition(placement, ref.current));
  }, [placement]);

  return notifications.length > 0 ? (
    <div className={styles.container} ref={ref} style={{ top: position.top, left: position.left }}>
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
