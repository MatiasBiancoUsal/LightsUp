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
        // Si est� en estado de apertura, activa la animaci�n de apertura
        if (_isOpening)
        {
            _animator.SetTrigger("Open");
        }
        else
        {
            // Si no est� en estado de apertura, activa la animaci�n de "Door_Idle"
            _animator.SetTrigger("Idle");
        }
    }

    public void SetIsOpening(bool value)
    {
        _isOpening = value;
    }
}