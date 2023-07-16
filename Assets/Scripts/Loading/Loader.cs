using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loader : MonoBehaviour
{
    private static string nextScene;

    [SerializeField] private Slider slider;

    private float loadSpeed = 0.4f;


    private void Start()
    {
        StartCoroutine(LoadSceneProcess());
    }

    private IEnumerator LoadSceneProcess()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        while (!op.isDone)
        {
            yield return null;

            if (slider.value < 1f)
            {
                slider.value = Mathf.Lerp(slider.value, 1f, Time.deltaTime * loadSpeed);
            }
            else if (slider.value >= 1f)
            {
                op.allowSceneActivation = true;
            }
        }
    }

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("Loading");
    }
}
