import React, { useEffect, useState } from "react";
import { useTranslation } from "react-i18next";
import { Platform } from "react-native";
import styled from "styled-components/native";
import Logo from "../../assets/logo.svg";
import { Container } from "../common/container";

const QuoteText = styled.Text`
  font-size: 16px;
  font-weight: 300;
  color: #2c3e50;
  text-align: center;
  line-height: 24px;
  font-family: ${Platform.OS === 'ios' ? 'Georgia' : 'serif'};
  font-style: italic;
  opacity: 0.9;
`;

interface WelcomeSectionProps {
  logoSize?: number;
}

export const WelcomeSection: React.FC<WelcomeSectionProps> = ({
  logoSize = 140,
}) => {
  const { t, i18n } = useTranslation();
  const [randomQuote, setRandomQuote] = useState<string>("");

  useEffect(() => {
    const getRandomQuote = () => {
      const quotes = t('quotes', { returnObjects: true }) as string[];
      const randomIndex = Math.floor(Math.random() * quotes.length);
      return quotes[randomIndex];
    };

    setRandomQuote(getRandomQuote());
  }, [i18n.language, t]);

  return (
    <Container align="center" marginVertical={5} blur>
      <Container align="center" justify="center" style={{ marginTop: 0, marginBottom: 8 }}>
        <Logo width={logoSize} height={logoSize} />
      </Container>
      <Container 
        align="center" 
        justify="center" 
        paddingHorizontal={20} 
        paddingVertical={16}
        marginHorizontal={30}
        style={{ minHeight: 80 }}
      >
        <QuoteText>"{randomQuote}"</QuoteText>
      </Container>
    </Container>
  );
};
