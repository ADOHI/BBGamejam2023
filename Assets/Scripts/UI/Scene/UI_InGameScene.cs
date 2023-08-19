using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace RabbitResurrection
{
    public class UI_InGameScene : UI_Scene
    {
        enum Texts
        {
            Text_ZaraHealth,
        }

        [SerializeField] private int zaraHealth;
        private TextMeshProUGUI textZaraHealth;
        [SerializeField] private GameObject pannel_RabbitHealths;
        [SerializeField] private GameObject pannel_RabbitAirs;
        [SerializeField] private GameObject pannel_ZaraHealths;

        public Stack<UI_RabbitHealth> RabbitHealths = new Stack<UI_RabbitHealth>();
        public Stack<UI_RabbitAir> RabbitAirs = new Stack<UI_RabbitAir>();
        public Stack<UI_ZaraHealth> ZaraHealths = new Stack<UI_ZaraHealth>();

        public override void Init()
        {
            base.Init();

            Bind<TextMeshProUGUI>(typeof(Texts));

            textZaraHealth = Get<TextMeshProUGUI>((int)Texts.Text_ZaraHealth);
            textZaraHealth.text = zaraHealth.ToString();
        }

        public void AddRabbitHealth()
        {
            RabbitHealths.Push(Managers.UI.MakeSubItem<UI_RabbitHealth>(pannel_RabbitHealths.transform));
        }

        public void AddRabbitAir()
        {
            RabbitAirs.Push(Managers.UI.MakeSubItem<UI_RabbitAir>(pannel_RabbitAirs.transform));
        }

        public void SetZaraHealth(int amount)
        {
            zaraHealth = amount;
        }
        public void AddZaraHealth()
        {
            ZaraHealths.Push(Managers.UI.MakeSubItem<UI_ZaraHealth>(pannel_ZaraHealths.transform));
        }

        public void DamageRabbitHealth()
        {
            Managers.Resource.Destroy(RabbitHealths.Pop().gameObject);
        }

        public void DamageRabbitAir()
        {
            Managers.Resource.Destroy(RabbitAirs.Pop().gameObject);
        }

        public void DamageZaraHealth()
        {
            //Managers.Resource.Destroy(ZaraHealths.Pop().gameObject);
            
            if(zaraHealth > 0)
            {
                zaraHealth--;
                textZaraHealth.text = zaraHealth.ToString();
            }
        }
    }
}