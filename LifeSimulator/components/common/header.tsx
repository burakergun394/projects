import React, { useState } from "react";
import { useTranslation } from "react-i18next";
import { Modal, TouchableWithoutFeedback } from "react-native";
import Flag from "react-native-round-flags";
import styled, { DefaultTheme } from "styled-components/native";

const Container = styled.View`
  flex-direction: row;
  justify-content: flex-end;
  align-items: center;
  padding: 20px 24px;
`;

const LanguageSelector = styled.TouchableOpacity`
  flex-direction: row;
  align-items: center;
  border-radius: 16px;
  padding: 12px 16px;
  min-width: 80px;
`;

const FlagContainer = styled.View<{ size?: "small" | "large" }>`
  width: ${({ size }: { size?: "small" | "large" }) =>
    size === "large" ? "32px" : "24px"};
  height: ${({ size }: { size?: "small" | "large" }) =>
    size === "large" ? "32px" : "24px"};
  border-radius: ${({ size }: { size?: "small" | "large" }) =>
    size === "large" ? "16px" : "12px"};
  margin-right: ${({ size }: { size?: "small" | "large" }) =>
    size === "large" ? "12px" : "8px"};
  overflow: hidden;
`;

const LanguageText = styled.Text`
  color: ${({ theme }: { theme: DefaultTheme }) => theme.text};
  font-size: 12px;
  font-weight: 600;
  text-transform: uppercase;
  margin-right: 4px;
`;

const DropdownArrow = styled.Text`
  color: ${({ theme }: { theme: DefaultTheme }) => theme.textMuted};
  font-size: 12px;
`;

const DropdownModal = styled(Modal)``;

const DropdownOverlay = styled.View`
  flex: 1;
  background-color: rgba(0, 0, 0, 0.3);
  justify-content: flex-start;
  align-items: flex-end;
  padding-top: 80px;
  padding-right: 24px;
`;

const DropdownContent = styled.View`
  background-color: ${({ theme }: { theme: DefaultTheme }) => theme.surface};
  border-radius: 16px;
  border-width: 1px;
  border-color: ${({ theme }: { theme: DefaultTheme }) => theme.borderLight};
  shadow-color: #000000;
  shadow-offset: 0px 8px;
  shadow-opacity: 0.2;
  shadow-radius: 16px;
  elevation: 8;
  min-width: 220px;
`;

const LanguageOption = styled.TouchableOpacity<{
  isFirst?: boolean;
  isLast?: boolean;
}>`
  flex-direction: row;
  align-items: center;
  padding: 16px 20px;
  border-top-left-radius: ${({ isFirst }: { isFirst?: boolean }) =>
    isFirst ? "16px" : "0px"};
  border-top-right-radius: ${({ isFirst }: { isFirst?: boolean }) =>
    isFirst ? "16px" : "0px"};
  border-bottom-left-radius: ${({ isLast }: { isLast?: boolean }) =>
    isLast ? "16px" : "0px"};
  border-bottom-right-radius: ${({ isLast }: { isLast?: boolean }) =>
    isLast ? "16px" : "0px"};
`;

const OptionLanguageText = styled.Text`
  color: ${({ theme }: { theme: DefaultTheme }) => theme.text};
  font-size: 14px;
  font-weight: 500;
  flex: 1;
`;

const Separator = styled.View`
  height: 1px;
  background-color: ${({ theme }: { theme: DefaultTheme }) =>
    theme.borderLight};
  margin-horizontal: 16px;
`;

const FlagComponent: React.FC<{
  country: string;
  size?: "small" | "large";
}> = ({ country, size = "small" }) => {
  const displaySize = size === "large" ? 32 : 24;
  
  return (
    <Flag 
      code={country.toUpperCase()} 
      size={64}
      style={{
        width: displaySize,
        height: displaySize,
        borderRadius: size === "large" ? 16 : 12,
      }}
    />
  );
};

const getCountryData = (language: string) => {
  switch (language) {
    case "tr":
      return {
        flag: "tr",
        label: "Türkçe",
        shortLabel: "Türkçe",
        code: "tr",
        displayCode: "TR",
      };
    case "en":
      return {
        flag: "us",
        label: "English",
        shortLabel: "English",
        code: "en",
        displayCode: "EN",
      };
    default:
      return {
        flag: "us",
        label: "Unknown",
        shortLabel: "Unknown",
        code: language,
        displayCode: "UN",
      };
  }
};

export const Header: React.FC = () => {
  const { i18n } = useTranslation();
  const [isDropdownOpen, setIsDropdownOpen] = useState(false);

  const changeLanguage = (lang: string) => {
    i18n.changeLanguage(lang);
    setIsDropdownOpen(false);
  };

  const languages = [getCountryData("tr"), getCountryData("en")];

  const currentLanguage = getCountryData(i18n.language);

  return (
    <Container>
      <LanguageSelector onPress={() => setIsDropdownOpen(true)}>
        <FlagContainer size="small">
          <FlagComponent country={currentLanguage.flag} size="small" />
        </FlagContainer>
        <LanguageText>{currentLanguage.displayCode}</LanguageText>
        <DropdownArrow>▼</DropdownArrow>
      </LanguageSelector>

      <DropdownModal
        visible={isDropdownOpen}
        transparent
        animationType="fade"
        onRequestClose={() => setIsDropdownOpen(false)}
      >
        <TouchableWithoutFeedback onPress={() => setIsDropdownOpen(false)}>
          <DropdownOverlay>
            <TouchableWithoutFeedback>
              <DropdownContent>
                {languages.map((lang, index) => (
                  <React.Fragment key={lang.code}>
                    <LanguageOption
                      isFirst={index === 0}
                      isLast={index === languages.length - 1}
                      onPress={() => changeLanguage(lang.code)}
                    >
                      <FlagContainer size="large">
                        <FlagComponent country={lang.flag} size="large" />
                      </FlagContainer>
                      <OptionLanguageText>{lang.label}</OptionLanguageText>
                    </LanguageOption>
                    {index < languages.length - 1 && <Separator />}
                  </React.Fragment>
                ))}
              </DropdownContent>
            </TouchableWithoutFeedback>
          </DropdownOverlay>
        </TouchableWithoutFeedback>
      </DropdownModal>
    </Container>
  );
};
