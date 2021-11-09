using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    private static List <IRestartable> restartScripts = new List<IRestartable> ();

    [SerializeField]
    private GameObject restartButton = null;
    
    private bool isMenuActive = false;

    public static List<IRestartable> RestartScripts { get => restartScripts; set => restartScripts = value; }

    public void Restart ()
    {
        for (int i = 0; i < restartScripts.Count; i++)
        {
            restartScripts[i].Restart ();
        }
        if (isMenuActive)
        {
            ChangeMenu ();                             
        }
    }
    private void ChangeMenu ()
    {
        isMenuActive = !isMenuActive;
        restartButton.SetActive (isMenuActive);

        Time.timeScale = isMenuActive ? 0 : 1;
        
        Cursor.lockState = isMenuActive ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isMenuActive;
    }

    void Update()
    {
        if (Input.GetKeyDown (KeyCode.Escape))
        {
            ChangeMenu ();
        }
    }
}

public interface IRestartable
{
    void Restart ();
}