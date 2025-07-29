using Common;
using System;
using System.Collections;
using UnityEngine;

public class TestCommand : MonoBehaviour
{
    private ProcessingCommands batch;
    private ProcessingCommands sequence;

    // Use this for initialization
    private void Start()
    {
        batch = new ProcessingBatchCommands();

        batch.AddCommand( new CommandCallMethod( "cmd_1", clb => { Debug.Log( "Call batch 1" ); } ) );

        batch.AddCommand( new CommandCallMethod( "cmd_2", clb => { Debug.Log( "Call batch 2" ); } ) );

        batch.AddCommand( new CommandCallMethod( "cmd_3", clb => { Debug.Log( "Call batch 3" ); } ) );

        sequence = new ProcessingSequenceCommands();

        sequence.AddCommand( new CommandCallMethod( "cmd_4", clb =>
        {
            Debug.Log( "Call sequence 1" );

            StartCoroutine( Wait( 5, clb ) );
        } ) );

        sequence.AddCommand( new CommandCallMethod( "cmd_5", clb =>
        {
            Debug.Log( "Call sequence 1" );
            StartCoroutine( Wait( 5, clb ) );
        } ) );

        sequence.AddCommand( new CommandCallMethod( "cmd_6", clb =>
        {
            Debug.Log( "Call sequence 1" );
            StartCoroutine( Wait( 5, clb ) );
        } ) );
    }

    private IEnumerator Wait( float time, Action callback )
    {
        yield return new WaitForSeconds( time );
        callback();
        yield break;
    }

    // Update is called once per frame
    private void Update()
    {
        batch.StartExecute();
        sequence.StartExecute();
    }
}