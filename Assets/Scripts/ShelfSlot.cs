using UnityEngine;

public class ShelfSlot : MonoBehaviour
{
    [SerializeField] private GameObject[] productVisuals;
    [SerializeField] private AudioClip restockSound;

    private AudioSource audioSource;
    private int currentStock = 0;

    public bool IsOccupied => currentStock >= productVisuals.Length;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        UpdateVisuals();
    }

    public bool PlaceProduct(ProductData product)
    {
        if (IsOccupied) return false;
        currentStock++;
        UpdateVisuals();
        if (restockSound != null && audioSource != null)
            audioSource.PlayOneShot(restockSound);
        Debug.Log($"[Estanteria] Colocado: {product.productName} | Stock en estanteria: {currentStock}/{productVisuals.Length}");
        return true;
    }

    private void UpdateVisuals()
    {
        for (int i = 0; i < productVisuals.Length; i++)
        {
            if (productVisuals[i] != null)
                productVisuals[i].SetActive(i < currentStock);
        }
    }
}
