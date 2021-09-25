using Cysharp.Threading.Tasks;
using System.IO;
using UnityEngine;

public static class ImageUtilties 
{
    public static class FileFormat {
        public const string JPG = "JPG";
        public const string PNG = "PNG";
    }

        public static class DirectoryUtils
        {
            public static void CheckDirectory(string dir)
            {
                if (!Directory.Exists(dir)) {
                    Directory.CreateDirectory(dir);
                }
            }
        }
        public static class FileUtils {

        public const int BUFFER_SIZE = 0x4096;

        /// <summary>
        /// Read file content as byte array  
        /// </summary>
        /// <param name="filePath"> image path </param>
        /// <returns> byte array of the image </returns>
        public static async UniTask<byte[]> ReadImageBytes(string filePath) {
            using (FileStream readStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, BUFFER_SIZE, true)) { 
                byte [] data = new byte[readStream.Length];
                await readStream.ReadAsync(data, 0, (int)data.Length);
                return data;
            }
        }

        /// <summary>
        /// Turns byte array into Texture2D
        /// </summary>
        /// <param name="data"></param>
        /// <returns> Texture2D from byte arrau </returns>
        public static Texture2D GetTextureFromByteArray(byte[] data) {
            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(data);
            texture.Apply();
            return texture;
        }

        /// <summary>
        /// writes the content od the byte array into and image file based on the provided file format 
        /// </summary>
        /// <param name="image"> Textute2D image</param>
        /// <param name="filePath"> path where to save image</param>
        /// <param name="format"> the format of the image </param>
        /// <returns></returns>
        public static async UniTask<string> SaveImage(Texture2D image, string filePath, string format)
        {
            byte[] imageData = null;
            switch (format)
            {
                case FileFormat.JPG:
                    imageData = image.EncodeToJPG();
                    break;

                case FileFormat.PNG:
                    imageData = image.EncodeToPNG();
                    break;
            }
            await WriteByteArray( imageData , filePath);
            return filePath;
        }

        /// <summary>
        /// writes a byte array content into a path
        /// </summary>
        /// <param name="data"> array of bytes </param>
        /// <param name="filePath"> file path </param>
        /// <returns></returns>
        public static async UniTask WriteByteArray(byte [] data, string filePath)
        {
            using (var fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None, BUFFER_SIZE, true))
            {
                await fileStream.WriteAsync(data, 0, data.Length);
            }
        }
    }



}
