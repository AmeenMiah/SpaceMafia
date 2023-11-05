using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Level2Button;
    public GameObject Level3Button;
    public GameObject Level4Button;
    public GameObject ChaosHordeButton;
    public AudioManager Audio;
    public GameManager GM;

    private void Start()
    {
        Audio = FindObjectOfType<AudioManager>();
        if (GM.Level1Completed == true)
        {
            Level2Button.SetActive(true);
        }
        else
        {
            Level2Button.SetActive(false);
        }

        if (GM.Level2Completed == true)
        {
            Level3Button.SetActive(true);
        }
        else
        {
            Level3Button.SetActive(false);
        }

        if (GM.Level3Completed == true)
        {
            Level4Button.SetActive(true);
        }
        else
        {
            Level4Button.SetActive(false);
        }

        if (GM.Level4Completed == true)
        {
            ChaosHordeButton.SetActive(true);
        }
        else
        {
            ChaosHordeButton.SetActive(false);
        }
    }
    public void StartGame()
    {
        Audio.Play("Success");
        SceneManager.LoadScene("Cutscene#1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Level1()
    {
        Audio.Play("Success");
        SceneManager.LoadScene(1);

    }

    public void Level2()
    {
        Audio.Play("Success");
        SceneManager.LoadScene(3);

    }

    public void Level3()
    {
        Audio.Play("Success");
        SceneManager.LoadScene(5);
    }

    public void Level4()
    {
        Audio.Play("Success");
        SceneManager.LoadScene(7);
    }

    public void CH()
    {
        Audio.Play("Success");
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(9);     
    }
}
