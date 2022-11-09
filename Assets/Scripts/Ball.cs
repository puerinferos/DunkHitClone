using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector2 _startPosition;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize(Vector2 startPosition)
    {
        _startPosition = startPosition;
    }

    public void SetStartPosition()
    {
        transform.position = _startPosition;
    }

    public void FreezeVelocity(Transform transformParent)
    {
        transform.SetParent(transformParent);
        transform.localPosition = new Vector2(.06f,.2f);
        _rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void SetVelocity(Vector2 force)
    {
        _rb.constraints = RigidbodyConstraints2D.None;

        _rb.AddForce(force,ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<IInteractable>() != null)
            col.gameObject.GetComponent<IInteractable>().Interact();
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<ICollectible>() != null)
            col.gameObject.GetComponent<ICollectible>().Collect();
    }
}