using UnityEngine;
using UnityEditor;
using TMPro;
using System.Collections.Generic;

public class CVGlobalEditor : EditorWindow
{
    private Vector2 scrollPos;
    private CVContentPreset preset;

    [MenuItem("Tools/CV Editor/Editor General")]
    public static void ShowWindow()
    {
        GetWindow<CVGlobalEditor>("CV Editor");
    }

    private void OnGUI()
    {
        GUILayout.Label("Global CV Editor", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        GameObject leftRoot = GameObject.FindGameObjectWithTag("Left");
        GameObject rightRoot = GameObject.FindGameObjectWithTag("Right");

        if (leftRoot == null || rightRoot == null)
        {
            EditorGUILayout.HelpBox("Please ensure that the scene has GameObjects tagged 'Left' and 'Right'.", MessageType.Warning);
            return;
        }

        var allElements = FindObjectsByType<CVContentElement>(FindObjectsSortMode.None);

        if (allElements.Length == 0)
        {
            EditorGUILayout.HelpBox("No CV Content Elements found in the scene.", MessageType.Warning);
            return;
        }

        Dictionary<CVContentElement.TypeOfContent, List<CVContentElement>> leftElements = new();
        Dictionary<CVContentElement.TypeOfContent, List<CVContentElement>> rightElements = new();


        foreach (var el in allElements)
        {
            bool isLeft = el.transform.IsChildOf(leftRoot.transform);
            var targetDict = isLeft ? leftElements : rightElements;

            if (!targetDict.ContainsKey(el.typeOfContent))
            {
                targetDict[el.typeOfContent] = new List<CVContentElement>();
            }
            targetDict[el.typeOfContent].Add(el);
        }

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.BeginVertical(GUILayout.Width(position.width / 2 - 10));
        GUILayout.Label("Left Section", EditorStyles.boldLabel);
        DrawSectionColumn(leftElements);
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical(GUILayout.Width(position.width / 2 - 10));
        GUILayout.Label("Right Section", EditorStyles.boldLabel);
        DrawSectionColumn(rightElements);
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndScrollView();

        GUILayout.Label("Preset", EditorStyles.boldLabel);
        preset = (CVContentPreset)EditorGUILayout.ObjectField("Preset", null, typeof(CVContentPreset), false);

        if (preset != null)
        {
            if (GUILayout.Button("Load Preset"))
            {
                // Load preset logic here

            }
            if (GUILayout.Button("Save Preset"))
            {
                // Save preset logic here

            }
        }
    }

    private void DrawSectionColumn(Dictionary<CVContentElement.TypeOfContent, List<CVContentElement>> dict)
    {
        bool hasPrincipalText = false;
        bool hasSecondaryText = false;
        bool hasTertiaryText = false;
        foreach (CVContentElement.TypeOfContent type in System.Enum.GetValues(typeof(CVContentElement.TypeOfContent)))
        {
            if (!dict.ContainsKey(type) || dict[type].Count == 0)
            {
                continue; // Skip if no elements of this type
            }

            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField($"Sección: {type}", EditorStyles.boldLabel);

            foreach (var el in dict[type])
            {
                EditorGUILayout.BeginVertical("box");
                GUILayout.Label($"Elemento: {el.name}", EditorStyles.miniLabel);

                if (el.PrincipalText != null)
                {
                    el.PrincipalText.text = EditorGUILayout.TextField("Texto principal", el.PrincipalText.text, GUILayout.Height(el.PrincipalText.preferredHeight / 2));
                    hasPrincipalText = true;
                }

                if (el.SecondaryText != null)
                {
                    el.SecondaryText.text = EditorGUILayout.TextField("Texto secundario", el.SecondaryText.text, GUILayout.Height(el.SecondaryText.preferredHeight / 2));
                    hasSecondaryText = true;
                }

                if (el.TertiaryText != null)
                {
                    el.TertiaryText.text = EditorGUILayout.TextField("Texto terciario", el.TertiaryText.text, GUILayout.Height(el.TertiaryText.preferredHeight / 2));
                    hasTertiaryText = true;
                }

                if (el.img != null)
                {
                    el.img.sprite = (Sprite)EditorGUILayout.ObjectField("Imagen", el.img.sprite, typeof(Sprite), false);
                }

                if (el.imgList != null && el.imgList.Count > 0)
                {
                    EditorGUILayout.LabelField("Lista de imágenes:");
                    for (int i = 0; i < el.imgList.Count; i++)
                    {
                        var img = el.imgList[i];
                        if (img != null)
                        {
                            img.sprite = (Sprite)EditorGUILayout.ObjectField($"Imagen {i + 1}", img.sprite, typeof(Sprite), false);
                        }
                    }
                }

                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("🅰 Ajuste Global de Fuente para esta Sección", EditorStyles.boldLabel);
            // Inicializar rangos
            float principalMin = 0, principalMax = 0;
            float secundarioMin = 0, secundarioMax = 0;
            float terciarioMin = 0, terciarioMax = 0;

            foreach (var el in dict[type])
            {
                if (el.PrincipalText != null)
                {
                    principalMin = el.PrincipalText.fontSizeMin;
                    principalMax = el.PrincipalText.fontSizeMax;
                }
                if (el.SecondaryText != null)
                {
                    secundarioMin = el.SecondaryText.fontSizeMin;
                    secundarioMax = el.SecondaryText.fontSizeMax;
                }
                if (el.TertiaryText != null)
                {
                    terciarioMin = el.TertiaryText.fontSizeMin;
                    terciarioMax = el.TertiaryText.fontSizeMax;
                }
                break; // Solo necesitamos los valores de uno de los elementos
            }

            // Mostrar campos de entrada
            if (hasPrincipalText)
            {
                principalMin = EditorGUILayout.FloatField("Principal - Min", principalMin);
                principalMax = EditorGUILayout.FloatField("Principal - Max", principalMax);
            }
            if (hasSecondaryText)
            {
                secundarioMin = EditorGUILayout.FloatField("Secundario - Min", secundarioMin);
                secundarioMax = EditorGUILayout.FloatField("Secundario - Max", secundarioMax);
            }
            if (hasTertiaryText)
            {
                terciarioMin = EditorGUILayout.FloatField("Terciario - Min", terciarioMin);
                terciarioMax = EditorGUILayout.FloatField("Terciario - Max", terciarioMax);
            }

            // Aplicar a todos
            if (GUILayout.Button("Aplicar tamaño de fuente"))
            {
                foreach (var el in dict[type])
                {
                    if (el != null)
                    {
                        el.fontRangePrincipal.min = principalMin;
                        el.fontRangePrincipal.max = principalMax;

                        el.fontRangeSecondary.min = secundarioMin;
                        el.fontRangeSecondary.max = secundarioMax;

                        el.fontRangeTertiary.min = terciarioMin;
                        el.fontRangeTertiary.max = terciarioMax;

                        // Actualizar visualmente si tienen referencias
                        if (hasPrincipalText && el.PrincipalText != null)
                        {
                            el.PrincipalText.fontSizeMin = principalMin;
                            el.PrincipalText.fontSizeMax = principalMax;
                        }

                        if (hasSecondaryText && el.SecondaryText != null)
                        {
                            el.SecondaryText.fontSizeMin = secundarioMin;
                            el.SecondaryText.fontSizeMax = secundarioMax;
                        }

                        if (hasTertiaryText && el.TertiaryText != null)
                        {
                            el.TertiaryText.fontSizeMin = terciarioMin;
                            el.TertiaryText.fontSizeMax = terciarioMax;
                        }

                        EditorUtility.SetDirty(el);
                    }
                }
            }
        }
    }
}
