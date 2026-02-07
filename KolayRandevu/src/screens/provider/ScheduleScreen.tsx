import React, { useState, useMemo } from 'react';
import { View, Text, Switch, TextInput, TouchableOpacity, ScrollView, StyleSheet } from 'react-native';
import { Colors } from '../../constants/colors';
import { Styles } from '../../constants/styles';
import Button from '../../components/common/Button';
import { defaultWorkingDays } from '../../data/mockData';
import type { WorkingDay } from '../../data/mockData';

const SLOT_DURATIONS = [30, 45, 60];

function generateSlots(startTime: string, endTime: string, duration: number): string[] {
  const slots: string[] = [];
  const [startH, startM] = startTime.split(':').map(Number);
  const [endH, endM] = endTime.split(':').map(Number);
  let current = startH * 60 + startM;
  const end = endH * 60 + endM;

  while (current + duration <= end) {
    const h = Math.floor(current / 60);
    const m = current % 60;
    const slotStart = `${String(h).padStart(2, '0')}:${String(m).padStart(2, '0')}`;
    current += duration;
    const h2 = Math.floor(current / 60);
    const m2 = current % 60;
    const slotEnd = `${String(h2).padStart(2, '0')}:${String(m2).padStart(2, '0')}`;
    slots.push(`${slotStart} - ${slotEnd}`);
  }
  return slots;
}

const ScheduleScreen: React.FC = () => {
  const [workingDays, setWorkingDays] = useState<WorkingDay[]>(defaultWorkingDays);
  const [slotDuration, setSlotDuration] = useState(30);

  const toggleDay = (index: number) => {
    setWorkingDays((prev) =>
      prev.map((d, i) => (i === index ? { ...d, enabled: !d.enabled } : d)),
    );
  };

  const updateTime = (index: number, field: 'startTime' | 'endTime', value: string) => {
    setWorkingDays((prev) =>
      prev.map((d, i) => (i === index ? { ...d, [field]: value } : d)),
    );
  };

  const firstEnabledDay = workingDays.find((d) => d.enabled);
  const previewSlots = useMemo(() => {
    if (!firstEnabledDay) return [];
    return generateSlots(firstEnabledDay.startTime, firstEnabledDay.endTime, slotDuration);
  }, [firstEnabledDay, slotDuration]);

  return (
    <ScrollView style={styles.container} showsVerticalScrollIndicator={false}>
      {/* Working Days */}
      <Text style={Styles.sectionTitle}>Working Days</Text>
      {workingDays.map((day, index) => (
        <View key={day.day} style={[Styles.card, styles.dayRow]}>
          <View style={[Styles.row, styles.dayHeader]}>
            <Text style={[styles.dayName, !day.enabled && styles.dayDisabled]}>{day.day}</Text>
            <Switch
              value={day.enabled}
              onValueChange={() => toggleDay(index)}
              trackColor={{ false: Colors.lightGray, true: '#C8E6C9' }}
              thumbColor={day.enabled ? '#4CAF50' : Colors.gray}
            />
          </View>
          {day.enabled && (
            <View style={[Styles.row, styles.timeRow]}>
              <TextInput
                style={[Styles.input, styles.timeInput]}
                value={day.startTime}
                onChangeText={(v) => updateTime(index, 'startTime', v)}
                placeholder="09:00"
                keyboardType="numbers-and-punctuation"
              />
              <Text style={styles.timeSeparator}>to</Text>
              <TextInput
                style={[Styles.input, styles.timeInput]}
                value={day.endTime}
                onChangeText={(v) => updateTime(index, 'endTime', v)}
                placeholder="18:00"
                keyboardType="numbers-and-punctuation"
              />
            </View>
          )}
        </View>
      ))}

      {/* Slot Duration */}
      <Text style={Styles.sectionTitle}>Slot Duration</Text>
      <View style={[Styles.row, styles.chipsRow]}>
        {SLOT_DURATIONS.map((d) => (
          <TouchableOpacity
            key={d}
            style={[styles.chip, slotDuration === d && styles.chipActive]}
            onPress={() => setSlotDuration(d)}
          >
            <Text style={[styles.chipText, slotDuration === d && styles.chipTextActive]}>
              {d} min
            </Text>
          </TouchableOpacity>
        ))}
      </View>

      {/* Generated Slots Preview */}
      <Text style={Styles.sectionTitle}>
        Generated Slots Preview{firstEnabledDay ? ` (${firstEnabledDay.day})` : ''}
      </Text>
      {previewSlots.length > 0 ? (
        <View style={styles.slotsGrid}>
          {previewSlots.map((slot) => (
            <View key={slot} style={styles.slotBadge}>
              <Text style={styles.slotText}>{slot}</Text>
            </View>
          ))}
        </View>
      ) : (
        <Text style={Styles.subtitle}>No working days enabled</Text>
      )}

      {/* Save */}
      <View style={styles.saveSection}>
        <Button title="Save Schedule" onPress={() => {}} />
      </View>
    </ScrollView>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: Colors.lightGray,
    padding: 16,
  },
  dayRow: {
    marginBottom: 8,
  },
  dayHeader: {
    justifyContent: 'space-between',
  },
  dayName: {
    fontSize: 16,
    fontWeight: '600',
    color: Colors.black,
  },
  dayDisabled: {
    color: Colors.gray,
  },
  timeRow: {
    marginTop: 10,
    gap: 8,
  },
  timeInput: {
    flex: 1,
    marginBottom: 0,
    textAlign: 'center',
  },
  timeSeparator: {
    fontSize: 14,
    color: Colors.gray,
  },
  chipsRow: {
    gap: 12,
    marginBottom: 8,
  },
  chip: {
    paddingVertical: 10,
    paddingHorizontal: 24,
    borderRadius: 20,
    borderWidth: 2,
    borderColor: Colors.primary,
    backgroundColor: 'transparent',
  },
  chipActive: {
    backgroundColor: Colors.primary,
  },
  chipText: {
    fontSize: 14,
    fontWeight: '600',
    color: Colors.primary,
  },
  chipTextActive: {
    color: Colors.white,
  },
  slotsGrid: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    gap: 8,
  },
  slotBadge: {
    backgroundColor: Colors.white,
    borderRadius: 8,
    paddingVertical: 8,
    paddingHorizontal: 14,
    borderWidth: 1,
    borderColor: Colors.primary,
  },
  slotText: {
    fontSize: 13,
    color: Colors.primary,
    fontWeight: '500',
  },
  saveSection: {
    marginTop: 28,
    marginBottom: 32,
  },
});

export default ScheduleScreen;
