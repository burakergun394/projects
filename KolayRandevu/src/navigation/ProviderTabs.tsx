import React from 'react';
import { createBottomTabNavigator } from '@react-navigation/bottom-tabs';
import DashboardScreen from '../screens/provider/DashboardScreen';
import ScheduleScreen from '../screens/provider/ScheduleScreen';
import RequestsScreen from '../screens/provider/RequestsScreen';
import { Colors } from '../constants/colors';
import { ProviderTabParamList } from './types';

const Tab = createBottomTabNavigator<ProviderTabParamList>();

const ProviderTabs: React.FC = () => {
  return (
    <Tab.Navigator
      screenOptions={{
        headerStyle: { backgroundColor: Colors.primary },
        headerTintColor: Colors.white,
        headerTitleStyle: { fontWeight: 'bold' },
        tabBarActiveTintColor: Colors.cta,
        tabBarInactiveTintColor: Colors.gray,
      }}
    >
      <Tab.Screen
        name="Dashboard"
        component={DashboardScreen}
        options={{ title: 'Dashboard' }}
      />
      <Tab.Screen
        name="Schedule"
        component={ScheduleScreen}
        options={{ title: 'Schedule' }}
      />
      <Tab.Screen
        name="Requests"
        component={RequestsScreen}
        options={{ title: 'Requests' }}
      />
    </Tab.Navigator>
  );
};

export default ProviderTabs;
