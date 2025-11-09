import { SIZES_MAPPING } from '@constants';
import { ComponentDirection, ComponentSize } from '@types';
import { getRotatedStyle } from '@utils';

type Props = {
  color?: string;
  direction?: ComponentDirection;
  size?: ComponentSize | number;
}

export const KeyboardArrowIcon = ({ color = 'black', direction = 'up', size = 'm' }: Props) => {
  const iconSize = typeof size === 'string' ? SIZES_MAPPING[size] : size;
  return (
    <svg
      xmlns="http://www.w3.org/2000/svg"
      height={iconSize}
      viewBox="0 -960 960 960"
      width={iconSize}
      fill={color}
      style={{ transform: getRotatedStyle(direction) }}
    >
      <path
        d="M480-528 296-344l-56-56 240-240 240 240-56 56-184-184Z"
      />
    </svg>
  );
};
