using UnityEditor;

namespace Game.Editor
{
    public static class CreateScriptTemplates
    {
        [MenuItem("Assets/Create/Code/MonoBehaviour", priority = 40)]
        public static void CreateMonoBehaviour()
        {
            string path = "Assets/!/Source/Editor/Templates/MonoBehaviour.cs.txt";
            
            ProjectWindowUtil.CreateScriptAssetFromTemplateFile(path, "NewScript.cs");
        }
    }
}