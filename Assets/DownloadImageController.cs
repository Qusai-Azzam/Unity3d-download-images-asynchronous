using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using static ImageUtilties;

public class DownloadImageController : MonoBehaviour
{
    private string firstImageURL = "https://i.ibb.co/g6V1VTn/image1.jpg";
    private string secondImageURL = "https://i.ibb.co/YL8MtZq/image2.jpg";
    private string spriteImageURL = "https://i.ibb.co/NrPkDSN/unity.png";

    public RawImage image1;
    public RawImage image2;
    public Image image3;

    void Start()
    {
        _ = DownloadImagesAsync();
    }

    public async UniTask DownloadImagesAsync()
    {
        image1.texture = await DownloadJPGImage(firstImageURL, "first");

        image2.texture = await DownloadJPGImage(secondImageURL, "second");

        image3.sprite = await DownloadPNGImage(spriteImageURL, "unity");
    }

    public async UniTask<Texture2D> DownloadJPGImage(string url , string name )
    {
        Texture2D img = await ImageDownloader.DownloadImage(url, name, FileFormat.JPG);
        return img;
    }

    public async UniTask<Sprite> DownloadPNGImage(string url, string name)
    {
        Texture2D img = await ImageDownloader.DownloadImage(url, name, FileFormat.PNG);
        return Sprite.Create(img, new Rect(0, 0, img.width, img.height), new Vector2(0.5f, 0.5f));
    }
}
