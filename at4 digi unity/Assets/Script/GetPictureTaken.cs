using UnityEngine;
using UnityEngine.UI;

public class GetPictureTaken : MonoBehaviour
{
    public Image image;

    void Start()
    {
        RectTransform rect = image.GetComponent<RectTransform>();

        float targetWidth = rect.sizeDelta.x;
        float targetHeight = targetWidth / KeepOnScenes.keepOnScenes.aspectRatio;

        rect.sizeDelta = new Vector2(targetWidth, targetHeight);
        image.sprite = KeepOnScenes.keepOnScenes.imageSource;
    }
}
