import styles from './Block.module.css';

export const Block = () => {
  return (
    <h1 className={styles['animated-text']}>
      <span className={styles.word}>Управляй</span>
      <span className={styles.word}>финансами</span>
      <span className={styles.word}>в одном</span>
      <span className={styles.word}>месте</span>
    </h1>
  );
};