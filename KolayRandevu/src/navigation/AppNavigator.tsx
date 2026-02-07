import React from 'react';
import { createStackNavigator } from '@react-navigation/stack';
import AuthStack from './AuthStack';
import CustomerTabs from './CustomerTabs';
import ProviderTabs from './ProviderTabs';
import { RootStackParamList } from './types';

const Stack = createStackNavigator<RootStackParamList>();

const AppNavigator: React.FC = () => {
  return (
    <Stack.Navigator screenOptions={{ headerShown: false }}>
      <Stack.Screen name="Auth" component={AuthStack} />
      <Stack.Screen name="CustomerTabs" component={CustomerTabs} />
      <Stack.Screen name="ProviderTabs" component={ProviderTabs} />
    </Stack.Navigator>
  );
};

export default AppNavigator;
