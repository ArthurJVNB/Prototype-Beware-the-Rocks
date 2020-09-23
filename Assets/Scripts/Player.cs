using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    public float acceleration = 10;
    //public Vector2 interactionPosition = Vector2.right;
    public float interactionDistance = 2f;
    public LayerMask interactionLayers;
    public int interactionDamage = 3;

    private Rigidbody2D rb2D;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            for (int i = 0; i < 1; i++)
            {
                Interact();
            }
        }
    }

    private void FixedUpdate()
    {
        MoveRight();
    }

    private void Jump()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * 10, ForceMode2D.Impulse);
    }

    private void MoveRight()
    {
        rb2D.AddForce(Vector3.right * acceleration, ForceMode2D.Force);

        //transform.position += Vector3.right * speed * Time.deltaTime;

        //if (rb2D.velocity.magnitude < acceleration)
        //{
        //    rb2D.AddForce(Vector3.right, ForceMode2D.Force);
        //}
    }

    private void Interact()
    {
        RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, Vector2.right, interactionDistance, interactionLayers);
        if (raycastHit2D && raycastHit2D.collider.TryGetComponent(out IDamagable damagable))
        {
            damagable.Damage(interactionDamage);
        }

        //Collider2D collider = Physics2D.OverlapBox((Vector2)transform.position + interactionPosition, new Vector2(.01f, .01f), 0);
        //if (collider && collider.TryGetComponent(out IDamagable damagable))
        //{
        //    damagable.Damage(0);
        //}
    }

    public void Damage(int damage)
    {
        Debug.Log("PLAYER DAMAGED");
        spriteRenderer.color = Color.red;
        Invoke("Deactivate", 3);

        TMPro.TextMeshProUGUI[] texts = FindObjectsOfType<TMPro.TextMeshProUGUI>();
        foreach (var text in texts)
        {
            text.text = "Morreeeeu";
        }
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + interactionDistance, transform.position.y));
        //Gizmos.DrawCube((Vector2)transform.position + interactionPosition, new Vector2(.1f, .1f));
    }
}
