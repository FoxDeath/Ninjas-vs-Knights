using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{
    [SerializeField] Transform recoilPosition;
    [SerializeField] Transform rotationPoint; 

    [SerializeField] float positionRecoilSpeed = 8f;
    [SerializeField] float rotationRecoilSpeed = 8f;
    [SerializeField] float positionReturnSpeed = 18f;
    [SerializeField] float rotationReturnSpeed = 38f;

    [SerializeField] Vector3 recoilRotation = new Vector3(10f, 5f, 7f);
    [SerializeField] Vector3 recoilKickBack = new Vector3(0.015f, 0f, -0.2f);
    [SerializeField] Vector3 recoilRotationAim = new Vector3(10f, 4f, 6f);
    [SerializeField] Vector3 recoilKickBackAim = new Vector3(0.015f, 0f, -0.2f);
    private Vector3 rotationRecoil;
    private Vector3 positionRecoil;
    private Vector3 rotation;

    void FixedUpdate()
    {
        rotationRecoil = Vector3.Lerp(rotationRecoil, Vector3.zero, rotationReturnSpeed * Time.deltaTime);
        positionRecoil = Vector3.Lerp(positionRecoil, Vector3.zero, positionReturnSpeed * Time.deltaTime);

        recoilPosition.localPosition = Vector3.Slerp(recoilPosition.localPosition + new Vector3(0f, 0f, 0.1f), positionRecoil, positionRecoilSpeed * Time.fixedDeltaTime);
        rotation = Vector3.Slerp(rotation, rotationRecoil, rotationRecoilSpeed * Time.fixedDeltaTime);
        rotationPoint.localRotation = Quaternion.Euler(rotation);
    }

    public void Fire(bool aiming)
    {
        if(aiming)
        {   
            rotationRecoil += new Vector3(-recoilRotationAim.x, Random.Range(-recoilRotationAim.y, recoilRotationAim.y), Random.Range(-recoilRotationAim.z, recoilRotationAim.z));
            positionRecoil += new Vector3(Random.Range(-recoilKickBackAim.x, recoilKickBackAim.x), Random.Range(-recoilKickBackAim.y, recoilKickBackAim.y), recoilKickBackAim.z);
        }
        else
        {
            rotationRecoil += new Vector3(-recoilRotation.x, Random.Range(-recoilRotation.y, recoilRotation.y), Random.Range(-recoilRotation.z, recoilRotation.z));
            positionRecoil += new Vector3(Random.Range(-recoilKickBack.x, recoilKickBack.x), Random.Range(-recoilKickBack.y, recoilKickBack.y), recoilKickBack.z);
        }
    }
}
