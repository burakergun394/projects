import React from 'react';
import { View, Text } from 'react-native';
import { Styles } from '../../constants/styles';

const MyAppointmentsScreen: React.FC = () => {
  return (
    <View style={Styles.screenContainer}>
      <Text style={Styles.screenTitle}>My Appointments</Text>
      <Text style={Styles.subtitle}>Track your upcoming and past appointments.</Text>
    </View>
  );
};

export default MyAppointmentsScreen;
