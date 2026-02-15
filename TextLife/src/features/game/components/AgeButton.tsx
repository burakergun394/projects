import React, { useCallback } from 'react';
import { View } from 'react-native';

import { Button } from '@/shared/components/ui';

import { useGameStore, useCharacter } from '../stores/gameStore';

export const AgeButton = () => {
  const character = useCharacter();
  const ageUp = useGameStore((s) => s.ageUp);

  const handlePress = useCallback(() => {
    ageUp();
  }, [ageUp]);

  const isDisabled = !character || !character.isAlive;

  return (
    <View className="px-lg py-md bg-bg-elevated border-t border-border">
      <Button
        label="+1 YAŞ İLERLE"
        onPress={handlePress}
        variant={isDisabled ? 'disabled' : 'primary'}
        className="py-lg"
      />
    </View>
  );
};
