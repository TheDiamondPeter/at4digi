using UnityEngine;
using UnityEngine.UI;

public class GetPictureTaken : MonoBehaviour
{
    public Image image; // Assign this in the Inspector

    void Start()
    {
        // ✅ Check if 'image' is assigned in the Inspector
        if (image == null)
        {
            Debug.LogError("❌ ERROR: 'image' is NULL. Assign it in the Inspector!");
            return;
        }

        // ✅ Check if 'KeepOnScenes.keepOnScenes' is initialized
        if (KeepOnScenes.keepOnScenes == null)
        {
            Debug.LogError("❌ ERROR: 'KeepOnScenes.keepOnScenes' is NULL. Ensure it is initialized.");
            return;
        }

        // ✅ Check if 'imageSource' is assigned (i.e., photo was taken)
        if (KeepOnScenes.keepOnScenes.imageSource == null)
        {
            Debug.LogWarning("⚠️ WARNING: 'imageSource' is NULL. A picture might not have been taken yet.");
            return; // Prevent setting a null sprite
        }

        // ✅ Resize the image based on aspect ratio
        RectTransform rect = image.GetComponent<RectTransform>();

        float targetWidth = rect.sizeDelta.x;
        float targetHeight = targetWidth / KeepOnScenes.keepOnScenes.aspectRatio;

        rect.sizeDelta = new Vector2(targetWidth, targetHeight);

        // ✅ Assign the captured image
        image.sprite = KeepOnScenes.keepOnScenes.imageSource;
        Debug.Log("✅ SUCCESS: Image assigned correctly!");
    }
}
