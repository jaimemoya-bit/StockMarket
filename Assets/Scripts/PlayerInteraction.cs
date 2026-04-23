using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Transform holdPoint;
    [SerializeField] private GameObject carriedVisual;
    [SerializeField] private float interactionRadius = 3f;

    private ProductData heldProduct;
    private WarehouseZone nearbyWarehouse;
    private ShelfSlot nearbyShelf;

    private void Update()
    {
        nearbyWarehouse = null;
        nearbyShelf = null;

        Collider[] colliders = Physics.OverlapSphere(transform.position, interactionRadius);
        foreach (var col in colliders)
        {
            if (col.TryGetComponent(out WarehouseZone wz))
                nearbyWarehouse = wz;
            if (col.TryGetComponent(out ShelfSlot ss))
                nearbyShelf = ss;
        }
    }

    public void OnFire(InputValue value)
    {
        if (!value.isPressed) return;
        TryInteract();
    }

    public void TryInteract()
    {
        Debug.Log($"[TryInteract] heldProduct: {heldProduct}, nearbyShelf: {nearbyShelf}, nearbyWarehouse: {nearbyWarehouse}");
        if (heldProduct == null)
        {
            if (nearbyWarehouse != null && nearbyWarehouse.HasStock())
            {
                heldProduct = nearbyWarehouse.TakeProduct();
                if (carriedVisual != null) carriedVisual.SetActive(true);
                Debug.Log("[Interaccion] Producto recogido: " + heldProduct.productName);
            }
        }
        else
        {
            Debug.Log($"[TryInteract] Intentando posar. nearbyShelf: {nearbyShelf}, IsOccupied: {nearbyShelf?.IsOccupied}");
            if (nearbyShelf != null && !nearbyShelf.IsOccupied)
            {
                nearbyShelf.PlaceProduct(heldProduct);
                heldProduct = null;
                if (carriedVisual != null) carriedVisual.SetActive(false);
            }
        }
    }
}
