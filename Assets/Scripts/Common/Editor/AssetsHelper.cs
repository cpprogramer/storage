using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[ InitializeOnLoad ]
public class AssetsHelper : AssetModificationProcessor
{
    public static readonly string ProjectDir;

    public static readonly string AssetsDir;

    public static readonly string BuildsDir;

    public static readonly string KeystoreInfo;

    static AssetsHelper()
    {
        AssetsDir = Application.dataPath;
        ProjectDir = Path.GetFullPath( Path.Combine( AssetsDir, "../" ) );
        BuildsDir = Path.GetFullPath( Path.Combine( ProjectDir, "Builds" ) );
        KeystoreInfo = Path.GetFullPath( Path.Combine( ProjectDir, "keystore.txt" ) );
        //VerifyUpdatePrefabsScenes();
    }

    public static string[] OnWillSaveAssets( string[] paths ) =>
        //VerifyUpdatePrefabsScenes();
        paths;

    public static string GetWindowsBuildPath() =>
        Path.GetFullPath( Path.Combine( BuildsDir,
            string.Format( "{1}{0}_Windows/{1}{0}.exe", BuildMenu.ReadVersion(), PlayerSettings.productName ) ) );

    public static string GetAndroidBuildPath( Builder.BuildMode mode )
    {
        string productName = PlayerSettings.productName.Replace( " ", "_" ).Replace( ":", "" );
        string extension = EditorUserBuildSettings.buildAppBundle ? ".aab" : ".apk";
        return Path.Combine( BuildsDir,
            $"{productName}{BuildMenu.ReadVersion()}_{PlayerSettings.Android.bundleVersionCode}_{mode}{extension}" );
    }

    public static string[] FindBuildScenes()
    {
        var list = new List< string >();
        foreach ( EditorBuildSettingsScene scene in EditorBuildSettings.scenes )
            if ( scene.enabled )
                list.Add( scene.path );

        return list.ToArray();
    }

    private static void VerifyUpdatePrefabsScenes()
    {
        string error = VerifyUniqueNames( FindResourcesPrefabs() );
        if ( !string.IsNullOrEmpty( error ) ) Debug.LogError( error );

        string[] scenes = FindBuildScenes();
        error = VerifyUniqueNames( scenes );
        if ( !string.IsNullOrEmpty( error ) ) Debug.LogError( error );
    }

    private static string VerifyUniqueNames( string[] paths )
    {
        var error = string.Empty;
        var map = new Dictionary< string, string >();
        foreach ( string path in paths )
        {
            string name = Path.GetFileNameWithoutExtension( path );
            if ( map.TryGetValue( name, out string prev ) )
                error += $"Duplicate \"{name}\" found in \"{path}\", original was \"{prev}\"";
            else
                map.Add( name, path );
        }

        return error;
    }

    private static string[] FindResourcesPrefabs()
    {
        var list = new List< string >();
        foreach ( string guid in AssetDatabase.FindAssets( "t:Prefab" ) )
        {
            string path = AssetDatabase.GUIDToAssetPath( guid );
            list.Add( path );
        }

        return list.ToArray();
    }

    private static void UpdateBuildScenes( string[] scenes )
    {
        var list = new List< EditorBuildSettingsScene >();
        foreach ( string scene in scenes )
            list.Add( new EditorBuildSettingsScene
            {
                enabled = true, path = scene, guid = new GUID( AssetDatabase.AssetPathToGUID( scene ) )
            } );

        EditorBuildSettings.scenes = list.ToArray();
    }
}