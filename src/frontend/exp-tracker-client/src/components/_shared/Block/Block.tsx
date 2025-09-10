import { Label } from '@components';
import styles from './Block.module.css';
import { ComponentSize } from '@constants';

type Props = {
	children: React.ReactNode | React.ReactNode[];
	title?: string;
	actions?: React.ReactNode;
};

export const Block = ({ children, title, actions }: Props) => {
	return (
		<div className={styles.block}>
			<div className={styles.header}>
				{title && <Label text={title} bold size={ComponentSize.l} />}
				{actions && (
					<div className={styles.actions}>{actions}</div>
				)}
			</div>
			{children}
		</div>
	);
};
