import React, { useState } from 'react';
import { View, Text, Switch, ScrollView, FlatList, StyleSheet } from 'react-native';
import { Colors } from '../../constants/colors';
import { Styles } from '../../constants/styles';
import { mockAppointments, mockCustomers } from '../../data/mockData';
import type { Appointment, Customer } from '../../data/mockData';

const DashboardScreen: React.FC = () => {
  const [isOpen, setIsOpen] = useState(true);

  const pendingCount = mockAppointments.filter((a) => a.status === 'pending').length;
  const confirmedToday = mockAppointments.filter(
    (a) => a.status === 'confirmed' && a.date === '2026-02-07',
  ).length;
  const totalCustomers = mockCustomers.length;

  const upcomingAppointments = mockAppointments
    .filter((a) => a.status === 'confirmed')
    .slice(0, 3);

  const renderAppointment = ({ item }: { item: Appointment }) => (
    <View style={[Styles.card, styles.appointmentCard]}>
      <Text style={styles.appointmentName}>{item.customerName}</Text>
      <Text style={styles.appointmentDetail}>{item.service}</Text>
      <Text style={styles.appointmentTime}>{item.time}</Text>
    </View>
  );

  const renderCustomer = ({ item }: { item: Customer }) => (
    <View style={[Styles.card, Styles.row, styles.customerCard]}>
      <View style={styles.customerAvatar}>
        <Text style={styles.customerInitial}>{item.name.charAt(0)}</Text>
      </View>
      <View style={styles.customerInfo}>
        <Text style={styles.customerName}>{item.name}</Text>
        <Text style={styles.customerVisits}>{item.visitCount} visits</Text>
      </View>
    </View>
  );

  return (
    <ScrollView style={styles.container} showsVerticalScrollIndicator={false}>
      {/* Header */}
      <View style={[Styles.row, styles.header]}>
        <View>
          <Text style={styles.businessName}>My Business</Text>
          <Text style={[styles.statusText, { color: isOpen ? '#4CAF50' : Colors.gray }]}>
            {isOpen ? 'Open' : 'Closed'}
          </Text>
        </View>
        <Switch
          value={isOpen}
          onValueChange={setIsOpen}
          trackColor={{ false: Colors.lightGray, true: '#C8E6C9' }}
          thumbColor={isOpen ? '#4CAF50' : Colors.gray}
        />
      </View>

      {/* Summary Cards */}
      <ScrollView horizontal showsHorizontalScrollIndicator={false} style={styles.cardsRow}>
        <View style={[styles.summaryCard, { backgroundColor: Colors.primary }]}>
          <Text style={styles.summaryValue}>{pendingCount}</Text>
          <Text style={styles.summaryLabel}>Pending Requests</Text>
        </View>
        <View style={[styles.summaryCard, { backgroundColor: Colors.cta }]}>
          <Text style={styles.summaryValue}>{confirmedToday}</Text>
          <Text style={styles.summaryLabel}>Confirmed Today</Text>
        </View>
        <View style={[styles.summaryCard, { backgroundColor: Colors.primary }]}>
          <Text style={styles.summaryValue}>{totalCustomers}</Text>
          <Text style={styles.summaryLabel}>Total Customers</Text>
        </View>
      </ScrollView>

      {/* Upcoming Appointments */}
      <Text style={Styles.sectionTitle}>Upcoming Appointments</Text>
      <FlatList
        data={upcomingAppointments}
        renderItem={renderAppointment}
        keyExtractor={(item) => item.id}
        scrollEnabled={false}
      />

      {/* Customer Insights */}
      <Text style={Styles.sectionTitle}>Customer Insights</Text>
      <FlatList
        data={mockCustomers}
        renderItem={renderCustomer}
        keyExtractor={(item) => item.id}
        scrollEnabled={false}
      />

      <View style={styles.bottomSpacer} />
    </ScrollView>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: Colors.lightGray,
    padding: 16,
  },
  header: {
    justifyContent: 'space-between',
    marginBottom: 20,
  },
  businessName: {
    fontSize: 26,
    fontWeight: 'bold',
    color: Colors.primary,
  },
  statusText: {
    fontSize: 14,
    fontWeight: '600',
    marginTop: 2,
  },
  cardsRow: {
    marginBottom: 8,
  },
  summaryCard: {
    width: 150,
    borderRadius: 12,
    padding: 20,
    marginRight: 12,
    alignItems: 'center',
  },
  summaryValue: {
    fontSize: 32,
    fontWeight: 'bold',
    color: Colors.white,
  },
  summaryLabel: {
    fontSize: 13,
    color: Colors.white,
    marginTop: 6,
    textAlign: 'center',
  },
  appointmentCard: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
  },
  appointmentName: {
    fontSize: 15,
    fontWeight: '600',
    color: Colors.black,
    flex: 1,
  },
  appointmentDetail: {
    fontSize: 14,
    color: Colors.gray,
    flex: 1,
    textAlign: 'center',
  },
  appointmentTime: {
    fontSize: 14,
    fontWeight: '600',
    color: Colors.primary,
  },
  customerCard: {
    gap: 12,
  },
  customerAvatar: {
    width: 40,
    height: 40,
    borderRadius: 20,
    backgroundColor: Colors.primary,
    justifyContent: 'center',
    alignItems: 'center',
  },
  customerInitial: {
    color: Colors.white,
    fontSize: 18,
    fontWeight: 'bold',
  },
  customerInfo: {
    flex: 1,
  },
  customerName: {
    fontSize: 15,
    fontWeight: '600',
    color: Colors.black,
  },
  customerVisits: {
    fontSize: 13,
    color: Colors.gray,
    marginTop: 2,
  },
  bottomSpacer: {
    height: 24,
  },
});

export default DashboardScreen;
