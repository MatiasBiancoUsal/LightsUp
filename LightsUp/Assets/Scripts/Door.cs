using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator _animator;
    private bool _isOpening = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Si está en estado de apertura, activa la animación de apertura
        if (_isOpening)
        {
            _animator.SetTrigger("Open");
        }
        else
        {
            // Si no está en estado de apertura, activa la animación de "Door_Idle"
            _animator.SetTrigger("Idle");
        }
    }

    public void SetIsOpening(bool value)
    {
        _isOpening = value;
    }
}