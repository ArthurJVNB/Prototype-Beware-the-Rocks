using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TestManager : MonoBehaviour
{
    public Terrain terrain;
    public TextMeshProUGUI textCountdown;
    public TextMeshProUGUI textControls;
    public TextMeshProUGUI textWhatIsHappening;
    public int countdownToRain = 5;
    public float rainRepeatRate = .1f;

    Coroutine showAndHideText;
    Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();

        textControls.text = 
            "CONTROLS\n" +
            "F = Dig Terrain\n" +
            "0 = Create Terrain\n" +
            "Backspace = Restart level\n" +
            "Alt F4 = Exit game";
        StartTerrain();
        InvokeRepeating("StartMeteors", countdownToRain, rainRepeatRate);
        InvokeRepeating("Countdown", 0, 1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.Keypad0) || Input.GetKeyDown(KeyCode.Alpha0))
        {
            StartTerrain();
        }

        if (player.transform.position.x > 100)
        {
            textCountdown.text = "Acaboooou :D";
            textCountdown.enabled = true;
        }
    }

    private void Countdown()
    {
        if (countdownToRain >= 0)
        {
            textCountdown.text = countdownToRain.ToString();
            countdownToRain--; 
        }
        else
        {
            textCountdown.enabled = false;
        }
    }

    private void StartTerrain()
    {
        GetComponent<TerrainGenerator>().GenerateTerrain();
        if (showAndHideText != null)
        {
            StopCoroutine(showAndHideText);
        }
        StartCoroutine(ShowAndHideText(textWhatIsHappening, "Generating terrain", 2));
    }

    private void StartMeteors()
    {
        GetComponent<RainingMeteors>().Rain();
        if (showAndHideText != null)
        {
            StopCoroutine(showAndHideText);
        }
        showAndHideText = StartCoroutine(ShowAndHideText(textWhatIsHappening, "Raining Rocks", 2));
    }

    private IEnumerator ShowAndHideText(TextMeshProUGUI textUGUI, string text, float timeToHide)
    {
        textUGUI.text = text;
        textUGUI.enabled = true;
        yield return new WaitForSeconds(timeToHide);
        textUGUI.enabled = false;
    }
}
