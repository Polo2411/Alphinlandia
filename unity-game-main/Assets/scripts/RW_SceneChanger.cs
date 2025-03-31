using UnityEngine;
using UnityEngine.SceneManagement;

public class RW_SceneChanger : MonoBehaviour
{
    private bool inCollision = false;
    public Helper helper;
    public Dialog dialog;
    public Bookmenu book;
    public OliviaDoorInteract oliviaDoor;
    public ConstanceDoorInteract constanceDoor;
    public int scene = 1;
    private bool scenechanged = false;
    [SerializeField] private AudioSource sound;
    private void Awake()
    {
        Load();
    }
    void Update()
    {
        // Check if the space bar is pressed and inCollision flag is true
        if (Input.GetKeyDown(KeyCode.Space) && inCollision)
        {
            // Load the specified scene
            if(scene == 1 && scenechanged == false)
            {
                sound.Play();
                if(helper.actualDialog < 1 )
                {
                    helper.actualDialog = 1;
                } 
                if(book.currentMemoryIndex < 1)
                {
                    book.currentMemoryIndex = 1;
                }
                dialog.actualDialog = 1;
                scene = 2;
                Save();
                scenechanged = true;
                SceneManager.LoadScene("lv1");
            }
            if (scene == 2 && scenechanged == false)
            {
                sound.Play();
                if (helper.actualDialog < 3)
                {
                    helper.actualDialog = 3;
                }
                if (book.currentMemoryIndex < 3)
                {
                    book.currentMemoryIndex = 3;
                }
                dialog.actualDialog = 2;
                scene = 3;
                Save();
                scenechanged = true;
                SceneManager.LoadScene("lv2");
            }
            if (scene == 3 && scenechanged == false)
            {
                sound.Play();
                if (helper.actualDialog < 5)
                {
                    helper.actualDialog = 5;
                }
                if (book.currentMemoryIndex < 5)
                {
                    book.currentMemoryIndex = 5;
                }
                dialog.actualDialog = 3;
                scene = 4;
                Save();
                scenechanged = true;
                SceneManager.LoadScene("lv3");
            }
            if (scene == 4 && scenechanged == false)
            {
                sound.Play();
                if (helper.actualDialog < 7)
                {
                    helper.actualDialog = 7;
                }
                if (book.currentMemoryIndex < 7)
                {
                    book.currentMemoryIndex = 7;
                }
                dialog.actualDialog = 7;
                scene = 0;
                Save();
                scenechanged = true;
                SceneManager.LoadScene("lv4");
            }

        }
    }

    // Set inCollision to true when colliding with the object
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inCollision = true;
        }
    }

    // Set inCollision to false when no longer colliding with the object
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inCollision = false;
        }
    }
    // Save function to save the game state
    public void Save()
    {
        // Save helper's actualDialog
        PlayerPrefs.SetInt("HelperActualDialog", helper.actualDialog);

        // Save dialog's actualDialog
        PlayerPrefs.SetInt("DialogActualDialog", dialog.actualDialog);

        // Save book's currentMemoryIndex
        PlayerPrefs.SetInt("BookCurrentMemoryIndex", book.currentMemoryIndex);

        //Save lv
        PlayerPrefs.SetInt("Level", scene);

        //Save doors state
        PlayerPrefs.SetInt("OliviaDoor", oliviaDoor.opened);
        PlayerPrefs.SetInt("ConstanceDoor", constanceDoor.opened);

        // Save PlayerPrefs data immediately
        PlayerPrefs.Save();
    }

    public void Load()
    {
        // Load helper's actualDialog
        int help = PlayerPrefs.GetInt("HelperActualDialog");
        helper.actualDialog = help;

        // Load dialog's actualDialog
        int dial = PlayerPrefs.GetInt("DialogActualDialog");
        dialog.actualDialog = dial;

        // Load book's currentMemoryIndex
        int index = PlayerPrefs.GetInt("BookCurrentMemoryIndex");
        book.currentMemoryIndex = index;

        int lv = PlayerPrefs.GetInt("Level");
        scene = lv;

        int opOl = PlayerPrefs.GetInt("OliviaDoor");
        oliviaDoor.opened = opOl;

        int opCo = PlayerPrefs.GetInt("ConstanceDoor");
        constanceDoor.opened = opCo;
    }

}

