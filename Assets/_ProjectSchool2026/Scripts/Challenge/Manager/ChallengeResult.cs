using ProjectSchool2026.Challenge.Behaviour;
using ProjectSchool2026.Challenge.Timer;
using ProjectSchool2026.Manager;
using UnityEngine;

namespace ProjectSchool2026.Challenge.Manager
{
    public class ChallengeResult : BootBehaviour
    {
        [SerializeField] private ScoreManager _scoreManager;
        [SerializeField] private ChallengeTimer _challengeTimer;
        [SerializeField] private ClickManager _clickManager;
        [SerializeField] private Arrow.Arrow _arrow;

        private bool _isFinished;

        public override void Boot()
        {
            _scoreManager.OnTargetScoreReached += Win;
            _challengeTimer.OnTimeIsUp += Lose;
        }

        private void Win()
        {
            Finish(true);
        }

        private void Lose()
        {
            Finish(false);
        }

        private void Finish(bool isWin)
        {
            if (_isFinished)
            {
                return;
            }
            _isFinished = true;

            _challengeTimer.StopTimer();
            _clickManager.DisableClicks();
            _arrow.StopRotate();

            if (InteractionManager.Instance != null)
            {
                InteractionManager.Instance.FinishChallenge(isWin);
            }
        }

        private void OnDestroy()
        {
            _scoreManager.OnTargetScoreReached -= Win;
            _challengeTimer.OnTimeIsUp -= Lose;
        }
    }
}
