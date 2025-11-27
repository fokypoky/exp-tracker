import { Dash, EmptyText } from '@components';
import { TABLE_CLICK_KEY } from '@constants';
import { TableHeader, TableRow, TableRowFnType } from '@types';

import styles from './Table.module.css';

type Props = {
  headers: TableHeader[];
  rows: TableRow[];
  emptyText?: string;
}

export const Table = ({ headers, rows, emptyText }: Props) => {
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
        {rows.length === 0 && emptyText && (
          <tr>
            <td colSpan={headers.length}>
              <EmptyText
                text={emptyText}
                padded
              />
            </td>
          </tr>
        )}
        {rows.map((row, rowIndex) => (
          <tr key={rowIndex} className={styles['table__row']} onClick={() => {
            const callback = row.get(TABLE_CLICK_KEY) as TableRowFnType;
            callback && callback();
          }}>
            {headers.map((header) => (
              <td
                key={`row_${rowIndex}_${header.key}`}
                className={styles['table__cell']}
                style={{ width: header.width }}
              >
                {(row.get(header.key) as string | React.ReactNode) || <Dash />}
              </td>
            ))}
          </tr>
        ))}
      </tbody>
    </table>
  );
};
