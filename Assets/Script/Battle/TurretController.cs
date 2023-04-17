using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretController : MonoBehaviour
{
    public GameObject Blast, Explosion;
    public float Timer;
    public Animator anim;
    public int textHeight;
    
     

    Transform mTransform;
    Transform mTextOverTransform;
    public int totallifes;
    int currentThuanklifes;
    SpriteRenderer boi;

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
        boi = transform.Find("FTurret_0").GetComponent<SpriteRenderer>();

    }
    void HitByRay()
    {
        Destroy(gameObject);
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
        Timer += Time.deltaTime;
        if (LocatePlayer(shotRange))
        {
            //attack
            Shoot();
            //animation
            
        }
       
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
    void Shoot()
    {
        if (Timer >= 3)
        {
            Timer = 0;
            GameObject newbullet = (GameObject)Instantiate(Blast, castPoint.transform.position, Quaternion.Euler(0, 0, 0));
            Destroy(newbullet, 2.0f);
            anim.SetTrigger("Attack");

        }

        
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
    public void CheckLifesTurret8()
    {
        currentThuanklifes = currentThuanklifes - 8;
        StartCoroutine(DamageEffectSequence(boi, Color.red, 2, 0.01f));
    }
    public void CheckLifesTurret80()
    {
        currentThuanklifes = currentThuanklifes - 80;
        StartCoroutine(DamageEffectSequence(boi, Color.red, 2, 0.01f));
        

    }
    IEnumerator DamageEffectSequence(SpriteRenderer sr,Color dmgColor, float duration, float delay)
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
