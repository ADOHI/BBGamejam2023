using UnityEngine;
using UnityEngine.SceneManagement;

namespace RabbitResurrection
{
    public class SceneManagerEx
    {
        public BaseScene CurrentScene
        {
            get { return Object.FindObjectOfType<BaseScene>(); }
        }

        public void LoadScene(Define.Scene type)
        {
            Managers.Clear();
            SceneManager.LoadScene(GetSceneName(type));
        }

        private string GetSceneName(Define.Scene type)
        {
            string name = System.Enum.GetName(typeof(Define.Scene), type);

            return name;
        }

        public void Clear()
        {
            CurrentScene.Clear();
        }
    }
}