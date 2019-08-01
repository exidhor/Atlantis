using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class HarborWindowFish : HarborWindow
{
    [Header("Fish Settings")]
    [SerializeField] Image _fishIcon;
    [SerializeField] TextMeshProUGUI _fishCount;
    [SerializeField] TextMeshProUGUI _fishPrice;

    public override void SetOpenInfo(Harbor harbor)
    {
        _fishIcon.sprite = harbor.priceIcon;
        _fishCount.text = harbor.priceCount.ToString();
        _fishPrice.text = "+" + harbor.rewardCount;
    }
}
