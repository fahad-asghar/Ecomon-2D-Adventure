using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    Rigidbody2D rb;

    [Header("For enemy or platform")]
    [SerializeField] bool platformMove;
    [SerializeField] bool enemyMove;


    [Header("Move Horizontally, Vertically or Freely")]
    [SerializeField] bool horizontalMove;
    [SerializeField] bool verticalMove;
    [SerializeField] bool moveFreely;
    
    [Header("Y Movement from point to point")]
    [SerializeField] float pointY1;
    [SerializeField] float pointY2;

    [Header("X Movement from point to point")]
    [SerializeField] float pointX1;
    [SerializeField] float pointX2;

    [Header("Free movement vectors")]
    [SerializeField] Vector2 point1;
    [SerializeField] Vector2 point2;

    [Header("Move Speed")]
    [SerializeField] float moveSpeed;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        rb.useFullKinematicContacts = true;

        Move();
    }

    private void Move()
    {
        if (platformMove)
        {
            if (horizontalMove)
            {
                transform.DOMoveX(pointX1, moveSpeed, false).SetSpeedBased().SetEase(Ease.Linear).OnComplete(delegate ()
                {
                    transform.DOMoveX(pointX2, moveSpeed, false).SetSpeedBased().SetEase(Ease.Linear).OnComplete(delegate ()
                    {
                        Move();
                    });
                });
            }

            if (verticalMove)
            {
                transform.DOMoveY(pointY1, moveSpeed, false).SetSpeedBased().SetEase(Ease.Linear).OnComplete(delegate ()
                {
                    transform.DOMoveY(pointY2, moveSpeed, false).SetSpeedBased().SetEase(Ease.Linear).OnComplete(delegate ()
                    {
                        Move();
                    });
                });
            }

            if (moveFreely)
            {
                transform.DOMove(point1, moveSpeed, false).SetSpeedBased().SetEase(Ease.Linear).OnComplete(delegate ()
                {
                    transform.DOMove(point2, moveSpeed, false).SetSpeedBased().SetEase(Ease.Linear).OnComplete(delegate ()
                    {
                        Move();
                    });
                });
            }
        }

        else if (enemyMove)
        {
            if (horizontalMove)
            {              
                transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
                rb.DOMoveX(pointX1, moveSpeed, false).SetSpeedBased().SetEase(Ease.Linear).OnComplete(delegate ()
                {
                    transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
                    rb.DOMoveX(pointX2, moveSpeed, false).SetSpeedBased().SetEase(Ease.Linear).OnComplete(delegate ()
                    {
                        Move();
                    });
                });
            }

            if (verticalMove)
            {               
                rb.DOMoveY(pointY1, moveSpeed, false).SetSpeedBased().SetEase(Ease.Linear).OnComplete(delegate ()
                {
                    rb.DOMoveY(pointY2, moveSpeed, false).SetSpeedBased().SetEase(Ease.Linear).OnComplete(delegate ()
                    {
                        Move();
                    });
                });
            }

            if (moveFreely)
            {
                transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
                rb.DOMove(point1, moveSpeed, false).SetSpeedBased().SetEase(Ease.Linear).OnComplete(delegate ()
                {
                    transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
                    rb.DOMove(point2, moveSpeed, false).SetSpeedBased().SetEase(Ease.Linear).OnComplete(delegate ()
                    {
                        Move();
                    });
                });
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(platformMove)
            collision.transform.parent = transform;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(platformMove)
            collision.transform.parent = null;
    }
}
