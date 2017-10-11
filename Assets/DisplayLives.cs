using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DisplayLives : MonoBehaviour {

    public GameObject player1;
    public GameObject player2;

    private new AudioSource audio;
    public AudioClip gameEnd;
    public AudioClip gameBegin;
    private int gameEndTimer = 0;

    private Text text;
    
	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        text.color = new Color(1, 1, 1);

        player1 = GameObject.FindGameObjectsWithTag("Player")[0];
        player2 = GameObject.FindGameObjectsWithTag("Player")[1];

        audio = GetComponent<AudioSource>();
        audio.PlayOneShot(gameBegin);
    }
	
	// Update is called once per frame
	void Update () {
        if(IsDead(player1) || IsDead(player2))
        {
            if(gameEndTimer == 0)
                audio.PlayOneShot(gameEnd);

            gameEndTimer += 1;

            if(gameEndTimer > 180)
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
        
        text.text = "Brogans: " + GetLivesString(player1) + "\n" + "Presses: " + GetLivesString(player2);
	}

    private bool IsDead(GameObject player)
    {
        return player == null || player.GetComponent<ActionController>().Life < 0;
    }

    private string GetLivesString(GameObject player)
    {
        if(player == null)
            return "DEAD";

        ActionController controller = player.GetComponent<ActionController>();
        if(controller == null)
            return "DEAD";
        
        if(controller.Life >= 0)
            return controller.Life.ToString();

        return "DEAD";
    }
}
