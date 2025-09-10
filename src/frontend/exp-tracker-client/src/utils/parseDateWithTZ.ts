import * as moment from 'moment-timezone';

import { DATETIME_FORMAT } from '@constants';

export const parseDateWithTZ = (date: Date | null | undefined): string => {
	if (!date) return '';

	const tz = Intl.DateTimeFormat().resolvedOptions().timeZone;
	return moment.utc(date).tz(tz).format(DATETIME_FORMAT);
};
