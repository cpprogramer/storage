using Common;
using Cysharp.Threading.Tasks;

namespace StorageTest.Model
{
    public sealed class HangarGamePlayManager
    {
        private readonly Hangar _hangar;
        private readonly HangarView _hangarView;

        public HangarGamePlayManager( IScenesManager scenesManager, int instanceId )
        {
            _hangar = new Hangar();
            _hangarView = new HangarView( _hangar, scenesManager, instanceId );
        }

        public void Start() => CreateAndStart().Forget();

        private async UniTaskVoid CreateAndStart()
        {
            _hangar.Initialize();
            await _hangarView.Initialize();
            _hangar.Start();
        }
    }
}