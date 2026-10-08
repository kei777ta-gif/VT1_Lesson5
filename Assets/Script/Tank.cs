using UnityEngine;
using UnityEngine.InputSystem;

public class Tank : MonoBehaviour
{
    [SerializeField] private Transform TopJoint;

    [SerializeField] private Transform CannonJoint;

    [SerializeField] private GameObject BulletPrefab;

    [SerializeField] private Transform ShotPoint;

    private Vector3 topAngles = Vector3.zero;

    private Vector3 cannonAngles = Vector3.zero;
    void Start()
    {
        
    }
    void Update()
    {
        if(Keyboard.current.wKey.isPressed==true)
        {
            transform.Translate(Vector3.forward *5* Time.deltaTime);
        }

        if(Keyboard.current.sKey.isPressed==true)
        {
            transform.Translate(Vector3.forward *-5* Time.deltaTime);
        }

        if (Keyboard.current.aKey.isPressed == true)
        {
            transform.Rotate(Vector3.up*-90* Time.deltaTime);
        }

        if (Keyboard.current.dKey.isPressed == true)
        {
            transform.Rotate(Vector3.up*90* Time.deltaTime);
        }   

        //マウスの移動量を計算する
        Vector2 mouseDelta=Mouse.current.delta.ReadValue();

        //Debug.Log("マウス移動量"+mouseDelta);

        //角度の増減
        topAngles.y+=mouseDelta.x*0.1f;
        cannonAngles.x-=mouseDelta.y*0.1f;
        cannonAngles.x = Mathf.Clamp(cannonAngles.x, - 10f, 30f); //キャノンの角度を制限する 

        //各ジョイントに角度を反映する 
        //ローカル座標は親中心　ワールド座標は世界中心
        TopJoint.localEulerAngles=topAngles;
        CannonJoint.localEulerAngles=cannonAngles;

       //弾丸の発射
       if(Mouse.current.leftButton.wasPressedThisFrame==true)
       {
            //弾丸を生成する
            GameObject bullet=Instantiate(BulletPrefab,ShotPoint.position,ShotPoint.rotation);

            Rigidbody rb=bullet.GetComponent<Rigidbody>();
            rb.AddForce(ShotPoint.forward*1000f,ForceMode.Impulse);

            Destroy(bullet,2f);
        }
    }
}
