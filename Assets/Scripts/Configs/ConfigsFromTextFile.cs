using Common.Configs;
using System.IO;
using UnityEditor;
using UnityEngine;

public static class ConfigsFromTextFile
{
    [ MenuItem( "Assets/Create/Config/Configs From Text", false, 0 ) ]
    public static void Create()
    {
        if ( Selection.objects.IsNullOrEmpty() ) return;

        var textAsset = Selection.objects[ 0 ] as TextAsset;
        if ( textAsset == default ) return;

        string[] array = textAsset.text.Split( "\r\n" );
        array.ForEach( item =>
        {
            var scriptable = ScriptableObject.CreateInstance< BaseConsumableItemConfig >();
            scriptable.name = item;
            string path = AssetDatabase.GetAssetPath( textAsset );
            path = Path.GetDirectoryName( path ).Replace( "\\", "/" );

            path = $"{path}/{item}.asset";
            Debug.Log( path );
            AssetDatabase.CreateAsset( scriptable, path );
        } );

        AssetDatabase.Refresh();
    }
}