using Common;
using Common.Models;
using GameFSM;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class TestFSM
{
    private IFSM _fsm;

    private sealed class TestState1 : BaseFiniteStateMachineObject
    {
        public TestState1( IFSM parentFsm ) : base( parentFsm ) {}

        protected override void OnStart() => Debug.Log( $"Start {GetType()}" );

        protected override void OnCreate() => Debug.Log( $"Create {GetType()}" );
        protected override void OnInitialize() => Debug.Log( $"Update {GetType()}" );
        public override void Dispose() => Debug.Log( $"Dispose {GetType()} \n" );
    }

    private sealed class TestState2 : BaseFiniteStateMachineObject
    {
        public TestState2( IFSM parentFsm ) : base( parentFsm ) {}

        protected override void OnStart() => Debug.Log( $"Start {GetType()}" );

        protected override void OnCreate() => Debug.Log( $"Create {GetType()}" );
        protected override void OnInitialize() => Debug.Log( $"Initialize {GetType()}" );
        public override void Dispose() => Debug.Log( $"Dispose {GetType()} \n" );
    }

    [ SetUp ]
    public void Setup()
    {
        _fsm = new GameFsm();
        _fsm.RegisterState( typeof(TestState1), m => new TestState1( _fsm ) );
        _fsm.RegisterState( typeof(TestState2), m => new TestState2( _fsm ) );
    }

    [ TestCase ]
    public void Test_FSMCommon()
    {
        try
        {
            IBaseModel model = new Mock< IBaseModel >().Object;
            List< Type > list = Enumerable.Repeat( typeof(TestState1), 10 ).ToList();
            list.AddRange( Enumerable.Repeat( typeof(TestState2), 10 ) );

            IEnumerable< Type > randomized = list.Randomize();

            randomized.ForEach( item => { _fsm.SetState( item, model /*DummyModel.Dummy*/ ); } );
        }
        catch ( Exception e )
        {
            Console.WriteLine( e );
            throw;
        }
    }

    [ TestCase ]
    public void Test_FSMChanges()
    {
        try
        {
            var i = 10;
            IBaseModel model = new Mock< IBaseModel >().Object;
            _fsm.OnChanged += ChangedHandler;
            _fsm.SetState( typeof(TestState1), model );

            void ChangedHandler()
            {
                Debug.Log( $"Changed {i}" );
                if ( i-- > 0 )
                {
                    _fsm.SetState( typeof(TestState1), model );
                    _fsm.SetState( typeof(TestState2), model );
                    _fsm.SetState( typeof(TestState1), model );
                }
            }
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