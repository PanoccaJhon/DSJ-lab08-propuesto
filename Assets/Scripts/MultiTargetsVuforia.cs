using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MultiTargetVuforia : MonoBehaviour
{
    [SerializeField] private GameObject startModel;
    [SerializeField] private Slider rotationSlider; // Referencia al Slider
    private int modelsCount;
    private int indexCurrentModel;
    // Start is called before the first frame update
    void Start()
    {
        modelsCount = transform.childCount;
        indexCurrentModel = startModel.transform.GetSiblingIndex();

        // Asegúrate de inicializar el slider
        if (rotationSlider != null)
        {
            rotationSlider.onValueChanged.AddListener(RotateActiveModel);
        }
    }
    public void ChangeARModel(int index)
    {
        transform.GetChild(indexCurrentModel).gameObject.SetActive(false);
        int newIndex = indexCurrentModel + index;
        if (newIndex < 0)
        {
            newIndex = modelsCount - 1;
        }
        else if (newIndex > modelsCount - 1)
        {
            newIndex = 0;
        }
        GameObject newModel = transform.GetChild(newIndex).gameObject;
        newModel.SetActive(true);
        indexCurrentModel = newModel.transform.GetSiblingIndex();

        // Reinicia el slider al cambiar el modelo
        if (rotationSlider != null)
        {
            rotationSlider.value = 0;
        }
    }

    // Rotar el modelo activo basado en el valor del slider
    public void RotateActiveModel(float value)
    {
        GameObject activeModel = transform.GetChild(indexCurrentModel).gameObject;
        activeModel.transform.localEulerAngles = new Vector3(0, value * 360, 0); // Rotación en el eje Y
    }
}
