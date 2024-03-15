using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using PageObjectUtility.DataObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace PageObjectUtility
{
    internal class PageObjectUtility
    {
        static SqlConnection? Connection = null;
        const string DefaultEnviorment = "FunctionalAutomation";
        static List<Clients> clients;
        static List<SynonymException> SynonymExceptions;
        static ConnectionStringSettingsCollection ConnectionStrings;
        static Dictionary<string, Dictionary<string, string>> clientLookupNames = new Dictionary<string, Dictionary<string, string>>();
        static string currentCient = Environment.GetEnvironmentVariable("UAT");

        static void Main(string[] args)
        {
            ConnectionStrings = ConfigurationManager.ConnectionStrings;
            LoadClients();
            LoadSynonymExceptions();
            LoadCaptionsForClients();
            GetMenu();

            var pages = GetAllPages();
            // Remove Extra Disputes page entry
            pages.RemoveAll(x => x.ContainsKey("LongDesc") && x["LongDesc"].ToString().Contains("Dashboard - Disputes"));
            foreach (var page in pages)
            {
                if (!GetDefaultPages(page))
                {
                    GetData(page["pageCaption"].ToString(), page);
                }
            }
            Connection.Close();
            Connection.Dispose();
        }

        private static void GetMenu()
        {
            JObject fileObject = new JObject();

            var dictionary = new Dictionary<string, List<Menu>>();
            var defaultClient = clients.First(x => x.Client == DefaultEnviorment);
            GetMenu(defaultClient, out List<Menu> menuList);

            if (!string.IsNullOrEmpty(currentCient) && currentCient != defaultClient.Client)
            {
                var client = clients.First(x => x.Client == currentCient);
                GetMenu(client, out List<Menu> currClientMenu);
                for (int i = 0; i < menuList.Count; i++)
                {
                    menuList[i].OriginalSubMenuDescription = menuList[i].Caption;
                    FixMenuNameForClient(menuList[i], currentCient, currClientMenu);
                }
                var menuListDiff = currClientMenu.Where(x => menuList.Any(y => y.Caption != x.Caption)).ToList();
                if (menuListDiff != null && menuListDiff.Count > 0)
                {
                    menuListDiff.ForEach(x =>
                    {
                        x.OriginalSubMenuDescription = x.Caption;
                    });
                    menuList.AddRange(menuListDiff);
                }
            }

            dictionary.Add(defaultClient.Client, menuList);

            //foreach (var client in clients)
            //{
            //    if (client != defaultClient)
            //    {
            //        GetMenu(client, out menuList);
            //        dictionary.Add(client.Client, menuList);
            //    }
            //}

            var defaultelements = dictionary[defaultClient.Client];

            dictionary.Remove(defaultClient.Client);
            defaultelements = defaultelements.GroupBy(x => x.Caption).Select(x => x.First()).ToList();

            if (!string.IsNullOrEmpty(currentCient) && !currentCient.ToLower().Equals(defaultClient.Client.ToLower()))
            {
                foreach (var element in defaultelements)
                {
                    fileObject.Add(element.OriginalSubMenuDescription, JObject.FromObject(element));
                }
            }
            else
            {
                foreach (var element in defaultelements)
                {
                    fileObject.Add(element.Caption, JObject.FromObject(element));
                }
            }

            WriteFile("Menu", fileObject);
        }

        private static void GetMenu(Clients client, out List<Menu> menuList)
        {
            menuList = new List<Menu>();
            Connection = new SqlConnection(ConnectionStrings[client.Type].ConnectionString);
            Connection.Open();
            Connection.ChangeDatabase(client.WebCoreDBName);

            string Query = "select View_Name, View_Description,Sp_LongDesc,Sp_ShortDesc from VIEW_SPLOOKUP_DATA s inner join sp_lookup spl on spl.Sp_ID=s.Sp_ID inner join VIEW_LOOKUP v on v.View_ID= s.View_ID inner join SP_LOOKUP_NAMES l on l.SpName_ID=spl.SpName_ID  where spl.isactive= 1 order by  View_Order";
            SqlCommand oCmd = new SqlCommand(Query, Connection);

            using (SqlDataReader oReader = oCmd.ExecuteReader())
            {
                while (oReader.Read())
                {
                    var menuItem = new Menu();

                    if (!oReader.IsDBNull(0))
                    {
                        menuItem.MenuName = oReader.GetString(0).Trim();
                    }

                    if (!oReader.IsDBNull(2))
                    {
                        menuItem.LongDescription = oReader.GetString(2).Trim();

                    }

                    if (!oReader.IsDBNull(3))
                    {
                        menuItem.Caption = oReader.GetString(3).Trim();

                    }
                    menuList.Add(menuItem);
                }
            }
            Connection.Close();
        }

        private static bool GetDefaultPages(Dictionary<string, object> page)
        {
            List<POM> list = new List<POM>();
            JObject Jobject = new JObject();
            var pom = new POM();

            if (!(bool)page["Has_DynamicFilter"])
            {

                pom = new POM
                {
                    ID = "Company Name",
                    xpath = By.XPath("//table[contains(@id,'cboCorpsMainsearch')]")
                };

                list.Add(pom);


                if (!(bool)page["HideDateStaticSearch"])
                {
                    pom = new POM
                    {
                        ID = "From",
                        xpath = By.XPath("//input[contains(@id,'StaticSearch_dxedit_dateFrom')]")
                    };
                    list.Add(pom);

                    pom = new POM
                    {
                        ID = "To",
                        xpath = By.XPath("//input[contains(@id,'StaticSearch_dxedit_dateTo')]")
                    };
                    list.Add(pom);

                }

                foreach (var element in list)
                {
                    Jobject.Add(element.ID, new JObject
                {
                    { "default", JObject.FromObject(element.xpath) }
                });
                };

                var pagePom = GetSpEdits(page["pageCaption"].ToString(), Jobject, clients.First(x => x.Client == DefaultEnviorment));
                WriteFile(page["pageCaption"].ToString().Replace("/", ""), pagePom);

                return true;
            }

            return false;
        }
        private static void GetData(string pageCaption, Dictionary<string, object> page)
        {
            var dictionary = new Dictionary<string, List<POM>>();
            SqlCommand oCmd;
            List<POM> list;
            var defaultClient = clients.First(x => x.Client == DefaultEnviorment);
            var filteredClients = clients.Where(x => x.Client == DefaultEnviorment || x.Client == currentCient).ToList();

            foreach (var client in filteredClients)
            {

                Connection = new SqlConnection(ConnectionStrings[client.Type].ConnectionString);
                Connection.Open();
                Connection.ChangeDatabase(client.WebCoreDBName);
                string Caption = ConvertCaption(pageCaption, client.Client);

                list = new List<POM>();

                //   string oString = "SELECT * FROM SP_SEARCH_PARAMETERS a INNER JOIN SP_LOOKUP b ON a.SP_ID = b.Sp_ID where a.Sp_ID = (select Top 1 Sp_ID from SP_LOOKUP  where Display_Description = @caption and IsActive = 1 Order by Creation_Date desc) and isVisible = 1 order by Param_Order;";
                string oString = "SELECT * FROM SP_SEARCH_PARAMETERS a where a.Sp_ID = (select Top 1 b.Sp_ID from SP_LOOKUP b inner join SP_SEARCH_PARAMETERS aa on b.sp_id = aa.SP_ID where b.Display_Description = @caption and b.IsActive = 1 Order by b.Creation_Date desc) and a.isVisible = 1 order by a.Param_Order;";
                oCmd = new SqlCommand(oString, Connection);
                oCmd.Parameters.AddWithValue("@caption", Caption);

                try
                {
                    using (SqlDataReader oReader = oCmd.ExecuteReader())
                    {

                        while (oReader.Read())
                        {
                            var pom = new POM();
                            switch (oReader["Param_SpDisplayControl"].ToString().ToLower())
                            {
                                case "aspxtextbox":
                                case "AspxTextBox":
                                case "aspxdateedit":
                                    pom.xpath = By.XPath("//input[contains(@id,'" + oReader["Param_Name"] + "_" + oReader["SpSearchParam_ID"] + "')]");
                                    break;
                                case "aspxcombobox":
                                case "multiselectcontrol":
                                    pom.xpath = By.XPath("//table[contains(@id,'" + oReader["Param_Name"] + "_" + oReader["SpSearchParam_ID"] + "')]");
                                    break;
                                case "aspxlabel":
                                    pom.xpath = By.XPath("//span[contains(@id,'" + oReader["Param_Name"] + "_" + oReader["SpSearchParam_ID"] + "')]");
                                    break;
                                case "aspxcheckbox":
                                case "AspxCheckbox":
                                    pom.xpath = By.XPath("//span[contains(@id,'" + oReader["Param_Name"] + "_" + oReader["SpSearchParam_ID"] + "_S_D')]");
                                    break;
                                case "AspxMemo":
                                    pom.xpath = By.XPath("//textarea[contains(@id,'" + oReader["Param_Name"] + "_" + oReader["SpSearchParam_ID"] + "_S_D')]");
                                    break;
                                default:
                                    pom.xpath = By.XPath("//input[contains(@id,'" + oReader["Param_Name"] + "_" + oReader["SpSearchParam_ID"] + "')]");
                                    break;
                            }

                            pom.ID = Regex.Replace(oReader["Param_Caption"].ToString(), "<.*?>", String.Empty).Trim();
                            pom.Name = oReader["Param_Name"].ToString();

                            list.Add(pom);

                            if (client == defaultClient)
                            {
                                list = CheckForDuplicates(list);
                            }
                        }
                    }

                    if (list.Count > 0)
                    {
                        dictionary.Add(client.Client, list);
                    }
                    else
                    {
                        if (page.ContainsKey("clientPage") != null && !Convert.ToBoolean(page.GetValueOrDefault("clientPage")))
                        {
                            GetDefaultPages(new Dictionary<string, object>
                            {
                                { "Has_DynamicFilter", false },
                                { "HideDateStaticSearch", false },
                                { "pageCaption", pageCaption }
                            });
                            break;
                        }
                    }
                }


                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    dictionary = null;
                }
            }

            if (dictionary != null && dictionary.Count > 0)
            {
                if ((string.IsNullOrEmpty(currentCient) && dictionary.Count == 1) || dictionary.Count == 2)
                {
                    var spEdits = LoadPOMForDefaultAndCurrentClient(pageCaption, dictionary);
                    WriteFile(pageCaption.Replace("/", ""), spEdits);
                }
                else if (dictionary.ContainsKey(currentCient))
                {
                    var spEdits = LoadPOMForCurrentClient(pageCaption, dictionary);
                    WriteFile(pageCaption.Replace("/", ""), spEdits);
                }
                Connection.Close();
            }
        }

        private static JObject? LoadPOMForDefaultAndCurrentClient(string pageCaption, Dictionary<string, List<POM>> dictionary)
        {
            var defaultClient = clients.First(x => x.Client == DefaultEnviorment);
            var defaultelements = dictionary[defaultClient.Client];
            dictionary.Remove(defaultClient.Client);
            JObject jObject = new JObject();
            JObject j1 = new JObject();
            foreach (var element in defaultelements)
            {
                j1 = new JObject
                        {
                            { "ParamName", element.Name },
                            { defaultClient.Client.ToLower(), JObject.FromObject(element.xpath) }
                        };

                foreach (var item in dictionary)
                {
                    var pom = item.Value.FirstOrDefault(x => x.Name == element.Name);
                    if (pom != null)
                    {
                        j1.Add(item.Key.ToLower(), JObject.FromObject(pom.xpath));
                        item.Value.Remove(pom);
                    }
                    else
                    {
                        pom = item.Value.FirstOrDefault(x => x.ID == element.ID);
                        if (pom != null)
                        {
                            j1.Add(item.Key.ToLower(), JObject.FromObject(pom.xpath));
                            item.Value.Remove(pom);
                        }
                    }
                }

                jObject.Add(element.ID, j1);
            }
            if (dictionary != null && dictionary.Count > 0 && dictionary.First().Value.Count > 0)
            {
                List<POM> values = dictionary.First().Value.Where(x => !string.IsNullOrEmpty(x.ID)).ToList();
                foreach (var element in values)
                {
                    j1 = new JObject
                 {
                    { "ParamName", element.Name },
                     { currentCient.ToLower(), JObject.FromObject(element.xpath) }
                  };
                    jObject.Add(element.ID, j1);
                }
            }

            var spEdits = GetSpEdits(pageCaption, jObject, defaultClient);
            if (!string.IsNullOrEmpty(currentCient) && currentCient != defaultClient.Client)
            {
                pageCaption = ConvertCaption(pageCaption, currentCient);
                JObject spEditsCurr = GetSpEdits(pageCaption, jObject, clients.First(x => x.Client == currentCient));
                foreach (var x in spEditsCurr)
                {
                    if (!spEdits.ContainsKey(x.Key))
                    {
                        spEdits.Add(x.Key, x.Value);
                    }
                }
            }
            return spEdits;
        }

        private static JObject LoadPOMForCurrentClient(string pageCaption, Dictionary<string, List<POM>> dictionary)
        {
            var defaultelements = dictionary[currentCient];
            JObject jObject = new JObject();
            JObject j1 = new JObject();
            foreach (var element in defaultelements)
            {
                j1 = new JObject
                        {
                            { "ParamName", element.Name },
                            { currentCient.ToLower(), JObject.FromObject(element.xpath) }
                        };

                jObject.Add(ConvertCaptionToDefault(element.ID, currentCient), j1);
            }
            var spEdits = GetSpEdits(pageCaption, jObject, clients.First(x => x.Client == currentCient));
            return spEdits;
        }

        private static JObject GetSpEdits(string page, JObject pagePom, Clients client)
        {
            var dataTable = new DataTable();
            string xpath = string.Empty;
            string id = string.Empty;
            var poms = new List<POM>();

            string query = "select Param_Caption, Param_Name, Param_SpDisplayControl ,Edit_Type from SP_PARAMETERS p inner join SP_EDITS e on p.SpEditable_ID=e.SpEditable_ID inner join SP_EDIT_TYPE_LOOKUP l on e.SPEditType_ID=l.SPEditType_ID  where Sp_ID = (select Top 1 Sp_ID from SP_LOOKUP where Display_Description = @page AND IsActive = 1 Order by Creation_Date desc) and IsVisible=1";

            Connection.Close();
            Connection = new SqlConnection(ConnectionStrings[client.Type].ConnectionString);
            Connection.Open();
            Connection.ChangeDatabase(client.WebCoreDBName);

            SqlCommand oCmd = new SqlCommand(query, Connection);
            oCmd.Parameters.AddWithValue("@Page", page);

            try
            {
                using (SqlDataReader oReader = oCmd.ExecuteReader())
                {
                    dataTable.Load(oReader);
                }
                Connection.Close();
                var duplicates = dataTable.AsEnumerable()
                    .GroupBy(r => r[0])
                    .Where(gr => gr.Count() > 1)
                    .Select(g => g.Key);


                foreach (string d in duplicates)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        if (dr["Param_Caption"].ToString() == d)
                        {
                            dr["Param_Caption"] = GetEditsButtonCaption(dr["Edit_Type"].ToString().Trim()) + "_" + dr["Param_Caption"];
                        }
                    }
                }

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    switch (dataRow["Param_SpDisplayControl"])
                    {
                        case "AspxTextBox":
                        case "AspxDateEdit":
                            xpath = "//input[contains(@id,'" + dataRow["Param_Name"] + "')]";
                            break;
                        case "ASPxComboBox":
                        case "MultiSelectControl":
                            xpath = "//table[contains(@id,'cboEdit_" + dataRow["Param_Name"] + "')]";
                            break;
                        case "AspxLabel":
                            xpath = "//span[contains(@id,'" + dataRow["Param_Name"] + "')]";
                            break;
                        case "AspxMemo":
                            xpath = "//textarea[contains(@id,'" + dataRow["Param_Name"] + "')]";
                            break;
                        case "aspxcheckbox":
                        case "AspxCheckbox":
                        case "ASPxCheckBox":
                            xpath = "//span[contains(@id,'" + dataRow["Param_Name"] + "_" + "0_S_D')]";
                            break;
                        default:
                            xpath = "//input[contains(@id,'" + dataRow["Param_Name"] + "')]";
                            break;
                    }

                    if (pagePom.Properties().Select(x => x.Name).Contains(dataRow["Param_Caption"].ToString()))
                    {
                        switch (dataRow["Edit_Type"].ToString().Trim())
                        {
                            case "Update":
                                id = "Edit_" + Regex.Replace(dataRow["Param_Caption"].ToString(), "<.*?>", String.Empty);
                                break;

                            case "Insert":
                                id = "New_" + Regex.Replace(dataRow["Param_Caption"].ToString(), "<.*?>", String.Empty);
                                break;
                        }
                    }
                    else
                    {
                        id = Regex.Replace(dataRow["Param_Caption"].ToString(), "<.*?>", String.Empty);
                    }

                    if (!id.StartsWith("New_New_") && !id.StartsWith("Edit_Edit_"))
                    {
                        poms.Add(new POM()
                        {
                            ID = id.Replace("*", string.Empty),
                            Name = dataRow["Param_Name"].ToString(),
                            xpath = By.XPath(xpath)
                        });
                    }
                }

                foreach (var pom in poms)
                {
                    if (!pagePom.ContainsKey(pom.ID))
                    {
                        pagePom.Add(pom.ID,
                        new JObject
                        {
                        { "default", JObject.FromObject(pom.xpath) }
                        });
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return pagePom;
        }

        private static List<Dictionary<string, object>> GetAllPages()
        {
            List<Dictionary<string, object>> defaultClientPages = GetAllPages(clients.First(x => x.Client == DefaultEnviorment));
            List<Dictionary<string, object>> currencClientPages = !string.IsNullOrWhiteSpace(currentCient) ? GetAllPages(clients.First(x => x.Client == currentCient)) : new List<Dictionary<string, object>>();
            List<Dictionary<string, object>> pages = new List<Dictionary<string, object>>();
            foreach (Dictionary<string, object> page in currencClientPages)
            {
                if (!defaultClientPages.Any(x => x.GetValueOrDefault("pageCaption") != null
                && ConvertCaption(x.GetValueOrDefault("pageCaption").ToString(), currentCient).Equals(page["pageCaption"].ToString())))
                {
                    page.Add("clientPage", true);
                    pages.Add(page);
                }
            }
            defaultClientPages.AddRange(pages);
            return defaultClientPages;
        }

        private static List<Dictionary<string, object>> GetAllPages(Clients client)
        {
            Connection = new SqlConnection(ConnectionStrings[client.Type].ConnectionString);
            Connection.Open();
            Connection.ChangeDatabase(client.WebCoreDBName);
            string oString = "select Display_Description, Has_DynamicFilter, HideDateStaticSearch, Sp_LongDesc  from SP_LOOKUP a inner join SP_LOOKUP_NAMES b on a.SpName_ID = b.SPName_ID ORDER BY Display_Description";

            var list = new List<Dictionary<string, object>>();

            SqlCommand oCmd = new SqlCommand(oString, Connection);
            using (SqlDataReader oReader = oCmd.ExecuteReader())
            {
                while (oReader.Read())
                {
                    var dictionary = new Dictionary<string, object>();

                    if (oReader.IsDBNull(1))
                    {
                        dictionary.Add("Has_DynamicFilter", false);
                    }
                    else
                    {
                        dictionary.Add("Has_DynamicFilter", oReader.GetBoolean(1));

                    }

                    if (oReader.IsDBNull(2))
                    {
                        dictionary.Add("HideDateStaticSearch", false);

                    }
                    else
                    {
                        dictionary.Add("HideDateStaticSearch", oReader.GetBoolean(2));

                    }

                    dictionary.Add("pageCaption", oReader.GetString(0));

                    if (!oReader.IsDBNull(3))
                    {
                        dictionary.Add("LongDesc", oReader.GetString(3));
                    }

                    list.Add(dictionary);
                }
            }
            Connection.Close();
            return list;
        }

        private static List<POM> CheckForDuplicates(List<POM> poms)
        {
            if (poms.Select(x => x.ID).Distinct().Count() != poms.Count)
            {
                var duplicates = poms
                       .GroupBy(x => x.ID)
                       .Where(x => x.Count() > 1)
                       .Select(x => x.Key)
                       .ToList();

                var reversedPom = poms.AsEnumerable().Reverse().ToList();

                foreach (var dup in duplicates)
                {
                    var data = poms.FindAll(a => a.ID == dup);

                    foreach (var item in data)
                    {
                        var index = reversedPom.IndexOf(item);
                        var check = reversedPom.Skip(index).Where(a => a.xpath.Criteria.Contains("//span")).FirstOrDefault();

                        if (check != null)
                        {
                            poms[poms.IndexOf(item)].ID = check.ID + "_" + poms[poms.IndexOf(item)].ID;
                        }
                    }
                }

                return poms;
            }

            return poms;
        }

        private static void WriteFile(string pageCaption, object data)
        {
            if (pageCaption.Trim() == "Fleet Credit")
            {
                pageCaption = "Fleet Credit Limit";
            }
            if (pageCaption.Trim() == "Open Authorization")
            {
                pageCaption = "Open Authorizations";
            }
            try
            {
                if (!Directory.Exists(@"..\..\..\..\..\Page elements\"))
                {
                    Directory.CreateDirectory(@"..\..\..\..\..\Page elements\");
                }

                using (StreamWriter file = File.CreateText(@"..\..\..\..\..\Page elements\" + pageCaption.Trim() + ".json"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, data);
                }
            }
            catch (Exception ex)
            { Console.WriteLine(ex); }
        }

        private static string GetEditsButtonCaption(string type)
        {
            string caption;

            switch (type)
            {
                case "Insert":
                    {
                        caption = "New";
                    }
                    break;
                case "Update":
                    {
                        caption = "Edit";
                    }
                    break;
                default:
                    throw new NotImplementedException();

            }

            return caption;
        }

        private static string ConvertCaption(string Caption, string client)
        {
            var values = new Dictionary<string, string>();

            if (client != DefaultEnviorment)
            {
                clientLookupNames.TryGetValue(client, out values);

                if (Caption.Contains("Dealer"))
                {
                    values.TryGetValue("DLR", out string caption);
                    Caption = Caption.Replace("Dealer", caption);
                }
                else if (Caption.Contains("Fleet"))
                {
                    values.TryGetValue("FLEET", out string caption);
                    Caption = Caption.Replace("Fleet", caption);
                }
                var tempExceptionMenus = MenuException.MenuExceptions.
                   FirstOrDefault(x => x.ClientNameLower == client.ToLower() && Caption.Contains(x.OriginalMenuName));
                if (tempExceptionMenus != null)
                {
                    Caption = Caption.Replace(tempExceptionMenus.OriginalMenuName, tempExceptionMenus.ExceptionalMenuName);
                }
            }
            return Caption;
        }

        private static string ConvertCaptionToDefault(string Caption, string client)
        {
            var values = new Dictionary<string, string>();
            try
            {
                if (client != DefaultEnviorment)
                {
                    clientLookupNames.TryGetValue(client, out values);
                    values.TryGetValue("DLR", out string dealerCaption);
                    values.TryGetValue("FLT", out string fleetCaption);
                    if (Caption.Contains(dealerCaption))
                    {
                        Caption = Caption.Replace(dealerCaption, "Dealer");
                    }
                    else if (Caption.Contains(fleetCaption))
                    {
                        Caption = Caption.Replace(fleetCaption, "Fleet");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return Caption;
        }

        internal static void GetCaptions(Clients client, SqlConnection Connection)
        {
            clientLookupNames.TryGetValue(client.Client, out var values);

            if (values == null)
            {
                string query = "select daimlercode, name from lookup_tb where parentlookupcode=1";
                var lookupNames = new Dictionary<string, string>();
                if (Connection.State == ConnectionState.Closed)
                {
                    Connection = new SqlConnection(ConnectionStrings[client.Type].ConnectionString);
                    Connection.Open();
                }
                Connection.ChangeDatabase(client.DBName);
                SqlCommand oCmd = new SqlCommand(query, Connection);
                using (SqlDataReader oReader = oCmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        lookupNames.Add(oReader.GetString(0), oReader.GetString(1));
                    }
                }
                clientLookupNames.Add(client.Client, lookupNames);
            }
        }

        internal static void LoadCaptionsForClients()
        {
            var filteredClients = clients.Where(x => x.Client == DefaultEnviorment || x.Client == currentCient).ToList();
            foreach (var client in filteredClients)
            {
                Connection = new SqlConnection(ConnectionStrings[client.Type].ConnectionString);
                Connection.Open();
                GetCaptions(client, Connection);
                Connection.Close();
            }
        }

        internal static void FixMenuNameForClient(Menu menu, string client, List<Menu> clientMenus)
        {
            string caption = ConvertCaption(menu.Caption, client);
            try
            {
                Menu tempMenu = clientMenus.FirstOrDefault(x => x.Caption == caption);
                if (tempMenu != null)
                {
                    menu.MenuName = tempMenu.MenuName;
                    clientMenus.Remove(tempMenu);
                }
                //var tempExceptionMenus = MenuException.MenuExceptions.FirstOrDefault(x => x.ClientNameLower == client.ToLower() && caption.Contains(x.OriginalMenuName));
                //if (tempExceptionMenus != null)
                //{
                //    CorrectExceptionalMenuCaptions(tempExceptionMenus, menu, clientMenus, client);
                //}
                var synonymException = SynonymExceptions.FirstOrDefault(x => x.ClientNameLower == client.ToLower());
                CorrectExceptionalMenuCaptions(synonymException, menu, clientMenus, client);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        //internal static void CorrectExceptionalMenuCaptions(MenuException exceptionalCaption, Menu menu, List<Menu> clientMenuList, string client)
        //{
        //    menu.Caption = menu.Caption.Replace(exceptionalCaption.OriginalMenuName, exceptionalCaption.ExceptionalMenuName);
        //    string caption = ConvertCaption(menu.Caption, client);
        //    var tempMenu = clientMenuList.First(x => x.Caption == caption);
        //    menu.MenuName = tempMenu.MenuName;
        //    clientMenuList.Remove(tempMenu);
        //}

        internal static void CorrectExceptionalMenuCaptions(SynonymException synonymException, Menu menu, List<Menu> clientMenuList, string client)
        {
            synonymException?.RegexReplacements.ForEach(x =>
            {
                menu.Caption = Regex.Replace(menu.Caption, x.Pattern, x.Replacement);
            });
            string caption = ConvertCaption(menu.Caption, client);
            var tempMenu = clientMenuList.FirstOrDefault(x => x.Caption == caption);
            if (tempMenu != null)
            {
                menu.MenuName = tempMenu.MenuName;
                clientMenuList.Remove(tempMenu);
            }
            else
            {
                menu.Caption = caption;
            }
        }

        internal static void LoadSynonymExceptions()
        {
            string fileLocation = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Resources", "SynonymExceptions.json");
            using (StreamReader r = new StreamReader(fileLocation))
            {
                string jsonString = r.ReadToEnd();
                SynonymExceptions = JsonConvert.DeserializeObject<List<SynonymException>>(jsonString);
            }
        }

        internal static void LoadClients()
        {
            string configLocation = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\ClientConfiguration", "ClientConfiguration.json");

            using (StreamReader r = new StreamReader(configLocation))
            {
                string jsonString = r.ReadToEnd();
                clients = JsonConvert.DeserializeObject<List<Clients>>(jsonString);
            }
        }
    }
}