import moment from 'moment-timezone';

import { DATE_TIME_FORMAT } from '@constants';

export const toLocalDate = (date: string): string => {
  return moment.utc(date).local().format(DATE_TIME_FORMAT);
};

export const toApiDate = (date: string): string => {
  throw new Error('not implemented');
};
