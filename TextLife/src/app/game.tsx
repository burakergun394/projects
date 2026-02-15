import React, { useEffect } from 'react';
import { View } from 'react-native';
import { router } from 'expo-router';
import { SafeAreaView } from 'react-native-safe-area-context';

import {
  GameHeader,
  StatBars,
  TabBar,
  LogList,
  AgeButton,
  ActionGrid,
  JobList,
  EduList,
} from '@/features/game/components';
import { useCharacter, useActiveTab } from '@/features/game/stores/gameStore';
import { useAutoSave } from '@/features/save';
import { useAppState } from '@/shared/hooks';
import { useSaveGame } from '@/features/save';

export default function GameScreen() {
  const character = useCharacter();
  const activeTab = useActiveTab();
  const { save } = useSaveGame();

  // Otomatik kaydetme
  useAutoSave();

  // Arka plana geçince kaydet
  useAppState(() => save(0));

  // Ölünce death ekranına yönlendir
  useEffect(() => {
    if (character && !character.isAlive) {
      router.replace('/death');
    }
  }, [character?.isAlive]);

  if (!character) return null;

  const renderTabContent = () => {
    switch (activeTab) {
      case 'life':
        return <LogList />;
      case 'job':
        return <JobList />;
      case 'edu':
        return <EduList />;
      case 'actions':
        return <ActionGrid />;
    }
  };

  return (
    <SafeAreaView className="flex-1 bg-bg" edges={['top']}>
      <GameHeader />
      <TabBar />
      <View className="flex-1">{renderTabContent()}</View>
      <StatBars />
      <AgeButton />
    </SafeAreaView>
  );
}
