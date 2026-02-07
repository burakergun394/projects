import { SafeAreaView } from 'react-native-safe-area-context';
import { View } from '@/src/tw';

export const Container = ({
  children,
  className = '',
}: {
  children: React.ReactNode;
  className?: string;
}) => {
  return (
    <SafeAreaView style={{ flex: 1 }}>
      <View className={`flex-1 p-6 bg-white ${className}`}>{children}</View>
    </SafeAreaView>
  );
};
