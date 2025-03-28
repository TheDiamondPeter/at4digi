using UnityEngine;

public class KeepOnScenes : MonoBehaviour
{
    public static KeepOnScenes keepOnScenes; // Singleton reference
    public Sprite imageSource; // Holds the captured image
    public float aspectRatio;  // Stores the image's aspect ratio

    void Awake()
    {
        if (keepOnScenes == null)
        {
            keepOnScenes = this;  // Initialize the singleton
            DontDestroyOnLoad(gameObject); // Keep it across scenes
        }
        else
        {
            Destroy(gameObject);  // Prevent duplicates
        }
    }
}
