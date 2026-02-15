import React, { useCallback } from 'react';
import { View, Text, Pressable } from 'react-native';

import { useGameStore, useActiveTab } from '../stores/gameStore';

import type { TabId } from '../types';

interface Tab {
  id: TabId;
  emoji: string;
  label: string;
}

const TABS: readonly Tab[] = [
  { id: 'life', emoji: '📜', label: 'Hayat' },
  { id: 'job', emoji: '💼', label: 'İş' },
  { id: 'edu', emoji: '🎓', label: 'Eğitim' },
  { id: 'actions', emoji: '⚡', label: 'Aktivite' },
  { id: 'relations', emoji: '❤️', label: 'İlişkiler' },
] as const;

export const TabBar = () => {
  const activeTab = useActiveTab();
  const setActiveTab = useGameStore((s) => s.setActiveTab);

  const handlePress = useCallback(
    (tabId: TabId) => {
      setActiveTab(tabId);
    },
    [setActiveTab],
  );

  return (
    <View className="flex-row bg-bg-elevated border-b border-border">
      {TABS.map((tab) => {
        const isActive = activeTab === tab.id;
        return (
          <Pressable
            key={tab.id}
            onPress={() => handlePress(tab.id)}
            className={`flex-1 items-center py-md ${
              isActive ? 'border-b-2 border-brand-primary' : ''
            }`}
          >
            <Text className="text-sm">{tab.emoji}</Text>
            <Text
              className={`text-xs mt-1 ${
                isActive
                  ? 'font-outfit-semibold text-brand-primary'
                  : 'font-outfit text-text-tertiary'
              }`}
            >
              {tab.label}
            </Text>
          </Pressable>
        );
      })}
    </View>
  );
};
