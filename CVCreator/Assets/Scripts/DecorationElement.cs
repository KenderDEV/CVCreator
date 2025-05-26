using UnityEngine;
using UnityEngine.UI;

public class DecorationElement : MonoBehaviour
{
    public enum DecorationType { Bar, Bar2, Point }
    public DecorationType decorationType;

    public Image decoration;
}
