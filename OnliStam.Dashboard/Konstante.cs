using OnliStam.Pomocnici;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnliStam.Dashboard
{
    public static class Konstante
    {

        public struct SqlProcedure
        {
            public static readonly SqlUpit DAJ_SVE_TABELE = new SqlUpit(
                "DajSveTabele",
@"SELECT table_name
FROM information_schema.tables
WHERE table_schema=@schemaName;",
                new List<string> { "schemaName" });

            public static readonly SqlUpit DAJ_KOLONE = new SqlUpit(
                "DajKolone",
@"SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE, COLUMN_KEY, EXTRA, COLUMN_DEFAULT
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = @tableName
    AND table_schema=@schemaName;",
                new List<string> { "tableName", "schemaName" });
        }

        #region ***** CONSTANTS ****



        #endregion

        #region ****** FIELDS ******



        #endregion

        #region **** PROPERTIES ****



        #endregion

        #region *** CONSTRUCTORS ***



        #endregion

        #region ****** METHODS *****



        #endregion
    }
}
