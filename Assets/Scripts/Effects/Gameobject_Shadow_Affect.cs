using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameobject_Shadow_Affect : MonoBehaviour
{
    private SpriteRenderer FocusObjectRenderer;
    public string TagOfObjectForShadow;
    private GameObject FocusObject;

    public float SpawnsBySecond;
    public float DisapearAfter;

    private float StartWait;
    private float DestroyTime;

    public int NumberOfShadows;
    public GameObject Shadow;

    private List<GameObject> shadows = new List<GameObject>();

    private void Start()
    {
        StartWait = Time.time + SpawnsBySecond;
        DestroyTime = Time.time + DisapearAfter;
        FocusObject = GameObject.FindGameObjectWithTag(TagOfObjectForShadow);
        FocusObjectRenderer = FocusObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (TimeIsReached())
        {
            CreateShadow();
        }
        DestroyAfterTime();
    }

    private void CreateShadow()
    {
        GameObject ShadowClone = Instantiate(Shadow);

        SpriteRenderer renderer = ShadowClone.GetComponent<SpriteRenderer>();

        renderer.sprite = FocusObjectRenderer.sprite;
        renderer.color = new Color(0f, 255f, 206f, .1f);

        ShadowClone.transform.position = FocusObject.transform.position;

        shadows.Add(ShadowClone);
    }

    private bool TimeIsReached()
    {
        if (Time.time > StartWait)
        {
            StartWait = Time.time + SpawnsBySecond;
            return true;
        }
        else
        {
            return false;
        }
    }

    private void DestroyAfterTime()
    {
        if (Time.time > DestroyTime)
        {
            DestroyTime = Time.time + SpawnsBySecond;

            int index = 0;

            while(NumberOfShadows <= shadows.Count)
            {
                Destroy(shadows[index]);
                shadows.RemoveAt(index);
            }
        }
    }

    private void OnDestroy()
    {
        foreach(GameObject shadow in shadows)
        {
            Destroy(shadow);
        }

        shadows.Clear();
    }
}
