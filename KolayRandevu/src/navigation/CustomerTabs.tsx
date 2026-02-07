import React from 'react';
import { createBottomTabNavigator } from '@react-navigation/bottom-tabs';
import HomeScreen from '../screens/customer/HomeScreen';
import BookingScreen from '../screens/customer/BookingScreen';
import MyAppointmentsScreen from '../screens/customer/MyAppointmentsScreen';
import { Colors } from '../constants/colors';
import { headerScreenOptions } from '../constants/styles';
import { CustomerTabParamList } from './types';

const Tab = createBottomTabNavigator<CustomerTabParamList>();

const CustomerTabs: React.FC = () => {
  return (
    <Tab.Navigator
      screenOptions={{
        ...headerScreenOptions,
        tabBarActiveTintColor: Colors.cta,
        tabBarInactiveTintColor: Colors.gray,
      }}
    >
      <Tab.Screen
        name="Home"
        component={HomeScreen}
        options={{ title: 'Search' }}
      />
      <Tab.Screen
        name="Booking"
        component={BookingScreen}
        options={{ title: 'Book' }}
      />
      <Tab.Screen
        name="MyAppointments"
        component={MyAppointmentsScreen}
        options={{ title: 'My Appointments' }}
      />
    </Tab.Navigator>
  );
};

export default CustomerTabs;
