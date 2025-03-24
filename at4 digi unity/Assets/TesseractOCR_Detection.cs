using UnityEngine;
using UnityEngine.Events;

public class TesseractOCR_Detection : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void onSetupComplete(){

    }
    public void DetectText(){
        TesseractDriver tesseractDriver = new TesseractDriver();
        tesseractDriver.Setup(onSetupComplete);

        Sprite sprite = KeepOnScenes.keepOnScenes.imageSource;
        var croppedTexture = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height );
        var pixels = sprite.texture.GetPixels((int)sprite.textureRect.x,
        (int)sprite.textureRect.y, 
        (int)sprite.textureRect.width, 
        (int)sprite.textureRect.height);

        croppedTexture.SetPixels( pixels );
        croppedTexture.Apply();

        string recognizedText = tesseractDriver.Recognize(croppedTexture);

        Debug.Log(recognizedText);
    }

    void Start(){
        DetectText();
    }
}