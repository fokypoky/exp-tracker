import { Button, ButtonColor, Card, CloseIcon, EmptyText } from '@components';
import { CardListItem } from '@types';

import styles from './CardList.module.css';

type Props = {
  items: CardListItem[];
  emptyText?: string;
}

export const CardList = ({ items, emptyText }: Props) => {
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
                <div
                  className={styles.card_header__close_btn}
                  onClick={() => item.onDelete!()}
                >
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
      ))}
      {items.length === 0 && emptyText && (
        <EmptyText text={emptyText} />
      )}
    </div>
  );
};
