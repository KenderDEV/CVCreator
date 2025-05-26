using UnityEngine;

public class ConfigColors : MonoBehaviour
{
    [Header("Colors")]
    [SerializeField] private Color BackgroundLeftColor = new Color(70f, 70f, 70f, 1f);
    [SerializeField] private Color BackgroundRightColor = new Color(49f, 49f, 49f, 1f);

    [SerializeField] private Color Text1Color = new Color(255f, 255f, 255f, 1f);
    [SerializeField] private Color Text2Color = new Color(255f, 255f, 255f, 1f);
    [SerializeField] private Color Text3Color = new Color(255f, 255f, 255f, 1f);
    [SerializeField] private Color Text4Color = new Color(255f, 255f, 255f, 1f);
}
