using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletScript : MonoBehaviour {

    [SerializeField]
    private int _bulletDamage;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private bool _enemyProjectile;
    [SerializeField]
    private bool _dartTrapProjectile;

    private bool _facingRight;

    private bool _ignoreGroundCollider;

    // Use this for initialization
    void Start () {
        if (!_enemyProjectile)
        {
            Physics2D.IgnoreLayerCollision(18, 0, true);
        }
        else
        {
            Physics2D.IgnoreLayerCollision(18, 0, false);
        }

        if (_dartTrapProjectile)
        {
            StartCoroutine(IgnoreGround());
        }
        Physics2D.IgnoreLayerCollision(18, 16);
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
        Destroy(gameObject, 3f);

    }

    IEnumerator IgnoreGround()
    {
        _ignoreGroundCollider = true;
        yield return new WaitForSeconds(1f);
        _ignoreGroundCollider = false;
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
        if (other.gameObject.tag == "Player" && _enemyProjectile)
        {

            other.gameObject.GetComponent<PlayerController>().Hurt(1);
            Destroy(gameObject);
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            other.gameObject.GetComponent<EnemyScript>().TakeDamage(_bulletDamage);
            Destroy(gameObject);
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground") && !_dartTrapProjectile)
        {

            Destroy(gameObject);
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Spikes") && !_dartTrapProjectile)
        {
            Destroy(gameObject);
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Platform") && !_dartTrapProjectile)
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
