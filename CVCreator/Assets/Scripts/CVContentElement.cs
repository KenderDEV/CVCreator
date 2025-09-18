using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CVContentElement : MonoBehaviour
{
    public enum TypeOfContent
    {
        Name, Description, Contact, Lenguaje, AvancedSkills, Tools, Biography, Education, Skills, Experience
    }

    [SerializeField] public TypeOfContent typeOfContent;

    [SerializeField] public TextMeshProUGUI PrincipalText;
    [SerializeField] public TextMeshProUGUI SecondaryText;
    [SerializeField] public TextMeshProUGUI TertiaryText;

    [SerializeField] public Image img;
    [SerializeField] public List<Image> imgList;

    [Header("Font Settings")]
    [SerializeField] public FontRange fontRangePrincipal;
    [SerializeField] public FontRange fontRangeSecondary;
    [SerializeField] public FontRange fontRangeTertiary;

}
[System.Serializable]
public struct FontRange
{
    public float min;
    public float max;
}
