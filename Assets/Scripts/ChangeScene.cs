using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Change_Scene : MonoBehaviour
{
    public Animator[] Transitions;
    private Animator transition;
    public Canvas loadingCanvas;
    public AudioClip loadingSound;


    void Start()
    {
        if (Transitions != null && Transitions.Length > 0)
        {
            int index = Random.Range(0, Transitions.Length);
            transition = Transitions[index];
            for (int i = 0; i < Transitions.Length; i++)
            {
                if (i == index)
                    Transitions[i].gameObject.SetActive(true);
                else
                    Transitions[i].gameObject.SetActive(false);
            }
        }
        else
        {
            Debug.LogWarning("Transitions array is empty or null!");
        }
    }
    public void go_to_scene_A()
    {
        LoadNextScene("SceneA");
    }
    public void go_to_scene_B()
    {
        LoadNextScene("SceneB");
    }

    public void LoadNextScene(string sceneName)
    {
        StartCoroutine(LoadMyScene(sceneName));
    }

    IEnumerator LoadMyScene(string sceneName)
    {
        transition.SetTrigger("Start");
        yield return null;

        float timeout = 2f;
        float timer = 0f;
        while (!transition.GetCurrentAnimatorStateInfo(0).IsTag("TransitionPart1"))
        {
            if ((timer += Time.deltaTime) > timeout)
            {
                break;
            }
            yield return null;
        }

        timer = 0f;
        while (transition.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.3f)
        {
            if ((timer += Time.deltaTime) > timeout)
            {
                break;
            }
            yield return null;
        }

        if (loadingSound != null)
        {
            AudioSource.PlayClipAtPoint(loadingSound, Camera.main.transform.position);
        }

        timer = 0f;
        while (transition.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            if ((timer += Time.deltaTime) > timeout)
            {
                break;
            }
            yield return null;
        }

        if (Random.Range(0, 3) == 0)
        {
            if (loadingCanvas != null)
            {
                loadingCanvas.gameObject.SetActive(true);
            }

            yield return new WaitForSeconds(3f);
        }

        if (loadingCanvas != null)
        {
            loadingCanvas.gameObject.SetActive(false);
        }

        SceneManager.LoadScene(sceneName);
    }


}