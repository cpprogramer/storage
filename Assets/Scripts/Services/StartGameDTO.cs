using Common.Models;

namespace StorageTest.Lobby
{
    public sealed class StartGameDTO : IBaseDTO
    {
        public PlayerInfo[] Players { get; }
        public string SceneName { get; }
        public string LevelName { get; }

        public StartGameDTO( string sceneName, string levelName, PlayerInfo[] players )
        {
            SceneName = sceneName;
            LevelName = levelName;
            Players = players;
        }
    }
}