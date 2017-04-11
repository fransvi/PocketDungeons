using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour {

    public float radius;
    public float power;
    public float fusetime;
    public float speed;
    public float damage;
    public GameObject explosionSprite;
    Rigidbody2D rb;


    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.TransformDirection(new Vector3(speed, 0, 0));
        rb.AddForce(transform.forward * 30, ForceMode2D.Impulse);
        StartCoroutine(Explode(fusetime));

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

        }

        Destroy(this.gameObject);



    }
    IEnumerator ExpEffect(float ft)
    {
        GameObject explosion = Instantiate(explosionSprite, transform.position, Quaternion.identity) as GameObject;
        Destroy(explosion, 1f);
        yield return new WaitForSeconds(ft);

    }
}

