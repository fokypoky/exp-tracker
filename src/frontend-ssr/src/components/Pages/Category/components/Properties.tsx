'use client';

import { UseFormReturn } from 'react-hook-form';

import { InfoField, Input } from '@components';
import { CategoryFormType } from '@types';

import styles from './Properties.module.css';

type Props = {
  editMode: boolean;
  form: UseFormReturn<CategoryFormType>;
}

export const Properties = (props: Props) => {
  return props.editMode ? <EditProperties {...props} /> : <ReadProperties {...props} />;
};

const ReadProperties = ({ form }: Props) => {
  return (
    <>
      <InfoField
        label="Название"
        text={form.watch('name')}
      />
      <InfoField
        label="Описание"
        text={form.watch('description') || ''}
      />
    </>
  );
};

const EditProperties = ({ form }: Props) => {
  return (
    <div className={styles.container}>
      <Input
        name="name"
        control={form.control}
        label="Название"
        placeholder="Введите название"
      />
      <Input
        name="description"
        control={form.control}
        label="Описание"
        placeholder="Введите описание"
      />
    </div>
  );
};
