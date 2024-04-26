using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour
{
    [SerializeField]
    private Button btnShare;
    void Start(){
        //GameObject.Find
        GameObject btnRate = GameObject.Find("btnRate");
        Button button = btnRate.GetComponent<Button>();
        button.onClick.AddListener(() => Rate());
        Debug.Log(btnRate.name);
        //a traves de editor
        btnShare.onClick.AddListener(() => Share());
    }
    void Rate(){
        Debug.Log("Click en Rate");
    }
    void Share(){
        Debug.Log("Click en Share");
    }
    //a traves de click event en button
    public void PlayGame(){
        //Debug.Log("Click en playGame");
        SceneManager.LoadScene("GameScreen");
    }
}
