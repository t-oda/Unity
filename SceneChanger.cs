using UnityEngine;
using System.Collections;
using UnitySampleAssets.CrossPlatformInput;

public class SceneChanger : MonoBehaviour {

    public string nextSceneName;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (0 < Input.touchCount)
        {

            //Shotボタンを押すと次のシーンに
            if (CrossPlatformInputManager.GetButtonDown("Fire1"))
            {
                Application.LoadLevel(nextSceneName);
            }
        }
        if (Input.GetKey(KeyCode.Escape))
            {
                // アプリケーション終了
                Application.Quit();
                return;
            }
	
	}
}
