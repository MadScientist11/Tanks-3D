using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Editor
{
    [InitializeOnLoad]
    public static class ApplicationRunnerTool
    {
        static ApplicationRunnerTool()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (!EditorApplication.isPlaying && EditorApplication.isPlayingOrWillChangePlaymode)
            {
                if (SceneManager.GetActiveScene().name == "Gameplay")
                {
                    EditorSceneManager.SaveOpenScenes();
                    EditorSceneManager.OpenScene("Assets/!/Scenes/Boot.unity");
                }
            }
        }
    }
}