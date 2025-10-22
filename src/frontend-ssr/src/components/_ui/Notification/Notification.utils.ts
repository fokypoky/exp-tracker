import { ComponentPlacement } from '@types';

import { Position } from './Notification.types';

export const calculatePosition = (placement: ComponentPlacement, element: HTMLDivElement | null): Position => {
  if (!element) return { top: 0, left: 0 };

  const top = 0;
  const left = 0;

  // TODO: зачем это???
  const { height } = element.getBoundingClientRect();

  switch (placement) {
    case 'bottom':
      break;
    case 'bottom-left':
      break;
    case 'bottom-right':
      break;
    case 'top':
      break;
    case 'top-right':
      break;
    case 'top-left':
      break;
    case 'left':
      break;
    case 'right':
      break;
  }

  return { top, left };
};
