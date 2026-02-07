import React, { useState } from 'react';
import { View, Text, TextInput, StyleSheet } from 'react-native';
import { Colors } from '../../constants/colors';
import { Styles } from '../../constants/styles';
import Button from '../../components/common/Button';
import type { StackScreenProps } from '@react-navigation/stack';
import type { AuthStackParamList } from '../../navigation/types';

type LoginScreenProps = StackScreenProps<AuthStackParamList, 'Login'>;

const LoginScreen: React.FC<LoginScreenProps> = ({ navigation }) => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  return (
    <View style={Styles.authContainer}>
      <Text style={Styles.authTitle}>KolayRandevu</Text>
      <Text style={[Styles.subtitle, localStyles.subtitleSpacing]}>Easy Appointment</Text>

      <TextInput
        style={Styles.input}
        placeholder="Email"
        value={email}
        onChangeText={setEmail}
        keyboardType="email-address"
        autoCapitalize="none"
      />
      <TextInput
        style={Styles.input}
        placeholder="Password"
        value={password}
        onChangeText={setPassword}
        secureTextEntry
      />

      <Button title="Login as Customer" onPress={() => (navigation as any).replace('CustomerTabs')} />
      <View style={localStyles.spacer} />
      <Button
        title="Login as Provider"
        onPress={() => (navigation as any).replace('ProviderTabs')}
        style={localStyles.providerButton}
      />
      <Text style={Styles.link} onPress={() => navigation.navigate('Register')}>
        Don't have an account? Register
      </Text>
    </View>
  );
};

const localStyles = StyleSheet.create({
  subtitleSpacing: {
    marginBottom: 40,
  },
  spacer: {
    height: 12,
  },
  providerButton: {
    backgroundColor: Colors.primary,
  },
});

export default LoginScreen;
