export type TableRowFnType = (() => void);

export type TableRow = Map<string, string | React.ReactNode | TableRowFnType>;

export type TableHeader = {
  key: string;
  name: string;
  width?: number;

  onClick?(): void;
}
