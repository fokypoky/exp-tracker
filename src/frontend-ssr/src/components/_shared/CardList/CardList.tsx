import { Button, ButtonColor, Card, CloseIcon } from '@components';
import { CardListItem } from '@types';

import styles from './CardList.module.css';

type Props = {
	items: CardListItem[];
}

export const CardList = ({ items }: Props) => {
	return (
		<div className={styles.card_list}>
			{items.map((item, index) => (
				<Card
					key={index}
					className={styles.card}
				>
					<div className={styles.card__content}>
						<div className={styles.card_header__container}>
							<span>{item.title}</span>
							{item.onDelete && (
								<div className={styles.card_header__close_btn}>
									<CloseIcon color="currentColor" />
								</div>
							)}
						</div>
						<div className={styles.card__description}>
							{item.description}
						</div>
						<div className={styles.card__action}>
							<Button
								color={ButtonColor.secondary}
								className={styles.action_button}
								onClick={() => item.onOpen()}
							>
								Открыть
							</Button>
						</div>
					</div>
				</Card>
			)) }
		</div>
	);
};
