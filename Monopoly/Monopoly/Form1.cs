using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monopoly
{
    public partial class Form1 : Form
    {
        #region Declarations

        List<Action> myActionTriggers = new List<Action>();
        List<Action> myBoardTriggers = new List<Action>();

        //Declare myTile Variable to Hold a Card
        public Tile myTile;

        //CurrentPlayer holds the currently active player.
        public Player CurrentPlayer;
        
        //List for holding all the tiles (Cards)
        public List<Tile> myListOfTiles = new List<Tile>();

        //List for holding All the players
        public List<Player> myListOfPlayers = new List<Player>();

        public int Rent;

        //List for adding all Lables(Tiles/Cards) on Load
        List<Control> TileControls = new List<Control>();

        //list for holding virtual labels created at runtime for Card/Tile Titles
        List<Label> listOfNameLabels = new List<Label>();

        //list for holding virtual labels created at runtime for Card/Tile purchase Values
        List<Label> listOfValueLabels = new List<Label>();

        //list for holding virtual labels created at runtime for Card/Tile Rent Values
        List<Label> listOfRentLabels = new List<Label>();

        //list for holding tile names assigned from listofNameLables
        List<string> listOfTileNames = new List<string>();

        Label nameLabel;
        Label valueLabel;
        Label rentLabel;

        CommunityTriggers trigger;
        BoardTriggers boardTrigger;

        Player Player;

        Random myRandom = new Random();
        #endregion


        public Form1()
        {
            InitializeComponent();
        }

       
        private void btnDice_Click(object sender, EventArgs e)
        {
            btnDice.Text = new Random().Next(1, 7).ToString();//Create A new random value for Dice.


            if (CurrentPlayer.Position + Convert.ToInt32(btnDice.Text) < 77)//Check if the Current position is < the total number of tiles on the board so we go back to the first tile.
            {
                CurrentPlayer.SetPosition(CurrentPlayer.Position + Convert.ToInt32(btnDice.Text));//myCurrentPostion increments by the number on the dice.
                foreach (Tile myTile in myListOfTiles)//iterate through all the tiles in myListOfTiles
                {
                    if (CurrentPlayer.Position == myTile.ID)//if the myCurrentPostion matches the TILE ID we set the position of the current tile to that Tile
                    {
                        CurrentPlayer.CurrentTile = myTile;
                        CurrentPlayer.CurrentTile = myTile;//Set the position of the current tile to the position tile.
                        CurrentPlayer.SetPosition(CurrentPlayer.CurrentTile.ID);
                    }
                }
            }
            else //if myCurretnPosition >= 22 then set the current position to the lower numbers
            {
                CurrentPlayer.SetPosition(CurrentPlayer.Position - ((78 - Convert.ToInt16(btnDice.Text)) - 1));
                foreach (Tile myTile in myListOfTiles)
                {
                    if (CurrentPlayer.Position == myTile.ID)
                    {
                        CurrentPlayer.CurrentTile = myTile;
                        CurrentPlayer.CurrentTile = myTile;
                        CurrentPlayer.SetPosition(CurrentPlayer.CurrentTile.ID);
                    }
                }
            }

            lblDice.Text = CurrentPlayer.Position.ToString();

            DistributeRent();

            CheckIfLandedOnCommunityChest();
            CheckIfLandedOnTriggerTile();
            DisplayCardDetails();
            DrawTileColors();
        }

        private void btnCreatePlayer_Click(object sender, EventArgs e)
        {
            Player = new Player();

            Player.CreatePlayer(txtFirstName.Text, txtLastName.Text);
            Player.AddMoney(1000000000);
            Player.SetColor(Convert.ToInt32(txtColorR.Text), Convert.ToInt32(txtColorG.Text), Convert.ToInt32(txtColorB.Text));
            Player.CurrentTile = myListOfTiles[0];

            myListOfPlayers.Add(Player);
            trigger.myTriggerPlayerList.Add(Player);

            AddPlayersToListBox();

            listBoxPlayers.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            trigger = new CommunityTriggers();
            boardTrigger = new BoardTriggers();

            LoadTiles(); //call the function which loads Tile Names

            LoadCommunityMethods();

            AddTilesToListOfTileControls();//Add all the black Labels as tiles to the list of Tile controls

            for (int i = 0; i < TileControls.Count; i++)
            {
                TileControls[i].Text = $"{i}";//Give a text label to each Tile/Label for clarity
                CreateNameLabels(i, myListOfTiles[i].Name); //Call the function of createing a label at runtime and assiging it a name from the names in the ListofTileNames
                if ((myListOfTiles[i].Name != "Community Chest") && (myListOfTiles[i].Name != "Chance") && (myListOfTiles[i].Name != "GO"))
                {
                    CreateValueLabels(i, myListOfTiles[i].PurchaseValue); //Create the virtual label to set the purchase value of the tile and rounding off the myNo var
                    //CreateRentLabels(i, myListOfTiles[i].Rent);
                }
                else
                {
                    CreateValueLabels(i, 0);
                    //CreateRentLabels(i, 0);
                }
            }
        }

        
        private void BtnPurchase_Click(object sender, EventArgs e)
        {
            bool hasHouse = false;

            foreach (Tile tile in CurrentPlayer.TilesOwned)
            {
                if (tile.IsHouse)
                {
                    hasHouse = true;
                }
            }

            if (hasHouse)
            {
                PurchaseTile(); //Call the Purchase method
                AddToListBoxOfAlltilesPurchased(); //Add the purchased tile to the litbox to hold all purchased tiles
            }
            else
            {
                if (CurrentPlayer.CurrentTile.IsHouse)
                {
                    PurchaseTile(); //Call the Purchase method
                    AddToListBoxOfAlltilesPurchased(); //Add the purchased tile to the litbox to hold all purchased tiles
                }
                else
                {
                    if (CurrentPlayer.CurrentTile.IsPurchaseAble)
                    {
                        MessageBox.Show("You Must Own A House First");
                    }
                }
                
            }
            
        }

        private void ListBoxPlayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void listBoxPlayers_Click(object sender, EventArgs e)
        {
            foreach (Player players in myListOfPlayers)
            {
                if (players.FirstName + " " + players.LastName == listBoxPlayers.Text)
                {
                    CurrentPlayer = players; //set the current player to the selected player in the listbox
                    lblMoney.Text = CurrentPlayer.Money.ToString("###,###,###");
                    lblDice.Text = CurrentPlayer.Position.ToString();
                }
            }

            DrawTileColors();

            AddToListBoxOfTilesOwned(); //Calls the mthod which checks all the Tiles the current player holds

            btnDice.Enabled = true;
            btnPurchase.Enabled = true;

            DisplayCardDetails();
        }

        //*********************************************CUSTOM FUNCTIONS*************************************************
        //*********************************************CUSTOM FUNCTIONS*************************************************
        //*********************************************CUSTOM FUNCTIONS*************************************************
        //*********************************************CUSTOM FUNCTIONS*************************************************
        //*********************************************CUSTOM FUNCTIONS*************************************************
        //*********************************************CUSTOM FUNCTIONS*************************************************
        //*********************************************CUSTOM FUNCTIONS*************************************************

        #region My Custom Functions

        void DistributeRent()
        {
            foreach (Tile tile in myListOfTiles)
            {
                if (tile.Purchased && CurrentPlayer.CurrentTile == tile)
                {
                    tile.Owner.AddMoney(tile.Rent);
                    CurrentPlayer.SubtractMoney(tile.Rent);
                }
            }
            lblMoney.Text = CurrentPlayer.Money.ToString("###,###,###");
        }

        void DisplayCardDetails()
        {
            lblCardName.Text = CurrentPlayer.CurrentTile.Name;
            lblPurchaseValue.Text = CurrentPlayer.CurrentTile.PurchaseValue.ToString("###,###,###");
            lblCardRent.Text = CurrentPlayer.CurrentTile.Rent.ToString("###,###,###");
        }

        void DrawTileColors()
        {
            //Change Color Of Each Tile Based On Current Tile Posistion
            foreach (Control myTile in TileControls)
            {
                if (myTile.Text == $"{CurrentPlayer.Position}")
                {
                    myTile.BackColor = CurrentPlayer.myChosenColor;
                    //myTile.BackColor = Color.Red;

                    //Set Position of Player Based on Tile Position
                    btnPlayer.Left = myTile.Left;
                    btnPlayer.Top = myTile.Top;
                }
                else
                {
                    //myTile.BackColor = CurrentPlayer.myChosenColor;
                    myTile.BackColor = Color.Black;
                }
            }
        }

        double RoundToNearest(int value)
        {
            if (value >= 100000 && value <= 999999)
            {
                return Math.Round((double)value / 100000, 0) * 100000;
            }
            else if(value >= 1000000 && value <= 9999999)
            {
                return Math.Round((double)value / 1000000, 0) * 1000000;
            }
            else if(value >= 10000000 && value <= 99999999)
            {
                return Math.Round((double)value / 10000000, 0) * 10000000;
            }
            else
            {
                return Math.Round((double)value / 100000000, 0) * 100000000;
            }
        }
        void LoadTiles() //initial Settings given to all tiles on load
        {  
            
            myTile = new Tile(0, "GO", 0, 0, false,false,false);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(1, "PHILIPS", (int)RoundToNearest(MyRandmonPurchaseValue()), (int)(RoundToNearest(Rent / 10)), true,false,true);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(2, "KANDAHAR", (int)RoundToNearest(MyRandmonPurchaseValue()), (int)(RoundToNearest(Rent / 10)), true,false,true);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(3, "HARRYS", (int)RoundToNearest(MyRandmonPurchaseValue()), (int)(RoundToNearest(Rent / 10)), true,false,true);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(4, "SERENGETI", (int)RoundToNearest(MyRandmonPurchaseValue()), (int)(RoundToNearest(Rent / 10)), true,false,true);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(5, "NYUKI LNAD 1", (int)RoundToNearest(MyRandmonPurchaseValue()), (int)(RoundToNearest(Rent / 10)), true,true, false);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(6, "NYUKI LAND 2", (int)RoundToNearest(MyRandmonPurchaseValue()), (int)(RoundToNearest(Rent / 10)),true,true, false);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(7, "TTCL", 0,0, false, false, false);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(8, "MSASANI 1", (int)RoundToNearest(MyRandmonPurchaseValue()), (int)(RoundToNearest(Rent / 10)), true, true, false);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(9, "JAIL", 0, 0, false, false, false);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(10, "MSASANI 2", (int)RoundToNearest(MyRandmonPurchaseValue()), (int)(RoundToNearest(Rent / 10)),false,true, false);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(11, "MSASANI 3", (int)RoundToNearest(MyRandmonPurchaseValue()), (int)(RoundToNearest(Rent / 10)), true,true, false);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(12, "CHANCE", 0, 0, false, false, false);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(13, "LOLKISALE 1", (int)RoundToNearest(MyRandmonPurchaseValue()), (int)(RoundToNearest(Rent / 10)), true, false, false);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(14, "LOLKISALE 2", (int)RoundToNearest(MyRandmonPurchaseValue()), (int)(RoundToNearest(Rent / 10)), true, false, false);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(15, "TANESCO", 0, 0, false, false, false);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(16, "COMMUNITY CHEST", 0, 0, false, false, false);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(17, "CHINI YA MTI", (int)RoundToNearest(MyRandmonPurchaseValue()), (int)(RoundToNearest(Rent / 10)), true, false,true);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(18, "MEGA LOTTERY", 0, 0, false, false, false);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(19, "SIMBA LAND 1", (int)RoundToNearest(MyRandmonPurchaseValue()), (int)(RoundToNearest(Rent / 10)), true,true, false);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(20, "SIMBA LAND 2", (int)RoundToNearest(MyRandmonPurchaseValue()), (int)(RoundToNearest(Rent / 10)), true,true, false);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(21, "CHANCE", 0, 0, false, false, false);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(22, "TAX", 0, 0, false, false, false);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(23, "TRAFFIC", 0, 0, false, false, false);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(24, "HOT PLATE", (int)RoundToNearest(MyRandmonPurchaseValue()), (int)(RoundToNearest(Rent / 10)), true, false,true);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(25, "COMMUNITY CHEST", 0, 0, false, false, false);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(26, "NHC 1", (int)RoundToNearest(MyRandmonPurchaseValue()), (int)(RoundToNearest(Rent / 10)), true,true, false);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(27, "TRA", 0, 0, false, false, false);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(28, "CHUMA 1", (int)RoundToNearest(MyRandmonPurchaseValue()), (int)(RoundToNearest(Rent / 10)), true,true, false);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(29, "CHUMA 2", (int)RoundToNearest(MyRandmonPurchaseValue()), (int)(RoundToNearest(Rent / 10)), true,true, false);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(30, "AIM STEEL 1", (int)RoundToNearest(MyRandmonPurchaseValue()), (int)(RoundToNearest(Rent / 10)), true, false,true);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(31, "AIM STEEL 2", (int)RoundToNearest(MyRandmonPurchaseValue()), (int)(RoundToNearest(Rent / 10)), true, false,true);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(32, "LEOPARD TOURS", (int)RoundToNearest(MyRandmonPurchaseValue()), (int)(RoundToNearest(Rent / 10)), true, false,true);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(33, "CORRUPTION", 0, 0, false, false, false);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(34, "STEEL CENTRE 1", (int)RoundToNearest(MyRandmonPurchaseValue()), (int)(RoundToNearest(Rent / 10)), true, false,true);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(35, "STEEL CENTRE 2", (int)RoundToNearest(MyRandmonPurchaseValue()), (int)(RoundToNearest(Rent / 10)), true, false,true);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(36, "WAREHOUSE 1-4", 0, 0, false, false, false);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(37, "SAMEER PARTS LTD", (int)RoundToNearest(MyRandmonPurchaseValue()), (int)(RoundToNearest(Rent / 10)), true, false,true);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(38, "VODACOM", (int)RoundToNearest(MyRandmonPurchaseValue()), (int)(RoundToNearest(Rent / 10)), true, false,true);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(39, "AIRTEL", (int)RoundToNearest(MyRandmonPurchaseValue()), (int)(RoundToNearest(Rent / 10)), true, false,true);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(40, "TIGO", (int)RoundToNearest(MyRandmonPurchaseValue()), (int)(RoundToNearest(Rent / 10)), true, false,true);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(41, "CRDB", 0, 0, false, false, false);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(42, "TANFOAM", (int)RoundToNearest(MyRandmonPurchaseValue()), (int)(RoundToNearest(Rent / 10)), true, false,true);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(43, "VEHICLE SHOWROOM", 0, 0, false, false, false);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(44, "NHC 2", (int)RoundToNearest(MyRandmonPurchaseValue()), (int)(RoundToNearest(Rent / 10)), true,true, false);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(45, "COMMUNITY CHEST", 0, 0, false, false, false);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(46, "NHC 3", (int)RoundToNearest(MyRandmonPurchaseValue()), (int)(RoundToNearest(Rent / 10)), true,true, false);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(47, "SEAVIEW", (int)RoundToNearest(MyRandmonPurchaseValue()), (int)(RoundToNearest(Rent / 10)), true,true, false);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(48, "MIC INSURANCE", 0, 0, false, false, false);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(49, "ROLLER", 0, 0, false, false, false);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(50, "HAJEES BUS", (int)RoundToNearest(MyRandmonPurchaseValue()), (int)(RoundToNearest(Rent / 10)), true, false,true);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(51, "BANK M", 0, 0, false, false, false);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(52, "COMMUNITY CHEST", 0, 0, false, false, false);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(53, "CHANCE", 0, 0, false, false, false);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(54, "GUPTA 1", (int)RoundToNearest(MyRandmonPurchaseValue()), (int)(RoundToNearest(Rent / 10)), true, false,true);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(55, "DANGER ZONE", 0, 0, false, false, false);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(56, "GUPTA 2", (int)RoundToNearest(MyRandmonPurchaseValue()), (int)(RoundToNearest(Rent / 10)), true, false,true);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(57, "AKHTAR SERVICE ST", (int)RoundToNearest(MyRandmonPurchaseValue()), (int)(RoundToNearest(Rent / 10)), true, false,true);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(58, "NHC 4", (int)RoundToNearest(MyRandmonPurchaseValue()), (int)(RoundToNearest(Rent / 10)), true,true, false);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(59, "STOP", 0, 0, false, false, false);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(60, "FERRY", 0, 0, false, false, false);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(61, "CHANCE", 0, 0, false, false, false);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(62, "KIPARA ENGINEERING", (int)RoundToNearest(MyRandmonPurchaseValue()), (int)(RoundToNearest(Rent / 10)), true, false,true);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(63, "IMPALA", (int)RoundToNearest(MyRandmonPurchaseValue()), (int)(RoundToNearest(Rent / 10)), true, false,true);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(64, "MC MOODYS", (int)RoundToNearest(MyRandmonPurchaseValue()), (int)(RoundToNearest(Rent / 10)), true, false,true);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(65, "JACKPOT BINGO", 0, 0, false, false, false);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(66, "COMMUNITY CHEST", 0, 0, false, false, false);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(67, "STOP", 0, 0, false, false, false);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(68, "MANJIS BP", (int)RoundToNearest(MyRandmonPurchaseValue()), (int)(RoundToNearest(Rent / 10)), true, false,true);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(69, "ARMORY", 0, 0, false, false, false);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(70, "WAREHOUSE 5-7", 0, 0, false, false, false);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(71, "MAKUYUNI 1", (int)RoundToNearest(MyRandmonPurchaseValue()), (int)(RoundToNearest(Rent / 10)), true,true, false);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(72, "MAKUYUNI 2", (int)RoundToNearest(MyRandmonPurchaseValue()), (int)(RoundToNearest(Rent / 10)), true,true, false);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(73, "AIRPORT", 0, 0, false, false, false);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(74, "NHC 5", (int)RoundToNearest(MyRandmonPurchaseValue()), (int)(RoundToNearest(Rent / 10)), true,true, false);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(75, "GO BACK 10 TILES", 0, 0, false, false, false);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
            myTile = new Tile(76, "JAIL", 0, 0, false, false, false);
            myListOfTiles.Add(myTile);
            trigger.myTriggerList.Add(myTile);
        }

        int MyRandmonPurchaseValue()
        {
            Rent = myRandom.Next(1000000, 200000000); ;
            return Rent;
        }
        void CreateNameLabels(int i,string tName)
        {
            nameLabel = new Label(); //Create a new Title Virtual Label everytime this method is called

            nameLabel.Name = "TileLabel" + i; //set the Name of the virtual label
            nameLabel.Font = new Font("Microsoft Sans Serif", 7);
            nameLabel.TextAlign = ContentAlignment.MiddleCenter;
            nameLabel.Top = TileControls[i].Top; //Set the top postion of the virtual label same as the Top postion of that tile
            nameLabel.Width = TileControls[i].Width; //Set the width of the virtual label to the same width as the Tile
            nameLabel.Left = TileControls[i].Left; // set the left property position of the virtual label same as the Tile
            nameLabel.Height = 12; //Give the Virtual label a height of 20
            nameLabel.BackColor = Color.Yellow; // Give the virtual lable a yellow color
            nameLabel.Text = tName; //Set the text property of the virtual label
            nameLabel.Visible = true; //Make it visible
            nameLabel.AutoSize = false; //Set the autosize propert of the lable to false.

            this.Controls.Add(nameLabel); //Add the visrtual label to the list of controls of the form

            nameLabel.BringToFront(); // Brings the virtaul label to the top most.

            listOfNameLabels.Add(nameLabel); //Add the virtual label to the listofNameLabels
        }

        void CreateRentLabels(int i, int value)
        {
            rentLabel = new Label(); //Create a new virtual label

            rentLabel.Name = "TileLabelValue" + i; //Assign the name of the virtual label
            rentLabel.Font = new Font("Microsoft Sans Serif", 7);
            rentLabel.Top = TileControls[i].Top + (TileControls[i].Height - 24); //Set the Top postion of the virtual label 20pxls less from the bottom of the Tile
            rentLabel.Width = TileControls[i].Width; //Set the Width of the virtual label same as the Tile
            rentLabel.Left = TileControls[i].Left; // Set the left property of the virtual labe same as that of the tile
            rentLabel.Height = 12; // Set the height porperty of the visrtual label to 20
            rentLabel.BackColor = Color.Silver; // Give a Cyan color to the virtual label
            rentLabel.Text = "Rent: " + value.ToString("###,###,###"); //Format the vistual label to give it commas to display the amounts properly
            rentLabel.Visible = true;
            rentLabel.AutoSize = false;

            this.Controls.Add(rentLabel); // Add the virtual label to the list of controls in the form

            rentLabel.BringToFront(); // Bring the virtual label to the top of other controls

            listOfRentLabels.Add(rentLabel); // add the virtual label to the listofrentLabels
        }

        void CreateValueLabels(int i, int value)
        {
            valueLabel = new Label(); //Create a new virtual label

            valueLabel.Name = "TileLabelValue" + i; //Assign the name of the virtual label
            valueLabel.Font = new Font("Microsoft Sans Serif", 7);
            valueLabel.TextAlign = ContentAlignment.MiddleCenter;
            valueLabel.Top = TileControls[i].Top + (TileControls[i].Height-12); //Set the Top postion of the virtual label 20pxls less from the bottom of the Tile
            valueLabel.Width = TileControls[i].Width; //Set the Width of the virtual label same as the Tile
            valueLabel.Left = TileControls[i].Left; // Set the left property of the virtual labe same as that of the tile
            valueLabel.Height = 12; // Set the height porperty of the visrtual label to 20
            valueLabel.BackColor = Color.Cyan; // Give a Cyan color to the virtual label
            valueLabel.Text = value.ToString("###,###,###"); //Format the virtual label to give it commas to display the amounts properly
            valueLabel.Visible = true;
            valueLabel.AutoSize = false;

            this.Controls.Add(valueLabel); // Add the virtual label to the list of controls in the form

            valueLabel.BringToFront(); // Bring the virtual label to the top of other controls

            listOfValueLabels.Add(valueLabel); // add the virtual label to the listofValueLabels
        }

        void AddTilesToListOfTileControls() //Add all black/tiel/card labels as tiles to the Tilecontrolslist
        {
            TileControls.Add(Tile0);
            TileControls.Add(Tile1);
            TileControls.Add(Tile2);
            TileControls.Add(Tile3);
            TileControls.Add(Tile4);
            TileControls.Add(Tile5);
            TileControls.Add(Tile6);
            TileControls.Add(Tile7);
            TileControls.Add(Tile8);
            TileControls.Add(Tile9);
            TileControls.Add(Tile10);
            TileControls.Add(Tile11);
            TileControls.Add(Tile12);
            TileControls.Add(Tile13);
            TileControls.Add(Tile14);
            TileControls.Add(Tile15);
            TileControls.Add(Tile16);
            TileControls.Add(Tile17);
            TileControls.Add(Tile18);
            TileControls.Add(Tile19);
            TileControls.Add(Tile20);
            TileControls.Add(Tile21);
            TileControls.Add(Tile22);
            TileControls.Add(Tile23);
            TileControls.Add(Tile24);
            TileControls.Add(Tile25);
            TileControls.Add(Tile26);
            TileControls.Add(Tile27);
            TileControls.Add(Tile28);
            TileControls.Add(Tile29);
            TileControls.Add(Tile30);
            TileControls.Add(Tile31);
            TileControls.Add(Tile32);
            TileControls.Add(Tile33);
            TileControls.Add(Tile34);
            TileControls.Add(Tile35);
            TileControls.Add(Tile36);
            TileControls.Add(Tile37);
            TileControls.Add(Tile38);
            TileControls.Add(Tile39);
            TileControls.Add(Tile40);
            TileControls.Add(Tile41);
            TileControls.Add(Tile42);
            TileControls.Add(Tile43);
            TileControls.Add(Tile44);
            TileControls.Add(Tile45);
            TileControls.Add(Tile46);
            TileControls.Add(Tile47);
            TileControls.Add(Tile48);
            TileControls.Add(Tile49);
            TileControls.Add(Tile50);
            TileControls.Add(Tile51);
            TileControls.Add(Tile52);
            TileControls.Add(Tile53);
            TileControls.Add(Tile54);
            TileControls.Add(Tile55);
            TileControls.Add(Tile56);
            TileControls.Add(Tile57);
            TileControls.Add(Tile58);
            TileControls.Add(Tile59);
            TileControls.Add(Tile60);
            TileControls.Add(Tile61);
            TileControls.Add(Tile62);
            TileControls.Add(Tile63);
            TileControls.Add(Tile64);
            TileControls.Add(Tile65);
            TileControls.Add(Tile66);
            TileControls.Add(Tile67);
            TileControls.Add(Tile68);
            TileControls.Add(Tile69);
            TileControls.Add(Tile70);
            TileControls.Add(Tile71);
            TileControls.Add(Tile72);
            TileControls.Add(Tile73);
            TileControls.Add(Tile74);
            TileControls.Add(Tile75);
            TileControls.Add(Tile76);
        }

        void AddPlayersToListBox()
        {
            listBoxPlayers.Items.Clear();
            foreach (Player players in myListOfPlayers)
            {
                listBoxPlayers.Items.Add($"{players.FirstName} {players.LastName}");
            }
        }

        public void PurchaseTile()
        {
            if (CurrentPlayer.CurrentTile.IsPurchaseAble)
            {
                if (CurrentPlayer.CurrentTile.Purchased == false)
                {
                    CurrentPlayer.CurrentTile.SetTilePurchased(); //Sets the purchased porpert of the tile to True
                    CurrentPlayer.CurrentTile.SetOwner(CurrentPlayer); //Sets the player who owns that property
                    CurrentPlayer.TilesOwned.Add(CurrentPlayer.CurrentTile); //Add the tile to the listoftiles in the currentplayer
                    CurrentPlayer.SubtractMoney((int)Convert.ToDouble(listOfValueLabels[CurrentPlayer.CurrentTile.ID].Text));
                    MessageBox.Show($"{listOfNameLabels[CurrentPlayer.CurrentTile.ID].Text} Purchased by {CurrentPlayer.FirstName} {CurrentPlayer.LastName} for {listOfValueLabels[CurrentPlayer.CurrentTile.ID].Text}");
                    listOfNameLabels[CurrentPlayer.Position].BackColor = CurrentPlayer.myChosenColor;
                }
                else
                {
                    MessageBox.Show("Tile Already Purchased");
                }
            }

            AddToListBoxOfTilesOwned(); //Checks all the tiles owned by the current player.
            
        }

        void AddToListBoxOfAlltilesPurchased() //Add All tiles purchased to the ListBox
        {
            listBoxTilesPurchased.Items.Clear();

            foreach (Tile tilesinlist in myListOfTiles)
            {
                if (tilesinlist.Purchased)
                {
                    listBoxTilesPurchased.Items.Add($"{tilesinlist.Owner.FirstName} {tilesinlist.Owner.LastName} Purchased {tilesinlist.Name} For {tilesinlist.PurchaseValue.ToString("###,###,###")}");
                    //listBoxTilesPurchased.Items.Add(tilesinlist.Name);
                    //listBoxTilesPurchased.Items.Add($"Value: {tilesinlist.PurchaseValue.ToString("###,###,###")}");
                }
            }
        }

        void AddToListBoxOfTilesOwned() //Refresh the tiles owned by the current player
        {

            listBoxTilesOwned.Items.Clear();

            try
            {
                foreach (Tile tiles in CurrentPlayer.TilesOwned)
                {
                    listBoxTilesOwned.Items.Add($"{tiles.Name}");
                }
            }
            catch
            {
                MessageBox.Show("Error");
            }

            lblMoney.Text = CurrentPlayer.Money.ToString("###,###,###");
        }

        void CheckIfLandedOnCommunityChest()
        {
            trigger.Randomize();

            if (CurrentPlayer.CurrentTile.Name == "COMMUNITY CHEST")
            {
                if (myActionTriggers.Count > 1)
                {
                    int myValue = myRandom.Next(0, myActionTriggers.Count);
                    myActionTriggers[myValue].Invoke();
                    lblMoney.Text = CurrentPlayer.Money.ToString("###,###,###");
                    lblCommunityCard.Text = trigger.Message;
                    //MessageBox.Show($"Invoked {myActionTriggers[myValue].ToString()}");
                    myActionTriggers.RemoveAt(myValue);
                }
                else
                {
                    int myValue = myRandom.Next(0, myActionTriggers.Count);
                    myActionTriggers[myValue].Invoke();
                    lblMoney.Text = CurrentPlayer.Money.ToString("###,###,###");
                    lblCommunityCard.Text = trigger.Message;
                    //MessageBox.Show($"Invoked {myActionTriggers[myValue].ToString()}");
                    myActionTriggers.RemoveAt(myValue);
                    LoadCommunityMethods();
                }
            }
            else
            {
                lblCommunityCard.Text = "COMMUNITY CHEST";
            }

            lblCardsinChest.Text = "Cards in Chest = " + myActionTriggers.Count.ToString();
            //DrawTileColors();
        }

        void LoadCommunityMethods()
        {
            myActionTriggers.Clear();

           
            myActionTriggers.Add(() => trigger.MoveToTile(CurrentPlayer));
            myActionTriggers.Add(() => trigger.AddToPlayerCash(CurrentPlayer, 1000000));
            myActionTriggers.Add(() => trigger.RemoveFromPlayerCash(CurrentPlayer, 500000));
            myActionTriggers.Add(() => trigger.ReceiveInterest(CurrentPlayer, 10));
            myActionTriggers.Add(() => trigger.ReceiveCashFromOtherPlayers(CurrentPlayer,20000000));
            
        }

        void CheckIfLandedOnTriggerTile()
        {
            switch (CurrentPlayer.CurrentTile.Name)
            {
                case "TTCL":
                    boardTrigger.PayForTTCL(CurrentPlayer, Convert.ToInt32(btnDice.Text));
                    break;
                case "TANESCO":
                    boardTrigger.PayForTANESCO(CurrentPlayer, Convert.ToInt32(btnDice.Text));
                    break;
            }
        }

        #endregion


        //*********************************************CUSTOM FUNCTIONS*************************************************
        //*********************************************CUSTOM FUNCTIONS*************************************************
        //*********************************************CUSTOM FUNCTIONS*************************************************
        //*********************************************CUSTOM FUNCTIONS*************************************************
        //*********************************************CUSTOM FUNCTIONS*************************************************
        //*********************************************CUSTOM FUNCTIONS*************************************************
        //*********************************************CUSTOM FUNCTIONS*************************************************

    }
}
