import { TransactionPage as Page } from '@components';

export default function TransactionPage({ params }) {
  const { id } = params;

  return (
    <Page id={id} />
  );
}
