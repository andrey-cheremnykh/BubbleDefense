using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] int money = 100;
    Text moneyText;

    // Start is called before the first frame update
    void Start()
    {
        moneyText = GetComponentInChildren<Text>();
        moneyText.text = money.ToString();
    }

    public void AddMoney(int addition)
    {
        money += addition;
        moneyText.text = money.ToString();
    }

    public bool SpendMoney(int price)
    {
        if(price <= money)
        {
            money -= price;
            moneyText.text = money.ToString();
            return true;
        }
        // add some sound or anim for unsuccessful bougth
        return false;
    }
    
}
