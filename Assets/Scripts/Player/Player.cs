using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] bool isGround;
    public bool IsGround { get { return isGround; } set { isGround = value; } }
}
