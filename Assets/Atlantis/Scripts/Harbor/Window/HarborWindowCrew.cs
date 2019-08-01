using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HarborWindowCrew : HarborWindow
{
    [Header("Crew Settings")]
    [SerializeField] TextMeshProUGUI _crewName;
    [SerializeField] Image _crewIcon;
    [SerializeField] TextMeshProUGUI _crewPrice;

    public override void SetOpenInfo(Harbor harbor)
    {
        _crewName.text = harbor.rewardName;
        _crewIcon.sprite = harbor.priceIcon;
        _crewPrice.text = "-" + harbor.priceCount.ToString();
    }
}
