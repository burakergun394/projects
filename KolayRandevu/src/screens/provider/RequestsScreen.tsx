import React from 'react';
import { View, Text } from 'react-native';
import { Styles } from '../../constants/styles';

const RequestsScreen: React.FC = () => {
  return (
    <View style={Styles.screenContainer}>
      <Text style={Styles.screenTitle}>Requests</Text>
      <Text style={Styles.subtitle}>Approve or reject reservation requests.</Text>
    </View>
  );
};

export default RequestsScreen;
