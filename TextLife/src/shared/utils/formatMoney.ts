/** Para miktarını Türk formatında göster: ₺142.500 */
export const formatMoney = (amount: number): string => {
  const prefix = amount < 0 ? '-₺' : '₺';
  const abs = Math.abs(Math.round(amount));
  const formatted = abs.toString().replace(/\B(?=(\d{3})+(?!\d))/g, '.');
  return `${prefix}${formatted}`;
};
