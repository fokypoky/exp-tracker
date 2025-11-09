import { PAGINATION_PAGES_COUNT } from '@constants';
import { sliceNumberToArray } from '@utils';

export const getTotalPages = (totalCount: number, itemsPerPage: number): number => {
  return Math.ceil(totalCount / itemsPerPage);
};

export const showLeftSeparator = (totalCount: number, itemsPerPage: number, page: number): boolean => {
  return page - Math.ceil(PAGINATION_PAGES_COUNT / 2) > 1;
};

export const showRightSeparator = (totalCount: number, itemsPerPage: number, page: number): boolean => {
  const totalPages = getTotalPages(totalCount, itemsPerPage); //8

  return (page + Math.floor(PAGINATION_PAGES_COUNT / 2) - 1) < totalPages;
};

export const getPages = (totalCount: number, itemsPerPage: number, page: number): number[] => {
  const totalPages = getTotalPages(totalCount, itemsPerPage);

  if (totalPages <= PAGINATION_PAGES_COUNT) return sliceNumberToArray(totalPages);

  const start = Math.max(1, page - Math.ceil(PAGINATION_PAGES_COUNT / 2));
  const end = start + PAGINATION_PAGES_COUNT;

  return sliceNumberToArray(totalPages).slice(start - 1, end - 1);
};
