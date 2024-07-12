using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxMap : MonoBehaviour
{
    Transform mainCamTf;
    SpriteRenderer[] mapSprites;
    Material[] spriteMats;
    Vector2[] offSets;
    float[] speed;

    int sprCnt = 0;
    float farthestDistance = 0f;

    private void Awake()
    {
        SetBackgroun();
    }

    public void SetBackgroun()
    {
        mainCamTf = Camera.main.transform;
        mapSprites = GetComponentsInChildren<SpriteRenderer>();
        sprCnt = mapSprites.Length;

        spriteMats = new Material[sprCnt];
        offSets = new Vector2[sprCnt];
        speed = new float[sprCnt];

        for (int idx = 0; idx < sprCnt; idx++)
        {
            spriteMats[idx] = mapSprites[idx].material;
            offSets[idx] = spriteMats[idx].mainTextureOffset;
            float calDistance = Mathf.Abs(mainCamTf.position.z - mapSprites[idx].transform.position.z);
            farthestDistance = (calDistance > farthestDistance) ? calDistance : farthestDistance;
        }

        for(int idx=0; idx<sprCnt; idx++)
        {
            speed[idx] = 1 - Mathf.Abs(mainCamTf.transform.position.z - mapSprites[idx].transform.position.z)/farthestDistance;
        }
    }

    private void LateUpdate()
    {
        if (mainCamTf == null)
            return;
        transform.position = new Vector3(mainCamTf.position.x, mainCamTf.position.y, 0);
        for(int idx=0; idx<sprCnt; idx++)
        {
            offSets[idx] += Vector2.right * speed[idx] * Time.deltaTime;
            offSets[idx].x = Mathf.Repeat(offSets[idx].x, 1.0f);
            spriteMats[idx].mainTextureOffset = offSets[idx];
        }
    }
}
