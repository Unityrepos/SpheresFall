using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour, IRestartable
{
    private float           speed           = 5;
    private float           score           = 0;
    private float           damage          = 0;
    private Transform       tr              = null;
    private ParticleSystem  particleSystem  = null;

    public float Speed 
    { 
        get => speed; 
        set 
        { 
            speed = value; 
            this.GetComponent<Rigidbody>().velocity = new Vector3 (0, -speed, 0);
        }
    }
    public float Score  { get => score;     set => score    = value; }
    public float Damage { get => damage;    set => damage   = value; }

    public void Init ()
    {
        Menu.RestartScripts.Add (this);
        tr = this.GetComponent <Transform>();

        particleSystem = tr.GetChild (0).GetComponent<ParticleSystem>();
        particleSystem.Pause ();
        particleSystem.startColor = tr.GetComponent<Renderer>().material.color;
    }
    public float Destroy ()
    {
        StartCoroutine (DestroyParticle ());
        return score;
    }
    public float Attack ()
    {
        StartCoroutine (DestroyParticle ());
        return damage;
    }

    public void Restart ()
    {
        Menu.RestartScripts.Remove (this);
        Destroy(this.gameObject);
    }

    private IEnumerator DestroyParticle ()
    {
        particleSystem.Play ();
        yield return new WaitForSeconds (.1f);
        particleSystem.Stop ();
        Menu.RestartScripts.Remove (this);
        Destroy (this.gameObject);
    }
}
