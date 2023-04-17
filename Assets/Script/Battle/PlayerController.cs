using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public Transform movePoint;

    public float lifetimeBL;
    public LayerMask blockMe;
    [SerializeField]
    public Transform castPoint;
    [SerializeField]
    public float shotRange;
    public float Timer, MoveRate;
    public GameObject Blast, fakeSword, LifeController, SceneManager, Explosion;
    public Animator anim;
    public bool died;
    SpriteRenderer boi;
    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
        boi = transform.Find("sprite_0").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        MoveRate += Time.deltaTime;
        died = LifeController.GetComponent<LiifeContrroller>().died;
        if (died == true)
        {
            Destroy(gameObject);
            Instantiate(Explosion, transform.position, Quaternion.identity);


        }
        if (Timer >= 0.25f && Input.GetKeyDown(KeyCode.Z))
        {
            Shoot();
        }
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, speed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.X))
        {
            Slash();
        }

        if (MoveRate >= 0.16f )
        {
            if (Vector3.Distance(transform.position, movePoint.position) <= 0.1f)
            {
                if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
                {
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, blockMe))
                    {
                        movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                        MoveRate = 0;
                    }

                }
                else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
                {
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, blockMe))
                    {
                        movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical") * 0.8f, 0f);
                        MoveRate = 0;
                    }

                }
            }
        }
        else
        {
            transform.position = movePoint.position;
        }



        
        #region
        if (LocateEnemy(shotRange))
        {
            //attack
            RaycastHit hit;
            Ray ray = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.isTrigger)
                {
                    hit.transform.SendMessage("HitByRay");
                }
            }
            //animation

        }
        #endregion
        //no usar por ahora
    }
    bool LocateEnemy(float distance)
    {
        bool val = false;
        float castDist = distance;


        Vector2 endPos = castPoint.position + Vector3.right * castDist;

        RaycastHit2D hitRay = Physics2D.Linecast(castPoint.position, endPos, 1 << LayerMask.NameToLayer("Action"));

        if (hitRay.collider != null)
        {
            if (hitRay.collider.gameObject.CompareTag("Enemy"))
            {
                val = true;

            }
            else
            {
                val = false;
            }
            Debug.DrawLine(castPoint.position, hitRay.point, Color.yellow);
        }
        else
        {
            Debug.DrawLine(castPoint.position, endPos, Color.blue);
        }
        return val;
    } //No usar por ahora

    void Shoot()
    {
        
        
            Timer = 0;
            GameObject newbullet = (GameObject)Instantiate(Blast, castPoint.transform.position, Quaternion.Euler(0, 0, 0));
            Destroy(newbullet, 2.0f);
            anim.SetTrigger("Attack");
    }

    void Slash()
    {
        GameObject newbullet = (GameObject)Instantiate(fakeSword, castPoint.transform.position, Quaternion.Euler(0, 0, 0));
        Destroy(newbullet, lifetimeBL);
        anim.SetTrigger("Slash");
    }

    public void PaintPlayer()
    {
        StartCoroutine(DamageEffectSequence(boi, Color.red, 2, 0.01f));
    }

    public IEnumerator DamageEffectSequence(SpriteRenderer sr, Color dmgColor, float duration, float delay)
    {
        // save origin color

        sr.color = boi.color;
        Color originColor = sr.color;

        // tint the sprite with damage color
        sr.color = dmgColor;

        // you can delay the animation
        yield return new WaitForSeconds(delay);

        // lerp animation with given duration in seconds
        for (float t = 0; t < 0.05f; t += Time.deltaTime / duration)
        {
            sr.color = Color.Lerp(dmgColor, originColor, t);

            yield return null;
        }

        // restore origin color
        sr.color = originColor;
    }






}
