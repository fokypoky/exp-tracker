import classNames from 'classnames';
import { Metadata } from 'next';
import { Geist } from 'next/font/google';
import '../globals.css';

import { Navbar, Notification } from '@components';
import { Providers } from '@providers';

import styles from './layout.module.css';


const geistSans = Geist({
  subsets: ['latin', 'cyrillic'],
  variable: '--font-geist-sans',
  display: 'swap',
});

export const metadata: Metadata = {
  title: 'ExpTracker',
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="ru">
      <body className={classNames(geistSans.className, styles['body__container'])}>
        <Providers>
          <Navbar />
          {children}
          <Notification placement="bottom-right" />
        </Providers>
      </body>
    </html>
  );
}
