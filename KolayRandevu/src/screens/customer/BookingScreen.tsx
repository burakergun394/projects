import React from 'react';
import { View, Text } from 'react-native';
import { Styles } from '../../constants/styles';

const BookingScreen: React.FC = () => {
  return (
    <View style={Styles.screenContainer}>
      <Text style={Styles.screenTitle}>Book Appointment</Text>
      <Text style={Styles.subtitle}>View available slots and book your appointment.</Text>
    </View>
  );
};

export default BookingScreen;
