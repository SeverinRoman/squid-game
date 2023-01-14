using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovementController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float speedTurn;

    private Rigidbody _rigidbody;
    public float CurrentSpeed => Mathf.Clamp01(new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z).magnitude);

    void Awake()
    {
        _rigidbody = this.GetComponent<Rigidbody>();
    }

    public void Move(float horizontal, float vertical)
    {
        horizontal *= speed;
        vertical *= speed;

        _rigidbody.velocity = Vector3.forward * vertical + Vector3.right * horizontal + Vector3.up * _rigidbody.velocity.y;
    }

    public void Rotate(float horizontal, float vertical)
    {
        _rigidbody.rotation = Quaternion.Slerp(_rigidbody.rotation,
           Quaternion.Euler(Vector3.up *
           (Mathf.Rad2Deg * Mathf.Atan2(horizontal, vertical))),
           speedTurn * Time.deltaTime);
    }
}
