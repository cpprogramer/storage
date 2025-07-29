namespace MonopolySpace.Net
{
    public static class MatchmakingConst
    {
        public const int BET_SLIDER_COUNT_STEPS = 7;
        public const string Closed = "closed";
        public const string Invite = "invite";

        public const int MinPlayersStartGameNoTeamMode = 2;
        public const int MinPlayersStartGameTeamMode = 4;
        public const int MinPlayersStartTournament = 4;
        public const int MaxPlayersInGameRoom = 4;
        public const int MaxPlayersInWaitingRoom = 2;

        public const float StartMatchCountdown = 15f;

        public const int RoomClosed = 1;
        public const int RoomOpened = 0;
        public const string ModeWithBet = "with_bet";
        public const string BetValue = "bet";
        public const string ModeTwoWithTwo = "team";
        public const string ModeWithBoosters = "boosters";
        public const string ModeQuickGame = "quick";
        public const string Tournament = "tournament";
        public const string BetIndexesArray = "bet_indexes_array";
        public const string ModeWithFriend = "mode_with_friend";
        public const string ModeBetsHigher = "bets_higher";
    }
}