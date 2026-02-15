import React, { useCallback, useMemo, memo } from 'react';
import { View, Text, ScrollView, Pressable } from 'react-native';

import { Card, ProgressBar, Button } from '@/shared/components/ui';
import { colors } from '@/shared/theme';

import { useGameStore, useCharacter } from '../stores/gameStore';

import type { Relationship } from '../types';

const RELATION_TYPE_LABELS: Record<Relationship['type'], string> = {
  parent: 'Aile',
  sibling: 'Aile',
  spouse: 'Eş',
  child: 'Çocuklar',
  friend: 'Arkadaşlar',
};

const RELATION_TYPE_EMOJI: Record<Relationship['type'], string> = {
  parent: '👨‍👩‍👦',
  sibling: '🧑‍🤝‍🧑',
  spouse: '💍',
  child: '👶',
  friend: '🤝',
};

interface RelationCardProps {
  relation: Relationship;
  onSpendTime: (id: string) => void;
  onArgue: (id: string) => void;
}

const RelationCard = memo(({ relation, onSpendTime, onArgue }: RelationCardProps) => (
  <Card className={`mb-sm ${!relation.isAlive ? 'opacity-50' : ''}`}>
    <View className="flex-row items-center justify-between">
      <View className="flex-1">
        <View className="flex-row items-center gap-xs">
          <Text className="text-base">{RELATION_TYPE_EMOJI[relation.type]}</Text>
          <Text className="text-sm font-outfit-semibold text-text-primary">
            {relation.name} {relation.surname}
          </Text>
          {!relation.isAlive && (
            <Text className="text-xs text-text-tertiary">(Vefat)</Text>
          )}
        </View>
        <Text className="text-xs font-mono text-text-secondary mt-1">
          {relation.age} yaş
        </Text>
        <View className="flex-row items-center gap-xs mt-1">
          <Text className="text-xs text-text-tertiary">Yakınlık:</Text>
          <View className="flex-1 max-w-[100px]">
            <ProgressBar
              value={relation.closeness}
              color={relation.closeness > 60 ? colors.success : relation.closeness > 30 ? colors.warning : colors.danger}
            />
          </View>
          <Text className="text-xs font-mono text-text-secondary">
            {relation.closeness}
          </Text>
        </View>
      </View>
      {relation.isAlive && (
        <View className="gap-xs">
          <Pressable
            onPress={() => onSpendTime(relation.id)}
            className="bg-brand-subtle px-sm py-1 rounded-sm"
          >
            <Text className="text-xs font-outfit-medium text-brand-primary">
              Zaman Geçir
            </Text>
          </Pressable>
          <Pressable
            onPress={() => onArgue(relation.id)}
            className="bg-danger-subtle px-sm py-1 rounded-sm"
          >
            <Text className="text-xs font-outfit-medium text-danger">
              Tartış
            </Text>
          </Pressable>
        </View>
      )}
    </View>
  </Card>
));

export const RelationList = () => {
  const character = useCharacter();
  const interactRelation = useGameStore((s) => s.interactRelation);
  const marry = useGameStore((s) => s.marry);
  const divorce = useGameStore((s) => s.divorce);
  const haveChild = useGameStore((s) => s.haveChild);

  const handleSpendTime = useCallback(
    (id: string) => interactRelation(id, 'spend_time'),
    [interactRelation],
  );

  const handleArgue = useCallback(
    (id: string) => interactRelation(id, 'argue'),
    [interactRelation],
  );

  const grouped = useMemo(() => {
    if (!character) return { family: [], spouse: [], children: [], friends: [] };
    const rels = character.relationships;
    return {
      family: rels.filter((r) => r.type === 'parent' || r.type === 'sibling'),
      spouse: rels.filter((r) => r.type === 'spouse'),
      children: rels.filter((r) => r.type === 'child'),
      friends: rels.filter((r) => r.type === 'friend'),
    };
  }, [character?.relationships]);

  if (!character) return null;

  return (
    <ScrollView className="flex-1" contentContainerClassName="p-lg gap-md">
      {/* Evlilik Aksiyonları */}
      <View className="flex-row gap-sm">
        {!character.isMarried && character.age >= 20 && (
          <View className="flex-1">
            <Button label="Evlen 💍" onPress={marry} variant="primary" />
          </View>
        )}
        {character.isMarried && (
          <>
            <View className="flex-1">
              <Button label="Boşan 💔" onPress={divorce} variant="danger" />
            </View>
            {character.age >= 22 && (
              <View className="flex-1">
                <Button label="Çocuk Sahibi Ol 👶" onPress={haveChild} variant="secondary" />
              </View>
            )}
          </>
        )}
      </View>

      {/* Eş */}
      {grouped.spouse.length > 0 && (
        <>
          <Text className="text-base font-outfit-bold text-text-primary">
            💍 Eş
          </Text>
          {grouped.spouse.map((r) => (
            <RelationCard
              key={r.id}
              relation={r}
              onSpendTime={handleSpendTime}
              onArgue={handleArgue}
            />
          ))}
        </>
      )}

      {/* Aile */}
      {grouped.family.length > 0 && (
        <>
          <Text className="text-base font-outfit-bold text-text-primary">
            👨‍👩‍👦 Aile
          </Text>
          {grouped.family.map((r) => (
            <RelationCard
              key={r.id}
              relation={r}
              onSpendTime={handleSpendTime}
              onArgue={handleArgue}
            />
          ))}
        </>
      )}

      {/* Çocuklar */}
      {grouped.children.length > 0 && (
        <>
          <Text className="text-base font-outfit-bold text-text-primary">
            👶 Çocuklar
          </Text>
          {grouped.children.map((r) => (
            <RelationCard
              key={r.id}
              relation={r}
              onSpendTime={handleSpendTime}
              onArgue={handleArgue}
            />
          ))}
        </>
      )}

      {/* Arkadaşlar */}
      {grouped.friends.length > 0 && (
        <>
          <Text className="text-base font-outfit-bold text-text-primary">
            🤝 Arkadaşlar
          </Text>
          {grouped.friends.map((r) => (
            <RelationCard
              key={r.id}
              relation={r}
              onSpendTime={handleSpendTime}
              onArgue={handleArgue}
            />
          ))}
        </>
      )}

      {character.relationships.length === 0 && (
        <Card>
          <Text className="text-sm font-outfit text-text-tertiary text-center">
            Henüz kimseyle tanışmadın.
          </Text>
        </Card>
      )}
    </ScrollView>
  );
};
