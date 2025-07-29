using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;
using UnityEngine.AddressableAssets;

public static class BuildMenu
{
    [ MenuItem( "BUILDER/Screenshot" ) ]
    public static void Screenshot() => ScreenCapture.CaptureScreenshot( $"Screenshot_{DateTime.Now}.png", 2 );

    [ MenuItem( "BUILDER/Open Local Storage" ) ]
    public static void OpenLocalStorage() => Application.OpenURL( Application.persistentDataPath );

    [ MenuItem( "BUILDER/Delete Local Storage" ) ]
    public static void DeleteLocalStorage() => Directory.Delete( Application.persistentDataPath, true );

    [ MenuItem( "BUILDER/Update Keystore" ) ]
    public static void UpdateKeystore() => Builder.UpdateKeystore();

    [ MenuItem( "BUILDER/Android Dev Run" ) ]
    public static void AndroidDevRun() => Builder.AndroidRun( Builder.BuildMode.Dev );

    [ MenuItem( "BUILDER/Android Dev Build" ) ]
    public static void AndroidDevBuildWithDialog() => Builder.AndroidBuild( Builder.BuildMode.Dev );

    [ MenuItem( "BUILDER/Android Dev Build (No Dialog)" ) ]
    public static void AndroidDevBuild() => Builder.AndroidBuild( Builder.BuildMode.Dev, false );

    [ MenuItem( "BUILDER/Android Live Run" ) ]
    public static void AndroidLiveRun() => Builder.AndroidRun( Builder.BuildMode.Live );

    [ MenuItem( "BUILDER/𝗔𝗻𝗱𝗿𝗼𝗶𝗱 𝗟𝗶𝘃𝗲 𝗕𝘂𝗶𝗹𝗱" ) ]
    public static void AndroidLiveBuildWithDialog() => Builder.AndroidBuild( Builder.BuildMode.Live );

    [ MenuItem( "BUILDER/𝗔𝗻𝗱𝗿𝗼𝗶𝗱 𝗟𝗶𝘃𝗲 𝗕𝘂𝗶𝗹𝗱 (No Dialog)" ) ]
    public static void AndroidLiveBuild() => Builder.AndroidBuild( Builder.BuildMode.Live, false );

    [ MenuItem( "BUILDER/Increase Revnum" ) ]
    public static void MenuIncreaseRevNumber() => IncreaseRevNumber();

    public static string ReadVersion() => PlayerSettings.bundleVersion;

    [ MenuItem( "Tools/clear addressables cache", false, 50 ) ]
    public static void ClearAddressablesCache()
    {
        Debug.Log( "clear cache at " + Application.persistentDataPath );
        string[] list = Directory.GetDirectories( Application.persistentDataPath );

        foreach ( string item in list )
        {
            Debug.Log( "Delete" + " " + item );
            Directory.Delete( item, true );
        }

        Caching.ClearCache();

        if ( Directory.Exists( Caching.defaultCache.path ) ) Directory.Delete( Caching.defaultCache.path, true );

        //
        AddressableAssetSettings aaSettings = AddressableAssetSettingsDefaultObject.Settings;
        string aaProfileId = aaSettings.activeProfileId;
        AddressableAssetProfileSettings aaProfileSettings = aaSettings.profileSettings;
        string remoteBuildPath = aaProfileSettings.EvaluateString( aaProfileId,
            aaProfileSettings.GetValueByName( aaProfileId, AddressableAssetSettings.kRemoteBuildPath ) );

        var bundleHashes = new Dictionary< string, string >();
        foreach ( string bundlePath in Directory.GetFiles( remoteBuildPath, "*.bundle" ) )
        {
            string fileName = Path.GetFileNameWithoutExtension( bundlePath );
            string bundleName = fileName.Substring( 0, fileName.LastIndexOf( '_' ) );
            string bundleHash = fileName.Substring( fileName.LastIndexOf( '_' ) + 1 );

            if ( !bundleHashes.ContainsKey( bundleName ) ) bundleHashes.Add( bundleName, bundleHash );
        }

        // Write out the bundle hashes dict
        string bundleHashesPath = Path.Combine( Addressables.BuildPath, "bundle_hashes.json" );
        File.WriteAllText( bundleHashesPath, JsonConvert.SerializeObject( bundleHashes ) );
        //
    }

    [ MenuItem( "BUILDER/Clean Prefs" ) ]
    public static void WindowsCleanPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

    [ MenuItem( "BUILDER/Android Dev Build'n'Run" ) ]
    public static void AndroidDevBuildNRun()
    {
        if ( Builder.AndroidBuild( Builder.BuildMode.Dev ) ) Builder.AndroidRun( Builder.BuildMode.Dev );
    }

    [ MenuItem( "BUILDER/Android Live Build'n'Run" ) ]
    public static void AndroidLiveBuildNRun()
    {
        if ( Builder.AndroidBuild( Builder.BuildMode.Live ) ) Builder.AndroidRun( Builder.BuildMode.Live );
    }

    private static void IncreaseRevNumber()
    {
        Version version = Version.Parse( PlayerSettings.bundleVersion );
        Debug.Log( version.ToString() );

        version = new Version( version.Major, version.Minor, version.Build + 1, 0 );
        PlayerSettings.Android.bundleVersionCode += 1;
        Debug.Log( version +
                   $"   {version.Major} {version.Minor} {version.Build} {version.Revision} {PlayerSettings.Android.bundleVersionCode}" );
        PlayerSettings.bundleVersion = $"{version.Major}.{version.Minor}.{version.Build}";
        PlayerSettings.macOS.buildNumber = PlayerSettings.Android.bundleVersionCode.ToString();
        PlayerSettings.iOS.buildNumber = PlayerSettings.Android.bundleVersionCode.ToString();
    }
}