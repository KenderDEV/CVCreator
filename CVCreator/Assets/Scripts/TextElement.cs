using UnityEngine;
using TMPro;
public class TextElement : MonoBehaviour
{
    public enum TextType { Title, Subtitle, SectionHeader, SectionHeader2, Body, Body2 }
    public TextType textType;

    public TextMeshProUGUI textComponent;
}
