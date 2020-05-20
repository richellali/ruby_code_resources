using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// UI manage;
/// </summary>
public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }

    void Awake()
    {
        instance = this;
    }

    public Image healthBar;

    public Text bulletCountText;

    /// <summary>
    /// update health bar
    /// </summary>
    /// <param name="curAmount"></param>
    /// <param name="maxAmount"></param>

    public void UpdateHealthBar(int curAmount, int maxAmount)
    {
        healthBar.fillAmount = (float)curAmount / (float)maxAmount;
    }

    // update bullet count.

    public void UpdateBulletCount(int curAmount, int maxAmount)
    {
        bulletCountText.text = curAmount.ToString() + "/" +
            maxAmount.ToString();
        
    }
}
