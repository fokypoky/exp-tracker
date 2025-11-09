export type LayoutProps = Readonly<{
  children: React.ReactNode
}>;

export type ComponentSize = 's' | 'm' | 'l';
export type ComponentPlacement = 'left' | 'top-left' | 'bottom-left' | 'top' | 'right' | 'top-right' | 'bottom-right' | 'bottom';
export type ComponentDirection = 'up' | 'right' | 'down' | 'left';

export type User = {
  login: string;
  guid: string;
};

export type Filters = {
  searchString?: string;
  limit?: number;
  offset?: number;
}
