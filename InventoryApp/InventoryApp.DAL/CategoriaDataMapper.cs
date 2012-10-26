using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace InventoryApp.DAL
{
    public class CategoriaDataMapper
    {
        public CategoriaCollection GetCategorias()
        {
            string sqlStmt = "exec SP_GET_CATEGORIA";
            CategoriaCollection categorias=new CategoriaCollection();

            try
            {
                DataTable dt = SrvDB.ExecuteQuery(sqlStmt);

                dt.AsEnumerable().ToList().ForEach(row =>
                {
                    categorias.Add(new Categoria(
                            row.IsNull("ID_CATEGORIA") ? 0 : Convert.ToInt64(row["ID_CATEGORIA"])
                            , row.IsNull("CATEGORIA") ? "" : row["CATEGORIA"].ToString()
                        ));
                }
                );
            }
            catch (Exception ex)
            {
                throw ex;
            }
            

            return categorias;
        }

        //public void InsertCategoria()
        //{

        //}

        //public void EliminarCategoria()
        //{

        //}
    }//endclass
}
