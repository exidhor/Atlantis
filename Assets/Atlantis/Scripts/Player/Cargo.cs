using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Tools;

public class Cargo : MonoSingleton<Cargo>
{
    [SerializeField] Text _fishCountText;

    int _fishCount;

    public void AddFish(int count)
    {
        _fishCount += count;

        _fishCountText.text = "Fish count : " + _fishCount;
    }
}
