using UnityEditor;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

[CustomEditor(typeof(CVContentElement))]
public class CVTextEditor : Editor
{
    public override void OnInspectorGUI()
    {
        CVContentElement el = (CVContentElement)target;

        el.typeOfContent = (CVContentElement.TypeOfContent)EditorGUILayout.EnumPopup("Type of Content", el.typeOfContent);

        EditorGUILayout.Space();

        switch (el.typeOfContent)
        {
            case CVContentElement.TypeOfContent.Name:
                el.PrincipalText = (TextMeshProUGUI)EditorGUILayout.ObjectField("Principal Text", el.PrincipalText, typeof(TextMeshProUGUI), true);
                break;
            case CVContentElement.TypeOfContent.Description:
                el.PrincipalText = (TextMeshProUGUI)EditorGUILayout.ObjectField("Principal Text", el.PrincipalText, typeof(TextMeshProUGUI), true);
                break;
            case CVContentElement.TypeOfContent.Biography:
                el.PrincipalText = (TextMeshProUGUI)EditorGUILayout.ObjectField("Texto", el.PrincipalText, typeof(TextMeshProUGUI), true);
                break;

            case CVContentElement.TypeOfContent.Contact:
                el.PrincipalText = (TextMeshProUGUI)EditorGUILayout.ObjectField("Texto", el.PrincipalText, typeof(TextMeshProUGUI), true);
                el.img = (Image)EditorGUILayout.ObjectField("Imagen", el.img, typeof(Image), true);
                break;

            case CVContentElement.TypeOfContent.Lenguaje:
                el.PrincipalText = (TextMeshProUGUI)EditorGUILayout.ObjectField("Texto (opcional)", el.PrincipalText, typeof(TextMeshProUGUI), true);
                el.img = (Image)EditorGUILayout.ObjectField("Imagen (opcional)", el.img, typeof(Image), true);
                break;

            case CVContentElement.TypeOfContent.AvancedSkills:
                el.PrincipalText = (TextMeshProUGUI)EditorGUILayout.ObjectField("Título", el.PrincipalText, typeof(TextMeshProUGUI), true);
                el.SecondaryText = (TextMeshProUGUI)EditorGUILayout.ObjectField("Descripción", el.SecondaryText, typeof(TextMeshProUGUI), true);
                break;

            case CVContentElement.TypeOfContent.Tools:
                el.PrincipalText = (TextMeshProUGUI)EditorGUILayout.ObjectField("Título", el.PrincipalText, typeof(TextMeshProUGUI), true);
                if (el.imgList == null)
                {
                    el.imgList = new List<Image>();
                }
                int removeIndex = -1;
                for (int i = 0; i < el.imgList.Count; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    el.imgList[i] = (Image)EditorGUILayout.ObjectField($"Imagen {i + 1}", el.imgList[i], typeof(Image), true);
                    if (GUILayout.Button("-", GUILayout.Width(20)))
                    {
                        removeIndex = i;
                    }
                    EditorGUILayout.EndHorizontal();
                }
                if (removeIndex >= 0)
                    el.imgList.RemoveAt(removeIndex);

                if (GUILayout.Button("Agregar imagen"))
                    el.imgList.Add(null);
                break;

            case CVContentElement.TypeOfContent.Education:
                el.PrincipalText = (TextMeshProUGUI)EditorGUILayout.ObjectField("Formación", el.PrincipalText, typeof(TextMeshProUGUI), true);
                el.SecondaryText = (TextMeshProUGUI)EditorGUILayout.ObjectField("Detalles", el.SecondaryText, typeof(TextMeshProUGUI), true);
                break;

            case CVContentElement.TypeOfContent.Skills:
                el.img = (Image)EditorGUILayout.ObjectField("Icono", el.img, typeof(Image), true);
                el.PrincipalText = (TextMeshProUGUI)EditorGUILayout.ObjectField("Texto", el.PrincipalText, typeof(TextMeshProUGUI), true);
                break;

            case CVContentElement.TypeOfContent.Experience:
                el.PrincipalText = (TextMeshProUGUI)EditorGUILayout.ObjectField("Puesto", el.PrincipalText, typeof(TextMeshProUGUI), true);
                el.SecondaryText = (TextMeshProUGUI)EditorGUILayout.ObjectField("Empresa", el.SecondaryText, typeof(TextMeshProUGUI), true);
                el.TertiaryText = (TextMeshProUGUI)EditorGUILayout.ObjectField("Descripción", el.TertiaryText, typeof(TextMeshProUGUI), true);
                break;
        }

        EditorGUILayout.Space();

        if (GUI.changed)
        {
            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(el);
        }
    }
}
