using UnityEngine;

public class SetOnWholeScreen : MonoBehaviour
{
    void Awake()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        float spriteWidth = sr.sprite.bounds.size.x;
        float spriteHeight = sr.sprite.bounds.size.y;

        float worldScreenHeight = Camera.main.orthographicSize * 2f;
        float worldScreenWidth = worldScreenHeight * Screen.width / Screen.height;

        Vector3 scale = transform.localScale;
        scale.x = worldScreenWidth / spriteWidth;
        scale.y = worldScreenHeight / spriteHeight;
        transform.localScale = scale;

        transform.position = new Vector3(Camera.main.transform.position.x,
                                         Camera.main.transform.position.y,
                                         transform.position.z);
    }
}