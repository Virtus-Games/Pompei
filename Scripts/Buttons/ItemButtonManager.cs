using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ItemButtonManager : MonoBehaviour
{

    // MarketPanel -> Main -> 
    // Background Panel -> Image Background -> 
    // Grid Panel - > Items  Compoenent Button


    public void GunBuyButton()
    {
        //AdmonController.Instance.SetStatus(AdmobStatus.Gun);
    }


    public void BuyCharackterItem()
    {
        // AdmonController.Instance.SetStatus(AdmobStatus.Charackter);
    }


    public void OpenBazuka()
    {
        // AdmonController.Instance.SetRequestAtGun(AdmobStatus.Bazuka);
    }

    public void OpenGrading()
    {
        //  AdmonController.Instance.SetRequestAtGun(AdmobStatus.Grading);
    }

}
