using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    class CommunityTriggers : Form1
    {
        string message;
        public string Message
        {
            get { return message; }
        }

        public Random myRandom = new Random();
        int myValue;

        public void Randomize()
        {
            myValue = myRandom.Next(0, 22);
        }

        public List<Tile> myTriggerList = new List<Tile>();
        public List<Player> myTriggerPlayerList = new List<Player>();

        public void MoveToTile(Player player)
        {
            //Tile x = null;
            foreach (Tile myTile in myTriggerList)//iterate through all the tiles in myListOfTiles
            {
                if (myValue == myTile.ID)//if the myCurrentPostion matches the TILE ID we set the position of the current tile to that Tile
                {
                    player.CurrentTile = myTile;
                    player.SetPosition(player.CurrentTile.ID);
                    //x = myTile;
                    //player.CurrentPosition = value;
                    
                }
            }
            //currentTile = x;
            message = $"{player.FirstName} Moved to {player.CurrentTile.Name}";
        }

        public void AddToPlayerCash(Player player, int value)
        {
            player.AddMoney(value);
            message = $"You Received {value.ToString("###,###,###")}";
        }

        public void RemoveFromPlayerCash(Player player, int value)
        {
            player.SubtractMoney(value);
            message = $"You Lost {value.ToString("###,###,###")}";
        }

        public void ReceiveInterest(Player currentPlayer, int valueInPercent)
        {
            currentPlayer.AddMoney(currentPlayer.Money / valueInPercent);
            message = $"You Received {valueInPercent}% Interest";
        }

        public void ReceiveCashFromOtherPlayers(Player currentPlayer,int cashAmount)
        {
            foreach (Player player in myTriggerPlayerList)
            {
                if (player.FirstName + " " + player.LastName != currentPlayer.FirstName + " " + currentPlayer.LastName)
                {
                    player.SubtractMoney(cashAmount);
                    currentPlayer.AddMoney(cashAmount);
                }
            }
            message = $"Received {cashAmount} from each player";
        }

    }
}
