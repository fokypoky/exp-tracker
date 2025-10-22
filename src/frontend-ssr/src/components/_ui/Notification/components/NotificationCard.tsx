import { CheckCircleIcon, CloseIcon, ErrorIcon, InfoIcon, WarningIcon } from '@components';
import { useNotifier } from '@hooks';
import { NotificationType } from '@types';

import styles from './NotificationCard.module.css';

type Props = {
  id: string;
  type: NotificationType;
  title: string;
  message: string | React.ReactNode;
  index: number;
}

const icons: Record<NotificationType, React.ReactNode> = {
  ['success']: <CheckCircleIcon  />,
  ['info']: <InfoIcon />,
  ['warning']: <WarningIcon />,
  ['error']: <ErrorIcon />,
};

export const NotificationCard = ({ id, type, title, message, index }: Props) => {
  const { remove } = useNotifier();

  const onCloseClick = () => remove(id);

  return (
    <div style={{ zIndex: 100 + index }} className={styles.card}>
      <div className={styles.header}>
        {icons[type]}
        <span className={styles.header__text}>{title}</span>
        <div className={styles.close_icon} onClick={() => onCloseClick()}>
          <CloseIcon />
        </div>
      </div>
      <div className={styles.body}>
        {message}
      </div>
    </div>
  );
};
