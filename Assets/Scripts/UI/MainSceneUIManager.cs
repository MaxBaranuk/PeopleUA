using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;
using System;

public class MainSceneUIManager : MonoBehaviour {

    public GameObject listPanel;
    GameObject newsPanelPrefab;
    private GameObject news;
    ScrollRect scroll;
    Dictionary<int, NewsMessage> messages;

//    private Action<int> action;
    // Use this for initialization
    void Start () {
        newsPanelPrefab = Resources.Load("NewsPanel") as GameObject;
        news = GameObject.Find("Canvas").transform.FindChild("Panel").FindChild("News").gameObject;
        messages = ServerManager.instanse.GetMessagesFromMemory();        
        scroll = listPanel.GetComponentInParent<ScrollRect>();
        ShowCachNews();
        StartCoroutine(LoadNewMessages());
        //        action = (id) => { OpenNews(id);};
    }
	
	// Update is called once per frame
	void Update () {
	if(Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
	}

    public void OpenARScene()
    {
        SceneManager.LoadScene(SceneNames.AR);
    }

    public void OpenNews(int t)
    {
        news.SetActive(true);
    }

    void AddMessageToList(int id)
    {
        StartCoroutine(ServerManager.instanse.DownloadMessage(id, returnValue => {
            if (returnValue != null)
            {
                GameObject panel = (GameObject)Instantiate(newsPanelPrefab, listPanel.transform);
 //               panel.transform.FindChild("Image").GetComponent<Image>().sprite = returnValue.image;
                panel.transform.FindChild("Text").GetComponent<Text>().text = id + " - " + returnValue.title;
                panel.transform.FindChild("Button").GetComponent<Button>().onClick.AddListener(()=> OpenNews(id));
                panel.GetComponent<RectTransform>().localScale = Vector3.one;
            }
        }));
    }

    void ShowCachNews()
    {
        foreach (KeyValuePair<int, NewsMessage> entry in messages)
        {
            GameObject panel = (GameObject)Instantiate(newsPanelPrefab, listPanel.transform);
//            panel.transform.FindChild("Image").GetComponent<Image>().sprite = entry.Value.image;
            panel.transform.FindChild("Text").GetComponent<Text>().text = entry.Value.title;
            panel.transform.FindChild("Button").GetComponent<Button>().onClick.AddListener(() => OpenNews(entry.Value.id));
            panel.GetComponent<RectTransform>().localScale = Vector3.one;
        }
    }

    IEnumerator LoadNewMessages() {

        yield return StartCoroutine(ServerManager.instanse.GetMessageIdsFromServer());
        List<int> IDsToLoad = ServerManager.instanse.GetNewIDs();
        foreach (int id in IDsToLoad) {
            AddMessageToList(id);
        }
    }
}
