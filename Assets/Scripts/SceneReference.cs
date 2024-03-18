// using UnityEngine;
// using UnityEngine.SceneManagement;

// public struct SceneReference
// {
//     private string Name;

//     public bool Load()
//     {
//         if (SceneManager.GetSceneByName(Name).IsValid())
//         {
//             SceneManager.LoadScene(Name);
//             return true;
//         }
//         else
//         {
//             Debug.LogError($"Scene, \"{Name}\", does not exist");
//             return false;
//         }
//     }
// }