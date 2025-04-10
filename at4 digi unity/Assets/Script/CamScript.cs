using UnityEngine;
using UnityEngine.UI;

public class CamScript : MonoBehaviour
{
    public RawImage cameraDisplay; // Assign a UI RawImage to show the camera feed
    public Button captureButton;   // Button to capture the picture
    private WebCamTexture webcamTexture;
    public static CamScript camScript;
    public Sprite imageSource;

    void Start()
    {
        Debug.Log("[CamScript] Initializing webcam...");
        
        webcamTexture = new WebCamTexture();
        
        if (cameraDisplay == null)
        {
            Debug.LogError("[CamScript] cameraDisplay is not assigned in the Inspector!");
            return;
        }
        if (captureButton == null)
        {
            Debug.LogError("[CamScript] captureButton is not assigned in the Inspector!");
            return;
        }

        cameraDisplay.texture = webcamTexture;
        cameraDisplay.material.mainTexture = webcamTexture;

        AdjustCameraDisplay();

        webcamTexture.Play();
        captureButton.onClick.AddListener(CapturePhoto);
        Debug.Log("[CamScript] Webcam started successfully.");
    }

    void AdjustCameraDisplay()
    {
        Debug.Log("[CamScript] Adjusting camera display...");
        RectTransform rect = cameraDisplay.GetComponent<RectTransform>();

        webcamTexture.Play();

        if (webcamTexture.isPlaying)
        {
            float aspectRatio = (float)webcamTexture.width / (float)webcamTexture.height;
            float targetWidth = rect.sizeDelta.x;
            float targetHeight = targetWidth / aspectRatio;

            KeepOnScenes.keepOnScenes.aspectRatio = aspectRatio;
            rect.sizeDelta = new Vector2(targetWidth, targetHeight);
            Debug.Log("[CamScript] Camera display adjusted successfully.");
        }
        else
        {
            Debug.LogWarning("[CamScript] WebCamTexture is not playing yet.");
        }
    }

    void CapturePhoto()
    {
        Debug.Log("[CamScript] Capturing photo...");
        if (webcamTexture.isPlaying)
        {
            Texture2D photo = new Texture2D(webcamTexture.width, webcamTexture.height);
            photo.SetPixels(webcamTexture.GetPixels());
            photo.Apply();

            Sprite sprite = Sprite.Create(photo, new Rect(0, 0, photo.width, photo.height), new Vector2(0.5f, 0.5f));
            
            imageSource = sprite;
            KeepOnScenes.keepOnScenes.imageSource = imageSource;
            Debug.Log("[CamScript] Photo captured successfully.");
        }
        else
        {
            Debug.LogError("[CamScript] Webcam is not active.");
        }
    }

    void OnDestroy()
    {
        if (webcamTexture != null)
        {
            webcamTexture.Stop();
            Debug.Log("[CamScript] Webcam stopped.");
        }
    }
}
