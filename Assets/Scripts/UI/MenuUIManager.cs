using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] menuButtons;
    private bool isMenuInMoving;
    private GameObject howToPanel;
    private GameObject contactPanel;

    void Start()
    {
        howToPanel = GameObject.Find("Canvas").transform.FindChild("Panel").transform.FindChild("HowToPanel").gameObject;
        contactPanel = GameObject.Find("Canvas").transform.FindChild("Panel").transform.FindChild("ContactPanel").gameObject;
    }

    public void MenuClick()
    {
        if (!isMenuInMoving)
        {
            if (menuButtons[0].activeInHierarchy) StartCoroutine(ClosenMenu());
            else StartCoroutine(OpenMenu());
        }
    }

    public void Home()
    {
        if (SceneManager.GetActiveScene().name == SceneNames.Main) StartCoroutine(ClosenMenu());
        else SceneManager.LoadScene(SceneNames.Main);
    }

    public void OpenContact()
    {
        contactPanel.SetActive(true);
    }

    public void OpenHowTo()
    {
        howToPanel.SetActive(true);
    }

    IEnumerator OpenMenu()
    {
        isMenuInMoving = true;
        for (int i = menuButtons.Length - 1; i >= 0; i--)
        {
            menuButtons[i].SetActive(true);
            yield return new WaitForSeconds(0.05f);
        }
        isMenuInMoving = false;
    }

    IEnumerator ClosenMenu()
    {
        isMenuInMoving = true;
        for (int i = 0; i < menuButtons.Length; i++)
        {
            menuButtons[i].SetActive(false);
            yield return new WaitForSeconds(0.05f);
        }
        isMenuInMoving = false;
    }

}
