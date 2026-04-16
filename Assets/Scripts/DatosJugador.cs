using UnityEngine;

// Esto crea la opción en el menú de clic derecho "Create"
[CreateAssetMenu(fileName = "NuevoDatosProducto", menuName = "Producto/Reposicion")]
public class DatosJugador : ScriptableObject
{   
    [Header("Datos del Producto")]

    public int id_producto; 
  
    public string nombreProducto;
    public string Descripcion;

    public GameObject itemprefab;
    public Sprite icono;


    [Header("Caracteristicas del Producto")]
    public int coste;
    public float tiempoReposicion;
}
