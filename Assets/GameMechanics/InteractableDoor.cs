using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractableDoor : Interactable
{
    public int toughness = 9;
    public GameObject roomPrefab;

    private Animator _animator;
    private bool _isOpen = false;
    private LevelManager _levelManager;
    private Vector3 roomPos;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        roomPos = new Vector3(transform.position.x, 0, 0);
    }

    public override void Interact(GameObject gameObject)
    {
        if (!_isOpen)
        {
            toughness -= 3;
            _animator.Play("onHit");
            _animator.SetInteger("toughness", toughness);

            if (toughness < 0)
            {
                GetComponent<SpriteRenderer>().enabled = false;
                _isOpen = true;
            }
        }
        else
        {
            _levelManager.LoadRoom(roomPrefab, roomPos);
            gameObject.transform.position = new Vector3(roomPos.x, gameObject.transform.position.y, 0);
        }
    }
}
