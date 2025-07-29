using UnityEngine;

namespace Configs
{
    public sealed class GamePlayConfig : ScriptableObject, IGamePlayConfig
    {
        [ SerializeField ] private int _waitDecisionTimeInSec = 5;
        [ SerializeField ] private int _waitMakeTurnTimeInSec = 5;
        public int WaitDecisionTimeInSec => _waitDecisionTimeInSec;

        public int WaitMakeTurnTimeInSec => _waitMakeTurnTimeInSec;
    }
}