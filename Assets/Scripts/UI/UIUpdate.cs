using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUpdate : MonoBehaviour
{
    public Text OrdersSoldText;
    public Text LayersText;
    public Text TimerText;

    // EndScreen
    public Text ScoreText;
    public Text OrdersCountText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance)
        {
            UpdateUI(
                GameManager.Instance.OrdersSold,
                GameManager.Instance.OrdersCount,
                GameManager.Instance.CurrentCandidateLayerCount,
                GameManager.Instance.CurrentOrderLayerCount,
                GameManager.Instance.Timer
            );
        }
    }

    void UpdateUI(int orderSold, int ordersCount, int lastLayerId, int orderLayerCount, int time)
    {
        var scoreString = string.Format("{0}", orderSold);
        ScoreText.text = scoreString;
        OrdersSoldText.text = scoreString;

        OrdersCountText.text = string.Format("{0}", ordersCount);

        LayersText.text = string.Format("{0} / {1}", lastLayerId, orderLayerCount);

        var minutes = time / 60;
        var secondes = time % 60;
        TimerText.text = string.Format("{0:00}:{1:00}", minutes, secondes);
    }
}
