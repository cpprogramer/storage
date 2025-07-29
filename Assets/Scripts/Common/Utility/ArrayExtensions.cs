using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public static class ArrayExtensions
{
    public static TSource
        MaxBy< TSource, TKey >( this IEnumerable< TSource > source, Func< TSource, TKey > selector ) =>
        source.MaxBy( selector, null );

    public static bool IsNullOrEmpty( this IList list ) => list == null || list.Count == 0;
    public static bool IsNullOrEmpty( this Array array ) => array == null || array.Length == 0;

    public static object Random( this Array array )
    {
        if ( array.IsNullOrEmpty() )
            return default;

        int index = UnityEngine.Random.Range( 0, array.Length );
        return array.GetValue( index );
    }

    public static object Random( this IList list )
    {
        if ( list.IsNullOrEmpty() )
            return default;

        int index = UnityEngine.Random.Range( 0, list.Count );
        return list[ index ];
    }

    public static Transform[] GetAllChildren( this Transform aParent )
    {
        var children = new Transform[aParent.childCount];

        for ( var i = 0; i < children.Length; ++i ) children[ i ] = aParent.GetChild( i );

        return children;
    }

    public static void ForEach< T >( this IEnumerable< T > source, Action< T > action )
    {
        foreach ( T element in source ) action?.Invoke( element );
    }

    public static void ForEach< T >( this IEnumerable< T > source, Action< T, int > action )
    {
        var index = 0;
        foreach ( T element in source ) action?.Invoke( element, index++ );
    }

    public static TSource MaxBy< TSource, TKey >(
        this IEnumerable< TSource > source,
        Func< TSource, TKey > selector,
        IComparer< TKey > comparer
    )
    {
        comparer ??= Comparer< TKey >.Default;

        using IEnumerator< TSource > sourceIterator = source.GetEnumerator();
        if ( !sourceIterator.MoveNext() ) throw new InvalidOperationException( "Sequence contains no elements" );

        TSource max = sourceIterator.Current;
        TKey maxKey = selector( max );
        while ( sourceIterator.MoveNext() )
        {
            TSource candidate = sourceIterator.Current;
            TKey candidateProjected = selector( candidate );
            if ( comparer.Compare( candidateProjected, maxKey ) > 0 )
            {
                max = candidate;
                maxKey = candidateProjected;
            }
        }

        return max;
    }

    public static void ForEach< T >( this T[] array, Action< T > callback )
    {
        if ( callback == null || array.IsNullOrEmpty() ) return;

        foreach ( T elt in array ) callback( elt );
    }

    public static void ForEach< T >( this T[] array, Action< T, int > callback )
    {
        if ( callback == null || array.IsNullOrEmpty() ) return;

        for ( var i = 0; i < array.Length; ++i ) callback( array[ i ], i );
    }

    public static IEnumerable< T > Randomize< T >( this IEnumerable< T > source )
    {
        var rnd = new Random();
        return source.OrderBy( item => rnd.Next() );
    }
}