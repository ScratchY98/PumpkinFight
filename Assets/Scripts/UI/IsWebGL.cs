using UnityEngine;

public class IsWebGL : MonoBehaviour
{
    public bool isRunInWebGL;
    public static bool isWebGL;

    [SerializeField] private GameObject[] gameObjectsToDisable;
    [SerializeField] private Behaviour[] behaviourToDisable;
    [SerializeField] private GameObject[] gameObjectsToUnable;


    void Awake()
    {
        isWebGL = isRunInWebGL;

        if (!isWebGL) return;


        for (int i = 0; i < gameObjectsToDisable.Length; i++){
            gameObjectsToDisable[i].SetActive(false); }

        for (int i = 0; i < behaviourToDisable.Length; i++){
            behaviourToDisable[i].enabled = false; }


        for (int i = 0; i < gameObjectsToUnable.Length; i++){
            gameObjectsToUnable[i].SetActive(true); }
    }
}
