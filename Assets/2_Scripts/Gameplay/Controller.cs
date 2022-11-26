using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    private void Start()
    {
    }

    private void Update()
    {
    }

    public void Reload()
    {
        SceneManager.LoadScene(0);
    }
}