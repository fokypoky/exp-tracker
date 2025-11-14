import { components } from 'react-select';

import { KeyboardArrowIcon } from '@components';

export const DropdownIndicator = (props: any) => {
	const isOpen = props.selectProps.menuIsOpen;

	return (
		<components.DropdownIndicator {...props}>
			{isOpen ? (
				<KeyboardArrowIcon direction="up" color="var(--text-secondary)" />
			) : (
				<KeyboardArrowIcon direction="down" color="var(--text-secondary)" />
			)}
		</components.DropdownIndicator>
	);
};
