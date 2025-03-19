using UnityEngine;
using UnityEditor;
using UnityEngine.U2D; // Import the necessary namespace for SpriteAtlas

public class AtlasSpriteUpdater : EditorWindow
{
  public SpriteAtlas spriteAtlas; // The Sprite Atlas
  public AnimationClip animationClip; // The AnimationClip to update

  [MenuItem("Tools/Update Animation Sprites")]
  static void Init()
  {
    AtlasSpriteUpdater window = (AtlasSpriteUpdater)GetWindow(typeof(AtlasSpriteUpdater));
    window.Show();
  }

  private void OnGUI()
  {
    spriteAtlas = (SpriteAtlas)EditorGUILayout.ObjectField("Sprite Atlas", spriteAtlas, typeof(SpriteAtlas), false);
    animationClip = (AnimationClip)EditorGUILayout.ObjectField("Animation Clip", animationClip, typeof(AnimationClip), false);

    if (GUILayout.Button("Update Animation"))
    {
      if (spriteAtlas != null && animationClip != null)
      {
        ReplaceSpritesWithAtlas(animationClip, spriteAtlas);
      }
      else
      {
        Debug.LogWarning("Please assign both the Sprite Atlas and Animation Clip.");
      }
    }
  }

  void ReplaceSpritesWithAtlas(AnimationClip clip, SpriteAtlas atlas)
  {
    // Get the bindings for all object reference properties in the animation
    var bindings = AnimationUtility.GetObjectReferenceCurveBindings(clip);

    foreach (var binding in bindings)
    {
      // Get the ObjectReferenceKeyframe array for this binding
      ObjectReferenceKeyframe[] keyframes = AnimationUtility.GetObjectReferenceCurve(clip, binding);

      // Process each keyframe and replace the sprite with the one from the atlas
      for (int i = 0; i < keyframes.Length; i++)
      {
        ObjectReferenceKeyframe keyframe = keyframes[i];

        // Check if the keyframe value is a sprite
        if (keyframe.value is Sprite sprite)
        {
          // Replace with the corresponding sprite from the atlas
          string spriteName = sprite.name;
          Sprite atlasSprite = atlas.GetSprite(spriteName);
          if (atlasSprite != null)
          {
            // Create a new keyframe with the atlas sprite
            keyframes[i] = new ObjectReferenceKeyframe
            {
              time = keyframe.time,
              value = atlasSprite
            };
            Debug.Log($"Replaced {spriteName} with atlas sprite.");
          }
          else
          {
            Debug.LogWarning($"Sprite {spriteName} not found in atlas.");
          }
        }
      }

      // Set the updated keyframes back to the animation clip
      AnimationUtility.SetObjectReferenceCurve(clip, binding, keyframes);
    }

    // Save the updated animation clip
    EditorUtility.SetDirty(clip);
    AssetDatabase.SaveAssets();
  }
}