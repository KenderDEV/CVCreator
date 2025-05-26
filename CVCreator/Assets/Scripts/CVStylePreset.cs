using TMPro;
using UnityEngine;
[CreateAssetMenu(fileName = "NewCVStyle", menuName = "CV/Style Preset", order = 1)]
public class CVStylePreset : ScriptableObject
{
    [Header("Colors")]
    public Color BackgroundLeftColor = Color.white;
    public Color BackgroundRightColor = Color.white;
    public Color TitleColor = Color.white;
    public Color SubtitleColor = Color.white;
    public Color SectionHeaderColor = Color.white;
    public Color SectionHeader2Color = Color.white;
    public Color BodyColor = Color.white;
    public Color Body2Color = Color.white;
    public Color BarColor = Color.white;
    public Color Bar2Color = Color.white;
    public Color PointColor = Color.white;
    [Header("Fonts")]
    public TMP_FontAsset TextFont;
    public Vector2 TitleFontSize = Vector2.one;
    public Vector2 SubtitleFontSize = Vector2.one;
    public Vector2 SectionHeaderFontSize = Vector2.one;
    public Vector2 SectionHeader2FontSize = Vector2.one;
    public Vector2 BodyFontSize = Vector2.one;
    public Vector2 Body2FontSize = Vector2.one;

}
