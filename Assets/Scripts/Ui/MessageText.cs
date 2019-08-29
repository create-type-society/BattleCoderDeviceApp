using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class MessageText : MonoBehaviour
    {
        [SerializeField] private Text messageText;

        public void SetText(string text)
        {
            messageText.text = text;
        }
    }
}