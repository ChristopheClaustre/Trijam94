using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<Texture2D> availableMasks;
    public List<Color> availableColors;

    public int rangeLayerCountMin;
    public int rangeLayerCountMax;
    public int rangeOrderCountMin;
    public int rangeOrderCountMax;

    protected List<Order> orders;

    private int score;

    public int Score { get => score; }

    public UnityEvent onError;
    public UnityEvent onSold;

    public CMYKMerger candidateScript;
    public CMYKMerger orderScript;

    public GameObject panelWin;
    public Text orderText;
    public Text goodText;

    public void Start()
    {
        Debug.Assert(rangeLayerCountMin > 0 && rangeLayerCountMax < 9 && rangeLayerCountMin < rangeLayerCountMax);
        Debug.Assert(rangeOrderCountMin > 0 && rangeOrderCountMin < rangeOrderCountMax);

        var rnd = new System.Random(DateTime.Now.Millisecond);
        // orders = orders.OrderBy(item => rnd.Next()).ToList();
        var count = rnd.Next(rangeOrderCountMin, rangeOrderCountMax);
        orders = new List<Order>();
        orders.Clear();
        for (int i = 0; i < count; i++)
            orders.Add(RandomOrder(rnd));

        orderText.text = "" + count;
        score = 0;

        Next();
    }

    public void Sell()
    {
        // add score
        if (orders[0].IsCandidateGood(candidateScript.Datum))
        {
            onSold.Invoke();
            score += 1;
            goodText.text = "" + score;
        }
        else
            onError.Invoke();
        
        // next one
        orders.RemoveAt(0);
        if (orders.Count > 0)
            Next();
        else
            End();
    }

    public void Next()
    {
        orderScript.Datum = orders[0].datum;
        candidateScript.Clear();
    }

    public void End()
    {
        panelWin.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public UpholsteringData RandomData(System.Random random)
    {
        return new UpholsteringData(availableMasks[random.Next(availableMasks.Count)], availableColors[random.Next(availableColors.Count)]);
    }

    public Order RandomOrder(System.Random random)
    {
        var order = new Order();
        order.datum = new List<UpholsteringData>();

        var count = random.Next(rangeLayerCountMin, rangeLayerCountMax);
        for (int i = 0; i < count; i++)
        {
            var data = RandomData(random);
            
            while(order.Contains(data))
                data = RandomData(random);

            order.datum.Add(data);
        }
        
        return order;
    }
}
