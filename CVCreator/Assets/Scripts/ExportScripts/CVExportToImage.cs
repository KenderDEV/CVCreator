using UnityEngine;
using itextsharp.pdfa.iTextSharp.text;
using itextsharp.pdfa.iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

public class CVExportToImage : MonoBehaviour
{
    public Camera exportCamera;
    public RectTransform panelToExport;
    public int width = 2480;
    public int height = 3508;
    public string outputName = "CV_Page";

    void Start()
    {
        MaxResolution();
        ExportPanelAsImage();
    }

    void MaxResolution()
    {
        width = width * 2;
        height = height * 2;
    }

    public void ExportPanelAsImage()
    {
        if (panelToExport == null) return;

        //camara temp
        GameObject camObj = new GameObject("ExportCamera");
        Camera cam = camObj.AddComponent<Camera>();
        cam.orthographic = true;
        cam.clearFlags = CameraClearFlags.SolidColor;
        cam.backgroundColor = new Color32(0, 0, 0, 0);
        cam.cullingMask = LayerMask.GetMask("UI");

        //render texture
        RenderTexture rt = new RenderTexture(width, height, 24, RenderTextureFormat.ARGB32);
        rt.antiAliasing = 8;
        rt.useMipMap = false;
        rt.filterMode = FilterMode.Bilinear;
        cam.targetTexture = rt;

        //Posicionar camera front of panel
        Canvas canvas = panelToExport.GetComponentInParent<Canvas>();
        if (canvas.renderMode != RenderMode.WorldSpace)
        {
            Debug.LogError("canvas debe estar en worldSpace");
            DestroyImmediate(camObj);
            return;
        }

        //asume que el panel esta bien escalado
        Vector2 panelSize = panelToExport.rect.size;
        Vector3 panelScale = panelToExport.lossyScale;
        float panelWidth = panelSize.x * panelScale.x;
        float panelHeight = panelSize.y * panelScale.y;

        float dpiMultplier = 2.0f;
        int newWidth = Mathf.RoundToInt(panelWidth * dpiMultplier);
        int newHeight = Mathf.RoundToInt(panelHeight * dpiMultplier);

        cam.orthographicSize = panelHeight / 2f;

        //Vector3 panelWorldCenter = panelToExport.position + new Vector3(0, -panelHeight / 2f, 0);
        cam.transform.position = new Vector3(0, 0, -10);
        cam.transform.rotation = Quaternion.identity;

        //capture
        cam.Render();
        RenderTexture.active = rt;

        Texture2D image = new Texture2D(width, height, TextureFormat.RGB24, false);
        image.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        image.Apply();

        byte[] bytes = image.EncodeToPNG();
        string path = Application.dataPath + "/" + outputName + ".png";
        File.WriteAllBytes(path, bytes);

        Debug.Log("Imagen Exportada" + path);

        //limpieza
        RenderTexture.active = null;
        cam.targetTexture = null;
        DestroyImmediate(rt);
        DestroyImmediate(camObj);

        string pdfPath = Application.dataPath + "/" + "CV" + ".pdf";
        ExportPageToPDF(path, pdfPath);
    }

    public static void ExportPageToPDF(string imagePath, string pdfPath)
    {
        Document doc = new Document(PageSize.A4);
        PdfWriter.GetInstance(doc, new FileStream(pdfPath, FileMode.Create));
        doc.Open();

        iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imagePath);
        img.ScaleToFit(PageSize.A4.Width, PageSize.A4.Height);
        img.SetAbsolutePosition(0, 0);
        doc.Add(img);

        doc.Close();
        Debug.Log("PDF creado. " + pdfPath);
    }
}

