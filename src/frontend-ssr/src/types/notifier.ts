export type NotificationType = 'success' | 'info' | 'warning' | 'error';

export type Notification = {
  id?: string;
  title: string;
  type: NotificationType;
  message?: string | React.ReactNode;
  duration?: number;
}
