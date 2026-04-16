using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cambioanim : MonoBehaviour
{
    Cliente cliente;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        cliente = GetComponentInParent<Cliente>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetInteger("satis", cliente.clientSatis);
        animator.SetBool("encaja",cliente.enCaja);
    }
}