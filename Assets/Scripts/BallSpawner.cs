using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour, IRestartable
{
    [SerializeField]
    private GameObject  prefab          = null;
    [SerializeField]
    private Vector3     startPosition   = Vector3.zero;
    [SerializeField]
    private float       radius          = 1;
    [SerializeField]
    private float       minInterval     = 0;
    [SerializeField]
    private float       maxInterval     = 1;
    [SerializeField]
    private float       acceleration    = 1;
    [SerializeField]
    private float       maxSize         = 1;

    private Vector3         position        = Vector3.zero;
    private GameObject      exemplar        = null;
    private BallBehaviour   scriptExemplar  = null;
    private float           speedMultiplier = 1;

    void Start ()
    {
        Menu.RestartScripts.Add (this);
        StartCoroutine (Cycle ());
    }

    void Update ()
    {
        speedMultiplier += acceleration * Time.deltaTime;
    }

    private void Spawn ()
    {
        position = Random.onUnitSphere * radius;
        position = new Vector3 (position.x, Mathf.Abs (position.y), position.z);
        position += startPosition;
        exemplar = GameObject.Instantiate (prefab, position, Quaternion.identity);
        exemplar.transform.GetComponent<Renderer>().material.color = Random.ColorHSV ();

        float randScale                 = Random.Range (1, maxSize);
        exemplar.transform.localScale   = new Vector3 (randScale, randScale, randScale);
        scriptExemplar                  = exemplar.GetComponent<BallBehaviour> ();
        scriptExemplar.Speed            = speedMultiplier * Random.value;
        scriptExemplar.Score            = scriptExemplar.Speed / randScale;
        scriptExemplar.Damage           = Mathf.Sqrt(scriptExemplar.Speed) * randScale;
        scriptExemplar.Init ();
    }

    public void Restart ()
    {
        speedMultiplier = 1;
    }

    private IEnumerator Cycle ()
    {
        while (true)
        {
            yield return new WaitForSeconds (Random.Range (minInterval, maxInterval));
            Spawn ();
        }
    }
}
