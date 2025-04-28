using UnityEngine;

public class LvlLoader: MonoBehaviour
{
    public void LoadLevel(string levelName)
    {
        // Load the specified level
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelName);
    }

    public void QuitGame()
    {
        // Quit the game
        Application.Quit();
        Debug.Log("Game is quitting...");
    }

}
