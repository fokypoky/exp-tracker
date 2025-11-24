export type CardListItem = {
  title: string;
  description: string | React.ReactNode;

  onOpen(): void;
  onDelete?(): void;
}
