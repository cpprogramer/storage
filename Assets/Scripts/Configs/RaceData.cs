using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace StorageTest.Configs
{
    [ Serializable ]
    public sealed class RaceData
    {
        [ SerializeField ] private RaceConfig _raceConfig;

        public string Uid => _raceConfig.Uid;

#if UNITY_EDITOR
        public void OnValidate( string context ) => Assert.IsNotNull( _raceConfig, $"_raceConfig == null {context}" );

        public void SetupConfig( RaceConfig item ) => _raceConfig = item;

        public bool IsEmpty() => _raceConfig == default;

#endif
    }
}