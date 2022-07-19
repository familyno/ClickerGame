using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct StructWeapon
{
    [SerializeField] private Weapons _weapon;
    [SerializeField] private GameObject _weaponMesh;
    [SerializeField] private AudioSource _weaponSound;
    public Weapons Weapon 
    { get { return _weapon; }
      set { value = _weapon; }
    }
    public GameObject WeaponMesh
    {
        get { return _weaponMesh; }
        set { value = _weaponMesh; }
    }
    public AudioSource WeaponSound
    {
        get { return _weaponSound; }
        set { value = _weaponSound; }
    }
}
