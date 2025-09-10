import classNames from "classnames";
import styles from './Label.module.css';
import { ComponentSize } from "@constants";

type Props = {
  text: string;
  size?: ComponentSize;
  bold?: boolean;
  black?: boolean;
}

export const Label = ({ text, size = ComponentSize.s, bold, black }: Props) => {
  const className = classNames(styles.label, {
    [styles['label__bold']]: bold,
    [styles['label__size_s']]: size === ComponentSize.s,
    [styles['label__size_m']]: size === ComponentSize.m,
    [styles['label__size_l']]: size === ComponentSize.l,
    [styles['label__size_xl']]: size === ComponentSize.xl,
    [styles['label__black']]: black,
    [styles['label__gray']]: !black,
  });

  return (
    <span className={className}>
      {text}
    </span>
  );
}