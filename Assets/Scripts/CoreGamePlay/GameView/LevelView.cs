using StorageTest.Model;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Debug = UnityEngine.Debug;

namespace StorageTest.GamePlay
{
    public sealed class LevelView : MonoBehaviour, ILevelView
    {
        [ SerializeField ] private Camera _camera;
        [ SerializeField ] private Transform _parentForPlayers;
        [ SerializeField ] private Transform[] _spawnPoints;

        public Vector3[] SpawnPoints => _spawnPoints.Select( x => x.position ).ToArray();

        private Vector2 _sideMaxMin;

        public void SetCameraOverlay( Camera main, Rect cameraDataViewPort )
        {
            UniversalAdditionalCameraData data = _camera.GetUniversalAdditionalCameraData();
            data.cameraStack.Add( main );
            _camera.rect = cameraDataViewPort;
        }

        [ Conditional( "UNITY_EDITOR" ) ]
        private void OnValidate()
        {
            Debug.Assert( _parentForPlayers != null, nameof(_parentForPlayers) + " == null" );
            Debug.Assert( _spawnPoints.All( item => item != null ), nameof(_spawnPoints) + " == null" );
        }
    }
}