import React, { useCallback, useMemo } from 'react';
import { FlatList, View, Text, Pressable } from 'react-native';

import { Card } from '@/shared/components/ui';
import { formatMoney } from '@/shared/utils';

import { useGameStore, useCharacter } from '../stores/gameStore';
import { ACTIVITIES } from '../data/activities';

import type { Activity } from '../types';

interface ActionCardProps {
  activity: Activity;
  onPress: (id: string) => void;
  canAfford: boolean;
}

const ActionCard = React.memo(
  ({ activity, onPress, canAfford }: ActionCardProps) => (
    <Pressable
      onPress={() => onPress(activity.id)}
      className="flex-1 m-1"
      disabled={!canAfford}
    >
      <Card
        className={`items-center py-md ${!canAfford ? 'opacity-50' : ''}`}
      >
        <Text className="text-2xl mb-xs">{activity.emoji}</Text>
        <Text className="text-sm font-outfit-semibold text-text-primary text-center">
          {activity.name}
        </Text>
        {activity.cost > 0 && (
          <Text className="text-xs font-mono text-text-tertiary mt-1">
            {formatMoney(activity.cost)}
          </Text>
        )}
      </Card>
    </Pressable>
  ),
);

export const ActionGrid = () => {
  const character = useCharacter();
  const doAction = useGameStore((s) => s.doAction);

  const handlePress = useCallback(
    (id: string) => {
      doAction(id);
    },
    [doAction],
  );

  const availableActivities = useMemo(() => {
    if (!character) return ACTIVITIES as unknown as Activity[];
    return (ACTIVITIES as unknown as Activity[]).filter(
      (a) => character.age >= a.minAge,
    );
  }, [character?.age]);

  const renderItem = useCallback(
    ({ item }: { item: Activity }) => (
      <ActionCard
        activity={item}
        onPress={handlePress}
        canAfford={(character?.money ?? 0) >= item.cost}
      />
    ),
    [handlePress, character?.money],
  );

  const keyExtractor = useCallback((item: Activity) => item.id, []);

  return (
    <FlatList
      data={availableActivities}
      renderItem={renderItem}
      keyExtractor={keyExtractor}
      numColumns={2}
      className="flex-1"
      contentContainerClassName="p-sm"
      showsVerticalScrollIndicator={false}
    />
  );
};
