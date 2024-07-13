using System.Collections;
using System.Collections.Generic;
using TMPro;
using TreeEditor;
using UnityEngine;

public class Rescuee : Interactable
{
    private Animator _animator;
    bool isRescued = false;
    public TextMeshPro _dialogue;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isRescued)
        {
            _dialogue.rectTransform.position = transform.position + (Vector3.up * 2);
        }
    }

    // Update is called once per frame
    public override void Interact(GameObject gameObject)
    {
        _dialogue.text = "Thank you for rescuing me!";
        _dialogue.enabled = true;

        _animator.Play("onRescued");
        _animator.SetBool("isRescued", isRescued);
    }

    public void OnAnimationEnd()
    {
        this.gameObject.SetActive(false);
    }
}
