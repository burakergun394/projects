import React from "react";
import styled from "styled-components/native";
import Logo from "../../assets/logo.svg";

const LogoContainer = styled.View`
  align-items: center;
  justify-content: center;
  margin-top: 10px;
  margin-bottom: 0px;
`;

interface LogoSectionProps {
  logoSize?: number;
}

export const LogoSection: React.FC<LogoSectionProps> = ({
  logoSize = 140,
}) => {
  return (
    <LogoContainer>
      <Logo width={logoSize} height={logoSize} />
    </LogoContainer>
  );
}; 