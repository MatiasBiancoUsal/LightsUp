using UnityEngine;

public class DoorKey : MonoBehaviour
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
            _isOpening = false; 
        }
    }


    public void SetIsOpening(bool value)
    {
        _isOpening = value;
    }
}
