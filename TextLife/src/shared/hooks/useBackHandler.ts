import { useEffect } from 'react';
import { BackHandler } from 'react-native';

/** Android geri tuşu yönetimi */
export const useBackHandler = (handler: () => boolean) => {
  useEffect(() => {
    const sub = BackHandler.addEventListener('hardwareBackPress', handler);
    return () => sub.remove();
  }, [handler]);
};
