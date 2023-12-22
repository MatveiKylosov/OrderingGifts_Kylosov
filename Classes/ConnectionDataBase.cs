using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OrderingGifts_Kylosov.Classes
{
    public class ConnectionDataBase
    {
        string PathToDataBase = "";
        OleDbConnection connect;

        public string err = "";

        public ConnectionDataBase(string PathToDataBase)
        {
            this.PathToDataBase = PathToDataBase;

            try
            {
                connect = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + PathToDataBase);
                connect.Open();
            }
            catch (Exception ex)
            {
                err = ex.Message;
            }
        }

        OleDbDataReader QueryAccess(string query)
        {
            try
            {
                OleDbCommand cmd = new OleDbCommand(query, connect);
                OleDbDataReader reader = cmd.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                err = (ex.Message.ToString());
                return null;
            }
        }
        
        public List<string> LoadCategory()
        {
            List<string> category = new List<string>();
            OleDbDataReader itemQuery = QueryAccess("SELECT * FROM [Category] ORDER BY [Code]");

            while (itemQuery.Read())
                category.Add(Convert.ToString(itemQuery.GetValue(1)));
            
            return category;
        }

        public bool CUDGift(string query)
        {
            OleDbDataReader itemQuery = QueryAccess(query);
            if (itemQuery != null) itemQuery.Close();
            return itemQuery != null;
        }

        public List<Classes.GiftInfo> LoadGift(string query = "SELECT * FROM [Gift] ORDER BY [Code]")
        {
            List <Classes.GiftInfo> gifts = new List<Classes.GiftInfo>();
            OleDbDataReader itemQuery = QueryAccess(query);

            while(itemQuery.Read())
            {
                gifts.Add(new GiftInfo(
                    Convert.ToInt32(itemQuery.GetValue(0)),
                    Convert.ToString(itemQuery.GetValue(1)),
                    Convert.ToString(itemQuery.GetValue(2)),
                    Convert.ToString(itemQuery.GetValue(3)),
                    Convert.ToString(itemQuery.GetValue(4)),
                    Convert.ToString(itemQuery.GetValue(5)),
                    Convert.ToString(itemQuery.GetValue(6))
                    ));
            }
            if (itemQuery != null) itemQuery.Close();
            return gifts;
        }
    }
}
