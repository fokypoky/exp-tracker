import { Button, ButtonColor } from '@components';

import styles from './PageActions.module.css';

type Props = {
  editMode: boolean;

  onApply?(): void;
  onCancel?(): void;
  onChangeMode?(): void;
}

export const PageActions = ({ editMode, onApply, onCancel, onChangeMode }: Props) => {
  return (
    <div className={styles.page_actions}>
      {editMode ? (
        <>
          <Button
            onClick={() => {
              onCancel && onCancel();
            }}
            color={ButtonColor.secondary}
          >
            Отменить
          </Button>
          <Button
            onClick={() => {
              onApply && onApply();
            }}
            color={ButtonColor.primary}
          >
            Сохранить
          </Button>
        </>
      ) : (
        <Button
          onClick={() => {
            onChangeMode && onChangeMode();
          }}
          color={ButtonColor.primary}
        >
          Редактировать
        </Button>
      )}
    </div>
  );
};
