using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace InventoryApp.DAL
{
    class ArticuloDataMapper
    {

        public ArticuloCollection GetArticulos(Categoria categoria)
        {
            string sqlStmt = "exec SP_GET_ARTS_CAT " + categoria.UnidCategoria;
            ArticuloCollection articulos = new ArticuloCollection();

            try
            {
                DataTable dt = SrvDB.ExecuteQuery(sqlStmt);

                dt.AsEnumerable().ToList().ForEach(row =>
                {
                    articulos.Add(new Articulo(
                            row.IsNull("ID_ARTICULO") ? 0 : Convert.ToInt64(row["ID_ARTICULO"])
                            , row.IsNull("ARTICULO") ? "" : row["ARTICULO"].ToString()
                            , row.IsNull("PESO") ? 0f : (float.Parse(row["PESO"].ToString()))
                            , row.IsNull("COLOR") ? "" : row["COLOR"].ToString()
                            , new Categoria(
                              row.IsNull("ID_CATEGORIA") ? 0 : Convert.ToInt64(row["ID_CATEGORIA"])
                            , row.IsNull("CATEGORIA") ? "" : row["CATEGORIA"].ToString())

                        ));
                }
                );
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return articulos;
        }



    }
}
