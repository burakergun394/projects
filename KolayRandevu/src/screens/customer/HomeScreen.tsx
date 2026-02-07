import React from 'react';
import { View, Text } from 'react-native';
import { Styles } from '../../constants/styles';

const HomeScreen: React.FC = () => {
  return (
    <View style={Styles.screenContainer}>
      <Text style={Styles.screenTitle}>Search Providers</Text>
      <Text style={Styles.subtitle}>Find and browse service providers near you.</Text>
    </View>
  );
};

export default HomeScreen;
