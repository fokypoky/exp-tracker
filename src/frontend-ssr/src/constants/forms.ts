import moment from 'moment-timezone';

import { TransactionIntervalType, TransactionType } from '@api';
import { UTC_DATE_TIME_FORMAT } from '@constants';
import { TransactionFormType } from '@types';

export const TRANSACTION_FORM_DEFAULTS = (): TransactionFormType => ({
  cost: 0,
  type: TransactionType.withdraw,
  intervalType: TransactionIntervalType.single,
  date: moment().utc().format(UTC_DATE_TIME_FORMAT),
});
