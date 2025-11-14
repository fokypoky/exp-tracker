import { useCallback, useState, useMemo } from 'react';

import { DEFAULT_FILTERS } from '@constants';
import { Filters } from '@types';

export const useFilters = () => {
  const [filters, setFilters] = useState<Filters>(DEFAULT_FILTERS);
  const page = useMemo(() => {
    const limit = filters.limit || 0;
    const offset = filters.offset || 0;

    if (!limit && !offset) return 1;

    return (offset / limit) + 1;
  }, [filters]);

  const setPage = useCallback((page: number) => {
    setFilters((prev) => ({
      ...prev,
      offset: (prev.limit || 0) * (page - 1),
    }));
  }, []);

  const setItemsPerPage = useCallback((itemsPerPage: number) => {
    setFilters((prev) => ({
      ...prev,
      offset: 0,
      limit: itemsPerPage,
    }));
  }, []);

  return { filters, setPage, setItemsPerPage, page, setFilters };
};
