using UnityEngine;

[SelectionBase]
public class JumpPad : TypedCollisionHandler<Bounceable>
{
    [SerializeField] protected float _jumpForce = 10f;

    public float JumpForce
    {
        get => _jumpForce;
        set => _jumpForce = value;
    }

    private void OnEnable()
    {
        CollisionEnter += CollisionWithDoodler;
        CollisionStay += CollisionStayWithDoodler;
    }

    private void OnDisable()
    {
        CollisionEnter -= CollisionWithDoodler;
        CollisionStay -= CollisionStayWithDoodler;
    }

    private void CollisionWithDoodler(Bounceable doodler, Collision2D collision)
    {
        if (collision.relativeVelocity.y < 0)
        {
            doodler.Jump(_jumpForce);
            OnJumped();
        }
    }

    private void CollisionStayWithDoodler(Bounceable doodler, Collision2D collision)
    {
        if (doodler.Rigidbody.velocity.y <= 0)
        {
            doodler.Jump(_jumpForce);
            OnJumped();
        }
    }

    protected virtual void OnJumped()
    { }
}