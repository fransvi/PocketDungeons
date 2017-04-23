using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour {

    [SerializeField]
    private GameObject _rubble;
    [SerializeField]
    private Sprite[] _rubbleSprites;
    [SerializeField]
    private int _rubbleAmount;

    public void BreakWall()
    {
        for (int i = 0; i < _rubbleAmount; i++)
        {
            Vector3 newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z- 2);
            GameObject go = Instantiate(_rubble, newPos, transform.rotation);
            go.GetComponent<SpriteRenderer>().sprite = _rubbleSprites[i];
            float itemForcey = Random.Range(15, 60);
            float itemForcex = Random.Range(-60, 60);
            go.GetComponent<Rigidbody2D>().AddForce(new Vector2(itemForcex, itemForcey));
            Destroy(go, 5f);
        }
        Destroy(gameObject);
    }
}
