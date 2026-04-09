using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Transform holdPoint;
    [SerializeField] private GameObject carriedVisual;

    private ProductData heldProduct;
    private WarehouseZone nearbyWarehouse;
    private ShelfSlot nearbyShelf;

    // Llamado por el componente PlayerInput (igual que OnMove en PlayerMovement)
    public void OnFire(InputValue value)
    {
        Debug.Log("[Interaccion] OnFire llamado, isPressed: " + value.isPressed);
        if (!value.isPressed) return;
        TryInteract();
    }

    public void TryInteract()
    {
        if (heldProduct == null)
        {
            if (nearbyWarehouse != null && nearbyWarehouse.HasStock())
            {
                heldProduct = nearbyWarehouse.TakeProduct();
                if (carriedVisual != null) carriedVisual.SetActive(true);
            }
        }
        else
        {
            if (nearbyShelf != null && !nearbyShelf.IsOccupied)
            {
                nearbyShelf.PlaceProduct(heldProduct);
                heldProduct = null;
                if (carriedVisual != null) carriedVisual.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("[Interaccion] Trigger detectado: " + other.gameObject.name);
        if (other.TryGetComponent(out WarehouseZone warehouse))
            nearbyWarehouse = warehouse;
        else if (other.TryGetComponent(out ShelfSlot shelf))
            nearbyShelf = shelf;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out WarehouseZone _))
            nearbyWarehouse = null;
        else if (other.TryGetComponent(out ShelfSlot _))
            nearbyShelf = null;
    }
}
