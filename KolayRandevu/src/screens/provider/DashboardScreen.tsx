import React from 'react';
import { View, Text } from 'react-native';
import { Styles } from '../../constants/styles';

const DashboardScreen: React.FC = () => {
  return (
    <View style={Styles.screenContainer}>
      <Text style={Styles.screenTitle}>Dashboard</Text>
      <Text style={Styles.subtitle}>Overview of your appointments and analytics.</Text>
    </View>
  );
};

export default DashboardScreen;
