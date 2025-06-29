import React, { useEffect, useState } from "react";
import { useTranslation } from "react-i18next";
import { Platform } from "react-native";
import styled from "styled-components/native";

const QuoteContainer = styled.View`
  align-items: center;
  justify-content: center;
  padding: 20px 30px;
  margin: 0 30px;
  min-height: 80px;
`;

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

export const QuoteSection: React.FC = () => {
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
    <QuoteContainer>
      <QuoteText>"{randomQuote}"</QuoteText>
    </QuoteContainer>
  );
}; 