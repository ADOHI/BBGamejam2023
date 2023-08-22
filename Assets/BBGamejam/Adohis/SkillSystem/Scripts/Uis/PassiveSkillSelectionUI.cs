using BBGamejam.Global.Ingame;
using BBGamejam.Global.Mode;
using BBGamejam.Ingame.Skill;
using LeTai.Asset.TranslucentImage;
using RabbitResurrection;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PassiveSkillSelectionUI : MonoBehaviour
{
    public enum PassiveType
    {
        AttackPower,
        Slow,
        AttackRange,
        Health
    }
    private int level;
    public PassiveType type;
    public TranslucentImage background;
    public TranslucentImage foreground;
    public TextMeshProUGUI levelText;

    private void Awake()
    {
        levelText.text = "Lv.0 > LV.1";
    }
    public void OnclickForUpgrade()
    {
        switch (type)
        {
            case PassiveType.AttackPower:
                OnPowerUp();
                break;
            case PassiveType.Slow:
                OnSlowUp();
                break;
            case PassiveType.AttackRange:
                OnRangeUp();
                break;
            case PassiveType.Health:
                OnHealthUp();
                break;
        }
        SoundManager.PlayFx(SkillManager.Instance.selectionSfx);
        OnPointerExit();
        SkillManager.Instance.OnSkillUpgradeEnd();
        level++;
        levelText.text = $"Lv.{level} > LV.{level + 1}";
    }

    public void OnPointerEnter()
    {
        background.color = Color.yellow;
        foreground.color = Color.cyan;

        SoundManager.PlayFx(SkillManager.Instance.hoverSfx);
    }

    public void OnPointerExit()
    {
        background.color = Color.black;
        foreground.color = Color.gray;
    }


    public void OnPowerUp()
    {
        var swipe = IngameManager.Instance.rabbit.Value.GetComponent<Swipe>();
        swipe.UpgradeSwipeForce();
    }

    public void OnSlowUp()
    {
        SlowModeManager.Instance.UpgradeSlowMode();
    }

    public void OnRangeUp()
    {
        var rabbit = IngameManager.Instance.rabbit.Value.GetComponent<Rabbit>();
        rabbit.UpgradeAttackRange();
    }

    public void OnHealthUp()
    {
        var zara = IngameManager.Instance.turtle.Value.GetComponent<Zara>();
        zara.UpgradeHealth();
    }
}
