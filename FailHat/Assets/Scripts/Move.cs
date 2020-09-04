using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Move : MonoBehaviour
{
    [FormerlySerializedAs("_speed")] public float Speed = 3f;

    private void Update() {

        if (Input.GetAxis("Vertical") != 0)
        {
            transform.position += transform.forward * (Time.deltaTime * Speed * Input.GetAxis("Vertical"));
        }

        if (Input.GetAxis("Horizontal") != 0)
        {
            transform.position += transform.right * (Time.deltaTime * Speed * Input.GetAxis("Horizontal"));
        }
    }
}
