import { UseFormReturn } from 'react-hook-form';

import { TransactionCategory, TransactionIntervalStrategy, TransactionIntervalType, TransactionType } from '@api';
import { Input, RadioButtonGroup, Select } from '@components';
import { INTERVAL_STRATEGY_OPTIONS, TRANSACTION_INTERVAL_TYPE_MAPPING, TRANSACTION_TYPE_MAPPING } from '@constants';
import { Option, RadioButtonGroupConfig, TransactionFormType } from '@types';

import styles from './Properties.module.css';

type Props = {
  form: UseFormReturn<TransactionFormType>;
  categories: TransactionCategory[];
  editMode: boolean;
}

export const Properties = (props: Props) => {
  return props.editMode ? <EditProperties {...props} /> : <ReadProperties {...props} />;
};

const ReadProperties = ({}: Props) => {
  return (
    <></>
  );
};

const EditProperties = ({ form, categories }: Props) => {
  const typeConfig: RadioButtonGroupConfig = {
    form,
    fieldName: 'type',
    items: [
      { value: TransactionType.withdraw,  label: TRANSACTION_TYPE_MAPPING[TransactionType.withdraw] },
      { value: TransactionType.deposit, label: TRANSACTION_TYPE_MAPPING[TransactionType.deposit] },
    ],
  };

  const intervalTypeConfig: RadioButtonGroupConfig = {
    form,
    fieldName: 'intervalType',
    items: [
      {
        value: TransactionIntervalType.single,
        label: TRANSACTION_INTERVAL_TYPE_MAPPING[TransactionIntervalType.single],
      },
      {
        value: TransactionIntervalType.repeatable,
        label: TRANSACTION_INTERVAL_TYPE_MAPPING[TransactionIntervalType.repeatable],
      },
    ],
  };

  const options: Option<TransactionCategory | undefined>[] = [
    { label: 'Не выбрана', value: undefined },
    ...categories.map((category): Option<TransactionCategory> => ({
      label: category.name,
      value: {
        id: category.id,
        name: category.name,
      },
    })),
  ];

  return (
    <div className={styles.properties_container}>
      <Input
        name="cost"
        control={form.control}
        type="number"
        label="Стоимость"
      />
      <RadioButtonGroup
        label="Тип транзакции"
        config={typeConfig}
      />
      <RadioButtonGroup
        label="Интервальный тип транзакции"
        config={intervalTypeConfig}
        onChange={(value: string) => {
          if (value !== TransactionIntervalType.single) return;

          form.setValue('intervalStrategy', undefined);
          form.setValue('dayOfMonth', undefined);
        }}
      />
      {form.watch('intervalType') === TransactionIntervalType.repeatable && (
        <Select
          control={form.control}
          name="intervalStrategy"
          label="Стратегия рассчета"
          placeholder="Выберите стратегию рассчета"
          options={INTERVAL_STRATEGY_OPTIONS}
        />
      )}
      {form.watch('intervalStrategy') === TransactionIntervalStrategy.specifiedDayOfMonth && (
        <Input
          name="dayOfMonth"
          control={form.control}
          type="number"
          label="День месяца"
        />
      )}
      <Select
        label="Категория"
        placeholder="Выберите категорию"
        control={form.control}
        name="category"
        options={options}
      />
    </div>
  );
};
