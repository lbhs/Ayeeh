﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideObjectScript : MonoBehaviour
{
    private Rigidbody rb;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // transform.LookAt(target);
        rb.AddForce(-transform.localPosition);
    }
}
