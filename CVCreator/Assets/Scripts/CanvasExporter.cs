using UnityEngine;
using System.IO;

public class CanvasExporter : MonoBehaviour
{
    [SerializeField] Camera renderCamera;
    [SerializeField] int width = 2480;
    [SerializeField] int height = 3508;

    public void CaptureCV()
    {
        RenderTexture rt = new RenderTexture(width, height, 24);
        renderCamera.targetTexture = rt;

        Texture2D screenShot = new Texture2D(width, height, TextureFormat.RGB24, false);
        renderCamera.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        renderCamera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);

        byte[] bytes = screenShot.EncodeToPNG();
        string filename = Application.dataPath + "/CV_Exported.png";
        File.WriteAllBytes(filename, bytes);
        Debug.Log($"Saved screenshot to: {filename}");
    }
}
