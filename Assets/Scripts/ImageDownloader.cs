using Cysharp.Threading.Tasks;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public static class ImageDownloader 
{
    static readonly string ImageDirectory = Application.persistentDataPath + "/Images";

    public static async UniTask<Texture2D> DownloadImage(string url,string imageName, string imageFormat )
    {
       

        string ImagePath = ImageDirectory + "/" + imageName + "." + imageFormat.ToLower();

        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url))
        {
            await request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log($"Error in downloading image : {request.error}");
                return null;
            }
            else
            {
                Texture2D myTexture = DownloadHandlerTexture.GetContent(request);
                ImageUtilties.DirectoryUtils.CheckDirectory(ImageDirectory);
                await ImageUtilties.FileUtils.SaveImage(myTexture, ImagePath, imageFormat);
                return myTexture;
            }

        }
    }
}
