import { TransactionIntervalStrategy } from '@api';
import { Option } from '@types';

export const DEFAULT_INTERVAL_STRATEGY_OPTION: Option<undefined> = {
  label: 'Не выбрана',
  value: undefined,
};

export const INTERVAL_STRATEGY_OPTIONS: Option<string | undefined>[] = [
  { ...DEFAULT_INTERVAL_STRATEGY_OPTION },
  { value: TransactionIntervalStrategy.firstDayOfMonth, label: 'Первый день месяца' },
  { value: TransactionIntervalStrategy.lastDayOfMonth, label: 'Последний день месяца' },
  { value: TransactionIntervalStrategy.specifiedDayOfMonth, label: 'Указанный день месяца' },
];
