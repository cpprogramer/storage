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

        private async UniTask Start()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;

            if ( _debugPlayerMode )
            {
                for ( var i = 1; i <= 4; i++ )
                {
                   var gameInst = CreateInstance( i );
                   await gameInst.InitializeAsync(i, _debugPlayerMode);
                   gameInst.Run();
                }
            }
            else
            {
                var gameInst = CreateInstance( 0 );
                await gameInst.InitializeAsync(0, _debugPlayerMode);
                gameInst.Run();
            }
        }

        private GameInstance CreateInstance( int id )
        {
            var go = new GameObject( $"Instance_{id}" );
            return go.AddComponent< GameInstance >();//.Setup( id, _debugPlayerMode );
        }
    }
}