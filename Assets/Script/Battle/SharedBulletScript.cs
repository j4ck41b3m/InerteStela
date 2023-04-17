using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedBulletScript : MonoBehaviour
{
    public float speed;
    
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            if (collision.transform.gameObject.GetComponent<ThughController>() != null)
            {
                collision.transform.gameObject.GetComponent<ThughController>().CheckLifesThugh8();

            }
            else if (collision.transform.gameObject.GetComponent<TurretController>())
            {
                collision.transform.gameObject.GetComponent<TurretController>().CheckLifesTurret8();

            }

            Destroy(gameObject);
        }
    }
}
