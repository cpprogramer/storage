using Common;
using Common.Profile;
using Configs;
using System;
using UnityEngine;

namespace MonopolySpace.Configs
{
    public abstract class CommandHandler : ScriptableObject, ICommand
    {
        public event Action OnCompleted;
        [ SerializeField ] private string EditorName;

        [ SerializeField ] private int _id;
        public string CommandKey => name;
        public int Uid => _id;
        public bool IsBreak { get; set; }

        protected string Args;
        protected IGamePlayConfig GamePlayConfig;
        protected IUserProfile UserProfile;

        public void SetupArgs( string args ) => Args = args;

        protected virtual void Complete()
        {
            Dispose();
            OnCompleted?.Invoke();
        }

        public void Setup( IUserProfile userProfile, IGamePlayConfig gamePlayConfig )
        {
            UserProfile = userProfile ?? throw new ArgumentNullException( nameof(userProfile) );
            GamePlayConfig = gamePlayConfig ?? throw new ArgumentNullException( nameof(gamePlayConfig) );
        }

        public void Dispose() {}

        public abstract void Do();
    }
}