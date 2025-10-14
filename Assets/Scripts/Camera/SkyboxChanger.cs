using UnityEngine;

public class SkyboxChanger : MonoBehaviour
{
    public static SkyboxChanger instance;

    public Material daySkybox;
    public Material nightSkybox;

    private void Awake()
    {
        instance = this;
    }

    public void SetDaySkybox()
    {
        RenderSettings.skybox = daySkybox;
        DynamicGI.UpdateEnvironment();
    }

    public void SetNightSkybox()
    {
        RenderSettings.skybox = nightSkybox;
        DynamicGI.UpdateEnvironment();
    }
}
