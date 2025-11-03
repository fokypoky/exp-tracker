import { TableHeader } from '@types';

export enum HEADER_CODES {
  name = 'name',
  description = 'description',
}

export const HEADERS_TABLE: TableHeader[] = [
  { key: HEADER_CODES.name, name: 'Название', width: 250 },
  { key: HEADER_CODES.description, name: 'Описание' },
];
