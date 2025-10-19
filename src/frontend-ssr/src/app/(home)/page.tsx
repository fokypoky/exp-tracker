import { Block } from './components';
import { Features } from './components';
import styles from './page.module.css';

export default function Home() {
  return (
    <div className={styles.container}>
      <Block />
      <Features />
    </div>
  );
}
