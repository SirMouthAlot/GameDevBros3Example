using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonDefinition : MonoBehaviour
{
    public bool _selected = false;
    public AudioClip _swapToSFX;
    public AudioClip _confirmSFX;
    public float _confirmTime;
    private Button _button;
    private Animator _animator;


    private void Start()
    {
        _button = GetComponent<Button>();

        TryGetComponent<Animator>(out _animator);
    }

    public void SwappedTo()
    {
        //Selected button
        _selected = true;

        //If there's SFX for swapping buttons, play it
        if (_swapToSFX != null)
        {
            AudioSource.PlayClipAtPoint(_swapToSFX, Vector3.zero);
        }

        //If there's an animator update the selected bool in the animator
        if (_animator != null)
        {
            _animator.SetBool("Selected", _selected);
        }
    }

    public void SwappedOff()
    {
        //Unselected button
        _selected = false;

        //If there's an animator, update the selected bool in the animator
        if (_animator != null)
        {
            _animator.SetBool("Selected", _selected);
        }
    }

    public IEnumerator ClickButton()
    {
        //If there's SFX for confirming button, play it
        if (_confirmSFX != null)
        {
            AudioSource.PlayClipAtPoint(_confirmSFX, Vector3.zero);
        }

        yield return new WaitForSeconds(_confirmTime);

        //Invoke button click
        _button.onClick.Invoke();
    }
}
