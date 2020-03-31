using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Inventory : MonoBehaviour
{
    SQLiteDB db;

    public Player player;
    public GameObject slotE;
    public GameObject slotR;

    public class Item
    {
        int ea;
        string type;
        Player player;
        public Item(string type, Player player)
        {
            ea = 1;
            this.type = type;
            this.player = player;
        }
        public virtual void Use() { }
        public int Ea
        {
            get
            {
                return ea;
            }
            set
            {
                ea = value;
            }
        }
        public string Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }
        public Player Player
        {
            get
            {
                return player;
            }
            set
            {
                player = value;
            }
        }
    }
    public class HpPortion : Item
    {
        public HpPortion(string type,Player player) : base(type,player)
        {
        }
        public override void Use()
        {
            if (Ea >= 1)
            {
                Ea--;
                if (Player.MaxHp - Player.Hp <= 30)
                {
                    Player.Hp = Player.MaxHp;
                }
                else
                {
                    Player.Hp += 30;
                }
            }
        }
    }
    public class MpPortion : Item
    {
        public MpPortion(string type, Player player) : base(type,player)
        {

        }
        public override void Use()
        {
            if (Ea >= 1)
            {
                Ea--;
                if (Player.MaxMp - Player.Mp <= 30)
                {
                    Player.Mp = Player.MaxMp;
                }
                else
                {
                    Player.Mp += 30;
                }
            }
        }
    }
    
    public List<GameObject> AllSlot;    // 모든 슬롯을 관리해줄 리스트.
    public Sprite[] sprites;

    public static List<Item> items;
    int EmptySlot;
    void Awake()
    {
        db = new SQLiteDB();
        
        string dbfilename = "MiniRPG.sqlite";
        string dbpath = Application.dataPath + "/StreamingAssets" + "/" + dbfilename;

        db.Open(dbpath);
        EmptySlot = 0;
        items = new List<Item>(4);
        InvenInit();
    }
    private void InvenInit()
    {
        string query = "SELECT * FROM Inventory;";

        SQLiteQuery qr = new SQLiteQuery(db, query);

        while (qr.Step())
        {
            Debug.Log("a");
            if (qr.GetString("SLOT") == "SLOT1")
            {
                if (qr.GetString("TYPE") == "hp")
                {
                    Item item = new HpPortion("hp", player);
                    items.Add(item);
                    items[items.IndexOf(item)].Ea = qr.GetInteger("EA");
                    Image slotImg, slotEImg;
                    Text slotText, slotEText;
                    slotImg = AllSlot[0].transform.Find("Image").GetComponent<Image>();
                    slotEImg = slotE.transform.Find("Image").GetComponent<Image>();
                    slotImg.sprite = slotEImg.sprite = sprites[0];
                    slotText = AllSlot[0].transform.Find("Text").GetComponent<Text>();
                    slotEText = slotE.transform.Find("EA").GetComponent<Text>();
                    slotText.text = slotEText.text = item.Ea.ToString();
                }
                else if (qr.GetString("TYPE") == "mp")
                {
                    Item item = new MpPortion("mp", player);
                    items.Add(item);
                    items[items.IndexOf(item)].Ea = qr.GetInteger("EA");
                    Image slotImg, slotRImg;
                    Text slotText, slotRText;
                    slotImg = AllSlot[0].transform.Find("Image").GetComponent<Image>();
                    slotRImg = slotR.transform.Find("Image").GetComponent<Image>();
                    slotImg.sprite = slotRImg.sprite = sprites[1];
                    slotText = AllSlot[0].transform.Find("Text").GetComponent<Text>();
                    slotRText = slotR.transform.Find("EA").GetComponent<Text>();
                    slotText.text = slotRText.text = item.Ea.ToString();
                }
            }
            else if (qr.GetString("SLOT") == "SLOT2")
            {
                if (qr.GetString("TYPE") == "hp")
                {
                    Item item = new HpPortion("hp", player);
                    items.Add(item);
                    items[items.IndexOf(item)].Ea = qr.GetInteger("EA");
                    Image slotImg, slotEImg;
                    Text slotText, slotEText;
                    slotImg = AllSlot[1].transform.Find("Image").GetComponent<Image>();
                    slotEImg = slotE.transform.Find("Image").GetComponent<Image>();
                    slotImg.sprite = slotEImg.sprite = sprites[0];
                    slotText = AllSlot[1].transform.Find("Text").GetComponent<Text>();
                    slotEText = slotE.transform.Find("EA").GetComponent<Text>();
                    slotText.text = slotEText.text = item.Ea.ToString();
                }
                else if (qr.GetString("TYPE") == "mp")
                {
                    Item item = new MpPortion("mp", player);
                    items.Add(item);
                    items[items.IndexOf(item)].Ea = qr.GetInteger("EA");
                    Image slotImg, slotRImg;
                    Text slotText, slotRText;
                    slotImg = AllSlot[1].transform.Find("Image").GetComponent<Image>();
                    slotRImg = slotR.transform.Find("Image").GetComponent<Image>();
                    slotImg.sprite = slotRImg.sprite = sprites[1];
                    slotText = AllSlot[1].transform.Find("Text").GetComponent<Text>();
                    slotRText = slotR.transform.Find("EA").GetComponent<Text>();
                    slotText.text = slotRText.text = item.Ea.ToString();
                }
            }
        }
        qr.Release();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (items[0].Type == "hp")
                OnClickSlot1();
            else if (items[1].Type == "hp")
                OnClickSlot2();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (items[0].Type == "mp")
                OnClickSlot1();
            else if (items[1].Type == "mp")
                OnClickSlot2();
        }
    }
    public void Add(string type)
    {
        if (items.Count >= 4)
        {
            return;
        }
        else
        {
            int idx=-1;
            bool exist = false;
            Item temp = null;
            foreach (Item item in items)
            {
                if (item.Type == type)
                {
                    idx = items.IndexOf(item);
                    exist = true;
                    temp = item;
                    break;
                }
            }
            GameObject slot;
            Image slotImg;
            Text slotText;
            if (idx == -1)
            {
                slot = AllSlot[EmptySlot++];
            }
            else
            {
                slot = AllSlot[idx];
            }
            slotImg = slot.transform.Find("Image").GetComponent<Image>();
            slotText = slot.transform.Find("Text").GetComponent<Text>();
            if (exist == true)
            {
                if (temp.Ea < 10)
                {
                    temp.Ea++;
                    slotText.text = temp.Ea.ToString();
                    if (temp.Type == "hp")
                    {
                        Text tmpEa = slotE.transform.Find("EA").GetComponent<Text>();
                        tmpEa.text = temp.Ea.ToString();
                    }
                    else
                    {
                        Text tmpEa = slotR.transform.Find("EA").GetComponent<Text>();
                        tmpEa.text = temp.Ea.ToString();
                    }
                }
            }
            else
            {
                if (type == "hp")
                {
                    Image tmpImg = slotE.transform.Find("Image").GetComponent<Image>();
                    Text tmpEa = slotE.transform.Find("EA").GetComponent<Text>();
                    temp = new HpPortion(type,player);
                    items.Add(temp);
                    tmpImg.sprite=slotImg.sprite = sprites[0];
                    tmpEa.text = temp.Ea.ToString();
                }
                else if (type == "mp")
                {
                    Image tmpImg = slotR.transform.Find("Image").GetComponent<Image>();
                    Text tmpEa = slotR.transform.Find("EA").GetComponent<Text>();
                    temp = new MpPortion(type,player);
                    items.Add(temp);
                    tmpImg.sprite=slotImg.sprite = sprites[1];
                    tmpEa.text = temp.Ea.ToString();
                }
                slotText.text = temp.Ea.ToString();
            }
        }
        Debug.Log(items.Count);
    }

    public void OnClickSlot1()
    {
        if (items.Count > 0)
        {
            items[0].Use();
            Text text = AllSlot[0].transform.Find("Text").GetComponent<Text>();
            text.text = items[0].Ea.ToString();
            Text tmpEa = slotE.transform.Find("EA").GetComponent<Text>();
            tmpEa.text = items[0].Ea.ToString();
        }
    }
    public void OnClickSlot2()
    {
        if (items.Count > 1)
        {
            items[1].Use();
            Text text = AllSlot[1].transform.Find("Text").GetComponent<Text>();
            text.text = items[1].Ea.ToString();
            Text tmpEa = slotR.transform.Find("EA").GetComponent<Text>();
            tmpEa.text = items[1].Ea.ToString();
        }
    }
}

