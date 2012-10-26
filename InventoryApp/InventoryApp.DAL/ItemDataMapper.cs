using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace InventoryApp.DAL
{
    public class ItemDataMapper
    {
        public ItemCollection getItems(Item item)
        {
            ItemCollection res = new ItemCollection();
            DataTable dt = SrvDB.ExecuteQuery("SP_SELECT_ITEM");

            return res;
            return null;
        }

        public void insertItems()
        {
            string sqlStmt = "";

            sqlStmt += "EXEC SP_INSERT_ITEM";
            sqlStmt += "";
            sqlStmt += "";
            sqlStmt += "";
            sqlStmt += "";
            sqlStmt += "";
            sqlStmt += "";

            SrvDB.ExecuteNonQuery(sqlStmt);
        }

        public ItemCollection getItems()
        {
            ItemCollection res = new ItemCollection();
            DataTable dt = SrvDB.ExecuteQuery("SP_SELECT_ITEM");

            return res;
        }

        public ItemCollection getItems(Articulo articulo)
        {
             ItemCollection res = new ItemCollection();
             DataTable dt = SrvDB.ExecuteQuery("SP_SELECT_ITEM ");

             return res;
        }

        public void deleteItems(Item item)
        {
            SrvDB.ExecuteNonQuery("EXEC SP_DELETE_ITEM @ = " + item);
        }

        public void updateItems(Item item)
        {
            SrvDB.ExecuteNonQuery("EXEC SP_UPDATE_ITEM @ = " + item.UnidArticulo);
        }
    }
}
