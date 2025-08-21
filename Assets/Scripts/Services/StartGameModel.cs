using Common.Models;

namespace StorageTest.Lobby
{
    public sealed class StartGameModel : IBaseModel
    {
        public PlayerInfo[] Players { get; }
        public string SceneName { get; }
        public string LevelName { get; }

        public StartGameModel( string sceneName, string levelName, PlayerInfo[] players )
        {
            SceneName = sceneName;
            LevelName = levelName;
            Players = players;
        }
    }
}