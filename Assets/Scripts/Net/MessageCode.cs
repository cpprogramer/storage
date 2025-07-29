namespace MonopolySpace.Net
{
    public enum MessageCode
    {
        Undefined,
        RequestPlayerCreation,
        //TODO - combine UPDATE_PLAYER and UPDATE_PLAYERS cases.
        UpdatePlayer,
        UpdatePlayers,
        DeletePlayer,
        SwapPlayer,
        SwitchPlayerId,
        PlayerConnected,

        Emoji,

        PlayerDecision,
        DecisionsHistoryObtained,
        ReconnectDeclined,
        ReturnPlayers,
        StartPlay,
        StartSearchRoom,
        BetManagerStartConfirmation,
        BetManagerStartReadyConfirmation,
        //TODO - combine GOTO_WAITING_ROOM and REQUEST_GOTO_WAITING_ROOM into a single parameter.
        //игра со ставкой - пригласивший - жмет отмена и уведомляет приглашенного о выходе в комнату ожидания
        GotoWaitingRoom,
        //игра со ставкой - приглашенный - жмет отмена и запрашивает у пригласившего выход в комнату ожидания
        RequestGotoWaitingRoom,
        BetManagerFinishGameData,
        AllPlacesAreOccupied,
        Leave,
        ForceGameEnd,
        Smile,
        RequestStartGameTimer,
        UpdateStartGameTimer
    }
}