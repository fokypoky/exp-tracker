import { APP_ROUTES } from '@constants';

import { MenuItem, Footer } from './components';
import styles from './LayoutMenu.module.css';
import { buildRoute } from './LayoutMenu.utils';

export const LayoutMenu = () => {
  return (
    <div className={styles['layout_menu']}>
      <MenuItem link={buildRoute(APP_ROUTES.profile)} text="Обзор" />
      <MenuItem link={buildRoute(APP_ROUTES.transactions)} text="Транзакции" />
      <MenuItem link={buildRoute(APP_ROUTES.categories)} text="Категории" />
      <MenuItem link={buildRoute(APP_ROUTES.groups)} text="Группы" />
      <Footer className={styles.footer} />
    </div>
  );
};
