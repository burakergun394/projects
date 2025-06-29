declare module 'react-native-round-flags' {
  import { ComponentType } from 'react';
    import { ImageStyle } from 'react-native';

  interface FlagProps {
    code: string;
    size?: number;
    style?: ImageStyle;
  }

  const Flag: ComponentType<FlagProps>;
  export default Flag;
} 