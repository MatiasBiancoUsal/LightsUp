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
        
        if (_isOpening)
        {
            _animator.SetTrigger("Open");
        }
        else
        {
            
            _animator.SetTrigger("Idle");
        }
    }

    public void SetIsOpening(bool value)
    {
        _isOpening = value;
    }
}