using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer;
using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.CreateNewEntity;
using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.DealerLocations;
using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.Resources;
using NUnit.Framework;
using OpenQA.Selenium;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using TechTalk.SpecFlow;

namespace AutomationTesting_CorConnect.Utils
{
    internal static class CommonUtils
    {
        //private const string DateFormat = "M/d/yyyy";
        private static readonly BaseDataAccessLayer baseDataAccessLayer = BaseDataAccessLayer.GetInstance();

        private static Regex rgx = new Regex("[^a-zA-Z0-9]");
        internal static string GetCurrentDate()
        {
            return DateTime.Now.ToString(GetClientDateFormat());
        }

        ///<summary>
        ///Generate Random string
        ///</summary>
        internal static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[new Random().Next(s.Length)]).ToArray());
        }


        ///<summary>
        ///Generate Random string based on Alphabets only
        ///</summary>
        internal static string RandomAlphabets(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[new Random().Next(s.Length)]).ToArray());
        }

        internal static string GetCountry()
        {
            return baseDataAccessLayer.GetCountry();
        }

        internal static int GetNumberOfWorkingDays(DateTime start, DateTime stop)
        {
            int days = 0;
            while (start <= stop)
            {
                if (start.DayOfWeek != DayOfWeek.Saturday && start.DayOfWeek != DayOfWeek.Sunday)
                {
                    ++days;
                }
                start = start.AddDays(1);
            }
            return days - 1;
        }

        internal static List<string> RemoveTimeZone(List<string> dates)
        {
            var list = new List<string>();


            foreach (var date in dates)
            {
                list.Add(date.ToString().Replace(" EST", ""));
            }

            return list;
        }

        internal static string RemoveTimeZone(string date)
        {
            return date.ToString().Replace(" EST", "");
        }

        internal static string ConvertDate(DateTime date)
        {
            return date.ToString(GetClientDateFormat());
        }

        ///<summary>
        ///Extension Method
        ///<para>Convert Date to Provided Date Format</para>
        ///</summary>
        internal static string ChangeDateFormat(this DateTime date, string dateFormat)
        {
            return date.ToString(dateFormat);
        }

        internal static string ConvertDate(DateTime date, string dateFormat)
        {
            return date.ToString(dateFormat);
        }

        internal static string GetDefaultFromDate()
        {
            int defaultDays = baseDataAccessLayer.GetDefaultFromDate() - 1;
            DateTime.Now.AddDays(-defaultDays);
            return ConvertDate(DateTime.Now.AddDays(-defaultDays));
        }
        internal static string GetDateAddTimeSpan(TimeSpan timeSpan)
        {
            return ConvertDate(DateTime.Now.Add(timeSpan));
        }

        internal static string GetDateAddTimeSpan(TimeSpan timeSpan, string dateTime, string format = "M/d/yyyy")
        {
            return ConvertDate(DateTime.ParseExact(dateTime, format, CultureInfo.InvariantCulture).Add(timeSpan));
        }

        internal static string ConvertDateDBFormat(string dateTime)
        {
            return ConvertDate(DateTime.ParseExact(dateTime, GetClientDateFormat(), CultureInfo.InvariantCulture), "yyyy-MM-dd");
        }

        internal static string GetTimeStamp()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssffff");
        }

        internal static string GetTime()
        {
            return DateTime.Now.ToString("HHmmssffff");
        }

        internal static string GetEditsButtonCaption(string type)
        {
            string caption;

            switch (type)
            {
                case "Insert":
                    {
                        caption = ButtonsAndMessages.New;
                    }
                    break;
                case "Update":
                    {
                        caption = ButtonsAndMessages.Edit;
                    }
                    break;
                default:
                    throw new NotImplementedException();

            }

            return caption;
        }

        internal static string GetClientLower()
        {
            return applicationContext.ApplicationContext.GetInstance().client.ToLower();
        }


        internal static string GetUserName()
        {
            return ScenarioContext.Current["UserName"].ToString();
        }

        internal static bool SearchInList<T>(this List<string> list, List<T> list2)
        {
            List<string> list3 = new List<string>();

            list3 = list2.Select(x => x.ToString()).ToList();


            foreach (string item in list3)
            {
                if (!list.Contains(item))
                {
                    return false;
                }
            }

            return true;
        }

        internal static string GetStringValue(this IDataReader reader, int index)
        {
            return reader.IsDBNull(index) ? "" : reader.GetString(index);
        }

        internal static string GetStringValue(this IDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                {
                    return reader.IsDBNull(i) ? "" : reader.GetString(i);
                }
            }
            return null;
        }

        internal static bool GetBooleanValue(this IDataReader reader, string columnName)
        {
            int i = reader.GetReaderIndex(columnName);
            return reader.IsDBNull(i) ? false : reader.GetBoolean(i);

        }

        internal static int GetReaderIndex(this IDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                {
                    return i;
                }
            }
            return 0;
        }

        internal static string GetDateTimeValue(this IDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                {
                    return reader.IsDBNull(i) ? string.Empty : ConvertDate(reader.GetDateTime(i));
                }
            }
            return null;
        }

        internal static string GetDateTimeValue(this IDataReader reader, string columnName, string format)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                {
                    return reader.IsDBNull(i) ? string.Empty : ConvertDate(reader.GetDateTime(i), format);
                }
            }
            return null;
        }

        internal static string GetRandomEmail()
        {
            return RandomString(6) + "@test.com";
        }

        internal static int GenerateRandom(int lowerBound, int upperBound)
        {
            if (lowerBound > upperBound)
            {
                throw new Exception("Lower Bound cannot be greater than Upper Bound");
            }
            Random r = new Random();
            return r.Next(lowerBound, upperBound);
        }

        internal static bool VerifySortOrder<T>(List<T> orderedList, List<T> unOrderedList, SortOrder sortOrder)
        {
            switch (sortOrder)
            {
                case SortOrder.Ascending:
                    unOrderedList = unOrderedList.OrderBy(x => x).ToList();
                    break;
                case SortOrder.Descending:
                    unOrderedList = unOrderedList.OrderByDescending(x => x).ToList();
                    break;
            }
            return orderedList.SequenceEqual(unOrderedList.Take(orderedList.Count).ToList());
        }

        internal static string FormatString(string message, string[] args)
        {
            string tempMessage = message;

            for (int i = 0; i < args.Length; i++)
            {
                tempMessage = tempMessage.Replace("{" + i + "}", args[i]);
            }

            return tempMessage;

        }

        internal static List<Fields> CheckForDuplicates(List<Fields> fields)
        {
            if (fields.Select(x => x.ParamCaption).Distinct().Count() != fields.Count)
            {
                var duplicates = fields
                       .GroupBy(x => x.ParamCaption)
                       .Where(x => x.Count() > 1)
                       .Select(x => x.Key)
                       .ToList();

                var reversedPom = fields.AsEnumerable().Reverse().ToList();

                foreach (var dup in duplicates)
                {
                    var data = fields.FindAll(a => a.ParamCaption == dup);

                    foreach (var item in data)
                    {
                        var index = reversedPom.IndexOf(item);
                        var check = reversedPom.Skip(index).Where(a => a.ParamType.ToUpper().Contains("ASPXLABEL")).FirstOrDefault();

                        if (check != null)
                        {
                            fields[fields.IndexOf(item)].ParamCaption = check.ParamCaption + "_" + fields[fields.IndexOf(item)].ParamCaption;
                        }
                    }
                }
                return fields;
            }
            return fields;
        }

        internal static string HtmlEncode(string decodedStr)
        {
            return HttpUtility.HtmlEncode(decodedStr);
        }

        internal static bool AreEqualCustom(this string s1, string s2)
        {
            s1 = Regex.Replace(s1, @"\s", "");
            s2 = Regex.Replace(s2, @"\s", "");
            return String.Equals(s1, s2, StringComparison.OrdinalIgnoreCase);
        }

        internal static List<string> GetPageHeaders(string pageName)
        {
            List<string> headers = null;
            pageName = RemoveNonAlphanumericChars(pageName).ToLower();
            applicationContext.ApplicationContext.GetInstance().PageHeaders.TryGetValue(pageName, out headers);
            if (headers == null)
            {
                throw new KeyNotFoundException($"Page name [{pageName}] not found in file.");
            }
            return headers;
        }

        internal static string RemoveNonAlphanumericChars(string str)
        {
            return rgx.Replace(str, "").ToLower();
        }

        internal static void FlushRedisDB(string serverName, int port)
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(
            new ConfigurationOptions
            {
                EndPoints = { $"{serverName}:{port}" },
                AllowAdmin = true
            });
            redis.GetServer(serverName, port).FlushDatabase();
        }
        internal static int GetSystemType(string invoiceNumber)
        {
            return new CommonDAL().GetSystemType(invoiceNumber);
        }
        internal static bool GetExportToAccounting(string invoiceNumber)
        {
            return new CommonDAL().GetExportToAccounting(invoiceNumber);
        }
        internal static decimal GetAvailableCreditLimit(string fleetName)
        {
            return new CommonDAL().GetAvailableCreditLimit(fleetName);
        }

        internal static int GetValidationStatus(string invNumber)
        {
            return new CommonDAL().GetValidationStatus(invNumber);
        }

        internal static bool ValidateTransactionError(string invNumber, string errorMsg)
        {
            return new CommonDAL().ValidateTransactionError(invNumber, errorMsg);
        }

        internal static Boolean ValidateTransactionErrorToken(string invNumber, string errorMsg)
        {
            return new CommonDAL().ValidateTransactionErrorToken(invNumber, errorMsg);
        }

        internal static string GetCustomizedDate()
        {
            return new CommonDAL().GetCustomizedDate();
       

        }
        internal static bool GetMultilingualtoken()
        {
            return new CommonDAL().GetMultilingualtoken();
        }
        public static string Localize(this string s)
        {
            return Translations.ResourceManager.GetString(s, applicationContext.ApplicationContext.GetInstance().CurrentCultureInfo) ?? s;
        }
        #region Common DAL Functions

        internal static string GetClientDateFormat()
        {
            if (string.IsNullOrEmpty(applicationContext.ApplicationContext.GetInstance().ClientDateFormat))
            {
                string localeSettings = new CommonDAL().GetLocaleSettingsForLoggedInUser();
                if (!string.IsNullOrEmpty(localeSettings) && localeSettings == "en-AU")
                {
                    applicationContext.ApplicationContext.GetInstance().ClientDateFormat = "dd/MM/yyyy";
                }
                else
                {
                    applicationContext.ApplicationContext.GetInstance().ClientDateFormat = "M/d/yyyy";
                }
            }
            return applicationContext.ApplicationContext.GetInstance().ClientDateFormat;
        }

        internal static string GetClientGridDateFormat()
        {
            if (string.IsNullOrEmpty(applicationContext.ApplicationContext.GetInstance().ClientGridDateFormat))
            {
                string localeSettings = new CommonDAL().GetLocaleSettingsForLoggedInUser();
                if (!string.IsNullOrEmpty(localeSettings) && localeSettings == "en-AU")
                {
                    applicationContext.ApplicationContext.GetInstance().ClientGridDateFormat = "dd/MM/yyyy";
                }
                else
                {
                    applicationContext.ApplicationContext.GetInstance().ClientGridDateFormat = "MM/dd/yyyy";
                }
            }
            return applicationContext.ApplicationContext.GetInstance().ClientGridDateFormat;
        }
        internal static void ActivateTokenPPV()
        {
            new CommonDAL().ActivateTokenPPV();

        }

        internal static void DeactivateTokenPPV()
        {
            new CommonDAL().DeactivateTokenPPV();

        }

        internal static void ActivateTokenGMC()
        {
            new CommonDAL().ActivateTokenGMC();

        }

        internal static void DeactivateTokenGMC()
        {
            new CommonDAL().DeactivateTokenGMC();

        }

        internal static bool IsInvoiceValidated(string invoiceNum)
        {
            return new CommonDAL().IsInvoiceValidated(invoiceNum);
        }

        internal static void ActivateFutureDateInMinsToken()
        {
            new CommonDAL().ActivateFutureDateInMinsToken();

        }

        internal static void SetFutureDateInMinsValue(int value)
        {
            new CommonDAL().SetFutureDateInMinsValue(value);

        }

        internal static void UpdateTransactionDateForInvoice(string invoiceNum, int day)
        {
            new CommonDAL().UpdateTransactionDateForInvoice(invoiceNum, day);

        }

        internal static void UpdateCreateDateForInvoice(string invoiceNum, int day)
        {
            new CommonDAL().UpdateCreateDateForInvoice(invoiceNum, day);
        }

        internal static void UpdateDisputeDateForInvoice(string invoiceNum, int day)
        {
            new CommonDAL().UpdateDisputeDateForInvoice(invoiceNum, day);
        }

        internal static void UpdateReceiveDateForInvoice(string invoiceNum, int day)
        {
            new CommonDAL().UpdateReceiveDateForInvoice(invoiceNum, day);
        }

        internal static void UpdateAPInvoiceDateForInvoice(string invoiceNum, int day)
        {
            new CommonDAL().UpdateAPInvoiceDateForInvoice(invoiceNum, day);
        }


        internal static void UpdateARStatementStartDateForInvoice(string invoiceNum, int day)
        {
            new CommonDAL().UpdateARStatementStartDateForInvoice(invoiceNum, day);
        }

        internal static void UpdateAPStatementStartDateForInvoice(string invoiceNum, int day)
        {
            new CommonDAL().UpdateAPStatementStartDateForInvoice(invoiceNum, day);
        }

        internal static void UpdateARStatementDateForInvoice(string invoiceNum, int day)
        {
            new CommonDAL().UpdateARStatementDateForInvoice(invoiceNum, day);
        }

        internal static void UpdateAPStatementDateForInvoice(string invoiceNum, int day)
        {
            new CommonDAL().UpdateAPStatementDateForInvoice(invoiceNum, day);
        }

        internal static void UpdateCreateDateInvoiceTbForInvoice(string invoiceNum, int day)
        {
            new CommonDAL().UpdateCreateDateInvoiceTbForInvoice(invoiceNum, day);
        }

        internal static void UpdateInvoiceDateForInvoice(string invoiceNum, int day)
        {
            new CommonDAL().UpdateInvoiceDateForInvoice(invoiceNum, day);
        }

        internal static void UpdateARInvoiceDateForInvoice(string invoiceNum, int day)
        {
            new CommonDAL().UpdateARInvoiceDateForInvoice(invoiceNum, day);
        }

        internal static void UpdateTransactionDateInvoiceTbForInvoice(string invoiceNum, int day)
        {
            new CommonDAL().UpdateTransactionDateInvoiceTbForInvoice(invoiceNum, day);
        }

        internal static LookupTb GetLookupTbDetails(string name)
        {
            return new CommonDAL().GetLookupTbDetails(name);
        }

        internal static InvoiceObject GetInvoiceByTransactionNumber(string transactionNumber)
        {
            return new CommonDAL().GetInvoiceByTransactionNumber(transactionNumber);
        }

        internal static InvoiceObject GetInvoiceSectionByInvoiceId(int InvoiceId)
        {
            return new CommonDAL().GetInvoiceSectionByInvoiceId(InvoiceId);
        }

        internal static List<string> GetInvoiceLineDetailsItemIds(int InvoiceSectionId)
        {
            return new CommonDAL().GetInvoiceLineDetailsItemIds(InvoiceSectionId);
        }

        internal static void GetInvoicebyTransactionStatus(int transactionStatus, out string transactionNumber)
        {
            new CommonDAL().GetInvoicebyTransactionStatus(transactionStatus, out transactionNumber);
        }

        internal static EntityDetails GetEntityDetails(string corcentricCode)
        {
            return new CreateNewEntityDAL().GetEntityDetails(corcentricCode);
        }
        internal static EntityDetails GetZipAndState(string corcentricCode)
        {
            return new CreateNewEntityDAL().GetZipAndState(corcentricCode);
        }

        internal static string GetDealerCodeHasNoChild(LocationType locationType)
        {
            return new CommonDAL().GetDealerCodeHasNoChild(locationType);
        }

        internal static string GetFleetCodeHasNoChild(LocationType locationType)
        {
            return new CommonDAL().GetFleetCodeHasNoChild(locationType);
        }

        internal static List<string> GetStates(string country = "US")
        {
            return new CommonDAL().GetStates(country);
        }

        internal static string GetDealerCode()
        {
            return new CommonDAL().GetDealerCode();
        }

        internal static string GetPartiallyEnrolledDealerCode()
        {
            return new CommonDAL().GetPartiallyEnrolledDealerCode();
        }

        internal static string GetInactiveDealerCode()
        {
            return new CommonDAL().GetInactiveDealerCode();
        }

        internal static string GetTerminatedDealerCode()
        {
            return new CommonDAL().GetTerminatedDealerCode();
        }

        internal static string GetSuspendedDealerCode()
        {
            return new CommonDAL().GetSuspendedDealerCode();
        }

        internal static string GetFleetCode()
        {
            return new CommonDAL().GetFleetCode();
        }

        internal static string GetPartiallyEnrolledFleetCode()
        {
            return new CommonDAL().GetPartiallyEnrolledFleetCode();
        }

        internal static string GetInactiveFleetCode()
        {
            return new CommonDAL().GetInactiveFleetCode();
        }

        internal static string GetTerminatedFleetCode()
        {
            return new CommonDAL().GetTerminatedFleetCode();
        }
        internal static string GetSuspendedFleetCode()
        {
            return new CommonDAL().GetSuspendedFleetCode();
        }
        internal static string GetPartByCategory(string category)
        {
            return new CommonDAL().GetPartByCategory(category);
        }

        internal static string GetPartNotInCategory()
        {
            return new CommonDAL().GetPartNotInCategory();
        }

        internal static string GetDealerEntityCount()
        {
            return new CommonDAL().GetDealerEntityCount();
        }

        internal static string GetFleetEntityCount()
        {
            return new CommonDAL().GetFleetEntityCount();
        }

        internal static string GetCorcentricLocation()
        {
            return new CommonDAL().GetCorcentricLocation();
        }

        internal static List<string> GetActiveCurrencies()
        {
            return new CommonDAL().GetActiveCurrencies();
        }
        internal static void UpdateFleetCreditLimits(int creditLimit, int availCreditLimit, string fleetCode)
        {
            new CommonDAL().UpdateFleetCreditLimits(creditLimit, availCreditLimit, fleetCode);

        }


        internal static void ToggleValidateProgramCode(bool activate)
        {
            new CommonDAL().ToggleValidateProgramCode(activate);
        }

        internal static void ToggleEnablePrgmCodeAsgnOnSubCommunity(bool activate)
        {
            new CommonDAL().ToggleEnablePrgmCodeAsgnOnSubCommunity(activate);
        }

        internal static List<EntityDetails> GetProgramCodeEntityLocations(string userName)
        {
            return new CommonDAL().GetProgramCodeEntityLocations(userName);
        }

        internal static List<EntityDetails> GetProgCdOppEntyLocForLoggedInEntyType(string userName, string entityType, out bool isProgamCodeAssigned)
        {
            return new CommonDAL().GetProgCodeOppEntyLocForLoggedInEntyType(userName, entityType, out isProgamCodeAssigned);
        }

        internal static List<EntityDetails> GetProgCdOppEntyLocForLoggedInEntyTypeFlsNeg(string userName, string entityType)
        {
            return new CommonDAL().GetProgCdOppEntyLocForLoggedInEntyTypeFlsNeg(userName, entityType);
        }

        internal static List<int> DeactivateActivatedDataContent()
        {
            return new CommonDAL().DeactivateActivatedDataContent();
        }

        internal static void ActivateDeactivatedDataContent(List<int> dataContentIds)
        {
            if (dataContentIds != null && dataContentIds.Count > 0)
                new CommonDAL().ActivateDeactivatedDataContent(dataContentIds);
        }

        internal static List<string> GetActiveTransactionStatus()
        {
            return new CommonDAL().GetActiveTransactionStatus();
        }

        internal static void ActivateStrongPassowordToken()
        {
            new CommonDAL().ActivateStrongPassowordToken();

        }
        internal static void DeactivateStrongPassowordToken()
        {
            new CommonDAL().DeactivateStrongPassowordToken();
        }

        internal static bool ToggleSeperateARAPCalcsToken(bool doActivate)
        {
            return new CommonDAL().ToggleLookupTbToken("SeperateARAPCalcs", doActivate);
        }

        internal static List<EntityDetails> GetActiveLocations(LocationType locationType, UserType userType)
        {
            return new CommonDAL().GetActiveLocations(locationType, userType);
        }

        internal static List<EntityDetails> GetActiveLocations(LocationType locationType, string userName)
        {
            return new CommonDAL().GetActiveLocations(locationType, userName);
        }
        internal static void RunResubmitDiscrepanciesJob()
        {
            new CommonDAL().RunResubmitDiscrepanciesJob();

        }
        internal static string GetInvoiceNumberForCurrentTransaction()
        {
            return new CommonDAL().GetInvoiceNumberForCurrentTransaction();
        }

        internal static string GetInvoiceNumberForDisputeCreation()
        {
            return new CommonDAL().GetInvoiceNumberForDisputeCreation();
        }

        internal static string GetSettledInvoice()
        {
            return new CommonDAL().GetSettledInvoice();
        }

        internal static string GetDiscrepantInvoice(string discrepantState, string dealer, string fleet)
        {
            return new CommonDAL().GetDiscrepantInvoice(discrepantState, dealer, fleet);
        }
        internal static string GetCustomizedDateDays()
        {
            return new CommonDAL().GetCustomizedDateDays();
        }

        internal static void MoveInvoiceToHistory(string dealerInv)
        {
            new CommonDAL().MoveInvoiceToHistory(dealerInv);

        }

        internal static string GetInvoiceExpirationDate(string invNumber)
        {
            return new CommonDAL().GetInvoiceExpirationDate(invNumber);
        }
        internal static string GetInvoiceTransactionDate(string invNumber)
        {
            return new CommonDAL().GetInvoiceTransactionDate(invNumber);
        }
        internal static void UpdateInvoiceValidityDays(int validityDays)
        {
             new CommonDAL().UpdateInvoiceValidityDays(validityDays);
        }

        internal static int GetEntityId(string corCentricCode)
        {
            return new CommonDAL().GetEntityId(corCentricCode);
        }

        internal static string GetSubcommunityforEntity(string corCentricCode)
        {
            return new CommonDAL().GetSubcommunityforEntity(corCentricCode);
        }

        internal static int GetCommunityIDOfClient()
        {
            return new CommonDAL().GetCommunityIDOfClient();

        }
        internal static int GetInvoiceTransactionAmount(string invNumber)
        {
            return new CommonDAL().GetInvoiceTransactionAmount(invNumber);
        }

        internal static void UpdateCreditLimitVarianceThreshHoldPct(int percent, int clientid)
        {
            new CommonDAL().UpdateCreditLimitVarianceThreshHoldPct(percent, clientid);
        }

        internal static void ToggleEntityActivationFlag(string corcentricCode, bool activate)
        {
            new CommonDAL().ToggleEntityActivationFlag(corcentricCode, activate);
        }

        internal static void ToggleEntityTerminationFlag(string corcentricCode, bool activate)
        {
            new CommonDAL().ToggleEntityTerminationFlag(corcentricCode, activate);
        }

        internal static void ToggleEntitySuspensionRelationship(string corcentricCode, bool activate)
        {
            new CommonDAL().ToggleEntitySuspensionRelationship(corcentricCode, activate);
        }

        internal static TransactionDetails GetAuthTrasactionDetails(string dealerCode, string fleetCode)
        {
            return new CommonDAL().GetAuthTrasactionDetails(dealerCode, fleetCode);
        }

        internal static bool ToggleLookupTbToken(string tokenName, bool doActivate)
        {
            return new CommonDAL().ToggleLookupTbToken(tokenName, doActivate);
        }
        internal static string GetDealerCodeforLocationType(string locationType)
        {
            return new CommonDAL().GetDealerCodeforLocationType(locationType);
        }

        internal static string GetFleetCodeforLocationType(string locationType)
        {
            return new CommonDAL().GetFleetCodeforLocationType(locationType);
        }


        #endregion Common DAL Functions
    }

}
