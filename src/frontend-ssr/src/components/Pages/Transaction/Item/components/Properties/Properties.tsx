import { UseFormReturn } from 'react-hook-form';

import { Input } from '@components';
import { TransactionFormType } from '@types';

import styles from './Properties.module.css';

type Props = {
  form: UseFormReturn<TransactionFormType>;
  editMode: boolean;
}

export const Properties = (props: Props) => {
  return props.editMode ? <EditProperties {...props} /> : <ReadProperties {...props} />;
};

const ReadProperties = ({}: Props) => {
  return (
    <></>
  );
};

const EditProperties = ({ form }: Props) => {
  return (
    <div className={styles.properties_container}>
      <Input
        name="cost"
        control={form.control}
        type="number"
      />
    </div>
  );
};
