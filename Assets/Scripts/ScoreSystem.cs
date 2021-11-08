using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour, IRestartable
{
    [SerializeField]
    private TextMeshProUGUI scoreText       = null;
    [SerializeField]
    private TextMeshProUGUI bestScoreText   = null;
    [SerializeField]
    private float           score           = 0;

    private Ray         ray     = new Ray ();
    private RaycastHit  hit     = new RaycastHit ();
    private Camera      camera  = null;
    
    private void AddScore (float scoreAdd)
    {
        score += scoreAdd;
        scoreText.SetText (score.ToString ());
    }

    private void Start() 
    {
        bestScoreText.SetText (PlayerPrefs.HasKey ("BestScore") ? "The Best Score: " + PlayerPrefs.GetFloat ("BestScore").ToString () : "You hasn`t previous records");
        camera = this.GetComponent <Camera> ();
        Menu.RestartScripts.Add (this);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown (0))
        {
            ray = camera.ScreenPointToRay (new Vector3(camera.pixelWidth / 2, camera.pixelHeight / 2, 0));
            if (Physics.Raycast (ray, out hit))
            {
                if (hit.transform.GetComponent<BallBehaviour>() != null)
                {
                    AddScore (hit.transform.GetComponent<BallBehaviour>().Destroy ());
                }
            }
        }
    }

    public void Restart ()
    {
        if (PlayerPrefs.HasKey ("BestScore"))
        {
            if (PlayerPrefs.GetFloat ("BestScore") < score)
            {
                PlayerPrefs.SetFloat ("BestScore", score);
            }
        }
        else
        {
            PlayerPrefs.SetFloat ("BestScore", score);
        }
        PlayerPrefs.Save ();
        bestScoreText.SetText ("The Best Score: " + PlayerPrefs.GetFloat ("BestScore").ToString ());
        AddScore (-score);
    }
}
