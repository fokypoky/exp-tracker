import { Button, Modal } from "antd";

type Props = {
  title: string;
  warning: string;
  isOpen: boolean;

  onApprove: (() => void) | (() => Promise<void>)
  onClose: () => void;
}

export const ApproveModal = ({ title, warning, isOpen, onApprove, onClose }: Props) => {
  return (
    <Modal
      title={title}
      open={isOpen}
      onOk={() => onApprove()}
      onCancel={() => onClose()}
      centered
      footer={[
        <Button key="back" onClick={onClose}>
          Отменить
        </Button>,
        <Button key="submit" type="primary" onClick={() => onApprove()}>
          Подтвердить
        </Button>
      ]}
    >
      {warning}
    </Modal>
  );
}