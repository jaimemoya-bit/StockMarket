using UnityEngine;

public class WarehouseZone : MonoBehaviour
{
    [SerializeField] private ProductData product;
    [SerializeField] private int stock = 10;
    [SerializeField] private AudioClip pickupSound;

    private AudioSource audioSource;

    public ProductData Product => product;
    public int Stock => stock;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public bool HasStock() => stock > 0;

    public ProductData TakeProduct()
    {
        if (stock <= 0) return null;
        stock--;
        if (pickupSound != null && audioSource != null)
            audioSource.PlayOneShot(pickupSound);
        Debug.Log($"[Almacen] Recogido: {product.productName} | Stock restante: {stock}");
        return product;
    }
}
