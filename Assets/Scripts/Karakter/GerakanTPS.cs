using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GerakanTPS : MonoBehaviour
{

    private CharacterController Movemen;
    private Vector2 _arahJalan;
    private Vector2 _arahAim;

    [SerializeField]
    private LayerMask layer;

    [SerializeField]
    private float Kecepatan;
    private bool _isAiming;
    private bool _isWithMouse;

    float gravitasi;

    void Start()
    {
        Movemen = GetComponent<CharacterController>();
    }

    void Update()
    {
        napak();
        Movemen.Move(DirectionBasedCamera(_arahJalan) * Kecepatan * Time.deltaTime);
    }

    public void GetMoveValue(InputAction.CallbackContext callbackContext)
    {
        _arahJalan.x = callbackContext.ReadValue<Vector2>().x;
        _arahJalan.y = callbackContext.ReadValue<Vector2>().y;

        if (!_isAiming)
            Rotasi(_arahJalan);
    }

    public void GetLoncatValue(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.started)
            Loncat();
    }

    public void GetAimValue(InputAction.CallbackContext callbackContext)
    {
        _arahAim = callbackContext.ReadValue<Vector2>();

        if (_arahAim != Vector2.zero)
            _isAiming = true;
        else
            _isAiming = false;

        Rotasi(_arahAim);

    }

 

    bool napak()
    {
        if (Physics.CheckSphere(new Vector3(transform.position.x, transform.position.y - .8f, transform.position.z), .3f, layer))
        {
            return true;
        }
        else
        {
            gravitasi -= 9 * Time.deltaTime;
            return false;
        }
    }

    void Rotasi(Vector2 input)
    {
        if (input != Vector2.zero)
        {
           transform.rotation = Quaternion.Slerp(transform.rotation, 
               Quaternion.Euler(new Vector3(0, AngleBasedCamera(input), 0)), .3f);
            //transform.eulerAngles = new Vector3(0, AngleBasedCamera(input), 0);
        }      
    }

    private Vector3 DirectionBasedCamera(Vector2 input)
    {
        //agar arah jalan player sesuai dari prespektif kamera. 
        float X = (((Mathf.Cos(Camera.main.transform.eulerAngles.y * Mathf.Deg2Rad)) * input.x) +
        (Mathf.Sin(Camera.main.transform.eulerAngles.y * Mathf.Deg2Rad) * input.y));

        float Z = (-((Mathf.Sin(Camera.main.transform.eulerAngles.y * Mathf.Deg2Rad)) * input.x) +
             (Mathf.Cos(Camera.main.transform.eulerAngles.y * Mathf.Deg2Rad) * input.y));

        return new Vector3(X, gravitasi, Z);
    }

    private float AngleBasedCamera(Vector2 input)
    {
        //agar arah jalan player sesuai dari prespektif kamera. 
        float X = (((Mathf.Cos(Camera.main.transform.eulerAngles.y * Mathf.Deg2Rad)) * input.x) +
        (Mathf.Sin(Camera.main.transform.eulerAngles.y * Mathf.Deg2Rad) * input.y));

        float Z = (-((Mathf.Sin(Camera.main.transform.eulerAngles.y * Mathf.Deg2Rad)) * input.x) +
             (Mathf.Cos(Camera.main.transform.eulerAngles.y * Mathf.Deg2Rad) * input.y));

        return Mathf.Atan2(X, Z) * Mathf.Rad2Deg;
    }

    public void Loncat()
    {
        if (napak())
        {
            gravitasi = 3;
        }
    }
}
