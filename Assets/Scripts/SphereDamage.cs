using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereDamage : MonoBehaviour, IRestartable
{
    [SerializeField]
    private float maxHp = 100;
    [SerializeField]
    private float hp = 100;
    [SerializeField]
    private Camera camera;
    private Menu menu;

    private void Start() 
    {
        menu = this.GetComponent <Menu> ();
        Menu.RestartScripts.Add (this);
    }

    private void Damage (float damage)
    {
        hp -= damage; 
        camera.backgroundColor = Color.Lerp (new Color (1,.3f,.3f,1), Color.white, hp / maxHp);
        if (hp <= 0)
        {
            menu.Restart ();
        }
    }

    public void Restart ()
    {
        Damage (hp-maxHp);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.GetComponent<BallBehaviour>() != null)
        {
            Damage (other.GetComponent<BallBehaviour>().Attack ());
        }
    }
}
