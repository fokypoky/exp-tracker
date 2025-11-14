import { Card, CoinIcon, PeopleGroupIcon, PlusIcon } from '@components';
import { FEATURES_CONTAINER_ID } from '@constants';

import styles from './Features.module.css';

export const Features = () => {
  return (
    <div className={styles.features} id={FEATURES_CONTAINER_ID}>
      <h1>Наши возможности</h1>
      <h2 className={styles['features__subtitle']}>
        ExpTracker предоставит мощный набор инструментов для управления и наблюдения
          <br />
          за Вашими финансами
      </h2>
      <div className={styles.cards}>
        <Card
          header="Пользовательские категории"
          className={styles.card}
          headIcon={<PlusIcon />}
        >
          <span>
            Создавайте персональные категории для более гибкого управления финансами
          </span>
        </Card>
        <Card
          header="Гибкое управление доходами и расходами"
          className={styles.card}
          headIcon={<CoinIcon />}
        >
          <span>
            Легко добавляйте, удаляйте и изменяйте категории
            <br />
            Получите детальный анализ каждой своей категории
          </span>
        </Card>
        <Card
          header="Группы"
          className={styles.card}
          headIcon={<PeopleGroupIcon />}
        >
          <span>
            Объединяйтесь с другими пользователями в группы для эффективного управления совместными финансами
          </span>
        </Card>
      </div>
    </div>
  );
};
