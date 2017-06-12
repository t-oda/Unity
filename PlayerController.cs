using UnityEngine;
using System.Collections;
using UnitySampleAssets.CrossPlatformInput;

//↓Serializableを入れることで、インスペクター上で動かすことができる
[System.Serializable]
//移動距離の限界値
public class Boundary
{
    //縦横の最少・最大移動値
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {
    //void startで使用
    //公式とは違うが、Unity5以降だとこれ書かないとエラーになる
    public Rigidbody rb;
    public AudioSource audio;

    //スピード
    public float speed;
    public float tilt;
    public Boundary boundary;

    public GameObject shot;
    public Transform[] shotSpawns;

    public float fireRate;

    private float nextFire;

    void Start()
    {
        //公式とは違うが、Unity5以降だとこれを書かないとエラーになります
        //componentから変数に持ってくる
        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        //弾をインスタンス化 
        //Fire1(左クリック)からShotボタンに切り替え
        if (CrossPlatformInputManager.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            //GameObject clone =
            foreach (var shotSpawn in shotSpawns)
            {
                Instantiate(shot, shotSpawn.position, shotSpawn.rotation); //as GameObject;
            }   
            //アタッチされているオーディオを鳴らす
            audio.Play();
        }
    }

    void FixedUpdate()
    {
        //水平移動
        float moveHorizontal = CrossPlatformInputManager.GetAxis("Horizontal");
        //前後移動
        float moveVertical = CrossPlatformInputManager.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        //移動距離をrigitbodyに反映(speedはインスペクターで調整)
        rb.velocity = movement * speed;

        //移動範囲について、Min～Maxまでの範囲で動く。y(上下)は動かない
        rb.position = new Vector3
            (   
                Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
                0.0f,
                Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
            );
        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
        if (Input.GetKey(KeyCode.Escape))
        {
            // アプリケーション終了
            Application.Quit();
            return;
        }
    }
    
}
