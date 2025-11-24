import classNames from 'classnames';
import { Metadata } from 'next';
import { Geist } from 'next/font/google';
import '../globals.css';

import { LayoutMenu, Logo, Notification } from '@components';
import { APP_ROUTES, MODAL_CONTAINER_ID } from '@constants';
import { ProtectedProviders } from '@providers';

import styles from './layout.module.css';

const geistSans = Geist({
  subsets: ['latin', 'cyrillic'],
  variable: '--font-geist-sans',
  display: 'swap',
});

export const metadata: Metadata = {
  title: 'ExpTracker. Управление финансами',
};

export default function RootLayout({ children }: Readonly<{children: React.ReactNode}>) {
  return (
    <html lang="ru">
      <body className={classNames(geistSans.className, styles.body)}>
        <ProtectedProviders>
          <div id={MODAL_CONTAINER_ID} />
          <div className={styles.nav}>
            <Logo url={APP_ROUTES.overview} />
          </div>
          <div className={styles['body__container']}>
            <div className={styles.menu}>
              <LayoutMenu />
            </div>
            {children}
          </div>
          <Notification placement="bottom-right" />
        </ProtectedProviders>
      </body>
    </html>
  );
}
