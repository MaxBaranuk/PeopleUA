using UnityEngine;
using System.Collections;

public class UIPanel : MonoBehaviour
{
    private RectTransform rect;
    [SerializeField] private Vector2 pivotOpen;
    [SerializeField]
    private Vector2 pivotClose;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    void OnEnable()
    {
        rect.pivot = new Vector2(pivotOpen.x, pivotOpen.y);
        StartCoroutine(ScaleOn());
    }

    public void Close()
    {
        rect.pivot = new Vector2(pivotClose.x, pivotClose.y);
        StartCoroutine(ScaleOff());
    }

    IEnumerator ScaleOn()
    {
        while (rect.localScale.x<1)
        {
            rect.localScale = new Vector3(rect.localScale.x+0.1f, rect.localScale.y + 0.1f, 1);
            yield return new WaitForFixedUpdate();
        }
        rect.localScale = Vector2.one;
    }

    IEnumerator ScaleOff()
    {
        while (rect.localScale.x > 0)
        {
            rect.localScale = new Vector3(rect.localScale.x - 0.1f, rect.localScale.y - 0.1f, 1);
            yield return new WaitForFixedUpdate();
        }
        rect.localScale = Vector2.zero;
        gameObject.SetActive(false);
    }
}
