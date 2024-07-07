using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.RailsBuildingSystem
{
    public class ValidationScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;

        [SerializeField] private Button yesBtn;
        [SerializeField] private Button noBtn;

        public ReactiveCommand<bool> OnClose = new ReactiveCommand<bool>();

        private void Start()
        {
            yesBtn.onClick.AddListener(() => Close(true));
            noBtn.onClick.AddListener(() => Close(false));
        }

        public void Show()
        {
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }

        private void Close(bool result)
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            OnClose.Execute(result);
        }
    }
}