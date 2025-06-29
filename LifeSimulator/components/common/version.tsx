import React from "react";
import { useTranslation } from "react-i18next";
import styled, { DefaultTheme } from "styled-components/native";

const Container = styled.View`
  align-items: center;
  padding: 24px;
  backdrop-filter: blur(10px);
`;

const VersionText = styled.Text`
  color: ${({ theme }: { theme: DefaultTheme }) => theme.textMuted};
  font-size: 11px;
  font-weight: 400;
`;

interface VersionProps {
  version?: string;
}

export const Version: React.FC<VersionProps> = ({ version = "1.0.0" }) => {
  const { t } = useTranslation();

  return (
    <Container>
      <VersionText>{`${t("version")} ${version}`}</VersionText>
    </Container>
  );
};
