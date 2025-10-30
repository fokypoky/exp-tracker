import { CategoryIcon, CreditCardIcon, DashboardIcon, GroupIcon } from '@components';
import { APP_ROUTES } from '@constants';

export const getRouteIcon = (link: string, active: boolean): React.ReactNode  => {
  const color = active ? 'white' : '#757575';

  switch (link) {
    case APP_ROUTES.profile:
      return <DashboardIcon color={color} />;
    case APP_ROUTES.transactions:
      return <CreditCardIcon color={color} />;
    case APP_ROUTES.categories:
      return <CategoryIcon color={color} />;
    case APP_ROUTES.groups:
      return <GroupIcon color={color} />;
    default:
      return <></>;
  }
};
