using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    public int Health_Max;
    public int Health_Current;
    public bool isHurt;

    //public GameObject GameOver;

    public Image HP_Image;
    public Text HP_Text;
    public Image MP_Image;
    public Text MP_Text;
    CapsuleCollider2D capsulecoll;
    Rigidbody2D rb;

    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        capsulecoll = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        Health_Current = Health_Max;
    }

    // Update is called once per frame
    void Update()
    {
        if(isHurt && Mathf.Abs(rb.velocity.x)<0.1f)
        {
            isHurt = false;
        }

        if(Health_Current<=0)
        {
            isHurt = false;
            FindObjectOfType<BossBehaviour>().enabled = false;
            FindObjectOfType<BossBehaviour>().rb.velocity = new Vector2(0,0);
            //GameOver.SetActive(true);
            Health_Current = 0;
            anim.SetTrigger("Dead");
        }

        HP_Image.fillAmount = (float)Health_Current/(float)Health_Max;
        HP_Text.text = Health_Current.ToString()+"/"+Health_Max.ToString();
    }

    private void OnCollisionEnter2D(Collision2D PlayerColl) 
    {
        if(PlayerColl.gameObject.tag == "Enemy")
        {
            //Health_Current -= GameObject.FindObjectOfType<Enemy_Health_Test>().Damage;
            if(transform.position.x < PlayerColl.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(-2f,rb.velocity.y);
                isHurt = true;
            }
            else if(transform.position.x > PlayerColl.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(2f,rb.velocity.y);
                isHurt = true;
            }
        }
    }

    public void DeadAnim()
    {
        Destroy(this.gameObject);
    }
}
