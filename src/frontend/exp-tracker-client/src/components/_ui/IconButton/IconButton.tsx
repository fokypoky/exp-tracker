import { Tooltip } from 'antd';
import styles from './IconButton.module.css';

type Props = {
  icon: React.ReactNode;
  tooltip?: string;

  onClick?: (event: React.MouseEvent<HTMLDivElement, MouseEvent>) => void;
}

export const IconButton = ({ icon, tooltip, onClick }: Props) => {
  return (
    <div onClick={onClick} className={styles['icon_button']}>
      <Tooltip title={tooltip}>
        {icon}
      </Tooltip>
    </div>
  )
}