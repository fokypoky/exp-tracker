'use client';

import { Control, Controller } from 'react-hook-form';
import Select from 'react-select';

import { Label } from '@components';
import { FONT_SIZES_MAPPING, SIZES_MAPPING } from '@constants';
import { ComponentSize, Option } from '@types';

import { DropdownIndicator } from './components';
import styles from './Select.module.css';

type Props = {
	control?: Control<any>;
	name?: string;
	error?: string;
	options?: Option<any>[];
	value?: Option<any>;
	disabled?: boolean;
	placeholder?: string;
	label?: string;
	labelGray?: boolean;
	menuPlacement?: 'top' | 'bottom';
	size?: ComponentSize;

	onChange?(value: Option<any>): void;
}

export const SelectComponent = (props: Props) => {
	const controlled = !!props.control && !!props.name;

	return controlled ? <ControlledSelect {...props} /> : <CustomSelect {...props} />;
};


const ControlledSelect = ({ onChange, value, ...props }: Props) => {
	return (
		<Controller
			name={props.name!}
			control={props.control!}
			render={(({ field, fieldState }) => {
				return (
					<CustomSelect
						{...props}
						error={fieldState.error?.message}
						onChange={(val) => {
							field.onChange(val.value);
							onChange && onChange(val);
						}}
						value={props.options?.find((opt) => opt.value === field.value)}
					/>
				);
			})}
		/>
	);
};

const CustomSelect = ({ menuPlacement = 'bottom', size = 'm', ...props }: Props) => {
	return (
		<div className={styles.select_container}>
			{props.label && (
				<Label
					size={size}
					gray={props.labelGray}
					text={props.label}
				/>
			)}
			<Select
				instanceId={'react-select'} // Для Next.js обязателен
				options={props.options}
				value={props.value}
				onChange={(value) => {
					value && props.onChange && props.onChange(value);
				}}
				menuPlacement={menuPlacement}
				isDisabled={props.disabled}
				placeholder={props.placeholder || 'Выберите'}
				noOptionsMessage={() => (
					<div className={styles.select__no_options}>
						<Label text="Нет доступных опций" gray />
					</div>
				)}
				components={{
					DropdownIndicator: (props) => <DropdownIndicator {...props} />,
					IndicatorSeparator: () => null,
				}}
				styles={{
					control: (base, state) => ({
						...base,
						border: '1px solid var(--text-secondary)',
						borderRadius: '8px',
						boxShadow: 'none', // !!! ВАЖНО ЧТОБЫ УБРАТЬ СИНЮЮ ОБВОДКУ КОГДА МЕНЮ ОТКРЫТО
						'&:hover': {
							borderColor: 'var(--text-primary)',
						},
						borderColor: state.isFocused ? '#000000' : 'var(--text-primary)',
						outline: 'none',
						cursor: 'pointer',
						height: `${SIZES_MAPPING[size || 'm']}px`,
						fontSize: `${FONT_SIZES_MAPPING[size]}`,
					}),
					option: (base, state) => ({
						...base,
						cursor: 'pointer',
						'&:hover': {
							backgroundColor: state.isSelected ? 'var(--text-primary)' : '#ededed',
						},
						backgroundColor: state.isSelected ? 'var(--text-primary)' : base.backgroundColor,
						color: state.isSelected ? 'white' : 'var(--text-primary)',

					}),
				}}
			/>
		</div>
	);
};
