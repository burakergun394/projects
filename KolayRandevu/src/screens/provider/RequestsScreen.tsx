import React, { useState } from 'react';
import { View, Text, FlatList, TouchableOpacity, StyleSheet } from 'react-native';
import { Colors } from '../../constants/colors';
import { Styles } from '../../constants/styles';
import Button from '../../components/common/Button';
import { mockAppointments } from '../../data/mockData';
import type { Appointment } from '../../data/mockData';

type Tab = 'pending' | 'approved';

const RequestsScreen: React.FC = () => {
  const [activeTab, setActiveTab] = useState<Tab>('pending');
  const [appointments, setAppointments] = useState<Appointment[]>(mockAppointments);

  const pending = appointments.filter((a) => a.status === 'pending');
  const approved = appointments.filter((a) => a.status === 'confirmed');

  const handleApprove = (id: string) => {
    setAppointments((prev) =>
      prev.map((a) => (a.id === id ? { ...a, status: 'confirmed' } : a)),
    );
  };

  const handleReject = (id: string) => {
    setAppointments((prev) =>
      prev.map((a) => (a.id === id ? { ...a, status: 'rejected' } : a)),
    );
  };

  const renderPendingItem = ({ item }: { item: Appointment }) => (
    <View style={Styles.card}>
      <View style={[Styles.row, styles.cardHeader]}>
        <Text style={styles.customerName}>{item.customerName}</Text>
        <Text style={styles.dateTime}>{item.date} {item.time}</Text>
      </View>
      <Text style={styles.service}>{item.service}</Text>
      <View style={[Styles.row, styles.actions]}>
        <Button
          title="Approve"
          onPress={() => handleApprove(item.id)}
          style={styles.approveButton}
        />
        <Button
          title="Reject"
          variant="outline"
          onPress={() => handleReject(item.id)}
          style={styles.rejectButton}
        />
      </View>
    </View>
  );

  const renderApprovedItem = ({ item }: { item: Appointment }) => (
    <View style={[Styles.card, Styles.row, styles.approvedCard]}>
      <View style={styles.approvedInfo}>
        <Text style={styles.customerName}>{item.customerName}</Text>
        <Text style={styles.service}>{item.service}</Text>
      </View>
      <View style={styles.approvedBadge}>
        <Text style={styles.approvedBadgeText}>Confirmed</Text>
      </View>
    </View>
  );

  return (
    <View style={styles.container}>
      {/* Tab Toggle */}
      <View style={[Styles.row, styles.tabRow]}>
        <TouchableOpacity
          style={[styles.tab, activeTab === 'pending' && styles.tabActive]}
          onPress={() => setActiveTab('pending')}
        >
          <Text style={[styles.tabText, activeTab === 'pending' && styles.tabTextActive]}>
            Pending ({pending.length})
          </Text>
        </TouchableOpacity>
        <TouchableOpacity
          style={[styles.tab, activeTab === 'approved' && styles.tabActive]}
          onPress={() => setActiveTab('approved')}
        >
          <Text style={[styles.tabText, activeTab === 'approved' && styles.tabTextActive]}>
            Approved ({approved.length})
          </Text>
        </TouchableOpacity>
      </View>

      {/* List */}
      {activeTab === 'pending' ? (
        pending.length > 0 ? (
          <FlatList
            data={pending}
            renderItem={renderPendingItem}
            keyExtractor={(item) => item.id}
            contentContainerStyle={styles.list}
          />
        ) : (
          <View style={styles.emptyState}>
            <Text style={Styles.subtitle}>No pending requests</Text>
          </View>
        )
      ) : (
        approved.length > 0 ? (
          <FlatList
            data={approved}
            renderItem={renderApprovedItem}
            keyExtractor={(item) => item.id}
            contentContainerStyle={styles.list}
          />
        ) : (
          <View style={styles.emptyState}>
            <Text style={Styles.subtitle}>No approved appointments yet</Text>
          </View>
        )
      )}
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: Colors.lightGray,
  },
  tabRow: {
    backgroundColor: Colors.white,
    padding: 4,
    margin: 16,
    borderRadius: 12,
    gap: 4,
  },
  tab: {
    flex: 1,
    paddingVertical: 12,
    borderRadius: 10,
    alignItems: 'center',
  },
  tabActive: {
    backgroundColor: Colors.primary,
  },
  tabText: {
    fontSize: 15,
    fontWeight: '600',
    color: Colors.gray,
  },
  tabTextActive: {
    color: Colors.white,
  },
  list: {
    paddingHorizontal: 16,
    paddingBottom: 24,
  },
  cardHeader: {
    justifyContent: 'space-between',
    marginBottom: 6,
  },
  customerName: {
    fontSize: 16,
    fontWeight: '600',
    color: Colors.black,
  },
  dateTime: {
    fontSize: 13,
    color: Colors.gray,
  },
  service: {
    fontSize: 14,
    color: Colors.darkGray,
    marginBottom: 12,
  },
  actions: {
    gap: 12,
  },
  approveButton: {
    flex: 1,
    backgroundColor: Colors.primary,
    paddingVertical: 12,
    paddingHorizontal: 0,
  },
  rejectButton: {
    flex: 1,
    paddingVertical: 12,
    paddingHorizontal: 0,
  },
  approvedCard: {
    justifyContent: 'space-between',
  },
  approvedInfo: {
    flex: 1,
  },
  approvedBadge: {
    backgroundColor: '#E8F5E9',
    borderRadius: 8,
    paddingVertical: 6,
    paddingHorizontal: 12,
  },
  approvedBadgeText: {
    color: '#4CAF50',
    fontSize: 13,
    fontWeight: '600',
  },
  emptyState: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
  },
});

export default RequestsScreen;
