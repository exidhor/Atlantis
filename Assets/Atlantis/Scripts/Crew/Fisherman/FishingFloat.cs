using UnityEngine;
using System.Collections;

public class FishingFloat : MonoBehaviour
{
    public void Appear()
    {
        gameObject.SetActive(true);
    }

    public void Stop()
    {
        gameObject.SetActive(false);
    }
}
