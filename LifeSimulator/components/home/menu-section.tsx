import { LinearGradient } from "expo-linear-gradient";
import React from "react";
import { useTranslation } from "react-i18next";
import styled from "styled-components/native";

const MenuContainer = styled.View`
  margin-horizontal: 40px;
  width: 100%;
  max-width: 300px;
  align-self: center;
`;

const MenuButton = styled.TouchableOpacity`
  margin-bottom: 16px;
  border-radius: 12px;
  overflow: hidden;
  shadow-color: #000000;
  shadow-offset: 0px 4px;
  shadow-opacity: 0.1;
  shadow-radius: 8px;
  elevation: 4;
  height: 80px;
`;

const MenuButtonContent = styled.View`
  padding: 20px 30px;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  flex: 1;
  position: relative;
`;

const MenuButtonTitle = styled.Text`
  color: white;
  font-size: 18px;
  font-weight: 700;
  text-align: center;
  margin-bottom: 4px;
`;

const MenuButtonSubtitle = styled.Text`
  color: white;
  font-size: 12px;
  font-weight: 400;
  text-align: center;
  opacity: 0.9;
`;

export const MenuSection: React.FC = () => {
  const { t } = useTranslation();

  const menuItems = [
    { 
      key: "newGame", 
      colors: ["#059669", "#10b981"],
    },
    { 
      key: "continue", 
      colors: ["#06b6d4", "#0891b2"],
    },
    { 
      key: "settings", 
      colors: ["#3b82f6", "#2563eb"],
    },
  ];

  return (
    <MenuContainer>
      {menuItems.map((item) => (
        <MenuButton key={item.key}>
          <LinearGradient 
            colors={item.colors as [string, string]} 
            style={{ flex: 1 }}
            start={{ x: 0, y: 0 }}
            end={{ x: 1, y: 0 }}
          >
            <MenuButtonContent>
              <MenuButtonTitle>{t(`menu.${item.key}`)}</MenuButtonTitle>
              <MenuButtonSubtitle>{t(`menu.${item.key}Subtitle`)}</MenuButtonSubtitle>
            </MenuButtonContent>
          </LinearGradient>
        </MenuButton>
      ))}
    </MenuContainer>
  );
};
