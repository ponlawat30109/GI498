// #if UNITY_EDITOR

// using UnityEditor;

// #endif
// using UnityEngine;

// namespace _Scripts.EditorScript
// {
//     [CustomEditor(typeof(SceneHandler))]
//     public class SceneHandlerEditor  : Editor 
//     {
//         public override void OnInspectorGUI()
//         {
//             SceneHandler sceneHandler = (SceneHandler)target;

//             EditorGUILayout.PropertyField(serializedObject.FindProperty("scenePackages"), true);
            
//             sceneHandler.sceneName = EditorGUILayout.TextField("Scene Name", sceneHandler.sceneName);
            
//             if(GUILayout.Button("Load Specific Scene"))
//             {
//                 sceneHandler.LoadSpecificScene();
//             }
            
//             if(GUILayout.Button("Unload Specific Scene"))
//             {
//                 sceneHandler.UnloadSpecificScene();
//             }
            
//             EditorGUILayout.Space();
//             EditorGUILayout.Space();
            
//             if(GUILayout.Button("Load All Scene"))
//             {
//                 sceneHandler.LoadAllScene();
//             }
            
//             if(GUILayout.Button("Unload All Scene"))
//             {
//                 sceneHandler.UnloadAllScene();
//             }
            
            
//         }
//     }
// }