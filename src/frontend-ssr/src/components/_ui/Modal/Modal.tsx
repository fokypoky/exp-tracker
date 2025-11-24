'use client';

import classNames from 'classnames';
import { useEffect, useRef } from 'react';
import { createPortal } from 'react-dom';

import { Button, ButtonColor, CloseIcon } from '@components';
import { MODAL_CONTAINER_ID } from '@constants';
import { ComponentSize } from '@types';

import styles from './Modal.module.css';

type Props = {
  children: React.ReactNode;
  hideButtons?: boolean;
  size?: ComponentSize;
  title?: string | React.ReactNode;

  onClose(): void;
  onApprove?(): void;
}

export const Modal = (props: Props) => {
  return (
    createPortal(
      <ModalComponent {...props} />,
      document.querySelector<HTMLElement>(`#${MODAL_CONTAINER_ID}`)!,
    )
  );
};

const ModalComponent = ({ children, size = 'm', title, hideButtons, onApprove, onClose }: Props) => {
  const contentRef = useRef<HTMLDivElement>(null);

  const className = classNames(styles.modal_content, {
    [styles.modal_content__s]: size === 's',
    [styles.modal_content__m]: size === 'm',
    [styles.modal_content__l]: size === 'l',
  });

  useEffect(() => {
    const handleEsc = (event: KeyboardEvent) => {
      if (event.key === 'Escape') {
        onClose && onClose();
      }
    };

    window.addEventListener('keydown', handleEsc);
    
    return () => window.removeEventListener('keydown', handleEsc);
  }, []);

  const onClickAway = (event: React.MouseEvent<HTMLDivElement>) => {
    if (!contentRef.current) return;
    if (!contentRef.current.contains(event.target as Node)) {
      onClose();
    }
  };

  return (
    <div
      className={styles.modal_container}
      onClick={onClickAway}
    >
      <div className={className} ref={contentRef}>
        <div className={styles.modal_header}>
          <span className={styles.modal_header__text}>
            {title || ''}
          </span>
          <div
            className={styles.modal_header__close_btn}
            onClick={() => onClose()}
          >
            <CloseIcon color="currentColor" />
          </div>
        </div>
        {children}
        {!hideButtons && (
          <div className={styles.modal_buttons}>
            <Button
              onClick={() => {
                onClose && onClose();
              }}
              color={ButtonColor.secondary}
            >
              Отменить
            </Button>
            <Button
              onClick={() => {
                onApprove && onApprove();
              }}
              color={ButtonColor.primary}
            >
              Подтвердить
            </Button>
          </div>
        )}
      </div>
    </div>
  );
};
