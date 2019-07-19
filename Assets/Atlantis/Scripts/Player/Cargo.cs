using UnityEngine;
using UnityEngine.UI;
using System;
using Tools;

public class Cargo : MonoSingleton<Cargo>
{
    [Serializable]
    class FishCounter
    {
        public int count
        {
            get { return _fishCount; }
        }

        [SerializeField] Text _countText;
        [SerializeField] Sprite _spriteEnable;
        [SerializeField] Image _image;
        int _fishCount;

        public void Add(int count)
        {
            _fishCount += count;
            _countText.text = _fishCount.ToString();

            _image.sprite = _spriteEnable;
        }
    }

    [SerializeField] FishCounter _littleFish;
    [SerializeField] FishCounter _bigFish;
    [SerializeField] FishCounter _octopus;

    public void AddFish(int count, FishType type)
    {
        switch(type)
        {
            case FishType.bigFish:
                _bigFish.Add(count);
                break;

            case FishType.littleFish:
                _littleFish.Add(count);
                break;

            case FishType.octopus:
                _octopus.Add(count);
                break;
        }

        CheckForWin();
    }

    void CheckForWin()
    {
        if(_littleFish.count > 0 
            && _bigFish.count > 0
            && _octopus.count > 0)
        {
            MainManager.instance.EndGame();
        }
    }
}
