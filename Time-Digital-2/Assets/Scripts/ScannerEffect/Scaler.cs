﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaler : MonoBehaviour
{
    public float scanVelocity = 0.3f;
    private Vector3 scaleChange;

    public float maxDistance;
    // Start is called before the first frame update
    void Start()
    {
        scaleChange = new Vector3(scanVelocity * Time.deltaTime, scanVelocity * Time.deltaTime, scanVelocity * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale += scaleChange;

        if (this.transform.localScale.x >= maxDistance)
        {
            Destroy(this.gameObject);
        }
    }
}
