using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using Debug = UnityEngine.Debug;

public static class Builder
{
    public static bool AndroidBuild( BuildMode mode, bool isShowDialog = true )
    {
        if ( !UpdateKeystore() ) return false;

        PlayerSettings.stripEngineCode = false;

        var options = new BuildPlayerOptions();
        options.scenes = AssetsHelper.FindBuildScenes();

        options.locationPathName = AssetsHelper.GetAndroidBuildPath( mode );

        options.target = BuildTarget.Android;
        string defines = PlayerSettings.GetScriptingDefineSymbols( NamedBuildTarget.Android );
        if ( mode == BuildMode.Dev )
        {
            string[] array = defines.Split( ';' );
            var builder = new StringBuilder();
            for ( var i = 0; i < array.Length; ++i )
            {
                string tmp = array[ i ];
                if ( tmp == "LIVE" ) continue;

                if ( tmp == "DISABLE_SRDEBUGGER" ) continue;

                builder.Append( tmp );
                builder.Append( ";" );
            }

            PlayerSettings.SetScriptingBackend( NamedBuildTarget.Android, ScriptingImplementation.IL2CPP );
        }
        else if ( mode == BuildMode.Live )
        {
            options.options = BuildOptions.StrictMode;
            PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARMv7 | AndroidArchitecture.ARM64;

            if ( !defines.Contains( ";LIVE" ) )
                PlayerSettings.SetScriptingDefineSymbols( NamedBuildTarget.Android, defines + ";LIVE" );

            if ( !defines.Contains( ";DISABLE_SRDEBUGGER" ) )
                PlayerSettings.SetScriptingDefineSymbols( NamedBuildTarget.Android, defines + ";DISABLE_SRDEBUGGER" );

            PlayerSettings.SetScriptingBackend( NamedBuildTarget.Android, ScriptingImplementation.IL2CPP );
        }

        bool result = BuildPlayer( options, isShowDialog );
        if ( result && mode == BuildMode.Live )
        {
            defines = defines.Replace( ";DISABLE_SRDEBUGGER", string.Empty );
            PlayerSettings.SetScriptingDefineSymbols( NamedBuildTarget.Android, defines );
            AssetDatabase.Refresh();
        }

        return result;
    }

    public static void AndroidRun( BuildMode mode )
    {
        string path = AssetsHelper.GetAndroidBuildPath( mode );

        var p = new Process();
        p.StartInfo = new ProcessStartInfo( "adb", $"-d install -r \"{path}\"" );
        p.StartInfo.UseShellExecute = false;
        p.StartInfo.RedirectStandardInput = true;
        p.StartInfo.RedirectStandardOutput = true;
        p.StartInfo.RedirectStandardError = true;
        p.Start();
        p.WaitForExit();
        Debug.Log( "Android Install: " + p.StandardOutput.ReadToEnd() );
        if ( p.ExitCode != 0 )
        {
            Debug.LogError( "Android Install Failed: " + p.StandardError.ReadToEnd() );
            return;
        }

        p = new Process();
        p.StartInfo = new ProcessStartInfo( "adb",
            "shell am start -n " + PlayerSettings.applicationIdentifier + "/com.unity3d.player.UnityPlayerActivity" );
        p.StartInfo.UseShellExecute = false;
        p.StartInfo.RedirectStandardInput = true;
        p.StartInfo.RedirectStandardOutput = true;
        p.StartInfo.RedirectStandardError = true;
        p.Start();
        p.WaitForExit();
        Debug.Log( "Android Run: " + p.StandardOutput.ReadToEnd() );
        if ( p.ExitCode != 0 ) Debug.LogError( "Android Run Failed: " + p.StandardError.ReadToEnd() );
    }

    public static bool UpdateKeystore()
    {
        string[] lines;
        try
        {
            lines = File.ReadAllLines( AssetsHelper.KeystoreInfo );
            if ( lines.Length < 4 ) throw new InvalidDataException();
        }
        catch ( Exception )
        {
            Debug.LogError( $"Can't read file \"{AssetsHelper.KeystoreInfo}\"" );
            return false;
        }

        PlayerSettings.Android.keystoreName = lines[ 0 ];
        PlayerSettings.Android.keystorePass = lines[ 1 ];
        PlayerSettings.Android.keyaliasName = lines[ 2 ];
        PlayerSettings.Android.keyaliasPass = lines[ 3 ];
        return true;
    }

    private static bool BuildPlayer( BuildPlayerOptions options, bool isShowDialog = true )
    {
        BuildReport result = BuildPipeline.BuildPlayer( options );

        AssetDatabase.Refresh();

        if ( isShowDialog )
            EditorUtility.DisplayDialog( "BUILD RESULT",
                $"{result.summary.result} totalErrors= {result.summary.totalErrors}", "OK" );

        return result.summary.result == BuildResult.Succeeded;
    }

    public enum BuildMode
    {
        Dev,
        Live
    }
}