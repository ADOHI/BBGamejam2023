using System.Collections.Generic;
using UnityEngine;

namespace RabbitResurrection
{
    public interface ILoader<Key, Value>
    {
        Dictionary<Key, Value> MakeDictionary();
    }

    public class DataManager
    {
        public void Init()
        {
        }

        private Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
        {
            TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
            return JsonUtility.FromJson<Loader>(textAsset.text);
        }

        public void Clear()
        {
        }
    }
}