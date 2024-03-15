using AutomationTesting_CorConnect.DataObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.CreditHistory
{
    internal class CreditHistoryDAL : BaseDataAccessLayer
    {

        internal List<CreditHistoryObjects> GetTableData(string corCentricCode, out decimal creditLimit, out string currency)
        {
            GetEntityID(corCentricCode, out string entityId, out creditLimit);

            currency = GetCurrency(entityId);

            var List = new List<CreditHistoryObjects>();
            string query = @"SELECT AuditPKId as entityDetailId,ActionDate as UpdateDate,CONVERT(DECIMAL(18,2),AuditNewValue) AS CreditLimit
                            , CASE WHEN IU.userId IS NULL THEN U.userName ELSE IU.userName END AS UserName
                            , CASE WHEN IU.userId IS NULL THEN U.firstName ELSE IU.firstName END AS FirstName
                            , CASE WHEN IU.userId IS NULL THEN U.lastName ELSE IU.lastName END AS LastName
                            , CASE WHEN IU.userid IS NULL THEN 'N' ELSE 'Y' END AS IsImpersonated
                            ,CASE WHEN IU.userid IS NULL THEN '' ELSE U.username END AS ImpersonatedUserName
                            FROM AuditTrail_tb at WITH(NOLOCK)
                            INNER JOIN user_tb U WITH(NOLOCK) on at.UserID = U.userId
                            LEFT JOIN user_tb IU WITH(NOLOCK) on IU.userId = at.ImpersonatingUser
                            WHERE AuditTable = 'entityDetails_tb' AND AuditColumn = 'creditLimit' AND AuditPKId = @entityID
                            ORDER BY ActionDate DESC; ";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@entityID", entityId),
            };

            using (var reader = ExecuteReader(query, sp, false))
            {
                while (reader.Read())
                {
                    Console.WriteLine(reader.GetDecimal(2));
                    List.Add(new CreditHistoryObjects()
                    {
                        updateDate = reader.GetDateTime(1).ToString("M/d/yyyy HH:mm:ss"),
                        creditLimit = reader.GetDecimal(2),
                        UserName = reader.GetString(3),
                        firstName = reader.GetString(4),
                        lastName = reader.GetString(5),
                        isImpersonated = reader.GetString(6),
                        impersonatedUserName = string.IsNullOrEmpty(reader.GetString(7)) ? " " : reader.GetString(7)
                    });

                }
            }

            return List;
        }

        internal string GetCorCentricCode()
        {
            string query = "SELECT top 1 e.corcentriccode FROM AuditTrail_tb at WITH (NOLOCK) INNER JOIN user_tb U WITH(NOLOCK) on at.UserID = U.userId inner join entitydetails_tb e on e.entitydetailid = at.AuditPKId and e.entitytypeid = 3 and e.locationtypeid = 25 LEFT JOIN user_tb IU WITH(NOLOCK) on IU.userId = at.ImpersonatingUser WHERE AuditTable = 'entityDetails_tb' AND AuditColumn = 'creditLimit' ORDER BY ActionDate DESC";

            using (var reader = ExecuteQuery(query, false))
            {
                if (reader.Read())
                {
                    return reader.GetString(0);
                }
            }

            return string.Empty;

        }

        internal string GetCurrency(string entityId)
        {
            string query = "select  dbo.udf_getMultipleReltionshipCurrecny(0, @entityID, 0)";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@entityID", entityId),
            };

            using (var reader = ExecuteReader(query, sp, false))
            {
                if (reader.Read())
                {
                    return reader.GetString(0);
                }
            }

            return string.Empty;
        }

        private void GetEntityID(string corCentricCode, out string entityId, out decimal creditLimit)
        {

            string query = "select * from entitydetails_tb where corcentriccode=@corCentricCode";
            entityId = string.Empty;
            creditLimit = 0;

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@corCentricCode", corCentricCode),
            };

            using (var reader = ExecuteReader(query, sp, false))
            {
                if (reader.Read())
                {
                    entityId = reader["entityDetailId"].ToString();
                    creditLimit = Convert.ToDecimal(reader["creditLimit"]);
                }
            }
        }
    }
}
