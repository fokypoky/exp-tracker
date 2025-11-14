import { Dash, Label } from '@components';

type Props = {
  label: string;
  text: string;
}

export const InfoField = ({ label, text }: Props) => {
  return (
    <div>
      <Label text={label} size="m" gray />
      <span>{text || <Dash />}</span>
    </div>
  );
};
