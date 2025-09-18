using TMPro;
using UnityEngine;

public class GrupeOfText : MonoBehaviour
{
    enum TypeOfGrupe { Type1, Type2, Type3 }

    [SerializeField] private TypeOfGrupe typeOfGrupe;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI header = null;
    [SerializeField] private TextMeshProUGUI body = null;


    public void SetText(string titleText, string headerText, string bodyText)
    {
        if (title != null)
        {
            title.text = titleText;
        }

        if (header != null)
        {
            header.text = headerText;
        }

        if (body != null)
        {
            body.text = bodyText;
        }
    }
}
