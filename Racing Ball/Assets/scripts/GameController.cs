using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Text CrystalCounterText;
    public Text CountDownText;
    public Text EndOfGameText;
    public Text TimerText;
    bool running = false;
    bool endGame = false;

    public AudioClip GameWinSound;
    public AudioClip GameLooseSound;
    public float startTime = 0;
    void Start ()
    {
        UpdateCrystalCounterText();

        EndOfGameText.enabled = false;
        StartCoroutine(CountDownCoroutine());
    }
    void Awake()
    {

        startTime = 0;

    }
    void Update()
    {
        if (running)
        {
            startTime += Time.deltaTime;
            TimerText.text = FormatTime(startTime);
        }
        if (endGame && Input.anyKeyDown)
        {
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }
    IEnumerator CountDownCoroutine()
    {
        SetIfSphereCanMove(false);
        CountDownText.enabled = true;

        for(int i=5; i>0; i--)
        {
            CountDownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        CountDownText.text = "Start!";
        running = true;
        SetIfSphereCanMove(true);
        yield return new WaitForSeconds(1f);

        CountDownText.enabled = false;
    }

    void SetIfSphereCanMove(bool canMove)
    {
        var sphere = FindObjectOfType<Sphere>();
        sphere.CanMove = canMove;

        sphere.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    public void UpdateCrystalCounterText()
    {
        var crystals = FindObjectsOfType<Crystal>();

        var crystalCount = crystals.Length;
        var crystalInactive = crystals.Count(crystal => !crystal.Active);

        var text = crystalInactive + " / " + crystalCount;
        CrystalCounterText.text = text;
    }

    public void CheckIfEndOfGame()
    {
        var endOfGame = FindObjectsOfType<Crystal>().All(crystal => !crystal.Active);

        if (!endOfGame) return;

        EndOfGame(true);
    }

    public void EndOfGame(bool win)
    {
        StartCoroutine(EndOfGameCoroutine(win));
    }

    IEnumerator EndOfGameCoroutine(bool win)
    {
        SetIfSphereCanMove(false);
        running = false;
        EndOfGameText.enabled = true;
        EndOfGameText.text = win ? "WIN" : "LOOSE";

        var audioSource = GetComponent<AudioSource>();
        audioSource.clip = win ? GameWinSound : GameLooseSound;
        audioSource.Play();

        yield return new WaitForSeconds(3f);
        endGame = true;
        Debug.Log("koniec gry");
        // Application.LoadLevel(0);
        
    }

    private string FormatTime(float time)
    {
        float totalTime = time;
        //int hours = (int)(totalTime / 3600);
        int minutes = (int)(totalTime / 60) % 60;
        int seconds = (int)totalTime % 60;
        var milliseconds = time - Mathf.Floor(time);

        milliseconds = Mathf.Floor(milliseconds * 1000.0f);

        string answer = minutes.ToString("00") + ":" + seconds.ToString("00") + "." + milliseconds.ToString("000");
        return answer;
    }
}
