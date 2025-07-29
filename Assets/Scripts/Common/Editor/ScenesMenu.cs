using UnityEditor;
using UnityEditor.SceneManagement;

public static class ScenesMenu
{
    [ MenuItem( "Scenes/Run", false, 1 ) ] public static void Run() => StartScene( "Bootstrapper", true );

    private static void StartScene( string name, bool run )
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene( $"Assets/Scenes/{name}.unity" );
        EditorApplication.isPlaying = run;
    }

    #region Scenes (100-103)

    [ MenuItem( "Scenes/MainMenu", false, 100 ) ]
    public static void OpenMainMenu() => StartScene( "MainMenu", false );

    [ MenuItem( "Scenes/Bootstrapper", false, 101 ) ]
    public static void OpenBootstrapper() => StartScene( "Bootstrapper", false );

    [ MenuItem( "Scenes/TestLevel", false, 102 ) ]
    public static void OpenTestLevel() => StartScene( "TestGamePlay", false );

    #endregion
}