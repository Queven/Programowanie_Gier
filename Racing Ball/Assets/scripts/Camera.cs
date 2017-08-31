using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform ObjectToTrack;
    public Vector3 delta;
	
	void FixedUpdate ()
    {
        transform.LookAt(ObjectToTrack);

        var trackedRigidbody = ObjectToTrack.GetComponent<Rigidbody>();
        var speed = trackedRigidbody.velocity.magnitude;

        var targetPosition = ObjectToTrack.position + delta * (speed / 20f + 1f);

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            Time.smoothDeltaTime*3f);
	}
}
