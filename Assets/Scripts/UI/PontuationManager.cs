using TMPro;
using UnityEngine;

public class PontuationManager : MonoBehaviour
{
    public int points;
    public TextMeshProUGUI pointsText;

    void Update()
    {
        pointsText.text = points.ToString();
    }
}
