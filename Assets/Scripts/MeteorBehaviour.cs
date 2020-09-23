using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorBehaviour : MonoBehaviour
{
    [Tooltip("In degrees")]
    public float fallingAngle = -45f;
    public float fallingSpeed = 100f;

    Rigidbody2D rb2d;
    Vector3 fallingDirection;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        fallingDirection = new Vector2(Mathf.Cos(fallingAngle), Mathf.Sin(fallingAngle));
    }

    private void FixedUpdate()
    {
        //transform.position += fallingDirection * fallingSpeed * Time.fixedDeltaTime;
        rb2d.MovePosition(transform.position + fallingDirection * fallingSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out IDamagable damagable))
        {
            damagable.Damage(1);
        }
        
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 direction = new Vector2(Mathf.Cos(fallingAngle), Mathf.Sin(fallingAngle));
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + direction * 3f);
    }
}
