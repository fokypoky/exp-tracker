import {Metadata} from "next";
import {Geist} from "next/font/google";
import classNames from "classnames";

import styles from './layout.module.css';
import {ProtectedProviders} from "@providers";

const geistSans = Geist({
  subsets: ['latin', 'cyrillic'],
  variable: '--font-geist-sans',
  display: 'swap',
})

export const metadata: Metadata = {
  title: 'ExpTracker. Управление финансами'
}

export default function RootLayout({children}: Readonly<{children: React.ReactNode}>) {
  return (
    <html lang="ru">
      <body className={classNames(geistSans.className, styles['body__container'])}>
        <ProtectedProviders>
          {children}
        </ProtectedProviders>
      </body>
    </html>
  )
}