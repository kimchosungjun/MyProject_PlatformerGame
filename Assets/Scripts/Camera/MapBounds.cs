using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBounds : MonoBehaviour
{
    Transform playerTf;
    Camera mainCam;
    Collider2D coll;
    Bounds curBound;
    [SerializeField] float upValue = 2f;
    private void Awake()
    {
        mainCam = Camera.main;
        coll = GetComponent<Collider2D>();
        playerTf = GameObject.FindWithTag("Player").transform;
        CheckBound();
    }

    private void Update()
    {
        mainCam.transform.position = new Vector3(
            Mathf.Clamp(playerTf.position.x, curBound.min.x, curBound.max.x),
            Mathf.Clamp(playerTf.position.y+ upValue, curBound.min.y, curBound.max.y),
            mainCam.transform.position.z
            );
    }

    void CheckBound()
    {
        float height = mainCam.orthographicSize;
        float width = height * mainCam.aspect;
        curBound = coll.bounds;

        float minX = curBound.min.x + width;
        float minY = curBound.min.y + height;

        float maxX = curBound.max.x - width;
        float maxY = curBound.max.y - height;
        curBound.SetMinMax(new Vector3(minX, minY), new Vector3(maxX, maxY));
    }
}
