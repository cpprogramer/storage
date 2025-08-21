using System.Diagnostics;
using System.Linq;
using Common;
using StorageTest.Model;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Debug = UnityEngine.Debug;

namespace StorageTest.GamePlay
{
    public sealed class LevelView : MonoBehaviour, ILevelView
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _parentForPlayers;
        [SerializeField] private Transform[] _spawnPoints;
       
        private Vector2 _sideMaxMin;

        public Vector3[] SpawnPoints => _spawnPoints.Select(x => x.position).ToArray();

        [Conditional("UNITY_EDITOR")]
        private void OnValidate()
        {
            Debug.Assert(_parentForPlayers != null, nameof(_parentForPlayers) + " == null");
            Debug.Assert(_spawnPoints.All(item => item != null), nameof(_spawnPoints) + " == null");
        }

        public void SetCameraOverlay(Camera main, Rect cameraDataViewPort)
        {
            var data = _camera.GetUniversalAdditionalCameraData();
            data.cameraStack.Add(main);
            _camera.rect = cameraDataViewPort;
        }
    }
}