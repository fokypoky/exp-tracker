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
            <th className={styles['table__header']} key={header.key}>
              {header.name}
            </th>
          ))}
        </tr>
      </thead>
      <tbody>
        {rows.map((row, rowIndex) => (
          <tr key={rowIndex}>
            {headers.map((header) => (
              <td key={`row_${rowIndex}_${header.key}`}>
                {row.get(header.key) || <span>&mdash;</span>}
              </td>
            ))}
          </tr>
        ))}
      </tbody>
    </table>
  );
};
