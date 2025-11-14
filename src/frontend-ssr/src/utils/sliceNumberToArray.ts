export const sliceNumberToArray = (num: number): number[] => {
  const result: number[] = [];

  for (let i = 0; i < num; i++) {
    result.push(i + 1);
  }

  return result;
};
