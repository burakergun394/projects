import { useCallback } from 'react';

import { useGameStore } from '../stores/gameStore';

import type { Education } from '../types';

export const useGameEngine = () => {
  const ageUp = useGameStore((s) => s.ageUp);
  const applyForJob = useGameStore((s) => s.applyForJob);
  const quitJob = useGameStore((s) => s.quitJob);
  const retire = useGameStore((s) => s.retire);
  const startEdu = useGameStore((s) => s.startEdu);
  const doAction = useGameStore((s) => s.doAction);
  const takeExam = useGameStore((s) => s.takeExam);
  const selectDepartment = useGameStore((s) => s.selectDepartment);
  const newGame = useGameStore((s) => s.newGame);

  const handleAgeUp = useCallback(() => ageUp(), [ageUp]);
  const handleApplyForJob = useCallback((jobId: string) => applyForJob(jobId), [applyForJob]);
  const handleQuitJob = useCallback(() => quitJob(), [quitJob]);
  const handleRetire = useCallback(() => retire(), [retire]);
  const handleStartEdu = useCallback(
    (edu: Education) => startEdu(edu),
    [startEdu],
  );
  const handleDoAction = useCallback(
    (actionId: string) => doAction(actionId),
    [doAction],
  );
  const handleTakeExam = useCallback(() => takeExam(), [takeExam]);
  const handleSelectDepartment = useCallback(
    (deptId: string) => selectDepartment(deptId),
    [selectDepartment],
  );
  const handleNewGame = useCallback(() => newGame(), [newGame]);

  return {
    ageUp: handleAgeUp,
    applyForJob: handleApplyForJob,
    quitJob: handleQuitJob,
    retire: handleRetire,
    startEdu: handleStartEdu,
    doAction: handleDoAction,
    takeExam: handleTakeExam,
    selectDepartment: handleSelectDepartment,
    newGame: handleNewGame,
  };
};
