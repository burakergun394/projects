import React, { useCallback } from 'react';
import { View, Text } from 'react-native';
import { router } from 'expo-router';
import { SafeAreaView } from 'react-native-safe-area-context';
import { MotiView } from 'moti';

import { Button } from '@/shared/components/ui';
import { useCharacter, useGameStore } from '@/features/game/stores/gameStore';
import { CharacterCard } from '@/features/character';

export default function DeathScreen() {
  const character = useCharacter();
  const newGame = useGameStore((s) => s.newGame);

  const handleNewGame = useCallback(() => {
    newGame();
    router.replace('/');
  }, [newGame]);

  if (!character) return null;

  return (
    <SafeAreaView className="flex-1 bg-bg" edges={['top', 'bottom']}>
      <View className="flex-1 justify-center">
        <MotiView
          from={{ opacity: 0, scale: 0.9 }}
          animate={{ opacity: 1, scale: 1 }}
          transition={{ type: 'timing', duration: 450 }}
        >
          <View className="items-center mb-xl">
            <Text className="text-4xl mb-md">🪦</Text>
            <Text className="text-3xl font-outfit-bold text-text-primary text-center">
              {character.name} {character.surname}
            </Text>
            <Text className="text-lg font-mono text-text-secondary mt-xs">
              {character.birthYear} — {character.birthYear + (character.deathAge ?? character.age)}
            </Text>
            {character.deathReason && (
              <Text className="text-base font-outfit text-text-tertiary mt-xs">
                {character.deathReason}
              </Text>
            )}
          </View>

          <CharacterCard character={character} />

          <View className="px-lg mt-xl">
            <Button
              label="Yeni Hayat Başlat"
              onPress={handleNewGame}
              className="py-lg"
            />
          </View>
        </MotiView>
      </View>
    </SafeAreaView>
  );
}
