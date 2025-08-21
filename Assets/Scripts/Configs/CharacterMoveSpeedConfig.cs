using UnityEngine;

namespace StorageTest.Configs
{
    public sealed class CharacterMoveSpeedConfig : ScriptableObject
    {
        [ SerializeField ] private float _speedForward = 10;
        [ SerializeField ] private float _speedBackward = 5;
        [ SerializeField ] private float _speedSide = 3;

        public float GetAverageSpeedByDirection( Vector2 data )
        {
            float leftRight = Mathf.Abs( Mathf.Lerp( -_speedSide, _speedSide, ( data.x + 1 ) * 0.5f ) );
            float forwardBackward = Mathf.Abs( Mathf.Lerp( _speedBackward, _speedForward, ( data.y + 1 ) * 0.5f ) );

            float averageSpeed = ( leftRight + forwardBackward ) * 0.5f;
            return averageSpeed;
        }
    }
}