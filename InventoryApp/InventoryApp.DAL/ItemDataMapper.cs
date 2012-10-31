using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace InventoryApp.DAL
{
    public class ItemDataMapper : IDataMapper
    {
        public Item getItems(Item item)
        {
            Item itemRes = null;
            ItemCollection res = new ItemCollection();
            if (item != null)
            {
                DataTable dt = SrvDB.ExecuteQuery("SP_GET_ITEM_ID_ITEM @ID_ITEM = " + item.IdItem);

                dt.AsEnumerable().ToList().ForEach(row =>
                {
                    res.Add(new Item(new Articulo(

                            row.IsNull("ID_ARTICULO") ? 0 : Convert.ToInt64(row["ID_ARTICULO"])
                            , row.IsNull("ARTICULO") ? "" : row["ARTICULO"].ToString()
                            , row.IsNull("PESO") ? 0 : (float)Convert.ToDouble(row["PESO"])
                            , row.IsNull("COLOR") ? "" : row["COLOR"].ToString()
                            , new Categoria(
                                row.IsNull("ID_CATEGORIA") ? 0 : Convert.ToInt64(row["ID_CATEGORIA"])
                                ,row.IsNull("CATEGORIA") ? "" : row["CATEGORIA"].ToString())
                            )
                            , row.IsNull("ID_ITEM") ? 0 : Convert.ToInt64(row["ID_ITEM"].ToString())
                            , row.IsNull("SKU") ? "" : row["SKU"].ToString()
                            , row.IsNull("SERIE") ? "" : row["SERIE"].ToString()
                            , row.IsNull("PRECIO") ? 0 : (float)Convert.ToDouble(row["PRECIO"])
                            , row.IsNull("IMPUESTO") ? 0 : (float)Convert.ToDouble(row["IMPUESTO"])
                        ));
                }
                );

                if (res.Count >= 0)
                {
                    itemRes = res[0];
                }
            }

            return itemRes;
        }

        public void insertItems(Item item)
        {
            if (item != null)
            {
                string sqlStmt = "";

                sqlStmt += "EXEC SP_INSERT_ITEM";
                sqlStmt += " @SKU = '" + item.Sku + "'";
                sqlStmt += " ,@SERIE = '" + item.SerialNbr + "'";
                sqlStmt += " ,@PRECIO = " + item.Precio;
                sqlStmt += " ,@IMPUESTO = " + item.Impuesto;
                sqlStmt += " ,@ID_ARTICULO = " + item.UnidArticulo;

                SrvDB.ExecuteNonQuery(sqlStmt); 
            } 
        }

        public ItemCollection getItems()
        {
            ItemCollection res = new ItemCollection();
            DataTable dt = SrvDB.ExecuteQuery("SP_GET_ITEMS");

            dt.AsEnumerable().ToList().ForEach(row =>
            {
                res.Add(new Item(new Articulo(

                        row.IsNull("ID_ARTICULO") ? 0 : Convert.ToInt64(row["ID_ARTICULO"])
                        , row.IsNull("ARTICULO") ? "" : row["ARTICULO"].ToString()
                        , row.IsNull("PESO") ? 0 : (float)Convert.ToDouble(row["PESO"]) 
                        , row.IsNull("COLOR") ? "" : row["COLOR"].ToString()
                        ,new Categoria(row.IsNull("ID_CATEGORIA") ? 0 : Convert.ToInt64(row["ID_CATEGORIA"])
                        , row.IsNull("CATEGORIA") ? "" : row["CATEGORIA"].ToString()))
                        , row.IsNull("ID_ITEM") ? 0 : Convert.ToInt64(row["ID_ITEM"].ToString())
                        , row.IsNull("SKU") ? "" : row["SKU"].ToString()
                        , row.IsNull("SERIE") ? "" : row["SERIE"].ToString()
                        , row.IsNull("PRECIO") ? 0 : (float)Convert.ToDouble(row["PRECIO"])
                        , row.IsNull("IMPUESTO") ? 0 : (float)Convert.ToDouble(row["IMPUESTO"])
                    ));
                }
            );

            return res;
        }

        public ItemCollection getItems(Articulo articulo)
        {
             ItemCollection res = new ItemCollection();
             if (articulo != null)
             {

                 DataTable dt = SrvDB.ExecuteQuery("SP_GET_ITEM_ID_ARTICULO @ID_ARTICULO = " + articulo.UnidArticulo);

                 dt.AsEnumerable().ToList().ForEach(row =>
                 {
                     res.Add(new Item(new Articulo(

                             row.IsNull("ID_ARTICULO") ? 0 : Convert.ToInt64(row["ID_ARTICULO"])
                             , row.IsNull("ARTICULO") ? "" : row["ARTICULO"].ToString()
                             , row.IsNull("PESO") ? 0 : (float)Convert.ToDouble(row["PESO"])
                             , row.IsNull("COLOR") ? "" : row["COLOR"].ToString()
                             , new Categoria(
                                 row.IsNull("ID_CATEGORIA") ? 0 : Convert.ToInt64(row["ID_CATEGORIA"])
                                ,row.IsNull("CATEGORIA") ? "" : row["CATEGORIA"].ToString())
                             )
                             , row.IsNull("ID_ITEM") ? 0 : Convert.ToInt64(row["ID_ITEM"].ToString())
                             , row.IsNull("SKU") ? "" : row["SKU"].ToString()
                             , row.IsNull("SERIE") ? "" : row["SERIE"].ToString()
                             , row.IsNull("PRECIO") ? 0 : (float)Convert.ToDouble(row["PRECIO"])
                             , row.IsNull("IMPUESTO") ? 0 : (float)Convert.ToDouble(row["IMPUESTO"])
                         ));
                 }); 
             }
             return res;
        }

        public void deleteItems(Item item)
        {
            //SrvDB.ExecuteNonQuery("EXEC SP_DELETE_ITEM @ = " + item);
        }

        public void updateItems(Item item)
        {
            if (item != null)
            {
                string sqlStmt = "";
                sqlStmt += "EXEC SP_UPDATE_ITEM";
                sqlStmt += " @SKU = '" + item.Sku + "'";
                sqlStmt += " ,@SERIE = '" + item.SerialNbr + "'";
                sqlStmt += " ,@PRECIO = " + item.Precio;
                sqlStmt += " ,@IMPUESTO = " + item.Impuesto;
                sqlStmt += " ,@ID_ARTICULO = " + item.UnidArticulo;
                sqlStmt += " ,@ID_ITEM = " + item.IdItem;

                SrvDB.ExecuteNonQuery(sqlStmt); 
            }
        }

        public object getElements()
        {
            ItemCollection items = this.getItems();
            return items;
        }

        public object udpateElement(object element)
        {
            throw new NotImplementedException();
        }

        public object insertElement(object element)
        {
            throw new NotImplementedException();
        }


        public object getElement(object element)
        {
            throw new NotImplementedException();
        }

        public object deleteElement(object element)
        {
            throw new NotImplementedException();
        }
    }
}


