import { Stack } from "expo-router";
import React from "react";
import { View } from "react-native";

export default function NotFoundScreen() {
  return (
    <View>
      <Stack.Screen options={{ title: "Oops!" }} />
    </View>
  );
}
