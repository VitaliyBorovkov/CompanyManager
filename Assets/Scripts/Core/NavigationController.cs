using UnityEngine;

public class NavigationController
{
    private readonly GameObject main;
    private readonly GameObject create;
    private readonly GameObject choose;

    public NavigationController(GameObject main, GameObject create, GameObject choose)
    {
        this.main = main;
        this.create = create;
        this.choose = choose;
    }

    public void ShowMain()
    {
        main.SetActive(true);
        create.SetActive(false);
        choose.SetActive(false);
    }

    public void ShowCreate()
    {
        main.SetActive(false);
        create.SetActive(true);
        choose.SetActive(false);
    }

    public void ShowChoose()
    {
        main.SetActive(false);
        create.SetActive(false);
        choose.SetActive(true);
    }
}
