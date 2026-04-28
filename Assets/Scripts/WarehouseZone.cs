using UnityEngine;

public class WarehouseZone : MonoBehaviour
{
    [SerializeField] private ProductData product;
    [SerializeField] private int maxStock = 5;
    [SerializeField] private float restockTime = 20f;
    [SerializeField] private AudioClip pickupSound;

    private AudioSource audioSource;
    private int stock;
    private float restockTimer = 0f;
    private bool restocking = false;

    public ProductData Product => product;
    public int Stock => stock;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        stock = maxStock;
    }

    private void Update()
    {
        if (restocking)
        {
            restockTimer -= Time.deltaTime;
            if (restockTimer <= 0f)
            {
                stock = maxStock;
                restocking = false;
                Debug.Log($"[Almacen] Restock completado: {product.productName} | Stock: {stock}");
            }
        }
    }

    public bool HasStock() => stock > 0;

    public ProductData TakeProduct()
    {
        if (stock <= 0) return null;
        stock--;
        if (pickupSound != null && audioSource != null)
            audioSource.PlayOneShot(pickupSound);
        Debug.Log($"[Almacen] Recogido: {product.productName} | Stock restante: {stock}");
        if (stock == 0)
        {
            restocking = true;
            restockTimer = restockTime;
            Debug.Log($"[Almacen] Sin stock. Reponiendo en {restockTime}s...");
        }
        return product;
    }
}
