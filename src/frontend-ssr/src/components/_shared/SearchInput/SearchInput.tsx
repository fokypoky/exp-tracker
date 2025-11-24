import { useForm } from 'react-hook-form';

import { Input } from '@components';
import { SearchFormType } from '@types';

type Props = {
  onSearch(search: string): void;
}

export const SearchInput = ({ onSearch }: Props) => {
  const { control, getValues } = useForm<SearchFormType>({
    defaultValues: {
      search: '',
    },
  });

  return (
    <Input
      name="search"
      control={control}
      placeholder="Поиск"
      onBlur={() => onSearch(getValues('search'))}
      onKeyDown={(e) => {
        e.key === 'Enter' && onSearch(getValues('search'));
      }}
    />
  );
};
