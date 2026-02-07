import React from 'react';
import { View, Text, StyleSheet } from 'react-native';
import { Colors } from '../../constants/colors';

const BookingScreen: React.FC = () => {
  return (
    <View style={styles.container}>
      <Text style={styles.title}>Book Appointment</Text>
      <Text style={styles.placeholder}>View available slots and book your appointment.</Text>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: Colors.white,
    padding: 24,
  },
  title: {
    fontSize: 24,
    fontWeight: 'bold',
    color: Colors.primary,
    marginBottom: 12,
  },
  placeholder: {
    fontSize: 16,
    color: Colors.gray,
    textAlign: 'center',
  },
});

export default BookingScreen;
