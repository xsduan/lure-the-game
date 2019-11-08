using UnityEngine;

public class GuiSubmenuManager : MonoBehaviour
{
    private UniquePrefabSwitch subMenus;
    void Start() => subMenus = new UniquePrefabSwitch(transform);
    public void ActivateMenu(GameObject prefabMenu) => subMenus.Activate(prefabMenu);
}
