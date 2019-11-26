using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    class CommunityChest
    {
        private int cardnumber;

        public int CardNumber
        {
            get { return cardnumber; }
        }

        private string carddetails;
        public string CradDetails
        {
            get { return carddetails; }
        }

        private string cardtrigger;

        private CommunityChest()
        {
            cardnumber++;
        }
    }
}
