import React from 'react';
import { View, Text } from 'react-native';
import { Styles } from '../../constants/styles';

const ScheduleScreen: React.FC = () => {
  return (
    <View style={Styles.screenContainer}>
      <Text style={Styles.screenTitle}>Schedule</Text>
      <Text style={Styles.subtitle}>Manage your working hours and availability.</Text>
    </View>
  );
};

export default ScheduleScreen;
