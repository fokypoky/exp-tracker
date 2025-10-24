import { useContext } from 'react';

import { NotifierContext } from '@providers';
import { Notification } from '@types';

type NotificationOverride = Omit<Notification, 'type'>;

export const useNotifier = () => {
  const { notifications, notify, remove } = useContext(NotifierContext)!;

  const success = (notification: NotificationOverride) => notify({ ...notification, type: 'success' });
  const error = (notification: NotificationOverride) => notify({ ...notification, type: 'error', title: `Ошибка. ${notification.title}` });
  const warning = (notification: NotificationOverride) => notify({ ...notification, type: 'warning' });
  const info = (notification: NotificationOverride) => notify({ ...notification, type: 'info' });

  return { success, error, warning, info, notifications, remove };
};
