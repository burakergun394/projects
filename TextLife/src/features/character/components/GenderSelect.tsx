import React, { useCallback } from 'react';
import { View, Text, Pressable } from 'react-native';
import { MotiView } from 'moti';

import { Card } from '@/shared/components/ui';

import type { Gender } from '@/features/game/types';

interface GenderSelectProps {
  onSelect: (gender: Gender) => void;
}

export const GenderSelect = ({ onSelect }: GenderSelectProps) => {
  const handleMale = useCallback(() => onSelect('M'), [onSelect]);
  const handleFemale = useCallback(() => onSelect('F'), [onSelect]);

  return (
    <View className="flex-row gap-lg px-lg">
      <MotiView
        from={{ opacity: 0, scale: 0.8 }}
        animate={{ opacity: 1, scale: 1 }}
        transition={{ type: 'timing', duration: 350, delay: 100 }}
        className="flex-1"
      >
        <Pressable onPress={handleMale}>
          <Card className="items-center py-3xl">
            <Text className="text-4xl mb-md">👦</Text>
            <Text className="text-xl font-outfit-bold text-text-primary">
              Erkek
            </Text>
          </Card>
        </Pressable>
      </MotiView>

      <MotiView
        from={{ opacity: 0, scale: 0.8 }}
        animate={{ opacity: 1, scale: 1 }}
        transition={{ type: 'timing', duration: 350, delay: 200 }}
        className="flex-1"
      >
        <Pressable onPress={handleFemale}>
          <Card className="items-center py-3xl">
            <Text className="text-4xl mb-md">👧</Text>
            <Text className="text-xl font-outfit-bold text-text-primary">
              Kadın
            </Text>
          </Card>
        </Pressable>
      </MotiView>
    </View>
  );
};
