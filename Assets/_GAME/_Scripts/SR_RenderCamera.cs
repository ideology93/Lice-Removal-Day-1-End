using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.UI;
public class SR_RenderCamera : MonoBehaviour
{
    public SR_RenderCamera_After srAfter;
    public RawImage rawImage;
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
        LoadFilesBefore();
    }
    [Button]
    public void LoadLS()
    {
        LoadLastScreenshot();
    }
    public async void LoadLastScreenshot()
    {
        string folderPath = Application.persistentDataPath;
        string[] filePaths = await LoadFilesAsync(folderPath, "Before" + "*.png");
        string lastFile = filePaths[filePaths.Length - 1];
        byte[] fileData = await LoadFileDataAsync(lastFile);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(fileData);
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        if (sprite != null)
            GameManager.Instance.lastBeforeSprite = sprite;
        lastSprite = sprite;
        UI_Manager.Instance.endPanel.GetComponent<EndScreenLoadImage>().sprite_before = lastSprite;


    }
    public static string ScreenShotName(int width, int height)
    {
        return string.Format(Application.persistentDataPath + "/Before" + PlayerPrefs.GetInt("ScreenshotCounterBefore") + ".png",
                             Application.persistentDataPath,
                             width, height,
                             System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }
    [Button]
    public void CamCaptureBefore()
    {
        PlayerPrefs.SetInt("ScreenshotCounterBefore", PlayerPrefs.GetInt("ScreenshotCounterBefore") + 1);
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
        Debug.Log(lastFileName);
        takeHiResShot = false;
        GameManager.Instance.newGame = true;
        GetComponent<Camera>().enabled = false;

        LoadLS();
    }
    [Button]
    async void LoadFilesBefore()
    {
        GameManager.Instance.lookBook.Clear();
        string folderPath = Application.persistentDataPath;
        string[] filePaths = await LoadFilesAsync(folderPath, "Before" + "*.png");
        foreach (string filePath in filePaths)
        {

            byte[] fileData = await LoadFileDataAsync(filePath);

            Texture2D texture = new Texture2D(2, 2);

            texture.LoadImage(fileData);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

            if (sprite != null)
            {

                GameManager.Instance.beforePictures.Add(sprite);
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
    private void OnApplicationQuit()
    {
        Debug.Log("Delete file");
        if (!srAfter.wasPictureTaken && GameManager.Instance.newGame)
        {
            File.Delete(Application.persistentDataPath + "/Before" + PlayerPrefs.GetInt("ScreenshotCounterBefore") + ".png");
            PlayerPrefs.SetInt("ScreenshotCounterBefore", PlayerPrefs.GetInt("ScreenshotCounterBefore") - 1);
        }
    }

}