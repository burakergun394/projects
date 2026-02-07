import { SafeAreaView } from 'react-native-safe-area-context';
import { View } from '@/src/tw';

interface ContainerProps {
  children: React.ReactNode;
  className?: string;
}

export const Container = ({ children, className = '' }: ContainerProps) => {
  return (
    <SafeAreaView style={{ flex: 1 }}>
      <View className={`flex-1 p-6 bg-white ${className}`}>{children}</View>
    </SafeAreaView>
  );
};
