import { APP_ROUTES } from '@constants';

import { MenuItem, Footer } from './components';
import styles from './LayoutMenu.module.css';

export const LayoutMenu = () => {
  return (
    <div className={styles['layout_menu']}>
      <MenuItem link={APP_ROUTES.profile} text="Обзор" />
      <MenuItem link={APP_ROUTES.transactions} text="Транзакции" />
      <MenuItem link={APP_ROUTES.categories} text="Категории" />
      <MenuItem link={APP_ROUTES.groups} text="Группы" />
      <Footer className={styles.footer} />
    </div>
  );
};
