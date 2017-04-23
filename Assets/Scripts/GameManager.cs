﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    // Use this for initialization
    public Transform inventory;
    public GameObject player;
    public static GameManager gm;
    public static Transform pmenu;
    public GameObject ui;
    public GameObject menu;
    public GameObject playerSpawnPoint;


    //Data for saving
    public int playerHealth;
    public int playerMainWeapon;
    public int playerOffWeapon;
    public float playerGold;
    void Awake()
    {
        if(gm == null)
        {
            DontDestroyOnLoad(gameObject);
            gm = this;
            inventory = transform.Find("PlayerManager");
            LoadData();
        }
        else if(gm != this)
        {
            inventory = transform.Find("PlayerManager");
            Destroy(gameObject);
        }
        LoadLevel1_2();


    }

    public void LoadPlayer()
    {
        playerSpawnPoint = GameObject.Find("PlayerSpawnPoint");
        GameObject playerClone = Instantiate(player, playerSpawnPoint.transform.position, playerSpawnPoint.transform
            .rotation);
        playerClone.GetComponent<PlayerController>().SetHealth(20);
        inventory.gameObject.GetComponent<PlayerInventory>().SetPlayer(playerClone);
        ui.GetComponent<HudManager>().SetPlayerStats(playerClone, inventory.gameObject);

    }

    public void ResetSaveData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        PlayerData data = new PlayerData();

        data.playerMainWeapon = 0;
        data.playerOffWeapon = 0;
        data.playerGold = 0;

        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Playerdata reset");

        LoadData();
    }
    public void SaveData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        PlayerData data = new PlayerData();

        data.playerMainWeapon = inventory.gameObject.GetComponent<PlayerInventory>().GetCurrentMainWeapon();
        data.playerOffWeapon = inventory.gameObject.GetComponent<PlayerInventory>().GetCurrentOffWeapon();
        data.playerGold = inventory.gameObject.GetComponent<PlayerInventory>().GetCurrentGold();

        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Playerdata saved " + playerMainWeapon + " " + playerOffWeapon + " " + playerGold);
    }
    public void LoadData()
    {
        if(File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            playerMainWeapon = data.playerMainWeapon;
            playerOffWeapon = data.playerOffWeapon;
            playerGold = data.playerGold;

            inventory.gameObject.GetComponent<PlayerInventory>().SetCurrentMainWeapon(playerMainWeapon);
            inventory.gameObject.GetComponent<PlayerInventory>().SetCurrentOffWeapon(playerOffWeapon);
            inventory.gameObject.GetComponent<PlayerInventory>().SetCurrentGold(playerGold);
            Debug.Log("Playerdata loaded "+ playerMainWeapon + " " + playerOffWeapon+ " " +playerGold);
        }
    }

    public void LoadGameOverScreen()
    {

    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    //Wait on scene change and initialize different scenes
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name != "GameMenu" && scene.name != "GameBoot")
        {
            ui = GameObject.Find("UI");
            LoadPlayer();

        }
        else if (scene.name == "GameMenu")
        {
            menu = GameObject.Find("GameMenuCanvas");
            menu.GetComponent<ButtonManage>().SetGameManager(this);
        }
        else
        {
        }
        Debug.Log("Scene Loaded: " + scene.name + " " + mode);
        inventory = transform.Find("PlayerManager");
        gm = this;
    }

    public void LoadLevel1_2()
    {
        StartCoroutine(ChangeLevel());
    }
    IEnumerator ChangeLevel()
    {
        float fadeTime = GetComponent<AutoFade>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        Application.LoadLevel(Application.loadedLevel + 1);
    }

    public void LoadMenu()
    {
        Application.LoadLevel("GameMenu");
    }

}
//Serialized class for saved player data
[Serializable]
class PlayerData
{
    public int playerMainWeapon;
    public int playerOffWeapon;
    public float playerGold;
}