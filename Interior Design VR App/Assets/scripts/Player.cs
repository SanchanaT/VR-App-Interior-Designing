using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputMode
{
    NONE,
    TELEPORT,
    WALK,
    FURNITURE,
    TRANSLATE,
    ROTATE,
    SCALE,
    DRAG
}

public class Player : MonoBehaviour
{
    public static Player instance;
    public InputMode activemode = InputMode.NONE;
    public Object activefurnitureprefab;

    [SerializeField]
    private float speed = 3.0f;

    public GameObject floor;
    public GameObject roof;
    public GameObject leftwall;
    public GameObject rightwall;
    public GameObject frontwall;
    public GameObject backwall;

    private void Awake()
    {
        if (instance != null)
        {
            GameObject.Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        TryWalk();
    }
    public void TryWalk()
    {
        if (Input.GetMouseButtonDown(0) && activemode == InputMode.WALK)
        {
            Vector3 forward = Camera.main.transform.forward;
            Vector3 newposition = transform.position + forward * Time.deltaTime * speed;

            if (newposition.x < rightwall.transform.position.x && newposition.x > leftwall.transform.position.x
                && newposition.y < roof.transform.position.y && newposition.y > floor.transform.position.y
                && newposition.z < frontwall.transform.position.z && newposition.z > backwall.transform.position.z)
                transform.position = newposition;
        }
    }
}
