using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.InvoiceEntry
{
    internal class InvoiceEntryDAL : BaseDataAccessLayer
    {
        internal void GetInvoice(out DateTime createDate, out string transactionNumber)
        {
            var query = "Select top 1 createDate, transactionNumber from transaction_tb where isActive = 1 and isHistorical = 0 and validationstatus = 8 and requestTypeCode = 'S' order by 1 desc";
            createDate = DateTime.MinValue;
            transactionNumber = string.Empty;

            try
            {
                using (var reader = ExecuteReader(query, false))
                {
                    if (reader.Read())
                    {
                        createDate = reader.GetDateTime(0);
                        transactionNumber = reader.GetString(1);
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                transactionNumber = null;
            }

        }

        internal void GetData(out string FromDate, out string ToDate)
        {
            FromDate = string.Empty;
            ToDate = string.Empty;
            string query = null;

            try
            {
                string userType = TestContext.CurrentContext.Test.Properties["Type"]?.First().ToString().ToUpper();
                if (userType == "ADMIN")
                {
                    query = @"SELECT    top 1 Convert (Date , transactiondate) as FromDate ,Convert (Date , transactiondate) as ToDate  FROM [transaction_tb] tr with (NOLOCK) where  isActive = 1 and validationStatus = 8 and requestTypeCode = 'S' ORDER BY transactiondate DESC";

                    using (var reader = ExecuteReader(query, false))
                    {
                        if (reader.Read())
                        {
                            FromDate = CommonUtils.ConvertDate(reader.GetDateTime(0));
                            ToDate = CommonUtils.ConvertDate(reader.GetDateTime(1));
                        }
                    }
                }
                else if (userType == "DEALER")
                {
                    query = @"DECLARE @@Userid AS INT; DECLARE @@DealerAccessLocations TABLE (entityDetailId INT primary key); select @@Userid = userid from [user_tb] where username='ARGENTINAQADEALER'; WITH RootNumber AS(SELECT entityDetailId, parentEntityDetailId, entityDetailId AS RootID FROM [entityDetails_tb] WITH(NOLOCK) WHERE entityDetailId iN( SELECT DISTINCT UR.entityId FROM [userRelationships_tb] UR WITH(NOLOCK) INNER JOIN [user_tb] U WITH(NOLOCK) ON UR.userId = U.userId WHERE U.userId = @@UserID AND UR.IsActive = 1 AND UR.hasHierarchyAccess = 1) UNION ALL SELECT C.entityDetailId, C.parentEntityDetailId, P.RootID FROM RootNumber AS P INNER JOIN [entityDetails_tb] AS C WITH(NOLOCK) ON P.entityDetailId = C.parentEntityDetailId) insert into @@DealerAccessLocations SELECT entityDetailId FROM RootNumber where parentEntityDetailId <> entityDetailId and parentEntityDetailId <> 0 UNION SELECT UR2.entityId As entityDetailId FROM [userRelationships_tb] UR2 WITH(NOLOCK) WHERE UR2.userId = @@UserID and IsActive = 1 AND UR2.entityId IS NOT NULL; SELECT top 1 Convert(Date, transactiondate) as FromDate ,Convert(Date, transactiondate) as ToDate FROM [transaction_tb] tr with (NOLOCK) INNER JOIN @@DealerAccessLocations AE ON AE.entityDetailId = tr.senderEntityDetailId where isActive = 1 and validationStatus = 8 and requestTypeCode = 'S' order by transactiondate desc;";
                    List<SqlParameter> sp = new()
                    {
                        new SqlParameter("@UserName", TestContext.CurrentContext.Test.Properties["UserName"]?.First().ToString()),
                    };

                    using (var reader = ExecuteReader(query, sp, false))
                    {
                        if (reader.Read())
                        {
                            FromDate = CommonUtils.ConvertDate(reader.GetDateTime(0));
                            ToDate = CommonUtils.ConvertDate(reader.GetDateTime(1));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                FromDate = null;
                ToDate = null;
            }
        }

        internal int GetInvoiceSectionCount(string transactionNumber)
        {
            var query = "Select Count(*)  as NumberOfSections from transaction_tb trn INNER JOIN invoice_Tb IV  ON IV.transactionId = trn.transactionId INNER JOIN invoiceSection_tb IVS ON IV.InvoiceId=IVS.InvoiceID where IV.transactionNumber=@transactionNumber and trn.validationStatus = 1";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@transactionNumber", transactionNumber),

            };

            try
            {
                using (var reader = ExecuteReader(query,sp, false))
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }
                }

            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
            }

            return -1;
        }

        internal int GetDiscrepantInvoiceSectionCount(string transactionNumber)
        {
            var query = "Select Count(*) from transaction_tb IV INNER JOIN transactionSection_tb IVS ON IV.transactionId=IVS.transactionId where requestTypeCode  IN ('S') AND IV.transactionNumber=@transactionNumber";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@transactionNumber", transactionNumber),

            };

            try
            {
                using (var reader = ExecuteReader(query, sp, false))
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }
                }

            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
            }

            return -1;
        }

        internal bool IsInvoiceEligiblForPaymentPortal(string invoiceNumber)
        {
            var query = "Select isPaymentPortal from invoice_tb where transactionNumber = @invNumber";
             List<SqlParameter> sp = new()
            {
                new SqlParameter("@invNumber", invoiceNumber),

            };

            using (var reader = ExecuteReader(query, sp, false))
            {
                while (reader.Read())
                {
                    return reader.GetBoolean(0);
                }

            }
            return false;
            
        }
        }
    }


