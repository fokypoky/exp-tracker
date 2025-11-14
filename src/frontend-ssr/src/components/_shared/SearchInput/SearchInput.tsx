import { useForm } from 'react-hook-form';

import { Input } from '@components';
import { SearchFormType } from '@types';

type Props = {
  onSearch(search: string): void;
}

export const SearchInput = ({ onSearch }: Props) => {
  const { control } = useForm<SearchFormType>({
    defaultValues: {
      search: '',
    },
  });

  return (
    <Input
      name="search"
      control={control}
      placeholder="Поиск"
    />
  );
};
