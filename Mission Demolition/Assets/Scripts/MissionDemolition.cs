using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameMode
{
    idle,
    playing,
    levelEnd
}

public class MissionDemolition : MonoBehaviour
{
    // private singleton
    static private MissionDemolition S;

    [Header("Set In Inspector")]
    public Text uitLevel;
    public Text uitShots;
    public Text uitHighScore;
    public Text uitButtons;

    public Vector3 castlePos;       // The place to put castles
    public GameObject[] castles;    // An array of castles

    [Header("Set Dynamically")]
    public int level;
    public int levelMax;
    public int shotsTaken;
    public int highScore;
    public GameObject castle;
    public GameMode mode = GameMode.idle;
    public string showing = "Show Slingshot";

    // Use this for initialization
    void Start()
    {
        S = this;   // Define the singleton

        level = 0;
        levelMax = castles.Length;
        StartLevel();
    }

    void StartLevel()
    {
        // Get rid of the old castle if one exists
        if (castle != null) { Destroy(castle); }

        // Destroy old projectile if they exist
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (GameObject pTemp in gos)
        {
            Destroy(pTemp);
        }

        // Instantiate the new castle
        castle = Instantiate<GameObject>(castles[level]);
        castle.transform.position = castlePos;
        shotsTaken = 0;

        // If the PlayerPrefs HighScore already exists, read it
        if (PlayerPrefs.HasKey("HighScore" + level))
        {
            highScore = PlayerPrefs.GetInt("HighScore" + level);
        }

        else
        {
            // Assign the high score to HighScore
            PlayerPrefs.SetInt("HighScore" + level, value: 100);
        }

        // Reset the camera
        SwitchView("Show Both");
        ProjectileLine.S.Clear();

        // Reset the goal
        Goal.goalMet = false;

        UpdateGUI();

        mode = GameMode.playing;
    }

    void UpdateGUI()
    {
        // Show the data in the GUITexts
        uitLevel.text = "Level: " + (level + 1) + " of " + levelMax;
        uitShots.text = "Shots Taken: " + shotsTaken;
        if (highScore == 100) {
            uitHighScore.text = "High Score: ";
        } else {
            uitHighScore.text = "High Score: " + highScore;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGUI();

        // Check for level end
        if ((mode == GameMode.playing) && (Goal.goalMet))
        {
            // Change mode to stop checking for level end
            mode = GameMode.levelEnd;

            // Zoom out
            SwitchView("Show Both");

            // Start the next level in 2 seconds
            // Update the PlayerPrefs HighScore if necessary
            if (shotsTaken < PlayerPrefs.GetInt("HighScore" + level))
            {
                PlayerPrefs.SetInt("HighScore" + level, shotsTaken);
            }
            Invoke("NextLevel", 2f);
        }
    }

    void NextLevel()
    {
        level++;
        if (level == levelMax) { level = 0; }
        StartLevel();
    }

    public void SwitchView(string eView = "")
    {
        if (eView == "") { eView = uitButtons.text; }
        showing = eView;

        switch (showing)
        {
            case "Show Slingshot":
                FollowCam.POI = null;
                uitButtons.text = "Show Castle";
                break;

            case "Show Castle":
                FollowCam.POI = S.castle;
                uitButtons.text = "Show Both";
                break;

            case "Show Both":
                FollowCam.POI = GameObject.Find("ViewBoth");
                uitButtons.text = "Show Slingshot";
                break;
        }
    }

    // Static method that allows code anywhere to increment shotsTaken
    public static void ShotFired()
    {
        S.shotsTaken++;
    }
}
