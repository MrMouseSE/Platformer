using System;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public Rigidbody SelfRb;
    private Rigidbody[] rbs;
    private void Start()
    {
        rbs = FindObjectsOfType<Rigidbody>();
    }

    private void Update()
    {
        foreach (var rb in rbs)
        {
            if (rb == SelfRb) continue;
            rb.AddForce(Vector3.Normalize(transform.position - rb.position) * CalculateGravityPower(rb));
        }
    }

    private float CalculateGravityPower(Rigidbody obj)
    {
        float gravityPower = 6.67f * (obj.mass * SelfRb.mass) /
                             Vector3.Magnitude(obj.transform.position - transform.position);
        if (Vector3.Magnitude(obj.transform.position - transform.position)>30)
        {
            gravityPower *= 2;
        }
        return gravityPower;
    }
}