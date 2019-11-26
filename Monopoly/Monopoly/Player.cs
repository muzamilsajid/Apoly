using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Monopoly
{
    public class Player
    {

        string firstname;
        public string FirstName
        {
            get { return firstname; }
        }

        string lastname;
        public string LastName
        {
            get { return lastname; }
        }

        int money = 0;
        public int Money
        {
            get { return money; }
        }

        bool turn = false;
        public bool Turn
        {
            get { return turn; }
        }

        int position;

        public int Position
        {
            get { return position; }
        }

        public int CurrentPosition;

        public Tile CurrentTile;

        Color mychosencolor;
        public Color myChosenColor
        {
            get { return mychosencolor; }
        }

        public List<Tile> TilesOwned = new List<Tile>();


        public void CreatePlayer(string fName, string lName)
        {
            firstname = fName;
            lastname = lName;
        }

        public void AddMoney(int value)
        {
            money += value;
        }

        public void SubtractMoney(int value)
        {
            money -= value;
        }

        public void SetPosition(int pos)
        {
            position = pos;
        }

        public void SetColor(int R,int G,int B)
        {
            mychosencolor = Color.FromArgb(R, G, B);
        }
    }
}
