export type LayoutProps = Readonly<{
  children: React.ReactNode
}>;

export type ComponentSize = 's' | 'm' | 'l';
export type ComponentPlacement = 'left' | 'top-left' | 'bottom-left' | 'top' | 'right' | 'top-right' | 'bottom-right' | 'bottom';

export type User = {
  login: string;
  guid: string;
};
