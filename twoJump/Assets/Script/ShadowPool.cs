using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowPool : MonoBehaviour
{
    public static ShadowPool Instance;
    //队列
    private Queue queueS = new Queue();

    public GameObject shadowPrefab;

    public int createpPrefabCount;

    private void Awake()
    {
        Instance = this;
        FillPool();
    }
    
    private void FillPool()
    {
        for (int i = 0; i < createpPrefabCount; i++)
        {
            var player = Instantiate(shadowPrefab);
            player.transform.SetParent(transform);

            //取消启动 返回对象池
            EnqueueS(player);
        }
    }

    public void EnqueueS(GameObject gameObject)
    {
        gameObject.SetActive(false);

        queueS.Enqueue(gameObject);
    }

    public GameObject DequeueS()
    {
        if (queueS.Count == 0)
        {
            FillPool();
        }

        var showShadow = (GameObject)queueS.Dequeue();

        showShadow.SetActive(true);

        return showShadow;
    }
}
