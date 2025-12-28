import { ComponentPlacement } from '@types';

import { Position } from './Popper.types';

export const isContentClick = (
  ref: React.RefObject<HTMLDivElement | null> | HTMLElement,
  event: MouseEvent,
): boolean => {
  const element = ref instanceof HTMLElement ? ref : ref.current;

  if (!element) return false;

  const { top, left, height, width } = element.getBoundingClientRect();
  const { clientX: x, clientY: y } = event;

  const includesX = x >= left && x <= left + width;
  const includesY = y >= top && y <= top + height;

  return includesX && includesY;
};

export const calculatePosition = (
  anchorEl: HTMLElement,
  contentRef: React.RefObject<HTMLDivElement | null>,
  placement: 'top' | 'left' | 'right' | 'bottom',
): Position => {
  const {
    top: anchorT,
    left: anchorL,
    right: anchorR,
    height: anchorH,
    width: anchorW,
  } = anchorEl.getBoundingClientRect();
  const {
    height: contentH,
    width: contentW,
  } = contentRef.current!.getBoundingClientRect();

  let top = 0;
  let left = 0;

  switch (placement) {
    case 'top':
      top = anchorT - contentH;
      left = anchorL + (anchorW / 2) - (contentW / 2);
      break;
    case 'bottom':
      top = anchorT + anchorH;
      left = anchorL + (anchorW / 2) - (contentW / 2);
      break;
    case 'left':
      top = anchorT + (anchorH / 2) - (contentH / 2);
      left = anchorL - contentW;
      break;
    case 'right':
      top = anchorT + (anchorH / 2) - (contentH / 2);
      left = anchorR;
      break;
    default:
      throw new Error('Unknown placement');
  }

  return { top, left };
};
