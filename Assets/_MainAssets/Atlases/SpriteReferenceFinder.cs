using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class SpriteReferenceFinder : EditorWindow
{
  public Sprite spriteToSearchFor;

  [MenuItem("Tools/Find Sprite References")]
  static void Init()
  {
    SpriteReferenceFinder window = (SpriteReferenceFinder)GetWindow(typeof(SpriteReferenceFinder));
    window.Show();
  }

  private void OnGUI()
  {
    spriteToSearchFor = (Sprite)EditorGUILayout.ObjectField("Sprite to Search For", spriteToSearchFor, typeof(Sprite), false);

    if (GUILayout.Button("Find References"))
    {
      if (spriteToSearchFor != null)
      {
        FindReferencesInPrefabs(spriteToSearchFor);
        FindReferencesInAnimations(spriteToSearchFor);
      }
      else
      {
        Debug.LogWarning("Please assign a sprite to search for.");
      }
    }
  }


  private void FindReferencesInPrefabs(Sprite sprite)
  {
    string[] prefabPaths = AssetDatabase.FindAssets("t:Prefab"); // Find all prefabs in the project
    List<string> foundPrefabs = new List<string>();

    foreach (var prefabPath in prefabPaths)
    {
      string path = AssetDatabase.GUIDToAssetPath(prefabPath);
      GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
      var spriteRenderers = prefab.GetComponentsInChildren<SpriteRenderer>();

      foreach (var renderer in spriteRenderers)
      {
        if (renderer.sprite == sprite)
        {
          foundPrefabs.Add(path);
          break;
        }
      }
    }

    if (foundPrefabs.Count > 0)
    {
      Debug.Log("Found references in the following prefabs:");
      foreach (var prefab in foundPrefabs)
      {
        Debug.Log(prefab);
      }
    }
    else
    {
      Debug.Log("No references found in prefabs.");
    }
  }

  private void FindReferencesInAnimations(Sprite sprite)
  {
    string[] animationPaths = AssetDatabase.FindAssets("t:AnimationClip"); // Find all animation clips in the project
    List<string> foundAnimations = new List<string>();

    foreach (var animationPath in animationPaths)
    {
      string path = AssetDatabase.GUIDToAssetPath(animationPath);
      AnimationClip clip = AssetDatabase.LoadAssetAtPath<AnimationClip>(path);
      var bindings = AnimationUtility.GetObjectReferenceCurveBindings(clip);

      foreach (var binding in bindings)
      {
        var keyframes = AnimationUtility.GetObjectReferenceCurve(clip, binding);
        foreach (var keyframe in keyframes)
        {
          if (keyframe.value == sprite)
          {
            foundAnimations.Add(path);
            break;
          }
        }
      }
    }

    if (foundAnimations.Count > 0)
    {
      Debug.Log("Found references in the following animation clips:");
      foreach (var animation in foundAnimations)
      {
        Debug.Log(animation);
      }
    }
    else
    {
      Debug.Log("No references found in animations.");
    }
  }
}