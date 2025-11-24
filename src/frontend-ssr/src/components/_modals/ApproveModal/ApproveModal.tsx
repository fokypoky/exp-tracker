import { Modal } from '@components';

type Props = {
  title: string | React.ReactNode;
  message: string;

  onApprove(): void;
  onCancel(): void;
}

export const ApproveModal = ({ title, message, onApprove, onCancel }: Props) => {
  return (
    <Modal
      onClose={onCancel}
      onApprove={onApprove}
      size="s"
      title={title}
    >
      <>{message}</>
    </Modal>
  );
};
