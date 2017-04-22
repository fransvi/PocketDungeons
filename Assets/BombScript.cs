using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour {

    public float radius;
    public float power;
    public float fusetime;
    public float speed;
    public float damage;
    public float _speed;
    public Animator _animator;

    public GameObject explosionSprite;
    Rigidbody2D rb;
    private bool _facingRight;


    // Use this for initialization
    void Start()
    {
        _animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.TransformDirection(new Vector3(speed, 0, 0));
        rb.AddForce(transform.forward * 30, ForceMode2D.Impulse);
        StartCoroutine(Explode(fusetime));
        _animator.Play("BombFuseAnim");

        if (_facingRight)
        {
            GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(new Vector3(_speed, 2f, 0));
            Flip();
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(new Vector3(-_speed, 2f, 0));
        }

    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    IEnumerator Explode(float ft)
    {

        yield return new WaitForSeconds(ft);
        StartCoroutine(ExpEffect(2f));
        Vector3 explosionPos = transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPos, radius);
        foreach (Collider2D hit in colliders)
        {
            if (hit.gameObject.tag == "Enemy" && hit.gameObject.GetComponent<Rigidbody2D>())
            {
                hit.gameObject.GetComponent<EnemyScript>().TakeDamage(damage);
            }
            if (hit.gameObject.tag == "Breakable")
            {
                hit.gameObject.GetComponent<BreakableWall>().BreakWall();
            }

        }

        Destroy(this.gameObject);



    }

    public void CreateBomb(bool b, float spd)
    {
        _facingRight = b;
        _speed = spd;
    }
    IEnumerator ExpEffect(float ft)
    {
        GameObject explosion = Instantiate(explosionSprite, transform.position, Quaternion.identity) as GameObject;
        Destroy(explosion, 0.5f);
        yield return new WaitForSeconds(ft);

    }
}

