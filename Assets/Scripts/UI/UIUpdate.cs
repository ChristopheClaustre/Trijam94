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
        UpdateLayerCount();
        UpdateOrders();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance)
        {
            internalUpdateTimer(GameManager.Instance.Timer);
        }
    }

    public void UpdateOrders()
    {
        internalUpdateOrders(GameManager.Instance.OrdersSold, GameManager.Instance.OrdersCount);
    }

    void internalUpdateOrders(int orderSold, int ordersCount)
    {
        var scoreString = string.Format("{0}", orderSold);
        ScoreText.text = scoreString;
        OrdersSoldText.text = scoreString;

        OrdersCountText.text = string.Format("{0}", ordersCount);
    }

    public void UpdateLayerCount()
    {
        internalUpdateLayerCount(GameManager.Instance.CurrentCandidateLayerCount, GameManager.Instance.CurrentOrderLayerCount);
    }

    void internalUpdateLayerCount(int lastLayerId, int orderLayerCount)
    {
        LayersText.text = string.Format("{0} / {1}", lastLayerId, orderLayerCount);
    }

    void internalUpdateTimer(int time)
    {
        var minutes = time / 60;
        var secondes = time % 60;
        TimerText.text = string.Format("{0:00}:{1:00}", minutes, secondes);
    }
}
