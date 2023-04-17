using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThughController : MonoBehaviour
{
    public float speed = 5f;
    public Transform movePoint;
    public Transform target;
    public LayerMask blockHim;
    float timer, attackRate;
    public GameObject Blast, Explosion;
    public Animator anim;
    public int textHeight;
    public int totallifes;
   int currentThuanklifes, randomMovement;
    SpriteRenderer boi;

    //int xDir = 0;
    int yDir = 0;

    Transform mTransform;
    Transform mTextOverTransform;

    [SerializeField]
    public Text mTextOverHead;
    [SerializeField]
    public Transform castPoint;
    [SerializeField]
    public float shotRange;
    // Start is called before the first frame update
    void Start()
    {
        
            currentThuanklifes = totallifes;
        movePoint.parent = null;
        if (GameObject.FindWithTag("Player") != null)
        {
            target = GameObject.FindWithTag("Player").transform;
            
        }
        boi = transform.Find("thug_0").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        string _crlifes = currentThuanklifes.ToString();
        mTextOverHead.text = _crlifes;
        if (currentThuanklifes <= 0)
        {
            Destroy(gameObject);
            //print("me mueri Wei");
            Instantiate(Explosion, transform.position, Quaternion.identity);

        }

        timer += Time.deltaTime;
        attackRate += Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, speed * Time.deltaTime);

        Movement();
        
    }
    bool LocatePlayer(float distance)
    {
        bool val = false;
        float castDist = distance;


        Vector2 endPos = castPoint.position + Vector3.left * castDist;

        RaycastHit2D hitRay = Physics2D.Linecast(castPoint.position, endPos, 1 << LayerMask.NameToLayer("Action"));

        if (hitRay.collider != null)
        {
            if (hitRay.collider.gameObject.CompareTag("Player"))
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
    }
    void Movement()
    {
        if (timer >= 0.5f)
        {
            timer = 0;
            //Ya no sirve, por ahora
            #region
            /*float dife = Mathf.Abs(target.position.y - transform.position.y);
             print(dife);

             if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, dife, 0f), .2f, blockHim))
             {
                 if (dife >= 1)
                 {
                     print("arriba");
                     movePoint.position += new Vector3(0f, transform.position.y + 1, 0f);
                 }
                 else if (dife <= 0.7f)
                 {
                     print("abajo");
                     movePoint.position += new Vector3(0f, transform.position.y - 1, 0f);
                 }
             }*/
            #endregion
            randomMovement = Random.Range(0, 4);
            print(randomMovement);
            if (target != null)
            {
                if (randomMovement == 1 || randomMovement == 3)
                {
                    if (Mathf.Abs(target.position.y - transform.position.y) > 0.1f)
                    {
                        yDir = target.position.y > transform.position.y ? 1 : -1;
                        print(yDir);

                        if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .4f, blockHim))
                        {
                            if (yDir == 1)
                            {
                                //print("arriba");
                                movePoint.position = new Vector2(transform.position.x, transform.position.y + 0.8f);
                            }
                            else if (yDir == -1)
                            {
                                //print("abajo");
                                movePoint.position = new Vector2(transform.position.x, transform.position.y - 0.8f);
                            }
                        }
                    }
                }
                




                else
                {
                    print("igual");

                    movePoint.position = transform.position;
                    timer = 0;
                    
                    if (attackRate >= 1.7f)
                    {
                        attackRate = 0;
                        Shoot();
                    }
                }
            }
        }
    }




















    void Shoot()
    {
        
        
            
            GameObject newbullet = (GameObject)Instantiate(Blast, castPoint.transform.position, Quaternion.Euler(0, 0, 0));
            Destroy(newbullet, 2.0f);
            anim.SetTrigger("Attack");

        
    }
    void Awake()
    {
        mTransform = transform;
        mTextOverTransform = mTextOverHead.transform;
    }
    void LateUpdate()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(mTransform.position);
        screenPos.y += textHeight;
        mTextOverTransform.position = screenPos;
    }
    public void CheckLifesThugh8()
    {
        currentThuanklifes = currentThuanklifes - 8;
        StartCoroutine(DamageEffectSequence(boi, Color.red, 2, 0.01f));
    }

    public void CheckLifesThugh80()
    {
        currentThuanklifes = currentThuanklifes - 80;
        StartCoroutine(DamageEffectSequence(boi, Color.red, 2, 0.01f));
    }
    IEnumerator DamageEffectSequence(SpriteRenderer sr, Color dmgColor, float duration, float delay)
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
