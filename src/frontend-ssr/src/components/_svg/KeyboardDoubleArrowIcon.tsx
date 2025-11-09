import { SIZES_MAPPING } from '@constants';
import { ComponentDirection, ComponentSize } from '@types';
import { getRotatedStyle } from '@utils';

type Props = {
  color?: string;
  direction?: ComponentDirection;
  size?: ComponentSize | number;
}

export const KeyboardDoubleArrowIcon = ({ color = 'black', direction = 'up', size = 'm' }: Props) => {
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
        d="m296-224-56-56 240-240 240 240-56 56-184-183-184 183Zm0-240-56-56 240-240 240 240-56 56-184-183-184 183Z"
      />
    </svg>
  );
};
