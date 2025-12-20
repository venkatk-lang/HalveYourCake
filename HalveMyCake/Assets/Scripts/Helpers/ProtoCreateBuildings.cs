using IACGGames;
using IACGGames.UISystem;
using UnityEditor.Events;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static Unity.Collections.AllocatorManager;

[ExecuteInEditMode]
public class ProtoCreateBuildings : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    [SerializeField] Image imagePrefab;
    [SerializeField] Transform parentTransform;
    [SerializeField] int number = 5;
    [SerializeField] MainMenu mainMenu;
    [SerializeField] bool buttonIsActive = false;
    [ContextMenu("CreateBuildings")]
    void CreateBuildings()
    {
        for(int i = 0; i < number; i++)
        {
            Image img = Instantiate(imagePrefab, parentTransform);
            img.sprite = sprites[Random.Range(0, sprites.Length)];
            img.name = "Building_" + i;
            Button btn = img.GetComponent<Button>();
            btn.interactable = buttonIsActive;
            UnityEditor.Events.UnityEventTools.AddPersistentListener(btn.onClick, new UnityAction(ButtonFunction));
        }
    }
    public void ButtonFunction()
    {
        AudioManager.Instance.PlaySFX(SFXAudioID.Click);
        mainMenu.OnPlayButtonPressed();
    }
}
