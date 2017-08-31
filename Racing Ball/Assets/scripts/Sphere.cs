using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    public bool CanMove = false;
    public float Speed = 10f;

	void Update ()
    {
        if (!CanMove) return;

        var direction = Vector3.zero;

        if (Input.GetKey(KeyCode.UpArrow))
            direction += Vector3.forward;

        if (Input.GetKey(KeyCode.DownArrow))
            direction += Vector3.back;

        if (Input.GetKey(KeyCode.RightArrow))
            direction += Vector3.right;

        if (Input.GetKey(KeyCode.LeftArrow))
            direction += Vector3.left;

        var rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddForce(direction * Speed);
	}
}
