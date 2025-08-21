using Cysharp.Threading.Tasks;
using System.Globalization;
using System.Threading;
using UnityEngine;

namespace StorageTest
{
    public sealed class Bootstrapper : MonoBehaviour
    {
        [ Header( "вкл 4 окна" ) ]
        [ SerializeField ]
        private bool _debugPlayerMode;

        private void Start()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;

            if ( _debugPlayerMode )
            {
                for ( var i = 1; i <= 4; i++ )
                {
                    CreateInstance( i );
                }
            }
            else
            {
                CreateInstance( 0 );
            }
        }

        private void CreateInstance( int id )
        {
            var go = new GameObject( $"Instance_{id}" );
            go.AddComponent< GameInstance >().Setup( id, _debugPlayerMode );
        }
    }
}