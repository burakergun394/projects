import { useCallback } from 'react';

import { useGameStore } from '../stores/gameStore';

import type { Job, Education } from '../types';

export const useGameEngine = () => {
  const ageUp = useGameStore((s) => s.ageUp);
  const getJob = useGameStore((s) => s.getJob);
  const quitJob = useGameStore((s) => s.quitJob);
  const startEdu = useGameStore((s) => s.startEdu);
  const doAction = useGameStore((s) => s.doAction);
  const newGame = useGameStore((s) => s.newGame);

  const handleAgeUp = useCallback(() => ageUp(), [ageUp]);
  const handleGetJob = useCallback((job: Job) => getJob(job), [getJob]);
  const handleQuitJob = useCallback(() => quitJob(), [quitJob]);
  const handleStartEdu = useCallback(
    (edu: Education) => startEdu(edu),
    [startEdu],
  );
  const handleDoAction = useCallback(
    (actionId: string) => doAction(actionId),
    [doAction],
  );
  const handleNewGame = useCallback(() => newGame(), [newGame]);

  return {
    ageUp: handleAgeUp,
    getJob: handleGetJob,
    quitJob: handleQuitJob,
    startEdu: handleStartEdu,
    doAction: handleDoAction,
    newGame: handleNewGame,
  };
};
