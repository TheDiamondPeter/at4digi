using UnityEngine;
using UnityEngine.UI;

public class ImageDisplay : MonoBehaviour
{
    public Image displayImage;  // Reference to UI Image component

    public void SetImage(Sprite newImage)
    {
        if (displayImage != null)
        {
            displayImage.sprite = newImage;
        }
        else
        {
            Debug.LogError("Display Image is not assigned!");
        }
    }
}
