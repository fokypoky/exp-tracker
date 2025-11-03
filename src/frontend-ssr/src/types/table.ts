export type TableRow = Map<string, string | React.ReactNode>;

export type TableHeader = {
  key: string;
  name: string;
  width?: number;

  onClick?(): void;
}
