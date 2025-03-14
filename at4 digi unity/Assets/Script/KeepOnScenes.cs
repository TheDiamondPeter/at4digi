using UnityEngine;

public class KeepOnScenes : MonoBehaviour
{
    public static KeepOnScenes keepOnScenes;
    public Sprite imageSource;
    public float aspectRatio = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);   
    }
}