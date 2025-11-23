using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpSpeed = 6f;
    public Rigidbody rb;
    public TextMeshProUGUI puanYazisi;
    private int puan = 0;
    public int hedefPuan = 2;
    public GameObject winText;
    private bool onTheFloor = true;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if(winText != null) 
            winText.SetActive(false);
    }

    void FixedUpdate()
    {
        float yatay = 0;
        float dikey = 0;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) 
                dikey = 1;
            
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) 
                dikey = -1;

            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) 
                yatay = 1;

            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) 
                yatay = -1;

            if (Keyboard.current.spaceKey.wasPressedThisFrame && onTheFloor)
            {
                rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
                
                onTheFloor = false;
                }
        }
        
        Vector3 movement = new Vector3(yatay, 0.0f, dikey);
        rb.AddForce(movement*speed);

        if (transform.position.y < -10)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Çarpışma oldu! Çarpılan nesne: " + other.gameObject.name + " | Etiketi: " + other.gameObject.tag);

        if (other.gameObject.CompareTag("Yem"))
        {
            Debug.Log("Etiket doğru, nesne yok ediliyor.");
            Destroy(other.gameObject);
            puan++;
            puanYazisi.text = "Score: " + puan;

            if (puan >= hedefPuan)
            {
                KazanmaDurumu();
            }
        }
    }
    void KazanmaDurumu()
    {
        winText.SetActive(true);

        Time.timeScale = 0;
    }

    void OnCollisionEnter(Collision collision)
    {
        onTheFloor = true;
    }
}