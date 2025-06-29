import { Link } from 'expo-router';
import React from 'react';
import { useTranslation } from 'react-i18next';
import { SafeAreaView } from 'react-native-safe-area-context';
import styled, { DefaultTheme } from 'styled-components/native';

const Container = styled(SafeAreaView)`
  flex: 1;
  background-color: ${({ theme }: { theme: DefaultTheme }) => theme.background};
  align-items: center;
  justify-content: center;
  padding: 24px;
`;

const ErrorContainer = styled.View`
  align-items: center;
  margin-bottom: 48px;
`;

const ErrorCode = styled.Text`
  font-size: 64px;
  font-weight: 700;
  color: ${({ theme }: { theme: DefaultTheme }) => theme.primary};
  margin-bottom: 16px;
`;

const ErrorTitle = styled.Text`
  font-size: 20px;
  font-weight: 600;
  color: ${({ theme }: { theme: DefaultTheme }) => theme.text};
  text-align: center;
  margin-bottom: 8px;
`;

const ErrorMessage = styled.Text`
  font-size: 14px;
  color: ${({ theme }: { theme: DefaultTheme }) => theme.textSecondary};
  text-align: center;
  line-height: 20px;
  margin-bottom: 32px;
`;

const BackButton = styled.TouchableOpacity`
  background-color: ${({ theme }: { theme: DefaultTheme }) => theme.primary};
  padding: 12px 24px;
  border-radius: 8px;
  shadow-color: #000000;
  shadow-offset: 0px 2px;
  shadow-opacity: 0.08;
  shadow-radius: 4px;
  elevation: 2;
`;

const BackButtonText = styled.Text`
  color: #FFFFFF;
  font-size: 14px;
  font-weight: 600;
  text-align: center;
`;

export default function NotFoundScreen() {
  const { t } = useTranslation();
  
  return (
    <Container>
      <ErrorContainer>
        <ErrorCode>404</ErrorCode>
        <ErrorTitle>Sayfa Bulunamadı</ErrorTitle>
        <ErrorMessage>
          Aradığınız sayfa mevcut değil veya taşınmış olabilir.
          Ana sayfaya dönmek için aşağıdaki butonu kullanabilirsiniz.
        </ErrorMessage>
      </ErrorContainer>
      
      <Link href="/" asChild>
        <BackButton>
          <BackButtonText>Ana Sayfaya Dön</BackButtonText>
        </BackButton>
      </Link>
    </Container>
  );
}
