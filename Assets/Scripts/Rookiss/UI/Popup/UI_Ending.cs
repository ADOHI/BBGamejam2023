using System.Collections.Generic;
using TMPro;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BBGamejam.Ending
{
    public class UI_Ending : MonoBehaviour
    {
/*        enum Buttons
        {
            Button_Next
        }*/

        public List<GameObject> contents = new List<GameObject>();

        public Button button_Next;

        private int index = 0;

        public TextMeshProUGUI scoreText;
        public IntReference score;
        public AudioClip bgm;
        public AudioClip buttonSfx;
        private void Start()
        {
            button_Next.onClick.AddListener(OnNext);
            SoundManager.StopAll();
            SoundManager.PlayMusic(bgm);
        }

        private void OnNext()
        {
            SoundManager.PlayFx(buttonSfx);

            index++;
            if (index < 6)
            {
                contents[index-1].SetActive(false);
                contents[index].SetActive(true);
            }
            else
            {
                contents[contents.Count - 1].SetActive(false);
                button_Next.GetComponentInChildren<TextMeshProUGUI>().text = "TO TITLE";

                //button_Next.gameObject.SetActive(false);
                scoreText.text = $"SCORE: {score.Value}";
                scoreText.gameObject.SetActive(true);
                //Managers.UI.ShowPopupUI<UI_GameClear>();

                button_Next.onClick.AddListener(Retry);
            }
        }

        private void Retry()
        {
            SceneManager.LoadScene(1);
        }
    }
}