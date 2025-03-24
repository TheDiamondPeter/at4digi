using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TesseractOCR_Detection : MonoBehaviour
{
    public Wolfram wolframScript;  // Reference to Wolfram script
    public Image imageDisplay;  // UI Image to display uploaded image
    public TMP_InputField ocrTextField; // InputField for extracted text
    public Button confirmButton; // Button to confirm text

    private TesseractDriver tesseractDriver;
    private Texture2D processedTexture; // Processed image for OCR

    void Start()
    {
        tesseractDriver = new TesseractDriver();
        tesseractDriver.Setup(() => Debug.Log("Tesseract setup complete!"));
        
        if (tesseractDriver == null)
            Debug.LogError("❌ Tesseract driver is not initialized!");

        if (confirmButton != null)
            confirmButton.onClick.AddListener(OnConfirmText);
        else
            Debug.LogError("❌ Confirm button not assigned in Inspector!");

        // Test: Ensure the InputField is active
        if (ocrTextField != null)
        {
            ocrTextField.text = "TEST MESSAGE";  // Check if UI updates
            Debug.Log("✅ InputField test text assigned.");
        }
        else
        {
            Debug.LogError("❌ ocrTextField is NULL! Assign it in the Inspector.");
        }
    }

    public void DetectText()
    {
        Debug.Log("🟢 DetectText() function was called!");

        if (KeepOnScenes.keepOnScenes.imageSource == null)
        {
            Debug.LogError("❌ No image found in KeepOnScenes!");
            return;
        }

        // Assign and display the uploaded image
        Sprite sprite = KeepOnScenes.keepOnScenes.imageSource;
        imageDisplay.sprite = sprite;
        Debug.Log("✅ Image assigned successfully!");

        // Convert Sprite to Texture2D
        processedTexture = ConvertSpriteToTexture(sprite);
        processedTexture = ConvertToBlackAndWhite(processedTexture); // Preprocessing for better OCR

        if (processedTexture == null)
        {
            Debug.LogError("❌ Failed to convert sprite to texture!");
            return;
        }

        // Save the image for debugging
        byte[] bytes = processedTexture.EncodeToPNG();
        System.IO.File.WriteAllBytes(Application.persistentDataPath + "/debug_image.png", bytes);
        Debug.Log("📂 Saved image at: " + Application.persistentDataPath + "/debug_image.png");

        // Perform OCR
        Debug.Log("🔍 OCR is analyzing texture...");
        string recognizedText = tesseractDriver.Recognize(processedTexture);

        // Debugging: Check what text is detected before assigning it
        if (string.IsNullOrEmpty(recognizedText))
        {
            Debug.LogError("❌ OCR returned an EMPTY STRING! Check image clarity.");
        }
        else
        {
            Debug.Log("✅ OCR Extracted Text: " + recognizedText);
        }

        // Assign extracted text to InputField
        if (ocrTextField == null)
        {
            Debug.LogError("❌ OCR Text Field is NULL! Make sure it's assigned in the Inspector.");
            return;
        }

        ocrTextField.text = recognizedText;
        Debug.Log("✅ Text assigned to InputField: " + ocrTextField.text);

        // Force UI update
        ocrTextField.ForceLabelUpdate();
        ocrTextField.onValueChanged.Invoke(ocrTextField.text);
    }

    public void OnConfirmText()
    {
        string finalText = ocrTextField.text.Trim(); // Removes spaces & newlines

        if (string.IsNullOrEmpty(finalText))
        {
            Debug.LogError("⚠️ Cannot send empty or whitespace-only text to Wolfram!");
            return;
        }

        Debug.Log("📝 Final text sent to Wolfram: '" + finalText + "'");

        if (wolframScript != null)
        {
            wolframScript.ProcessEquation(finalText);
            Debug.Log("✅ Equation sent to Wolfram: " + finalText);
        }
        else
        {
            Debug.LogError("❌ Wolfram script is not assigned!");
        }
    }

    private Texture2D ConvertSpriteToTexture(Sprite sprite)
    {
        if (sprite == null)
        {
            Debug.LogError("❌ Sprite is NULL in ConvertSpriteToTexture!");
            return null;
        }

        Debug.Log("✅ Converting Sprite to Texture2D...");

        Texture2D texture = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
        Color[] pixels = sprite.texture.GetPixels(
            (int)sprite.textureRect.x, 
            (int)sprite.textureRect.y, 
            (int)sprite.textureRect.width, 
            (int)sprite.textureRect.height
        );
        texture.SetPixels(pixels);
        texture.Apply();

        if (texture == null)
        {
            Debug.LogError("❌ Conversion failed! Texture is NULL.");
        }
        else
        {
            Debug.Log("✅ Texture converted successfully.");
        }

        return texture;
    }

    private Texture2D ConvertToBlackAndWhite(Texture2D original)
    {
        Texture2D bwTexture = new Texture2D(original.width, original.height);
        Color[] pixels = original.GetPixels();

        for (int i = 0; i < pixels.Length; i++)
        {
            float grayscale = pixels[i].grayscale > 0.5f ? 1f : 0f;
            pixels[i] = new Color(grayscale, grayscale, grayscale);
        }

        bwTexture.SetPixels(pixels);
        bwTexture.Apply();
        return bwTexture;
    }
}
