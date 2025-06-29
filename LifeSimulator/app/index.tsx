import React from "react";
import { SafeAreaView } from "react-native-safe-area-context";
import {
  BackgroundGradient,
  Container,
  Header,
  Version,
} from "../components/common";
import { LogoSection, MenuSection, QuoteSection } from "../components/home";

export default function HomeScreen() {
  return (
    <BackgroundGradient>
      <SafeAreaView style={{ flex: 1 }}>
        <Container flex={1} justify="space-between">
          <Container>
            <Header />
          </Container>
          <Container flex={1} justify="space-evenly" align="center">
            <LogoSection />
            <QuoteSection />
            <MenuSection />
          </Container>
          <Version />
        </Container>
      </SafeAreaView>
    </BackgroundGradient>
  );
}
