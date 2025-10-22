'use client';

import React from 'react';

import { NotifierProvider } from './notifier';

type Props = {
  children: React.ReactNode;
}

export const Providers = ({ children }: Props) => {
  return (
    <NotifierProvider>
      {children}
    </NotifierProvider>
  );
};
