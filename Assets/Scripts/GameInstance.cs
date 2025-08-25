using Common;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace StorageTest
{
    public sealed class GameInstance : MonoBehaviour, IParentHolder
    {
        private GameRoot _gameRoot;

        public void Attach( Transform child ) => child.SetParent( transform, false );

        public async UniTask InitializeAsync( int instanceUid, bool debugPlayerMode )
        {
            try
            {
                var tickable = gameObject.AddComponent< Tickable >();
                _gameRoot = new GameRoot( instanceUid, tickable, this, debugPlayerMode );
                _gameRoot.Create();
                await _gameRoot.InitializeAsync();
            }
            catch ( Exception e )
            {
                throw new Exception( $"GameRoot Initialize failed: {e.Message}" );
                throw;
            }
        }

        public void Run()
        {
            if ( _gameRoot == null ) throw new Exception( "GameRoot is null . Initialize first!" );
            _gameRoot.Run();
        }

        private void Awake() => DontDestroyOnLoad( gameObject );
    }
}