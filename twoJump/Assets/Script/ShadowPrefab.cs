
using UnityEngine;

public class ShadowPrefab : MonoBehaviour
{
    private Transform player;

    private SpriteRenderer plyerSprite;
    private SpriteRenderer thisSprite;

    [Header("����")]
    private Color thisColor;

    [Header("�¼�")]
    private float startTime;
    public float time;

    [Header("�����͸����")]
    private float lucency;
    public float startLucencyValue;
    public  float lucencyRide;

   

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        plyerSprite = player.GetComponent<SpriteRenderer>();
        thisSprite = GetComponent<SpriteRenderer>();
        thisSprite.sprite = plyerSprite.sprite;


        lucency=startLucencyValue;

        transform.position = player.transform.position;
        transform.rotation = player.rotation;
        transform.localScale = player.localScale;

        startTime = Time.time;

    }

    private void Update()
    {
        lucency *= lucencyRide;

        thisColor = new Color(0.5f, 0.5f, 1, lucency);

        thisSprite.color = thisColor;

        if (Time.time >= startTime + time)
        {
            //���ض����
            ShadowPool.Instance.EnqueueS(this.gameObject);
        }
    }
}
