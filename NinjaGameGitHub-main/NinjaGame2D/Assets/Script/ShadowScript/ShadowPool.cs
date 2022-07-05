using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowPool : MonoBehaviour
{
    public static ShadowPool instance;

    public GameObject shadowPrefab;

    public int shadowCount;

    Queue<GameObject> availableObjects = new Queue<GameObject>(); //新的隊列

    private void Awake() 
    {
        instance = this;

        //初始化對象池
        FillPool();
    }

    public void FillPool()
    {
        for(int i = 0;i < shadowCount; i++)
        {
            var newShadow = Instantiate(shadowPrefab); //產生預置體
            newShadow.transform.SetParent(transform); //成為子物件

            //取消啟用,返回對象池
            ReturnPool(newShadow);
        }
    }

    public void ReturnPool(GameObject gameObject)
    {
        gameObject.SetActive(false); //不顯示

        availableObjects.Enqueue(gameObject); //回到隊伍後面等待
    }
    
    public GameObject GetFormPool()
    {
        if(availableObjects.Count == 0) //不夠了
        {
            FillPool(); //再生成
        }
        var outShadow = availableObjects.Dequeue(); //從隊伍前排取出

        outShadow.SetActive(true);

        return outShadow; //傳出生成的物件
    }
}
