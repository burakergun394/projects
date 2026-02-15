import React, { useCallback } from 'react';
import { View, Text, Pressable } from 'react-native';
import { router } from 'expo-router';
import { SafeAreaView } from 'react-native-safe-area-context';
import { MotiView } from 'moti';

import { useGameStore } from '@/features/game/stores/gameStore';
import { GenderSelect } from '@/features/character';

import type { Gender } from '@/features/game/types';

export default function CreateScreen() {
  const createCharacter = useGameStore((s) => s.createCharacter);

  const handleSelect = useCallback(
    (gender: Gender) => {
      createCharacter(gender);
      router.replace('/game');
    },
    [createCharacter],
  );

  const handleBack = useCallback(() => {
    router.back();
  }, []);

  return (
    <SafeAreaView className="flex-1 bg-bg" edges={['top', 'bottom']}>
      <View className="px-lg py-md">
        <Pressable onPress={handleBack}>
          <Text className="text-sm font-outfit-semibold text-brand-primary">
            ← Geri
          </Text>
        </Pressable>
      </View>

      <View className="flex-1 justify-center">
        <MotiView
          from={{ opacity: 0, translateY: -10 }}
          animate={{ opacity: 1, translateY: 0 }}
          transition={{ type: 'timing', duration: 350 }}
        >
          <Text className="text-3xl font-outfit-bold text-text-primary text-center mb-xl">
            Karakterini Oluştur
          </Text>
          <Text className="text-base font-outfit text-text-secondary text-center mb-3xl px-xl">
            Cinsiyetini seç ve yeni hayatına başla!
          </Text>
        </MotiView>

        <GenderSelect onSelect={handleSelect} />
      </View>
    </SafeAreaView>
  );
}
