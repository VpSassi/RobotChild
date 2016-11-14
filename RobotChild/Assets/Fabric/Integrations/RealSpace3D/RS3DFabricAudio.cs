using UnityEngine;
using System.Collections;
using RealSpace3D;

public class RS3DFabricAudio : MonoBehaviour, Fabric.IVRAudio
{
    RealSpace3D_AudioSource_SpatialParameters realSpace3D_AudioSource;

    Fabric.Component fabricComponent = null;

    void Fabric.IVRAudio.Initialise()
    {
        realSpace3D_AudioSource = GetComponent<RealSpace3D_AudioSource_SpatialParameters>();
    }

    void Fabric.IVRAudio.Shutdown()
    {
        //
    }

    void Fabric.IVRAudio.Set(Fabric.Component component)
    {
        realSpace3D_AudioSource = gameObject.GetComponentInChildren<RealSpace3D_AudioSource_SpatialParameters>();

        if (realSpace3D_AudioSource != null && component.ParentGameObject != null)
        {
            realSpace3D_AudioSource.transform.SetParent(component.ParentGameObject.transform, false);

            fabricComponent = component;

            UpdateParameters();
        }
    }

    void Fabric.IVRAudio.Unset()
    {
        realSpace3D_AudioSource = gameObject.GetComponentInChildren<RealSpace3D_AudioSource_SpatialParameters>();

        if (realSpace3D_AudioSource != null)
        {
            realSpace3D_AudioSource.rs3d_StopAllSounds();

            realSpace3D_AudioSource.transform.parent = Fabric.AudioSourcePool.Instance.gameObject.transform;
        }
    }

    void Fabric.IVRAudio.Update()
    {
        UpdateParameters();
    }

    void UpdateParameters()
    {
        if (realSpace3D_AudioSource != null && fabricComponent != null)
        {
            if (realSpace3D_AudioSource.fSoundRange_MinDist != fabricComponent.UpdateContext._minDistance)
            {
                realSpace3D_AudioSource.fSoundRange_MinDist = fabricComponent.UpdateContext._minDistance;
            }

            if (realSpace3D_AudioSource.fSoundRange_MaxDist != fabricComponent.UpdateContext._maxDistance)
            {
                realSpace3D_AudioSource.fSoundRange_MaxDist = fabricComponent.UpdateContext._maxDistance;
            }

            //realSpace3D_AudioSource.rs3d_AdjustVolume(fabricComponent.UpdateContext._volume, 0);
            //realSpace3D_AudioSource.rs3d_AdjustPitch(fabricComponent.UpdateContext._pitch, 0);
        }
    }
}
