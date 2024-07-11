using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlatform : MonoBehaviour
{
    [SerializeField] float jumpForce;
    [SerializeField] ParticleSystem jumpParticle;
    public float JumpForce { get { return jumpForce; } }

    public void PlayParticle()
    {
        jumpParticle.Play();
    }
}
