using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class HarborWindowCrew : HarborWindow
{
    [Header("Crew Settings")]
    [SerializeField] Image _crewIcon;
    [SerializeField] TextMeshProUGUI _crewPrice;

    public override void SetOpenInfo(Harbor harbor)
    {
        _crewIcon.sprite = harbor.priceIcon;
        _crewPrice.text = harbor.priceCount.ToString();
    }
}
