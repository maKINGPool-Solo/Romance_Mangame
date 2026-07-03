using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainSceneClickManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);

            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);
            if (hit.collider != null)
            {
                CharacterInfo character = hit.collider.GetComponent<CharacterInfo>();
                if (character != null)
                {

                    Debug.Log("Clicked: " + character.characterId);
                    DialogueData.SelectedCharacterId = character.characterId;
                    //SceneManager.LoadScene("");
                }
            }
        }
    }

    //int today = TimeManager.Instance.currentDay; 이걸로 날짜 가져가실 수 있고
    //string clickedCharacter = DialogueData.SelectedCharacterId; 이런 식으로 캐릭터 id 가져가실 수 있습니다!
}
