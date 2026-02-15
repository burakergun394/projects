import React, { useCallback, useRef, useEffect } from 'react';
import { FlatList, View } from 'react-native';

import { useLog } from '../stores/gameStore';
import { LogEntryItem } from './LogEntryItem';

import type { LogEntry } from '../types';

const LOG_ITEM_HEIGHT = 48;

const getItemLayout = (_: unknown, index: number) => ({
  length: LOG_ITEM_HEIGHT,
  offset: LOG_ITEM_HEIGHT * index,
  index,
});

const keyExtractor = (_: LogEntry, index: number) => `log-${index}`;

export const LogList = () => {
  const log = useLog();
  const listRef = useRef<FlatList<LogEntry>>(null);

  const renderItem = useCallback(
    ({ item, index }: { item: LogEntry; index: number }) => (
      <LogEntryItem item={item} index={index} />
    ),
    [],
  );

  useEffect(() => {
    if (log.length > 0) {
      setTimeout(() => {
        listRef.current?.scrollToEnd({ animated: true });
      }, 100);
    }
  }, [log.length]);

  return (
    <FlatList
      ref={listRef}
      data={log}
      renderItem={renderItem}
      keyExtractor={keyExtractor}
      getItemLayout={getItemLayout}
      maxToRenderPerBatch={20}
      className="flex-1"
      contentContainerClassName="pb-sm"
      showsVerticalScrollIndicator={false}
    />
  );
};
