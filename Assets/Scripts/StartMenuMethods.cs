using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuMethods : MonoBehaviour
{

    [Header("Scene To switch within")]
    [SerializeField] public string SceneName;

    [Header("Fade in and out Objects")]
    public Image fadeImage;
    public Animator anim;

    IEnumerator Fading()
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => fadeImage.color.a == 1);
        SceneManager.LoadScene(SceneName);
    }


    public void Startbutton()
    {

        
        StartCoroutine(Fading());
          
             
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void PlayFadeAnimation()
    {
        StartCoroutine(Fading());
    }



}
