import { ComponentDirection } from '@types';

export const getRotatedStyle = (direction: ComponentDirection): string => {
  switch (direction) {
    case 'up':
      return 'rotate(0deg)';
    case 'right':
      return 'rotate(90deg)';
    case 'down':
      return 'rotate(180deg)';
    case 'left':
      return 'rotate(270deg)';
  }
};
