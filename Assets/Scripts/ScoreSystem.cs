using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour, IRestartable
{
    private Ray ray;
    private RaycastHit hit;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private float score;
    private Camera camera;
    
    private void AddScore (float scoreAdd)
    {
        score += scoreAdd;
        scoreText.SetText (score.ToString ());
    }

    private void Start() 
    {
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
        AddScore (-score);
    }
}
