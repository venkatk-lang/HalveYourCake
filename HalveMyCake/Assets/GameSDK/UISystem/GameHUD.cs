
using UnityEngine;
namespace IACGGames.UISystem
{

    public class GameHUD : UIPanelBase
    {

        [SerializeField] private GameObject topBarGO;

        protected override void OnEnable()
        {
            base.OnEnable();

        }

        protected override void OnDisable()
        {
            base.OnDisable();

        }

        public override void Show(float animTime = 0)
        {
            base.Show(animTime);
            ShowTopBar(true);
        }
        public override void Hide(float animTime = 0)
        {
            base.Hide(animTime);
        }


       
        public void ShowTopBar(bool show)
        {
            topBarGO.gameObject.SetActive(show);
           
        }
      
    }
}
