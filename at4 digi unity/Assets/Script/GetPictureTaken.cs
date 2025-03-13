using UnityEngine;
using UnityEngine.UI;

public class GetPictureTaken : MonoBehaviour
{
    public Image image;

    void Start()
    {
        image.sprite = CamScript.camScript.imageSource;   
    }
}
