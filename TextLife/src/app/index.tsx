import React, { useCallback, useState, useEffect } from 'react';
import { View, Text } from 'react-native';
import { router } from 'expo-router';
import { SafeAreaView } from 'react-native-safe-area-context';
import { MotiView } from 'moti';

import { Button } from '@/shared/components/ui';
import { useLoadGame } from '@/features/save';
import { useGameStore } from '@/features/game/stores/gameStore';

export default function MenuScreen() {
  const { hasSave, load } = useLoadGame();
  const [saveExists, setSaveExists] = useState(false);

  useEffect(() => {
    setSaveExists(hasSave(0));
  }, [hasSave]);

  const handleNewGame = useCallback(() => {
    router.replace('/create');
  }, []);

  const handleContinue = useCallback(() => {
    const success = load(0);
    if (success) {
      router.replace('/game');
    }
  }, [load]);

  return (
    <SafeAreaView className="flex-1 bg-bg" edges={['top', 'bottom']}>
      <View className="flex-1 items-center justify-center px-xl">
        <MotiView
          from={{ opacity: 0, translateY: -20 }}
          animate={{ opacity: 1, translateY: 0 }}
          transition={{ type: 'timing', duration: 450 }}
        >
          <Text className="text-4xl text-center mb-xs">🧬</Text>
          <Text className="text-4xl font-outfit-black text-text-primary text-center">
            TEXT LIFE
          </Text>
          <Text className="text-base font-outfit text-text-secondary text-center mt-xs">
            YAŞAM SİMÜLASYONU
          </Text>
        </MotiView>

        <MotiView
          from={{ opacity: 0, translateY: 20 }}
          animate={{ opacity: 1, translateY: 0 }}
          transition={{ type: 'timing', duration: 450, delay: 200 }}
          className="w-full mt-3xl gap-md"
        >
          <Button
            label="Yeni Hayat Başlat"
            onPress={handleNewGame}
            className="py-lg"
          />
          {saveExists && (
            <Button
              label="Devam Et"
              onPress={handleContinue}
              variant="secondary"
              className="py-lg"
            />
          )}
        </MotiView>
      </View>
    </SafeAreaView>
  );
}
