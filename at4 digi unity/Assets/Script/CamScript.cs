using UnityEngine;
using UnityEngine.UI;

public class CamScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public RawImage cameraDisplay; // Assign a UI RawImage to show the camera feed
    public Button captureButton;   // Button to capture the picture
    private WebCamTexture webcamTexture;
    public static CamScript camScript;
    public Sprite imageSource;

    void Start()
    {
        // Initialize the webcam
        webcamTexture = new WebCamTexture();

        // Set the camera feed to the RawImage texture
        cameraDisplay.texture = webcamTexture;
        cameraDisplay.material.mainTexture = webcamTexture;

        // Adjust the RawImage to match the desired size and aspect ratio
        AdjustCameraDisplay();

        // Start the camera feed
        webcamTexture.Play();

        // Assign the button click event
        captureButton.onClick.AddListener(CapturePhoto);
    }

    void AdjustCameraDisplay()
    {
        // Get the RectTransform of the RawImage
        RectTransform rect = cameraDisplay.GetComponent<RectTransform>();

        // Wait for the webcam texture to start
        webcamTexture.Play();

        // Ensure the WebCamTexture has started before getting its properties
        if (webcamTexture.isPlaying)
        {
            // Calculate the aspect ratio
            float aspectRatio = (float)webcamTexture.width / (float)webcamTexture.height;

            // Match the RawImage size to the desired aspect ratio
            float targetWidth = rect.sizeDelta.x;
            float targetHeight = targetWidth / aspectRatio;

            rect.sizeDelta = new Vector2(targetWidth, targetHeight);

            KeepOnScenes.keepOnScenes.aspectRatio = aspectRatio;
        }
        else
        {
            Debug.LogWarning("WebCamTexture is not playing yet.");
        }
    }

    void CapturePhoto()
    {
        if (webcamTexture.isPlaying)
        {
            // Create a Texture2D from the camera feed
            Texture2D photo = new Texture2D(webcamTexture.width, webcamTexture.height);
            photo.SetPixels(webcamTexture.GetPixels());
            photo.Apply();

            Sprite sprite = Sprite.Create(photo, new Rect(0, 0, photo.width, photo.height), new Vector2(0.5f, 0.5f));

            // Encode the texture to PNG
            byte[] photoBytes = photo.EncodeToPNG();
            //image.sprite = sprite;
            imageSource = sprite;

            KeepOnScenes.keepOnScenes.imageSource = imageSource;

            /*
            // Save the PNG to the desktop
            string desktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
            string filePath = System.IO.Path.Combine(desktopPath, "CapturedPhoto.png");
            System.IO.File.WriteAllBytes(filePath, photoBytes);

            Debug.Log($"Photo saved to {filePath}");
            */

        }
        else
        {
            Debug.LogError("Webcam is not active.");
        }
    }

    void OnDestroy()
    {
        // Stop the webcam when the script is destroyed
        if (webcamTexture != null)
        {
            webcamTexture.Stop();
        }
    }
}
