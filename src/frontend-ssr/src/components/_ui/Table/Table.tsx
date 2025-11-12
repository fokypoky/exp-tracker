import { TableHeader, TableRow } from '@types';

import styles from './Table.module.css';

type Props = {
  headers: TableHeader[];
  rows: TableRow[];
}

export const Table = ({ headers, rows }: Props) => {
  return (
    <table className={styles.table}>
      <thead>
        <tr>
          {headers.map((header) => (
            <th
              key={header.key}
              className={styles['table__header']}
              style={{ width: header.width }}
            >
              {header.name}
            </th>
          ))}
        </tr>
      </thead>
      <tbody>
        {rows.map((row, rowIndex) => (
          <tr key={rowIndex} className={styles['table__row']}>
            {headers.map((header) => (
              <td
                key={`row_${rowIndex}_${header.key}`}
                className={styles['table__cell']}
                style={{ width: header.width }}
              >
                {row.get(header.key) || <span>&mdash;</span>}
              </td>
            ))}
          </tr>
        ))}
      </tbody>
    </table>
  );
};
