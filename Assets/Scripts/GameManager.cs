using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Order generation
    [Header("Order generation")]
    public List<Texture2D> masks;
    public List<Color> colors;
    public int layerCountMin;
    public int layerCountMax;

    // Events
    [Header("Game events")]
    public UnityEvent onError;
    public UnityEvent onSold;
    public UnityEvent onEnd;

    // Textures script
    [Header("Textures scripts")]
    public CMYKMerger candidateScript;
    public CMYKMerger orderScript;

    [Header("Timer")]
    [SerializeField]protected float timer;
    public int Timer { get => Mathf.CeilToInt(timer); }

    // Orders
    protected List<Order> orders;
    protected int currentOrder;

    // Instance management
    private static GameManager instance;
    public static GameManager Instance
    {
        get => instance;
        set
        {
            if (instance)
                GameObject.Destroy(instance.gameObject);

            instance = value;
        }
    }

    // Score
    private int ordersSold;
    public int OrdersSold { get => ordersSold; }

    // Utility property
    public int OrdersCount { get => orders.Count; }
    public int CurrentOrderLayerCount { get => orders[currentOrder].datum.Count; }
    public int CurrentCandidateLayerCount { get => candidateScript.Datum.Count; }

    // Others
    private System.Random RNG;

    public void Start()
    {
        Debug.Assert(layerCountMin > 0 && layerCountMax < 9 && layerCountMin < layerCountMax);

        Instance = this;

        RNG = new System.Random(DateTime.Now.Millisecond);

        orders = new List<Order>();
        ordersSold = 0;

        Next();
    }

    public void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                End();
            }
        }
    }

    public void Sell()
    {
        if (timer < 0) return;

        // add score
        if (orders[currentOrder].IsCandidateGood(candidateScript.Datum))
        {
            onSold.Invoke();
            ordersSold += 1;
        }
        else
            onError.Invoke();
        
        // next one
        Next();
    }

    private void Next()
    {
        NewOrder();
        orderScript.Datum = orders[currentOrder].datum;
    }

    public void End()
    {
        onEnd.Invoke();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void NewOrder()
    {
        orders.Add(RandomOrder(RNG, layerCountMin, layerCountMax, masks, colors));
        currentOrder = orders.Count-1;
    }

    private Order RandomOrder(System.Random random, int layerMin, int layerMax, List<Texture2D> masks, List<Color> colors)
    {
        var order = new Order();
        order.datum = new List<UpholsteringData>();

        var count = random.Next(layerMin, layerMax+1);
        for (int i = 0; i < count; i++)
        {
            var data = RandomData(random, masks, colors);
            
            while(order.Contains(data))
                data = RandomData(random, masks, colors);

            order.datum.Add(data);
        }
        
        return order;
    }

    private UpholsteringData RandomData(System.Random random, List<Texture2D> masks, List<Color> colors)
    {
        return new UpholsteringData(masks[random.Next(masks.Count)], colors[random.Next(colors.Count)]);
    }
}
