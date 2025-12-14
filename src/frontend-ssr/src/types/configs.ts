import { UseFormReturn } from 'react-hook-form';

export type RadioButtonGroupConfig = {
  form: UseFormReturn<any>;
  fieldName: string;
  items: {
    label: string;
    value: unknown;
  }[]
}
