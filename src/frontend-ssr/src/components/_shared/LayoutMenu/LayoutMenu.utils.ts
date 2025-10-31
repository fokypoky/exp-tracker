import { APP_ROUTES } from '@constants';

export const buildRoute = (link: string): string => {
  if (link === APP_ROUTES.profile) return link;

  return `${APP_ROUTES.profile}${link}`;
};
