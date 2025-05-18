import { Stack } from "expo-router";
import { StatusBar } from "expo-status-bar";
import { View } from "react-native";

export default function Layout() {
  return (
    <View>
      <StatusBar style="dark" />
      <Stack
        screenOptions={{
          headerStyle: { backgroundColor: "#f0f0f0" },
          headerTintColor: "#333",
          headerTitleAlign: "center",
        }}
      >
        <Stack.Screen name="index" options={{ title: "Ana Sayfa" }} />
      </Stack>
    </View>
  );
}
