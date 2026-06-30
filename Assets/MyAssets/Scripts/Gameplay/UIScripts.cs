using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIScripts : MonoBehaviour
{
    [SerializeField] private TMP_Text diamondText;
    [SerializeField] private TMP_Text lvlText;
    [SerializeField] private TMP_Text enemyCountText;
    [SerializeField] private Image ExpFillUI;


    private void Start()
    {
        diamondText = GameObject.Find("DiamondCountUI").GetComponent<TMP_Text>();
        lvlText = GameObject.Find("LevelCountUI").GetComponent<TMP_Text>();
        enemyCountText = GameObject.Find("EnemyCountUI").GetComponent<TMP_Text>();
        ExpFillUI = GameObject.Find("ExpFillUI").GetComponent<Image>();
    }
    void OnEnable()
    {
        UIEvents.OnDiamondCountChanged += UpdateDiamond;
        UIEvents.OnEnemyCountChanged += UpdateEnemyCount;
    }

    void OnDisable()
    {
        UIEvents.OnDiamondCountChanged -= UpdateDiamond;
        UIEvents.OnEnemyCountChanged -= UpdateEnemyCount;
    }

    private void UpdateDiamond(int count)
    {
        diamondText.text = count.ToString();
        ExpFillUI.fillAmount += 0.05f;
        StartCoroutine(UIChangeAnimation(diamondText));
    }

    private void UpdateEnemyCount(int count)
    {
        enemyCountText.text = count.ToString();
        StartCoroutine(UIChangeAnimation(enemyCountText));
    }

    private IEnumerator UIChangeAnimation(TMP_Text target)
    {
        for (int i = 0; i < 10; i++)
        {
            target.fontSize += 5;
            yield return new WaitForSeconds(0.01f);
        }

        for (int i = 0; i < 10; i++)
        {
            target.fontSize -= 5;
            yield return new WaitForSeconds(0.01f);
        }
    }

}
