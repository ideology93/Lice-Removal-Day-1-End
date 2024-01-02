using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using NaughtyAttributes;
public class SR_RenderCamera_After : MonoBehaviour
{
    public bool wasPictureTaken;
    public int FileCounter = 0;
    public int resWidth = 480;
    public int resHeight = 640;
    public Camera cam;
    private bool takeHiResShot = false;
    public bool isLoaded;
    public string lastFileName;
    public Sprite lastSprite;
    private void Start()
    {
        cam = GetComponent<Camera>();
        LoadFiles();
    }

    public void LoadLS()
    {
        LoadLastScreenshot();
    }
    public async void LoadLastScreenshot()
    {
        string folderPath = Application.persistentDataPath;
        string[] filePaths = await LoadFilesAsync(folderPath, "After"+"*.png");
        string lastFile = filePaths[filePaths.Length - 1];
        byte[] fileData = await LoadFileDataAsync(lastFile);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(fileData);
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        if (sprite != null)
            GameManager.Instance.lastAfterSprite = sprite;
        lastSprite = sprite;
        Debug.Log("Loaded sprite is" + sprite.name);
        UI_Manager.Instance.endPanel.GetComponent<EndScreenLoadImage>().sprite_after = lastSprite;
    }
    public static string ScreenShotName(int width, int height)
    {
        return string.Format(Application.persistentDataPath + "/After" + PlayerPrefs.GetInt("ScreenshotCounterAfter") + ".png",
                             Application.persistentDataPath,
                             width, height,
                             System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }
    [Button]
    public void CamCaptureAfter()
    {
        PlayerPrefs.SetInt("ScreenshotCounterAfter", PlayerPrefs.GetInt("ScreenshotCounterAfter") + 1);
        RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
        cam.targetTexture = rt;
        Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
        cam.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
        cam.targetTexture = null;
        RenderTexture.active = null; // JC: added to avoid errors
        Destroy(rt);
        byte[] bytes = screenShot.EncodeToPNG();
        string filename = ScreenShotName(resWidth, resHeight);
        System.IO.File.WriteAllBytes(filename, bytes);
        lastFileName = filename;
        takeHiResShot = false;
        GetComponent<Camera>().enabled = false;
        wasPictureTaken = true;
        LoadLS();
    }
    [Button]
    async void LoadFiles()
    {
        GameManager.Instance.lookBook.Clear();
        string folderPath = Application.persistentDataPath;
        string[] filePaths = await LoadFilesAsync(folderPath, "After" + "*.png");
        foreach (string filePath in filePaths)
        {

            byte[] fileData = await LoadFileDataAsync(filePath);

            Texture2D texture = new Texture2D(2, 2);
            Debug.Log("File Data" + fileData);
            texture.LoadImage(fileData);
            Debug.Log("Texture name is " + texture.name);
            Debug.Log("File name" + string.Format(filePath));
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            Debug.Log("Sprite name is " + sprite.name);
            if (sprite != null)
            {

                GameManager.Instance.afterPictures.Add(sprite);
            }
        }
    }
    private async Task<string[]> LoadFilesAsync(string folderPath, string searchPattern)
    {
        return await Task.Run(() => Directory.GetFiles(folderPath, searchPattern));
    }
    private async Task<byte[]> LoadFileDataAsync(string filePath)
    {
        return await File.ReadAllBytesAsync(filePath);
    }
    

}