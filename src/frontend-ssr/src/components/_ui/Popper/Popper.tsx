'use client';

import { useEffect, useLayoutEffect, useRef, useState } from 'react';
import { createPortal } from 'react-dom';

import { POPPER_CONTAINER_ID } from '@constants';

import styles from './Popper.module.css';
import { Position } from './Popper.types';
import { calculatePosition, isContentClick } from './Popper.utils';

type Props = {
  children: React.ReactNode;
  anchorEl: HTMLElement;
  placement?: 'top' | 'left' | 'right' | 'bottom';
  offset?: number;

  onClose(): void;
}

export const Popper = ({ children, placement = 'top', anchorEl, offset = 8, onClose }: Props) => {
  const contentRef = useRef<HTMLDivElement | null>(null);
  const [position, setPosition] = useState<Position>({ top: 0, left: 0 });

  const handleClickOutside = (event: MouseEvent) => {
    const isTriggerRefClick = isContentClick(anchorEl, event);
    const isContentRefClick = isContentClick(contentRef, event);

    !isTriggerRefClick && !isContentRefClick && onClose();
  };

  const handleKeyClick = (event: KeyboardEvent) => {
    event.key === 'Enter' && onClose();
  };

  useLayoutEffect(() => {
    if (!anchorEl || !contentRef.current) return;

    const newPosition = calculatePosition(anchorEl, contentRef, placement );

    setPosition(newPosition);
  }, []);

  useEffect(() => {
    window.addEventListener('mousedown', handleClickOutside);
    window.addEventListener('keydown', handleKeyClick);

    return () => {
      window.removeEventListener('mousedown', handleClickOutside);
      window.removeEventListener('keydown', handleKeyClick);
    };
  }, []);
  
  return createPortal(
    <div
      ref={contentRef}
      className={styles.popper}
      style={{ top: position.top, left: position.left }}
    >
      {children}
    </div>,
    document.getElementById(POPPER_CONTAINER_ID)!,
  );
};
