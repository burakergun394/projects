import React from 'react';
import { View, Text, StyleSheet } from 'react-native';
import { Colors } from '../../constants/colors';

const HomeScreen: React.FC = () => {
  return (
    <View style={styles.container}>
      <Text style={styles.title}>Search Providers</Text>
      <Text style={styles.placeholder}>Find and browse service providers near you.</Text>
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

export default HomeScreen;
