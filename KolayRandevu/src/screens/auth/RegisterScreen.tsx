import React, { useState } from 'react';
import { View, Text, TextInput, StyleSheet } from 'react-native';
import { Colors } from '../../constants/colors';
import Button from '../../components/common/Button';
import type { StackScreenProps } from '@react-navigation/stack';
import type { AuthStackParamList } from '../../navigation/types';

type RegisterScreenProps = StackScreenProps<AuthStackParamList, 'Register'>;

const RegisterScreen: React.FC<RegisterScreenProps> = ({ navigation }) => {
  const [name, setName] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  return (
    <View style={styles.container}>
      <Text style={styles.title}>Create Account</Text>

      <TextInput
        style={styles.input}
        placeholder="Full Name"
        value={name}
        onChangeText={setName}
      />
      <TextInput
        style={styles.input}
        placeholder="Email"
        value={email}
        onChangeText={setEmail}
        keyboardType="email-address"
        autoCapitalize="none"
      />
      <TextInput
        style={styles.input}
        placeholder="Password"
        value={password}
        onChangeText={setPassword}
        secureTextEntry
      />

      <Button title="Register" onPress={() => navigation.goBack()} />
      <Text style={styles.link} onPress={() => navigation.goBack()}>
        Already have an account? Login
      </Text>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: 'center',
    padding: 24,
    backgroundColor: Colors.white,
  },
  title: {
    fontSize: 28,
    fontWeight: 'bold',
    color: Colors.primary,
    textAlign: 'center',
    marginBottom: 40,
  },
  input: {
    borderWidth: 1,
    borderColor: Colors.gray,
    borderRadius: 8,
    padding: 14,
    fontSize: 16,
    marginBottom: 16,
  },
  link: {
    color: Colors.primary,
    textAlign: 'center',
    marginTop: 24,
    fontSize: 14,
  },
});

export default RegisterScreen;
