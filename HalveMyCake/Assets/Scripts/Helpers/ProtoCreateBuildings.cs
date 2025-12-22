#if UNITY_EDITOR
using IACGGames;
using IACGGames.UISystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ProtoCreateBuildings : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    [SerializeField] Image imagePrefab;
    [SerializeField] Transform parentTransform;
    [SerializeField] int number = 5;
    [SerializeField] MainMenu mainMenu;
    [SerializeField] bool isSelectableLevel = false;
    [ContextMenu("CreateBuildings")]
    void CreateBuildings()
    {
        for (int i = 0; i < number; i++)
        {
            Image img = Instantiate(imagePrefab, parentTransform);
            img.sprite = sprites[Random.Range(0, sprites.Length)];
            img.name = "Building_" + i;
            if (!isSelectableLevel)
            {
                Helpers.DestroyChildren(img.transform);
                return;
            }
            Button btn = img.GetComponent<Button>();
            btn.interactable = true;
            UnityEditor.Events.UnityEventTools.AddIntPersistentListener(btn.onClick, new UnityAction<int>(mainMenu.PlayButtonPressedWithInt), i);
            var buildingItem = img.GetComponent<BuildingItem>();
            for (int j = 0; j < 3; j++)
            {
                buildingItem.stars[j] = img.transform.GetChild(0).GetChild(j).GetComponent<Image>();
            }
        }
    }
    //public void ButtonFunction()
    //{
    //    AudioManager.Instance.PlaySFX(SFXAudioID.Click);
    //    mainMenu.OnPlayButtonPressed();
    //}
}
#endif