using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.FleetCreditLimit
{
    internal class FleetCreditLimitDAL : BaseDataAccessLayer
    {
        internal void GetData(out string location)
        {
            location = string.Empty;
            string query = null;
            
            try
            {
                string userType;

                if (TestContext.CurrentContext.Test.Properties["Type"].Count() > 0)
                {
                    userType = TestContext.CurrentContext.Test.Properties["Type"]?.First().ToString().ToUpper();
                }
                else
                {
                    userType = ScenarioContext.Current["ActionResult"].ToString().ToUpper();
                }
                if (userType == "ADMIN")
                {
                    query = @"With invoices as (select receiverBillToEntityDetailId,transactionAmount,currencyCode,lookUpId, arPostedToAccounting from [invoice_tb] where isActive =1), OpenBalances as (SELECT I.receiverBillToEntityDetailId, SUM(curTransactionAmt)  OpenInv, I.currencyCode from invoices I join [gpOpenInvoices_tb] G on G.lookupId =I.lookUpId where(G.lookupId is not null and  G.lookupId <> '') AND G.isActive = 1 and G.isARFlag = 1 and I.arPostedToAccounting = 1 group by I.receiverBillToEntityDetailId,I.currencyCode UNION ALL select I.receiverBillToEntityDetailId, SUM(I.transactionAmount)  OpenInv, I.currencyCode from  invoices I where I.arPostedToAccounting = 0 group by I.receiverBillToEntityDetailId,I.currencyCode),OpenBalancesAndAuthAmt as (select receiverBillToEntityDetailId, 0 OpenAuthorizationAmount,SUM(OpenInv) OpenInvAmt,currencyCode from OpenBalances group by receiverBillToEntityDetailId, currencyCode union all SELECT receiverBillToEntityDetailId, sum(authorizationAmount)OpenAuthorizationAmount, 0 OpenInvs, currencyCode FROM [transaction_tb] T WHERE T.isActive =1 and requestTypeCode in('T', 'A') and T.validationStatus in (1,2,7) group by receiverBillToEntityDetailId, currencyCode), Fleets as (SELECT T.receiverBillToEntityDetailId,SUM(isnull(T.OpenAuthorizationAmount,0)) OpenAuthorizationAmount,SUM(isnull(T.OpenInvAmt, 0))  OpenInvAmt,CurrencyCode FROM OpenBalancesAndAuthAmt T GROUP BY T.receiverBillToEntityDetailId,currencyCode) SELECT top 1 E.corcentriccode FROM [entityDetails_tb] E LEFT OUTER JOIN Fleets T on T.receiverBillToEntityDetailId = E.entityDetailId WHERE E.isActive =1  and entitytypeid=3 and locationtypeid=25 order by E.corcentriccode desc;";
                    using (var reader = ExecuteReader(query, false))
                    {
                        if (reader.Read())
                        {
                            location = reader.GetString(0);

                        }
                    }
                }

                else if (userType == "FLEET")
                {
                    query = @"IF object_id('tempdb..#entityList') IS NOT NULL DROP TABLE #entityList; CREATE TABLE #entityList(entityDetailId int,corcentricCode nvarchar(30),accountingCode varchar(30),communityCode varchar(30),legalName varchar(100), creditLimit decimal(18, 2), availablecreditLimit decimal(18, 2)) IF object_id('tempdb..#CTEOpenBalances') IS NOT NULL DROP TABLE #CTEOpenBalances CREATE TABLE #CTEOpenBalances(receiverBillToEntityDetailId int,UnAppliedPymt decimal(18,4),OpenInv decimal(18,4),ARCurrencyCode char(3)) DECLARE @@DatabaseName sysname, @@SQL NVARCHAR(MAX); SELECT @@DatabaseName = DataSourceName FROM WEBCORE_DATA_SOURCE WHERE ConStringName = 'developmentString' SET @@SQL = N'USE ' + QUOTENAME(@@DatabaseName); SET @@SQL = @@SQL + ';DECLARE @@Userid as int declare @@masterStatementId as int DECLARE @@FleetAccessLocations table (entityDetailId INT primary key) select @@masterStatementId = lookupid from lookup_tb where parentlookupcode = 119 and lookupcode = 3 select @@Userid = userid from user_tb where username = @Username; WITH RootNumber AS(SELECT entityDetailId, parentEntityDetailId, entityDetailId AS RootID FROM entityDetails_tb WITH(NOLOCK) WHERE entityDetailId iN(SELECT DISTINCT userRelationships_tb.entityId FROM userRelationships_tb WITH(NOLOCK) INNER JOIN user_tb WITH(NOLOCK) ON userRelationships_tb.userId = user_tb.userId WHERE user_tb.userId = @@UserID AND userRelationships_tb.IsActive = 1 AND userRelationships_tb.hasHierarchyAccess = 1) UNION ALL SELECT C.entityDetailId, C.parentEntityDetailId, P.RootID FROM RootNumber AS P INNER JOIN entityDetails_tb AS C WITH(NOLOCK) ON P.entityDetailId = C.parentEntityDetailId) insert into @@FleetAccessLocations SELECT entityDetailId FROM RootNumber where parentEntityDetailId<> entityDetailId and parentEntityDetailId <> 0 UNION SELECT userRelationships_tb.entityId As entityDetailId FROM userRelationships_tb WITH(NOLOCK)WHERE userRelationships_tb.userId = @@UserID and IsActive = 1 AND userRelationships_tb.entityId IS NOT NULL; Select E.entityDetailId,E.corcentricCode,E.accountingCode,E.communityCode,E.legalName,E.creditLimit,E.availablecreditLimit from entityDetails_tb E with(nolock) INNER JOIN @@FleetAccessLocations AE ON AE.entityDetailId = E.entityDetailId WHERE E.isActive = 1 AND enrollmentStatusId = 13 and entitytypeid = 3 and locationtypeid = 25; ' insert into #entityList(entityDetailId,corcentricCode,accountingCode,communityCode,legalName,creditLimit,availablecreditLimit) EXECUTE(@@SQL); SET @@SQL = N'USE ' + QUOTENAME(@@DatabaseName); SET @@SQL = @@SQL + ' declare @@masterStatementId as int select @@masterStatementId = lookupid from lookup_tb where parentlookupcode = 119 and lookupcode = 3; With OpenBalances as(SELECT E.entitydetailid receiverBillToEntityDetailId, SUM(ISNULL(curTransactionAmt, 0)) UnAppliedPymt, 0 OpenInv, G.CurrencyCode ARCurrencyCode FROM gpOpenInvoices_tb G WITH(NOLOCK) INNER JOIN #entityList E ON E.accountingCode = G.accountingCode where G.isActive = 1 and G.isARFlag = 1 and ISNULL(G.lookupId, '''') = '''' AND G.curTransactionAmt <> 0 group by E.entityDetailId, G.CurrencyCode UNION ALL SELECT E.entitydetailid as receiverBillToEntityDetailId, 0 UnAppliedPymt, SUM(ISNULL(I.ARTransactionAmount, 0)) OpenInv, I.ARCurrencyCode from invoice_tb I with(nolock) INNER JOIN #entityList E on E.entityDetailId = I.receiverBillToEntityDetailId INNER JOIN statementDetail_tb STD WITH(NOLOCK) ON STD.invoiceId = I.invoiceId INNER JOIN statement_tb ST WITH(NOLOCK) ON ST.statementId = STD.statementId AND ST.statementType = ''AR'' WHERE ISNULL(I.arpaidinFull, 0) = 0 AND I.systemType = 0 AND I.arpostedtoAccounting = 0 AND I.isActive = 1 AND ARstatementTypeId <> @@masterStatementId GROUP BY E.entitydetailid, I.ARCurrencyCode UNION ALL SELECT E.entitydetailid as receiverBillToEntityDetailId, 0 UnAppliedPymt, SUM(ISNULL(I.statementAmount, 0)) OpenInv, I.statementCurrency from statement_tb I with(nolock) INNER JOIN #entityList E on E.entityDetailId = I.entityDetailId WHERE ISNULL(I.paidInFull, 0) = 0 AND ISNULL(I.postedtoAccounting, 0) = 0 AND I.isActive = 1 AND i.statementTypeId = @@masterStatementId AND i.statementStatus = 1 GROUP BY E.entityDetailId, I.statementCurrency UNION ALL SELECT E.entitydetailid as receiverBillToEntityDetailId, 0 UnAppliedPymt, SUM(ISNULL(G.curTransactionAmt, 0)) OpenInv, G.CurrencyCode from gpOpenInvoices_tb G with(nolock) INNER JOIN #entityList E on E.accountingCode = G.accountingCode INNER JOIN invoice_tb I WITH(NOLOCK) ON G.lookupId = I.lookUpId AND I.systemType = 0 INNER JOIN statementDetail_tb STD WITH(NOLOCK) ON STD.invoiceId = I.invoiceId INNER JOIN statement_tb ST WITH(NOLOCK) ON ST.statementId = STD.statementId AND ST.statementType = ''AR'' WHERE isARFlag = 1 AND G.isActive = 1 AND G.curTransactionAmt <> 0 and ISNULL(G.lookupId, '''') <> '''' AND I.ARstatementTypeId <> @@masterStatementId     group by E.entityDetailId, G.CurrencyCode     UNION ALL     SELECT E.entitydetailid as receiverBillToEntityDetailId     , 0 UnAppliedPymt     , SUM(ISNULL(G.curTransactionAmt, 0)) OpenInv     , G.CurrencyCode     from gpOpenInvoices_tb G with(nolock)     INNER JOIN #entityList E on E.accountingCode = G.accountingCode INNER JOIN statement_tb I WITH(NOLOCK) ON G.lookupId = I.statementNumber      WHERE isARFlag = 1 AND G.isActive = 1 AND G.curTransactionAmt <> 0 AND I.statementTypeId = @@masterStatementId AND G.invlookuptype = ''MI'' and ISNULL(G.lookupId, '''') <> ''''      group by E.entityDetailId, G.CurrencyCode) select* from OpenBalances' insert into #CTEOpenBalances(receiverBillToEntityDetailId,UnAppliedPymt,OpenInv,ARCurrencyCode) EXECUTE(@@SQL);              SET @@SQL = N'USE ' + QUOTENAME(@@DatabaseName);             SET @@SQL = @@SQL + ';declare @@masterStatementId as int select @@masterStatementId = lookupid from lookup_tb where parentlookupcode = 119 and lookupcode = 3     ; with OpenBalancesAndAuthAmt as(      select receiverBillToEntityDetailId      , SUM(ISNULL(UnAppliedPymt, 0)) UnAppliedPymtFromGP ,0 OpenAuthorizationAmount ,SUM(ISNULL(OpenInv, 0)) OpenInvAmt ,ARCurrencyCode from #CTEOpenBalances group by receiverBillToEntityDetailId,ARCurrencyCode union all SELECT receiverBillToEntityDetailId ,0 UnAppliedPymtFromGP ,sum(ISNULL(T.ARAuthorizationAmount, 0)) OpenAuthorizationAmount ,0 OpenInvs ,T.ARCurrencyCode FROM transaction_tb T with(nolock) INNER JOIN #entityList E on E.entityDetailId = T.receiverBillToEntityDetailId WHERE T.isActive = 1 AND(T.requestTypeCode = ''T'' or T.requestTypeCode = ''A'') AND(T.validationStatus = 1 or T.validationStatus = 2) AND expirationDate >= CAST(GETDATE() as Date) GROUP BY T.receiverBillToEntityDetailId,T.ARCurrencyCode), Fleets as (SELECT T.receiverBillToEntityDetailId ,SUM(isnull(T.OpenAuthorizationAmount, 0)) OpenAuthorizationAmount ,SUM(isnull(T.OpenInvAmt, 0)) OpenInvAmt ,SUM(isnull(T.UnAppliedPymtFromGP, 0)) UnAppliedPymtFromGP ,ARCurrencyCode FROM OpenBalancesAndAuthAmt T GROUP BY T.receiverBillToEntityDetailId ,ARCurrencyCode) SELECT E.corcentricCode FROM #entityList E LEFT OUTER JOIN Fleets T on T.receiverBillToEntityDetailId = E.entityDetailId' EXECUTE(@@SQL);";

                    List<SqlParameter> sp = new()
                    {
                        new SqlParameter("@UserName", TestContext.CurrentContext.Test.Properties["UserName"]?.First().ToString()),
                    };

                    using (var reader = ExecuteReader(query, sp, true))
                    {
                        if (reader.Read())
                        {
                            location = reader.GetString(0);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                location = null;

            }
        }
    }
}
