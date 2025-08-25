using Common;
using Cysharp.Threading.Tasks;
using Moq;
using NUnit.Framework;
using StorageTest;
using System;

public sealed class TestAllFSM
{
    private GameRoot _gameRoot;

    [ SetUp ]
    public void Setup() =>
        _gameRoot = new GameRoot( 0, new Mock< ITickable >().Object, new Mock< IParentHolder >().Object, false );

    [ TestCase ] public void Test_AllFSMs() => Test().Forget();

    private async UniTaskVoid Test()
    {
        try
        {
            _gameRoot.Create();
            await _gameRoot.InitializeAsync();
            _gameRoot.Run();
            _gameRoot.TestFSM();
        }
        catch ( Exception e )
        {
            Console.WriteLine( e );
            throw;
        }
    }

    // A Test behaves as an ordinary method
    /*[ TestCase( 0, 0, ExpectedResult = 0 ) ] //+
    [ TestCase( 10, 29, ExpectedResult = 14 ) ]
    [ TestCase( 6, 15, ExpectedResult = 12 ) ]
    [ TestCase( 3, 15, ExpectedResult = 10 ) ]
    [ TestCase( 4, 15, ExpectedResult = 10 ) ]
    public int TestSpeedBonus_Coins( int countInChain, int totalCount )
    {
        return 0;
    }*/
}