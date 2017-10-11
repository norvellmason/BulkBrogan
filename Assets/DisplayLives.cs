using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DisplayLives : MonoBehaviour {

    public GameObject[] players;

    private new AudioSource audio;
    public AudioClip gameEnd;
    public AudioClip gameBegin;
    private int gameEndTimer = 0;

    private Text text;
    
	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        text.color = new Color(1, 1, 1);

        players = GameObject.FindGameObjectsWithTag("Player");

        audio = GetComponent<AudioSource>();
        audio.PlayOneShot(gameBegin);
    }
	
	// Update is called once per frame
	void Update () {
        if(Count() <= 1)
        {
            if(gameEndTimer == 0)
                audio.PlayOneShot(gameEnd);

            gameEndTimer += 1;

            if(gameEndTimer > 180)
                SceneManager.LoadScene("MainMenu");

            text.text = "Game Over";
        }
        else
        {
            string healthString = "";
            foreach(GameObject player in players)
            {
                if(!IsDead(player))
                    healthString += GetLivesString(player) + "\n";
            }

            text.text = healthString;
        }
	}

    private int Count()
    {
        int count = 0;

        foreach(GameObject player in players)
            if(!IsDead(player))
                count += 1;

        return count;
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
            return controller.name + ": " + controller.Life.ToString();

        return "DEAD";
    }
}
