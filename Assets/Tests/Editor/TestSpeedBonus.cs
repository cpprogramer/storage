using Common.UI;
using Common.UI.Messages;
using Configs;
using MonopolySpace.UI.Controllers;
using NUnit.Framework;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UniRx;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class TestSpeedBonus
{
    private SpeedBonusHandler _speedBonusHandler;
    private IMessageBroker _messageBroker;

    public sealed class SpeedBonusHandler
    {
        public SpeedBonusHandler( IGamePlayConfig config ) {}

        public void Initialize() {}

        public void CollectItem() {}

        public int GetCountBonusCoin( int count ) => 0;
    }

    [ SetUp ]
    public void Setup()
    {
        var gamePlayConfig = Resources.Load< GamePlayConfig >( "Configs/GamePlayConfig" );
        _speedBonusHandler = new SpeedBonusHandler( gamePlayConfig ); //Mock Stub
        _messageBroker = new MessageBroker();
    }

    // A Test behaves as an ordinary method
    [ TestCase( 0, 0, ExpectedResult = 0 ) ] //+
    [ TestCase( 10, 29, ExpectedResult = 14 ) ]
    [ TestCase( 6, 15, ExpectedResult = 12 ) ]
    [ TestCase( 3, 15, ExpectedResult = 10 ) ]
    [ TestCase( 4, 15, ExpectedResult = 10 ) ]
    public int TestSpeedBonus_Coins( int countInChain, int totalCount )
    {
        _speedBonusHandler.Initialize();
        IEnumerable< int > enumerable = Enumerable.Repeat( 1, countInChain );
        foreach ( int item in enumerable ) _speedBonusHandler.CollectItem();
        int result = _speedBonusHandler.GetCountBonusCoin( totalCount );
        Debug.Log( $"Coins={result}" );
        return result;
    }

    [ TestCase ]
    public void TestSpeedBonus_UniRx()
    {
        _messageBroker.Receive< UICloseWindowMessage >().Subscribe( UICloseWindowMessageHandler );

        var stopWatch = new Stopwatch();
        stopWatch.Start();
        for ( var i = 0; i < 1000; i++ )
        {
            _messageBroker.Publish( new UICloseWindowMessage( typeof(UIMainMenuController), WindowResult.Back ) );
        }

        stopWatch.Stop();
        Debug.Log( $"Total time={stopWatch.ElapsedMilliseconds} ms" );
    }

    private void UICloseWindowMessageHandler( UICloseWindowMessage msg ) { Debug.Log( "Close Window" ); }
}