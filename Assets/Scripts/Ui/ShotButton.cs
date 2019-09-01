using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShotButton : MonoBehaviour
{
    [SerializeField] private Button button;

    public void AddEventHandler(UnityAction action)
    {
        button.onClick.AddListener(action);
    }
}