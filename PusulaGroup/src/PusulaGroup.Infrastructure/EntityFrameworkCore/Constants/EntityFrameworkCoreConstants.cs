namespace PusulaGroup.Infrastructure.EntityFrameworkCore.Constants
{
    public static class EntityFrameworkCoreConstants
    {
        #region DbSchema
        private static string DbSchemaDbo => "dbo";


        public static string GetDefaultDbSchema() => $"{DbSchemaDbo}";

        #endregion


        #region ColumnTypes

        private static string ColumnTypeNVarChar => "NVARCHAR";
        private static string ColumnTypeDecimal => "DECIMAL";
        private static string ColumnTypeInt => "INT";

        public static string GetColumnTypeNVarChar(int length) => $"{ColumnTypeNVarChar}({length})";
        public static string GetColumnTypeDecimal(int precision, int scale) => $"{ColumnTypeDecimal}({precision},{scale})";
        public static string GetColumnTypeInt() => $"{ColumnTypeInt}";

        #endregion
    }
}
