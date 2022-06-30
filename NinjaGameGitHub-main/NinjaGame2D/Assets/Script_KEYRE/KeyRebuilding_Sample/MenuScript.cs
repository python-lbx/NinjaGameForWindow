using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public Transform menuPanel;
    Event keyEvent;
    Text buttonText;
    KeyCode newKey;

    bool waitingForKey;

    // Start is called before the first frame update
    void Start()
    {
        //menuPanel.gameObject.SetActive(false);
        waitingForKey = false;

        //各對應元物件底下的子物件
        for(int i = 0; i < menuPanel.childCount; i++)
        {
            if(menuPanel.GetChild(i).name == "LeftKey")
            {
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.left.ToString();
            }
            else if(menuPanel.GetChild(i).name == "RightKey")
            {
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.right.ToString();
            }
            else if(menuPanel.GetChild(i).name == "CrouchKey")
            {
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.crouch.ToString();
            }
            else if(menuPanel.GetChild(i).name == "JumpKey")
            {
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.jump.ToString();
            }
            else if(menuPanel.GetChild(i).name == "AttackKey")
            {
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.attack.ToString();
            }
            else if(menuPanel.GetChild(i).name == "FireballKey")
            {
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.fireball.ToString();
            }
            else if(menuPanel.GetChild(i).name == "DashKey")
            {
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.dash.ToString();
            }
            else if(menuPanel.GetChild(i).name == "ShootKey")
            {
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.shoot.ToString();
            }            
            else if(menuPanel.GetChild(i).name == "EscapeKey")
            {
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.escape.ToString();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //UI控制
    private void OnGUI() 
    {
        keyEvent = Event.current;

        if(keyEvent.isKey && waitingForKey)
        {
            newKey = keyEvent.keyCode;
            waitingForKey = false;
        }
    }

    public void StartAssignment(string keyName)
    {
        if(!waitingForKey)
        {
            StartCoroutine(AssignKey(keyName));
        }
    }


    //顯示按鍵字元
    public void SendText(Text text)
    {
        buttonText = text;
    }

    IEnumerator WaitForKey()
    {
        while(!keyEvent.isKey)
        {
            yield return null;
        }
    }


    //更改按鍵
    public IEnumerator AssignKey(string keyName)
    {
        waitingForKey = true;
        yield return WaitForKey();

        switch(keyName)
        {
            case "left":
            GameManager.GM.left = newKey;
            buttonText.text = GameManager.GM.left.ToString();
            PlayerPrefs.SetString("leftKey",GameManager.GM.left.ToString());
            break;

            case "right":
            GameManager.GM.right = newKey;
            buttonText.text = GameManager.GM.right.ToString();
            PlayerPrefs.SetString("rightKey",GameManager.GM.right.ToString());
            break;

            case "crouch":
            GameManager.GM.crouch = newKey;
            buttonText.text = GameManager.GM.crouch.ToString();
            PlayerPrefs.SetString("crouchKey",GameManager.GM.crouch.ToString());
            break;

            case "jump":
            GameManager.GM.jump = newKey;
            buttonText.text = GameManager.GM.jump.ToString();
            PlayerPrefs.SetString("jumpKey",GameManager.GM.jump.ToString());
            break;

            case "attack":
            GameManager.GM.attack = newKey;
            buttonText.text = GameManager.GM.attack.ToString();
            PlayerPrefs.SetString("attackKey",GameManager.GM.attack.ToString());
            break;

            case "fireball":
            GameManager.GM.fireball = newKey;
            buttonText.text = GameManager.GM.fireball.ToString();
            PlayerPrefs.SetString("fireballKey",GameManager.GM.fireball.ToString());
            break;

            case "dash":
            GameManager.GM.dash = newKey;
            buttonText.text = GameManager.GM.dash.ToString();
            PlayerPrefs.SetString("dashKey",GameManager.GM.dash.ToString());
            break;

            case "shoot":
            GameManager.GM.shoot = newKey;
            buttonText.text = GameManager.GM.shoot.ToString();
            PlayerPrefs.SetString("shootKey",GameManager.GM.shoot.ToString());
            break;

            case "escape":
            GameManager.GM.escape = newKey;
            buttonText.text = GameManager.GM.escape.ToString();
            PlayerPrefs.SetString("escapeKey",GameManager.GM.escape.ToString());
            break;
        }

        yield return null;
    }

    public void ResetKey(string keyName)
    {
        waitingForKey = true;

        switch(keyName)
        {
            case "left":
            GameManager.GM.left = KeyCode.A;
            buttonText.text = GameManager.GM.left.ToString();
            PlayerPrefs.SetString("leftKey",GameManager.GM.left.ToString());
            break;

            case "right":
            GameManager.GM.right = KeyCode.D;
            buttonText.text = GameManager.GM.right.ToString();
            PlayerPrefs.SetString("rightKey",GameManager.GM.right.ToString());
            break;

            case "crouch":
            GameManager.GM.crouch = KeyCode.S;
            buttonText.text = GameManager.GM.crouch.ToString();
            PlayerPrefs.SetString("crouchKey",GameManager.GM.crouch.ToString());
            break;

            case "jump":
            GameManager.GM.jump = KeyCode.Space;
            buttonText.text = GameManager.GM.jump.ToString();
            PlayerPrefs.SetString("jumpKey",GameManager.GM.jump.ToString());
            break;

            case "attack":
            GameManager.GM.attack = KeyCode.Z;
            buttonText.text = GameManager.GM.attack.ToString();
            PlayerPrefs.SetString("attackKey",GameManager.GM.attack.ToString());
            break;

            case "fireball":
            GameManager.GM.fireball = KeyCode.F;
            buttonText.text = GameManager.GM.fireball.ToString();
            PlayerPrefs.SetString("fireballKey",GameManager.GM.fireball.ToString());
            break;

            case "dash":
            GameManager.GM.dash = KeyCode.C;
            buttonText.text = GameManager.GM.dash.ToString();
            PlayerPrefs.SetString("dashKey",GameManager.GM.dash.ToString());
            break;

            case "shoot":
            GameManager.GM.shoot = KeyCode.X;
            buttonText.text = GameManager.GM.shoot.ToString();
            PlayerPrefs.SetString("shootKey",GameManager.GM.shoot.ToString());
            break;

            case "escape":
            GameManager.GM.escape = KeyCode.Escape;
            buttonText.text = GameManager.GM.escape.ToString();
            PlayerPrefs.SetString("escapeKey",GameManager.GM.escape.ToString());
            break;
        }

        waitingForKey = false;
    }
}
