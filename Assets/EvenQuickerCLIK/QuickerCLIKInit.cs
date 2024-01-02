using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CrazyLabsHubs
{
    public class QuickerCLIKInit : MonoBehaviour
    {
        void Awake()
        {
            //kinda a hack to check if any of these defines are included into project, if so it means that CLIK plugin is present and we can init the CLIK, this is so that the order of operations
            //between the CLIK and the QuickCLIK setup does not throw compile time errors due the missing namespace if QuickCLIK is imported first
#if TTP_ANALYTICS || TTP_REWARDED_INTERSTITIALS || TTP_PRIVACY_SETTINGS || TTP_APPSFLYER || TTP_REWARDED_ADS || TTP_PROMOTION || TTP_INTERSTITIALS || TTP_GAMEPROGRESSION || TTP_RATEUS || TTP_BANNERS || TTP_POPUPMGR || TTP_CRASHTOOL || TTP_OPENADS
            Tabtale.TTPlugins.TTPCore.Setup();
#endif
        }
    }
}
