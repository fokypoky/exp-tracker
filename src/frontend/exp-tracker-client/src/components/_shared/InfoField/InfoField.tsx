import { Label } from '@components';
import { ComponentSize } from '@constants';
import styles from './InfoField.module.css';

type Props = {
	label: string;
	text: string;
	size?: ComponentSize;
	boldLabel?: boolean;
};

export const InfoField = ({ label, text, size = ComponentSize.m, boldLabel }: Props) => {
	return (
		<div className={styles.info_container}>
			<Label text={label} size={size} bold={boldLabel} />
			<Label text={text} size={ComponentSize.m} black />
		</div>
	);
};
