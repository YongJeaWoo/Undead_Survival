using SingletonComponent.Component;
using System.Collections;
using UnityEngine;

public class PanelManager : SingletonComponent<PanelManager>
{
    #region Singleton

    protected override void AwakeInstance()
    {

    }

    protected override bool InitInstance()
    {
        return true;
    }

    protected override void ReleaseInstance()
    {

    }

    #endregion

    #region Public Methods

    private Coroutine currentCoroutine = null;

    public void TogglePanel<T>() where T : BasePanel
    {
        var panel = PopupManager.Instance.Find<T>();

        if (panel != null)
        {
            if (panel.hasAnimation)
            {
                if (currentCoroutine != null)
                {
                    StopCoroutine(currentCoroutine);
                }

                panel.Hide();
                currentCoroutine = StartCoroutine(MovePanel(panel, panel.hidePosition, () => {
                    PopupManager.Instance.RemovePopUp<T>();
                    currentCoroutine = null;
                }));
            }
            else
            {
                panel.Hide();
                PopupManager.Instance.RemovePopUp<T>();
            }
        }
        else
        {
            panel = PopupManager.Instance.GetPopUp<T>();

            if (panel.hasAnimation)
            {
                panel.GetComponent<RectTransform>().anchoredPosition = panel.hidePosition;

                if (currentCoroutine != null)
                {
                    StopCoroutine(currentCoroutine);
                }

                panel.Show();
                currentCoroutine = StartCoroutine(MovePanel(panel, panel.showPosition, () => {
                    currentCoroutine = null;
                }));
            }
            else
            {
                panel.Show();
            }
        }
    }

    #endregion

    private IEnumerator MovePanel(BasePanel panel, Vector2 targetPosition, System.Action onComplete)
    {
        RectTransform rectTrans = panel.GetComponent<RectTransform>();
        float startTime = Time.time;
        float endTime = startTime + 0.5f; 

        while (Time.time < endTime)
        {
            float t = (Time.time - startTime) / 0.5f;

            rectTrans.anchoredPosition = Vector2.Lerp(rectTrans.anchoredPosition, targetPosition, t);

            yield return null;
        }

        rectTrans.anchoredPosition = targetPosition;

        onComplete?.Invoke();
    }
}
