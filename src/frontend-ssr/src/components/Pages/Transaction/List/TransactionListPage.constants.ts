import { TableHeader } from '@types';

export enum HeaderCodes {
  transactionType = 'transactionType',
  date = 'date',
  cost = 'cost',
  category = 'category',
  description = 'description',
};

export const HEADERS_TABLE: TableHeader[] = [
  { key: HeaderCodes.transactionType, name: 'Тип' },
  { key: HeaderCodes.date, name: 'Дата' },
  { key: HeaderCodes.cost, name: 'Количество' },
  { key: HeaderCodes.category, name: 'Категория' },
  { key: HeaderCodes.description, name: 'Описание' },
];
