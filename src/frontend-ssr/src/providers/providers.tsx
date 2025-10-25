'use client';

import React from 'react';

import { NotifierProvider } from './notifier';
import {AuthProvider} from "./auth";

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

export const ProtectedProviders = ({ children }: Props) => {
  return (
    <AuthProvider>
      <Providers>
        {children}
      </Providers>
    </AuthProvider>
  )
}