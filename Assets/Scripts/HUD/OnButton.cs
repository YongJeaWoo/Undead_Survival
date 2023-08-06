using UnityEngine;

public class OnButton : MonoBehaviour
{
    public GameObject buttonObj;

    private bool isVisible = true;

    public void OnButtonClick()
    {
        isVisible = !isVisible;
        buttonObj.SetActive(isVisible);
    }
}
