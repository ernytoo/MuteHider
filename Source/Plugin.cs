using BepInEx;
using mutehider.Source.Tools;
using System;
using System.ComponentModel;
using UnityEngine;
using Utilla;
using GorillaExtensions;
using GorillaLocomotion;
using GorillaNetworking;
using GorillaTag;
using GorillaTagScripts;
using Photon;
using Photon.Pun;

namespace mutehider.Source
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.6.11")]
    public class Plugin : BaseUnityPlugin
    {
        void Awake()
        {
            Logging.Init();
        }

        void Start()
        {
            Utilla.Events.GameInitialized += OnGameInitialized;
        }
        void OnGameInitialized(object _, EventArgs __)
        {
            this.gameObject.AddComponent<Controls>();
        }

        void Update()
        {
            if (PhotonNetwork.InRoom && PhotonNetwork.IsMasterClient)
            {
                Transform parentTransform = GameObject.Find("Player Objects/RigCache/Rig Parent").transform;
                for (int i = 0; i < parentTransform.childCount; i++)
                {
                    Transform childTransform = parentTransform.GetChild(i);
                    if (childTransform.gameObject.activeSelf == true)
                    {
                        VRRig plrRig = childTransform.gameObject.GetComponent<VRRig>();
                        RigContainer plrContainer = childTransform.gameObject.GetComponent<RigContainer>();
                        if (plrContainer.Muted == true && plrContainer.gameObject.activeSelf == true)
                        {
                            if (plrRig.photonView != null)
                            {
                                childTransform.position = new Vector3(9999, 9999, 9999);
                            }
                        }
                    }
                }
            }
        }

        void OnEnable() => HarmonyPatches.ApplyHarmonyPatches();

        void OnDisable() => HarmonyPatches.RemoveHarmonyPatches();
    }
}
