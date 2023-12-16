using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bounceable : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    public Rigidbody2D Rigidbody
    {
        get
        {
            if (_rigidbody == null)
                _rigidbody = GetComponent<Rigidbody2D>();

            return _rigidbody;
        }
    }

    public void Jump(float jumpVelocity)
    {
        var velocity = Rigidbody.velocity;
        velocity.y = jumpVelocity;
        Rigidbody.velocity = velocity;
    }
}