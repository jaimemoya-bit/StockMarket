using UnityEngine;

[CreateAssetMenu(fileName = "NewProduct", menuName = "StockMarket/Product")]
public class ProductData : ScriptableObject
{
    public string productName;
    public GameObject visualPrefab;
}
