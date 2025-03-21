using UnityEngine;
using System.Collections;

public class FileUploader : MonoBehaviour
{
    // Example code doesn't use this function but it is here for reference. It's recommended to ask for permissions manually using the
    // RequestPermissionAsync methods prior to calling NativeFilePicker functions
    private async void RequestPermissionAsynchronously( bool readPermissionOnly = false ) {
        NativeFilePicker.Permission permission = await NativeFilePicker.RequestPermissionAsync( readPermissionOnly );
        Debug.Log( "Permission result: " + permission );
    }
    public void OpenFilePicker() {
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
}
