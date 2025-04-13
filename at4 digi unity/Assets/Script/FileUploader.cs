using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class FileUploader : MonoBehaviour {
    public Sprite imageSprite;

    // Example code doesn't use this function but it is here for reference. It's recommended to ask for permissions manually using the
    // RequestPermissionAsync methods prior to calling NativeFilePicker functions
    private async void RequestPermissionAsynchronously( bool readPermissionOnly = false ) {
        NativeFilePicker.Permission permission = await NativeFilePicker.RequestPermissionAsync( readPermissionOnly );
        Debug.Log( "Permission result: " + permission );
    }

    public void OpenFilePickerOld() {
        if (NativeFilePicker.IsFilePickerBusy()) return;

        RequestPermissionAsynchronously();

        #if UNITY_IOS 
            string[] fileTypes = new string[] { "public.image"};
        #else //ANDROID
            string[] fileTypes = new string[] { "image/*"};
        #endif

        NativeFilePicker.Permission permission = NativeFilePicker.PickMultipleFiles( ( paths ) =>
			{
				if( paths == null )
					Debug.Log( "Operation cancelled" );
				else
				{
					for( int i = 0; i < paths.Length; i++ )
						Debug.Log( "Picked file: " + paths[i] );
				}
			}, fileTypes );

			Debug.Log( "Permission result: " + permission );
    }
    public void OpenFilePicker()
    {
        string filePath = Application.persistentDataPath + "/uploadedImage.png"; // Path to store the selected image

        // Open file explorer and allow user to select an image (only works in standalone builds)
        #if UNITY_EDITOR
        string selectedPath = UnityEditor.EditorUtility.OpenFilePanel("Select Image", "", "png,jpg");

        // Check if a valid file is selected
        if (!string.IsNullOrEmpty(selectedPath))
        {
            // Copy the selected image to the application’s persistent data storage
            File.Copy(selectedPath, filePath, true);

            // Save the image path so it can be accessed later
            /*
            PlayerPrefs.SetString("UploadedImagePath", filePath);
            PlayerPrefs.Save();
            */

            byte[] imageBytes = File.ReadAllBytes(filePath);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(imageBytes);

            if (texture.LoadImage(imageBytes)){
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

                imageSprite = sprite;
                KeepOnScenes.keepOnScenes.imageSource = sprite;

                float aspectRatio = (float)texture.width / (float)texture.height;
                KeepOnScenes.keepOnScenes.aspectRatio = aspectRatio;

                Debug.Log("Image loaded successfully!");
            }
            else{
                Debug.LogError("Failed to load image.");
            }
        }
        #endif
    }
}