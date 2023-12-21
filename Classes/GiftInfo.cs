using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingGifts_Kylosov.Classes
{
    public class GiftInfo
    {
        public int id;
        public string FIO;
        public string TextMsg;
        public string Address;
        public string DateAndTime;
        public string Mail;
        public string Category;

        public GiftInfo(int id, string FIO, string TextMsg, string Address, string DateAndTime, string Mail)
        {
            this.id = id;
            this.FIO = FIO;
            this.TextMsg = TextMsg;
            this.Address = Address;
            this.DateAndTime = DateAndTime;
            this.Mail = Mail;
        }
    }

}
