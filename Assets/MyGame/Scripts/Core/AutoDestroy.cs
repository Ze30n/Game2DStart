using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("XuanTien/AutoDestroy")]

public class AutoDestroy : MonoBehaviour
{
    public float timeDelay = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timeDelay);
    }
}
