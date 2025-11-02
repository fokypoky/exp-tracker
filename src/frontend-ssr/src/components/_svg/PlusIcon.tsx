import { SIZES_MAPPING } from '@constants';
import { ComponentSize } from '@types';

type Props = {
  size?: ComponentSize;
  fill?: string;
}

export const PlusIcon = ({ size = 'm', fill = 'currentColor' }: Props) => {
  return (
    <svg
      fill={fill}
      height={SIZES_MAPPING[size]}
      viewBox="0 0 256 256"
      width={SIZES_MAPPING[size]}
      xmlns="http://www.w3.org/2000/svg"
    >
      <path 
        d="M224,128a8,8,0,0,1-8,8H136v80a8,8,0,0,1-16,0V136H40a8,8,0,0,1,0-16h80V40a8,8,0,0,1,16,0v80h80A8,8,0,0,1,224,128Z"
      />
    </svg>
  );
};
