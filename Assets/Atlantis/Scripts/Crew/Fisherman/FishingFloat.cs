using UnityEngine;
using System.Collections;

public class FishingFloat : MonoBehaviour
{
    public void Appear()
    {
        gameObject.SetActive(true);
    }

    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }

    public void Stop()
    {
        gameObject.SetActive(false);
    }
}
