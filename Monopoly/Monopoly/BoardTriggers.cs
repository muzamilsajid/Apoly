using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    class BoardTriggers : Form1
    {
        public void PayForTTCL(Player player,int diceValue)
        {
            player.SubtractMoney(diceValue * 1000000);
        }

        public void PayForTANESCO(Player player, int diceValue)
        {
            player.SubtractMoney(diceValue * 1000000);
        }

        public void PayBusinessTAX(Player player)
        {
            int TotaltaxAmount = 0;
            foreach (Tile tile in player.TilesOwned)
            {
                if (tile.isBusiness)
                {
                    TotaltaxAmount += tile.PurchaseValue;
                }
            }

            TotaltaxAmount = TotaltaxAmount - (int)(TotaltaxAmount* 0.15);
        }
    }
}
