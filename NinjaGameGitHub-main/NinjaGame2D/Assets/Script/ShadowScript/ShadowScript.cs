using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowScript : MonoBehaviour
{
    Transform Player; //目標對象
    SpriteRenderer thisSprite;
    SpriteRenderer PlayerSprite; //目標對象
    Color color;

    [Header("時間控制參數")]
    public float activeTime; //顯示時間
    public float activeStart; //開始顯示的時間點

    [Header("不透明度控制")]
    float alpha;
    public float alphaSet; //初始值
    public float alphaMultiplier;


    private void OnEnable() 
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        thisSprite = GetComponent<SpriteRenderer>();
        PlayerSprite = Player.GetComponent<SpriteRenderer>();

        alpha = alphaSet;

        thisSprite.sprite = PlayerSprite.sprite;

        transform.position = Player.position;
        transform.localScale = Player.localScale;
        transform.rotation = Player.rotation;

        activeStart = Time.time;
    }
    // Update is called once per frame
    void Update()
    {
        alpha *= alphaMultiplier;

        color = new Color(0,0f,0.8f,alpha); //偏藍色

        thisSprite.color = color;

        if(Time.time >= activeStart + activeTime)
        {
            //返回對象池
            ShadowPool.instance.ReturnPool(this.gameObject);
        }
    }
}
