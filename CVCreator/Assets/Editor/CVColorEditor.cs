using UnityEngine;
using UnityEditor;
using TMPro;
using UnityEngine.UIElements;

public class CVColorEditor : EditorWindow
{
    // Preset
    private CVStylePreset stylePreset;
    //Background colors
    private Color backgroundLeftColor = new Color(70f / 255f, 70f / 255f, 70f / 255f, 1f);
    private Color backgroundRightColor = new Color(49f / 255f, 49f / 255f, 49f / 255f, 1f);
    //Text colors
    private Color TitleColor = new Color(255f / 255f, 255f / 255f, 255f / 255f, 1f);
    private Color SubtitleColor = new Color(255f / 255f, 255f / 255f, 255f / 255f, 1f);
    private Color SectionHeaderColor = new Color(255f / 255f, 255f / 255f, 255f / 255f, 1f);
    private Color SectionHeader2Color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 1f);
    private Color BodyColor = new Color(255f / 255f, 255f / 255f, 255f / 255f, 1f);
    private Color Body2Color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 1f);
    //Decoration colors
    private Color BarColor = new Color(255f / 255f, 255f / 255f, 255f / 255f, 1f);
    private Color Bar2Color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 1f);
    private Color PointColor = new Color(255f / 255f, 255f / 255f, 255f / 255f, 1f);

    // Menu
    [MenuItem("Tools/CV Editor/Color Manager")]
    public static void ShowWindow()
    {
        GetWindow<CVColorEditor>("Color Manager");
    }

    private void OnGUI()
    {
        GUILayout.Label("Preset", EditorStyles.boldLabel);
        stylePreset = (CVStylePreset)EditorGUILayout.ObjectField("Estilo", stylePreset, typeof(CVStylePreset), false);

        if (stylePreset != null)
        {
            if (GUILayout.Button("Load Preset"))
            {
                backgroundLeftColor = stylePreset.BackgroundLeftColor;
                backgroundRightColor = stylePreset.BackgroundRightColor;
                TitleColor = stylePreset.TitleColor;
                SubtitleColor = stylePreset.SubtitleColor;
                SectionHeaderColor = stylePreset.SectionHeaderColor;
                SectionHeader2Color = stylePreset.SectionHeader2Color;
                BodyColor = stylePreset.BodyColor;
                Body2Color = stylePreset.Body2Color;
                BarColor = stylePreset.BarColor;
                Bar2Color = stylePreset.Bar2Color;
                PointColor = stylePreset.PointColor;

                Debug.Log("Colores cargados desde el preset.");
            }
            if (GUILayout.Button("Save Preset"))
            {
                stylePreset.BackgroundLeftColor = backgroundLeftColor;
                stylePreset.BackgroundRightColor = backgroundRightColor;
                stylePreset.TitleColor = TitleColor;
                stylePreset.SubtitleColor = SubtitleColor;
                stylePreset.SectionHeaderColor = SectionHeaderColor;
                stylePreset.SectionHeader2Color = SectionHeader2Color;
                stylePreset.BodyColor = BodyColor;
                stylePreset.Body2Color = Body2Color;
                stylePreset.BarColor = BarColor;
                stylePreset.Bar2Color = Bar2Color;
                stylePreset.PointColor = PointColor;

                EditorUtility.SetDirty(stylePreset);
                AssetDatabase.SaveAssets();
                Debug.Log("Colores guardados en el preset.");
            }
        }


        GUILayout.Space(20);
        GUILayout.Label("Fondo", EditorStyles.boldLabel);
        backgroundLeftColor = EditorGUILayout.ColorField("Fondo Izquierda", backgroundLeftColor);
        backgroundRightColor = EditorGUILayout.ColorField("Fondo Derecha", backgroundRightColor);
        GUILayout.Space(5);
        GUILayout.Label("Textos", EditorStyles.boldLabel);
        TitleColor = EditorGUILayout.ColorField("Titulo", TitleColor);
        SubtitleColor = EditorGUILayout.ColorField("Subtitulo", SubtitleColor);
        SectionHeaderColor = EditorGUILayout.ColorField("Encabezado 1", SectionHeaderColor);
        SectionHeader2Color = EditorGUILayout.ColorField("Encabezado 2", SectionHeader2Color);
        BodyColor = EditorGUILayout.ColorField("Cuerpo 1", BodyColor);
        Body2Color = EditorGUILayout.ColorField("Cuerpo 2", Body2Color);
        GUILayout.Space(5);
        GUILayout.Label("Decoración", EditorStyles.boldLabel);
        BarColor = EditorGUILayout.ColorField("Barra 1", BarColor);
        Bar2Color = EditorGUILayout.ColorField("Barra 2", Bar2Color);
        PointColor = EditorGUILayout.ColorField("Punto", PointColor);
        GUILayout.Space(10);
        if (GUILayout.Button("Aplicar Colores"))
        {
            ApplyColors();
        }
    }

    private void ApplyColors()
    {
        foreach (var bg in FindObjectsByType<BackgroundElement>(FindObjectsSortMode.None))
        {
            if (bg.backgroundImage == null)
            {
                Debug.LogWarning($"Falta la Instancia: Nombre: {bg.name}, Padre: {bg.transform.parent.name}");
                continue;
            }
            if (bg.backgroundType == BackgroundElement.BackgroundType.Left)
            {
                bg.backgroundImage.color = backgroundLeftColor;
            }
            else if (bg.backgroundType == BackgroundElement.BackgroundType.Right)
            {
                bg.backgroundImage.color = backgroundRightColor;
            }
            EditorUtility.SetDirty(bg.backgroundImage);
        }

        foreach (var text in FindObjectsByType<TextElement>(FindObjectsSortMode.None))
        {
            if (text.textComponent == null)
            {
                Debug.LogWarning($"Falta la Instancia: Nombre: {text.name}, Padre: {text.transform.parent.name}");
                continue;
            }
            switch (text.textType)
            {
                case TextElement.TextType.Title:
                    text.textComponent.color = TitleColor;
                    break;
                case TextElement.TextType.Subtitle:
                    text.textComponent.color = SubtitleColor;
                    break;
                case TextElement.TextType.SectionHeader:
                    text.textComponent.color = SectionHeaderColor;
                    break;
                case TextElement.TextType.SectionHeader2:
                    text.textComponent.color = SectionHeader2Color;
                    break;
                case TextElement.TextType.Body:
                    text.textComponent.color = BodyColor;
                    break;
                case TextElement.TextType.Body2:
                    text.textComponent.color = Body2Color;
                    break;
            }
            EditorUtility.SetDirty(text.textComponent);
        }

        foreach (var deco in FindObjectsByType<DecorationElement>(FindObjectsSortMode.None))
        {
            if (deco.decoration == null)
            {
                Debug.LogWarning($"Falta la Instancia: Nombre: {deco.name}, Padre: {deco.transform.parent.name}");
                continue;
            }
            switch (deco.decorationType)
            {
                case DecorationElement.DecorationType.Bar:
                    deco.decoration.color = BarColor;
                    break;
                case DecorationElement.DecorationType.Bar2:
                    deco.decoration.color = Bar2Color;
                    break;
                case DecorationElement.DecorationType.Point:
                    deco.decoration.color = PointColor;
                    break;
            }
            EditorUtility.SetDirty(deco.decoration);
        }

        UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(UnityEngine.SceneManagement.SceneManager.GetActiveScene());
        Debug.Log("Colores aplicados correctamente.");
    }
}
