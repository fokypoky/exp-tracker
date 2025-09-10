import { Label } from '@components';
import styles from './Block.module.css';
import { ComponentSize } from '@constants';

type Props = {
	children: React.ReactNode | React.ReactNode[];
	title?: string;
};

export const Block = ({ children, title }: Props) => {
	return (
		<div className={styles.block}>
			{title && <Label text={title} bold size={ComponentSize.l} />}
			{children}
		</div>
	);
};
