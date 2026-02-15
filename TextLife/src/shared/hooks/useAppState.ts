import { useEffect, useRef } from 'react';
import { AppState } from 'react-native';

import type { AppStateStatus } from 'react-native';

/** Uygulama arka plana geçtiğinde callback çalıştır */
export const useAppState = (onBackground?: () => void) => {
  const appState = useRef(AppState.currentState);

  useEffect(() => {
    const sub = AppState.addEventListener('change', (nextState: AppStateStatus) => {
      if (
        appState.current === 'active' &&
        (nextState === 'background' || nextState === 'inactive')
      ) {
        onBackground?.();
      }
      appState.current = nextState;
    });

    return () => sub.remove();
  }, [onBackground]);
};
