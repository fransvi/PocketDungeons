using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletScript : MonoBehaviour {

    [SerializeField]
    private int _bulletDamage;
    [SerializeField]
    private float _speed;

    private bool _facingRight;

    // Use this for initialization
    void Start () {
       
        if (_facingRight)
        {
            GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(new Vector3(_speed, 2f, 0));
            Flip();
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(new Vector3(-_speed, 2f, 0));
        }
        GetComponent<Rigidbody2D>().AddForce(transform.forward * 30, ForceMode2D.Impulse);

    }

    public void createBullet(bool fr, float bf)
    {
        _facingRight = fr;
        _speed = bf;
        if(_speed < 10)
        {
            _speed = 10;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            other.gameObject.GetComponent<EnemyScript>().TakeDamage(_bulletDamage);
            Destroy(gameObject);
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject);
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Spikes"))
        {
            Destroy(gameObject);
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Platform"))
        {
            Destroy(gameObject);
        }
        if(other.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
        }

    }

    private void Flip()
    {
        // Switch player sprite heading
        _facingRight = !_facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    // Update is called once per frame
    void Update () {

        if (transform.GetComponent<Rigidbody2D>().velocity.x > 0 && !_facingRight)
        {
            //Flip();
        }
        else if (transform.GetComponent<Rigidbody2D>().velocity.x < 0 && _facingRight)
        {
            //Flip();
        }

    }
}
