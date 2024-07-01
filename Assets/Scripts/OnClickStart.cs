using UnityEngine;
using UnityEngine.SceneManagement;

public class OnClickExit : MonoBehaviour
{
    private string _mainScene = "SampleScene";

    public void StartGame()
    {
        Debug.Log("Start");
        SceneManager.LoadScene(_mainScene);
    }
}
