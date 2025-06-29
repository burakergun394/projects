import { LinearGradient } from "expo-linear-gradient";
import React from "react";
import styled, { useTheme } from "styled-components/native";

const GradientContainer = styled(LinearGradient)`
  flex: 1;
`;

interface BackgroundGradientProps {
  children: React.ReactNode;
  colors?: string[];
  locations?: number[];
  start?: { x: number; y: number };
  end?: { x: number; y: number };
}

export const BackgroundGradient: React.FC<BackgroundGradientProps> = ({
  children,
  colors,
  locations = [0, 0.5, 1],
  start = { x: 0, y: 0 },
  end = { x: 1, y: 1 },
}) => {
  const theme = useTheme();

  const defaultColors = [
    theme.backgroundPrimary,
    theme.backgroundSecondary,
    theme.backgroundPrimary,
  ];

  return (
    <GradientContainer
      colors={colors || defaultColors}
      start={start}
      end={end}
      locations={locations}
    >
      {children}
    </GradientContainer>
  );
};
