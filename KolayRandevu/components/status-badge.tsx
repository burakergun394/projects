import { View, Text } from '@/src/tw';
import { useTranslation } from 'react-i18next';
import type { AppointmentStatus } from '@/store/types';

const statusStyles: Record<AppointmentStatus, { bg: string; text: string }> = {
  pending: { bg: 'bg-orange/20', text: 'text-orange' },
  approved: { bg: 'bg-green-100', text: 'text-green-700' },
  rejected: { bg: 'bg-red-100', text: 'text-red-700' },
};

export const StatusBadge = ({ status }: { status: AppointmentStatus }) => {
  const { t } = useTranslation();
  const style = statusStyles[status];

  return (
    <View className={`px-3 py-1 rounded-full ${style.bg}`}>
      <Text className={`text-xs font-semibold ${style.text}`}>
        {t(`common.${status}`)}
      </Text>
    </View>
  );
};
