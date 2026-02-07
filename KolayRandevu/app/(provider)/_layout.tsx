import { Tabs } from 'expo-router';
import { useTranslation } from 'react-i18next';
import { Image } from 'expo-image';

function TabIcon({ name, focused }: { name: string; focused: boolean }) {
  return (
    <Image
      source={`sf:${name}`}
      style={{ width: 24, height: 24 }}
      tintColor={focused ? '#1A237E' : '#9CA3AF'}
    />
  );
}

export default function ProviderLayout() {
  const { t } = useTranslation();

  return (
    <Tabs
      screenOptions={{
        headerShown: false,
        tabBarActiveTintColor: '#1A237E',
        tabBarInactiveTintColor: '#9CA3AF',
        tabBarStyle: {
          borderTopColor: '#f3f4f6',
        },
      }}
    >
      <Tabs.Screen
        name="dashboard"
        options={{
          title: t('dashboard.title'),
          tabBarIcon: ({ focused }) => <TabIcon name="house.fill" focused={focused} />,
        }}
      />
      <Tabs.Screen
        name="schedule"
        options={{
          title: t('schedule.title'),
          tabBarIcon: ({ focused }) => <TabIcon name="calendar" focused={focused} />,
        }}
      />
      <Tabs.Screen
        name="requests"
        options={{
          title: t('requests.title'),
          tabBarIcon: ({ focused }) => <TabIcon name="bell.fill" focused={focused} />,
        }}
      />
      <Tabs.Screen
        name="customers"
        options={{
          title: t('customers.title'),
          tabBarIcon: ({ focused }) => <TabIcon name="person.2.fill" focused={focused} />,
        }}
      />
      <Tabs.Screen
        name="request-detail/[id]"
        options={{ href: null }}
      />
      <Tabs.Screen
        name="customer-detail/[id]"
        options={{ href: null }}
      />
      <Tabs.Screen
        name="day-detail/[day]"
        options={{ href: null }}
      />
    </Tabs>
  );
}
