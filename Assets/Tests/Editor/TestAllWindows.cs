using Common;
using Cysharp.Threading.Tasks;
using StorageTest;
using Moq;
using NUnit.Framework;
using System;

public sealed class TestAllWindows
{

    private GameRoot _gameRoot;
    
    [ SetUp ]
    public void Setup()
    {
        _gameRoot = new GameRoot( 0,new Mock<ITickable>().Object, new Mock<IParentHolder>().Object, false );
    }

    [ TestCase()]
    public async UniTask Test_Init( )
    {
        try
        {
            _gameRoot.Create();
            await _gameRoot.InitializeAsync();
            _gameRoot.TestUI();
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