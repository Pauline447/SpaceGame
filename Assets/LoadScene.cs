using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] bool m_ending;
    public void ReloadScene()
    {
        if(!m_ending)
            SceneManager.LoadScene(1);
        else
            SceneManager.LoadScene(0);
    }
}
