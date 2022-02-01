using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SetttingMenu : MonoBehaviour
{

    public Button buttonSinglePlayer, buttonMultiPlayer;


    public static bool instanceIsMultiPlayer = true;


    // Start is called before the first frame update
    void Start()
    {
        buttonSinglePlayer.onClick.AddListener(onClickSinglePlaye);
        buttonMultiPlayer.onClick.AddListener(onClickMultiPlayer);

    }

    void onClickSinglePlaye()
    {
        instanceIsMultiPlayer = false;
        Debug.Log("onClickSinglePlaye");
        SceneManager.LoadScene("integrationShootingNetworkOrders", LoadSceneMode.Single);
    }

    void onClickMultiPlayer()
    {
        instanceIsMultiPlayer = true;
        Debug.Log("onClickMultiPlayer");
        SceneManager.LoadScene("integrationShootingNetworkOrders", LoadSceneMode.Single);
    }


}
