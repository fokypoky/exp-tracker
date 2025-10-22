'use client';

import React, { createContext, useCallback, useState } from 'react';
import { v4 as uuidv4 } from 'uuid';

import { NOTIFIER_DEFAULT_DURATION } from '@constants';
import { Notification } from '@types';

export type NotifierContextType = {
  notifications: Notification[],
  notify(notification: Omit<Notification, 'id'>): void;
  remove(id: string): void;
}

export const NotifierContext = createContext<NotifierContextType | undefined>(undefined);

type Props = {
  children: React.ReactNode;
}

export const NotifierProvider = ({ children }: Props) => {
  const [notifications, setNotifications] = useState<Notification[]>([]);

  const notify = useCallback((notification: Notification) => {
    const newNotification = {
      ...notification,
      id: notification.id ?? uuidv4(),
      duration: notification.duration ?? NOTIFIER_DEFAULT_DURATION,
    };

    setNotifications((prev) => [...prev, newNotification]);

    setTimeout(() => remove(newNotification.id), newNotification.duration);
  }, []);

  const remove = useCallback((id: string) => {
    setNotifications((prev) => prev.filter((n) => n.id !== id));
  }, []);

  return (
    <NotifierContext.Provider value={{ notifications, notify, remove }}>
      {children}
    </NotifierContext.Provider>
  );
};
