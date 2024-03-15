using request = AutomationTesting_CorConnect.DMS.RequestObjects;
using RestSharp;
using System;
using System.IO;
using System.Xml.Serialization;
using AutomationTesting_CorConnect.DMS.Response;
using AutomationTesting_CorConnect.applicationContext;
using NUnit.Framework;
using System.Linq;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.DMS.RequestObjects;
using System.Collections.Generic;
using System.Resources;
using System.Drawing;
using TechTalk.SpecFlow;
using AutomationTesting_CorConnect.Constants;

namespace AutomationTesting_CorConnect.DMS
{
    internal class DMSServices
    {
        internal bool SubmitInvoice(string InvoiceNumber)
        {
            ClientConfiguration clients = ApplicationContext.GetInstance().ClientConfigurations.First(x => x.Client.ToLower() == CommonUtils.GetClientLower());
            string userType;
            string userName;
            if (TestContext.CurrentContext.Test.Properties["Type"].Count() > 0)
            {
                userType = TestContext.CurrentContext.Test.Properties["Type"]?.First().ToString();
                userName = TestContext.CurrentContext.Test.Properties["UserName"]?.First().ToString();
            }
            else
            {
                userType = ScenarioContext.Current["ActionResult"].ToString();
                userName = ScenarioContext.Current["UserName"].ToString();
            }
            var user = clients.Users.First(x => x.User.ToUpper() == userName.ToUpper());
            DateTime transactionDate = DateTime.Today;

            var processRequest = new request.ProcessRequest
            {
                UserName = user.User,
                Password = user.Password,
                CorRequest = new request.CorRequest
                {
                    CorRequestType = "S",
                    CorVendorCode = "d_poupd",
                    CorCustomerCode = "f_poupdcc",
                    CorCommunityCode = "DTN",
                    CorTransactionType = "P",
                    CorTransactionNumber = InvoiceNumber,
                    CorTransactionDate = transactionDate.ChangeDateFormat("yyyyMMdd"),
                    CorTransactionAmount = 2,
                    CorAuthorizationAmount = 72,
                    CorCurrencyCode = "USD",
                    CorPointOfSale = new request.CorPointOfSale(),
                    CorAccelerationTerms = new request.CorAccelerationTerms(),
                    CorReferences = new request.CorReferences(),
                    CorTaxes = new request.CorTaxes()
                    {
                        CorTax = new request.CorTax()
                        {
                            CorTaxType = "ST",
                            CorTaxAmount = 2,
                        }
                    },

                    CorSections = new request.CorSections()
                    {
                        CorSection = new System.Collections.Generic.List<request.CorSection> {
                        new request.CorSection
                        {
                            CorSectionNumber = "1",
                            CorComments = new request.CorComments(),
                            CorLineDetails = new request.CorLineDetails()
                        }
                        }
                    }

                }
            };

            using (var stringwriter = new request.Utf8StringWriter())
            {
                var serializer = new XmlSerializer(processRequest.GetType());
                serializer.Serialize(stringwriter, processRequest);
                var requestXML = stringwriter.ToString();

                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "text/xml");

                request.AddParameter("text/xml", requestXML, ParameterType.RequestBody);
                var client = new RestClient(clients.DMS);
                client.Timeout = -1;
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);

                using (StringReader reader = new StringReader(response.Content))
                {
                    var a = ((CorResponse)new XmlSerializer(typeof(CorResponse)).Deserialize(reader)).CorResponseStatusCode;

                    if (a == 3 || a == 2)
                    {
                        return true;
                    }

                    else return false;
                }
            }
        }

        internal bool SubmitPO(string InvoiceNumber, string dealerCode, string fleetCode)
        {
            ClientConfiguration clients = ApplicationContext.GetInstance().ClientConfigurations.First(x => x.Client.ToLower() == CommonUtils.GetClientLower());
            var userName = TestContext.CurrentContext.Test.Properties["UserName"]?.First().ToString();
            var user = clients.Users.First(x => x.User.ToUpper() == userName.ToUpper());
            DateTime transactionDate = DateTime.Today;

            var processRequest = new request.ProcessRequest
            {
                UserName = user.User,
                Password = user.Password,
                CorRequest = new request.CorRequest
                {
                    CorRequestType = "R",
                    CorVendorCode = dealerCode,
                    CorCustomerCode = fleetCode,
                    CorCommunityCode = "DTN",
                    CorTransactionType = "P",
                    CorTransactionNumber = InvoiceNumber,
                    CorTransactionDate = transactionDate.ChangeDateFormat("yyyyMMdd"),
                    CorTransactionAmount = 2,
                    CorAuthorizationAmount = 72,
                    CorCurrencyCode = "USD",
                    CorPointOfSale = new request.CorPointOfSale(),
                    CorAccelerationTerms = new request.CorAccelerationTerms(),
                    CorReferences = new request.CorReferences(),
                    CorTaxes = new request.CorTaxes(),
                    CorSections = new request.CorSections()
                    {
                        CorSection = new System.Collections.Generic.List<request.CorSection> {
                         new request.CorSection()
                        {
                            CorSectionNumber = "1",
                            CorComments = new request.CorComments(),
                            CorLineDetails = new request.CorLineDetails()
                            {
                                CorLineDetail = new System.Collections.Generic.List<request.CorLineDetail>
                             {
                                new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = 1,
                                    CorLineDetailType = "E",
                                    CorLineDetailItem = "Expense",
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = 1,
                                    CorLineDetailUnitPrice = 2.00,
                                    CorLineDetailCorePrice = 0.00,
                                    CorLineDetailFET = 0,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                }
                              }
                            }
                        }
                        }
                    }

                }
            };

            using (var stringwriter = new request.Utf8StringWriter())
            {
                var serializer = new XmlSerializer(processRequest.GetType());
                serializer.Serialize(stringwriter, processRequest);
                var requestXML = stringwriter.ToString();

                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "text/xml");

                request.AddParameter("text/xml", requestXML, ParameterType.RequestBody);
                var client = new RestClient(clients.DMS);
                client.Timeout = -1;
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);

                using (StringReader reader = new StringReader(response.Content))
                {
                    var a = ((CorResponse)new XmlSerializer(typeof(CorResponse)).Deserialize(reader)).CorResponseStatusCode;

                    if (a == 3 || a == 2)
                    {
                        return true;
                    }

                    else return false;
                }
            }
        }

        internal bool SubmitPOQ(string InvoiceNumber, string dealerCode, string fleetCode)
        {
            ClientConfiguration clients = ApplicationContext.GetInstance().ClientConfigurations.First(x => x.Client.ToLower() == CommonUtils.GetClientLower());
            var userName = TestContext.CurrentContext.Test.Properties["UserName"]?.First().ToString();
            var user = clients.Users.First(x => x.User.ToUpper() == userName.ToUpper());
            DateTime transactionDate = DateTime.Today;

            var processRequest = new request.ProcessRequest
            {
                UserName = user.User,
                Password = user.Password,
                CorRequest = new request.CorRequest
                {
                    CorRequestType = "Q",
                    CorVendorCode = dealerCode,
                    CorCustomerCode = fleetCode,
                    CorCommunityCode = "DTN",
                    CorTransactionType = "P",
                    CorTransactionNumber = InvoiceNumber,
                    CorTransactionDate = transactionDate.ChangeDateFormat("yyyyMMdd"),
                    CorTransactionAmount = 2,
                    CorAuthorizationAmount = 72,
                    CorCurrencyCode = "USD",
                    CorPointOfSale = new request.CorPointOfSale(),
                    CorAccelerationTerms = new request.CorAccelerationTerms(),
                    CorReferences = new request.CorReferences(),
                    CorTaxes = new request.CorTaxes(),
                    CorSections = new request.CorSections()
                    {
                        CorSection = new System.Collections.Generic.List<request.CorSection> {
                         new request.CorSection()
                        {
                            CorSectionNumber = "1",
                            CorComments = new request.CorComments(),
                            CorLineDetails = new request.CorLineDetails()
                            {
                                CorLineDetail = new System.Collections.Generic.List<request.CorLineDetail>
                             {
                                new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = 1,
                                    CorLineDetailType = "E",
                                    CorLineDetailItem = "Expense",
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = 1,
                                    CorLineDetailUnitPrice = 2.00,
                                    CorLineDetailCorePrice = 0.00,
                                    CorLineDetailFET = 0,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                }
                              }
                            }
                        }
                        }
                    }

                }
            };

            using (var stringwriter = new request.Utf8StringWriter())
            {
                var serializer = new XmlSerializer(processRequest.GetType());
                serializer.Serialize(stringwriter, processRequest);
                var requestXML = stringwriter.ToString();

                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "text/xml");

                request.AddParameter("text/xml", requestXML, ParameterType.RequestBody);
                var client = new RestClient(clients.DMS);
                client.Timeout = -1;
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);

                using (StringReader reader = new StringReader(response.Content))
                {
                    var a = ((CorResponse)new XmlSerializer(typeof(CorResponse)).Deserialize(reader)).CorResponseStatusCode;

                    if (a == 3 || a == 2)
                    {
                        return true;
                    }

                    else return false;
                }
            }
        }

        internal bool SubmitInvoice(string InvoiceNumber, string dealerCode, string fleetCode)
        {
            ClientConfiguration clients = ApplicationContext.GetInstance().ClientConfigurations.First(x => x.Client.ToLower() == CommonUtils.GetClientLower());
            string userName;

            if (TestContext.CurrentContext.Test.Properties["UserName"].Count() > 0)
            {
                userName = TestContext.CurrentContext.Test.Properties["UserName"]?.First().ToString();
            }
            else
            {
                userName = ScenarioContext.Current["UserName"].ToString().ToUpper();
            }
            
            var user = clients.Users.First(x => x.User.ToUpper() == userName.ToUpper());
            DateTime transactionDate = DateTime.Today;

            var processRequest = new request.ProcessRequest
            {
                UserName = user.User,
                Password = user.Password,
                CorRequest = new request.CorRequest
                {
                    CorRequestType = "S",
                    CorVendorCode = dealerCode,
                    CorCustomerCode = fleetCode,
                    CorCommunityCode = "DTN",
                    CorTransactionType = "P",
                    CorTransactionNumber = InvoiceNumber,
                    CorTransactionDate = transactionDate.ChangeDateFormat("yyyyMMdd"),
                    CorPurchaseOrderNumber = InvoiceNumber,
                    CorTransactionAmount = 2,
                    CorAuthorizationAmount = 72,
                    CorCurrencyCode = "USD",
                    CorPointOfSale = new request.CorPointOfSale(),
                    CorAccelerationTerms = new request.CorAccelerationTerms(),
                    CorReferences = new request.CorReferences(),
                    CorTaxes = new request.CorTaxes(),
                    CorSections = new request.CorSections()
                    {
                        CorSection = new System.Collections.Generic.List<request.CorSection> {
                        new request.CorSection()
                        {
                            CorSectionNumber = "1",
                            CorComments = new request.CorComments(),
                            CorLineDetails = new request.CorLineDetails()
                            {
                                CorLineDetail = new System.Collections.Generic.List<request.CorLineDetail>
                             {
                                new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = 1,
                                    CorLineDetailType = "E",
                                    CorLineDetailItem = "Expense",
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = 1,
                                    CorLineDetailUnitPrice = 2.00,
                                    CorLineDetailCorePrice = 0.00,
                                    CorLineDetailFET = 0,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                }
                              }
                            }
                        }
                        }
                    }

                }
            };

            using (var stringwriter = new request.Utf8StringWriter())
            {
                var serializer = new XmlSerializer(processRequest.GetType());
                serializer.Serialize(stringwriter, processRequest);
                var requestXML = stringwriter.ToString();

                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "text/xml");

                request.AddParameter("text/xml", requestXML, ParameterType.RequestBody);
                var client = new RestClient(clients.DMS);
                client.Timeout = -1;
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);

                using (StringReader reader = new StringReader(response.Content))
                {
                    var a = ((CorResponse)new XmlSerializer(typeof(CorResponse)).Deserialize(reader)).CorResponseStatusCode;

                    if (a == 3 || a == 2)
                    {
                        return true;
                    }

                    else return false;
                }
            }
        }

        internal bool SubmitInvoiceWithTransactionDate(string transactionType ,string InvoiceNumber, string dealerCode, string fleetCode , int days)
        {
            ClientConfiguration clients = ApplicationContext.GetInstance().ClientConfigurations.First(x => x.Client.ToLower() == CommonUtils.GetClientLower());
            string userName;            
            if (TestContext.CurrentContext.Test.Properties["UserName"].Count() > 0)
            {
                userName = TestContext.CurrentContext.Test.Properties["UserName"]?.First().ToString();
            }
            else
            {
                userName = ScenarioContext.Current["UserName"].ToString().ToUpper();
            }
            switch (transactionType) 
            {
                case "Parts":
                    transactionType = "P";
                    break;
                case "Service":
                    transactionType = "R";
                    break;
            }

            var user = clients.Users.First(x => x.User.ToUpper() == userName.ToUpper());

             DateTime transactionDate = Convert.ToDateTime(CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(days)));

            var processRequest = new request.ProcessRequest
            {
                UserName = user.User,
                Password = user.Password,
                CorRequest = new request.CorRequest
                {
                    CorRequestType = "S",
                    CorVendorCode = dealerCode,
                    CorCustomerCode = fleetCode,
                    CorCommunityCode = "DTN",
                    CorTransactionType = transactionType,
                    CorTransactionNumber = InvoiceNumber,
                    CorTransactionDate =transactionDate.ChangeDateFormat("yyyyMMdd"),
                    CorTransactionAmount = 2,
                    CorAuthorizationAmount = 72,
                    CorCurrencyCode = "USD",
                    CorPointOfSale = new request.CorPointOfSale(),
                    CorAccelerationTerms = new request.CorAccelerationTerms(),
                    CorReferences = new request.CorReferences(),
                    CorTaxes = new request.CorTaxes(),
                    CorSections = new request.CorSections()
                    {
                        CorSection = new System.Collections.Generic.List<request.CorSection> {
                        new request.CorSection()
                        {
                            CorSectionNumber = "1",
                            CorComments = new request.CorComments(),
                            CorLineDetails = new request.CorLineDetails()
                            {
                                CorLineDetail = new System.Collections.Generic.List<request.CorLineDetail>
                             {
                                new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = 1,
                                    CorLineDetailType = "E",
                                    CorLineDetailItem = "Expense",
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = 1,
                                    CorLineDetailUnitPrice = 2.00,
                                    CorLineDetailCorePrice = 0.00,
                                    CorLineDetailFET = 0,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                }
                              }
                            }
                        }
                        }
                    }

                }
            };

            using (var stringwriter = new request.Utf8StringWriter())
            {
                var serializer = new XmlSerializer(processRequest.GetType());
                serializer.Serialize(stringwriter, processRequest);
                var requestXML = stringwriter.ToString();

                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "text/xml");

                request.AddParameter("text/xml", requestXML, ParameterType.RequestBody);
                var client = new RestClient(clients.DMS);
                client.Timeout = -1;
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);

                using (StringReader reader = new StringReader(response.Content))
                {
                    var a = ((CorResponse)new XmlSerializer(typeof(CorResponse)).Deserialize(reader)).CorResponseStatusCode;

                    if (a == 3 || a == 2)
                    {
                        return true;
                    }

                    else return false;
                }
            }
        }

        internal bool SubmitInvoiceNotInBalance(string InvoiceNumber, string dealerCode, string fleetCode)
        {
            ClientConfiguration clients = ApplicationContext.GetInstance().ClientConfigurations.First(x => x.Client.ToLower() == CommonUtils.GetClientLower());
            string userName;

            if (TestContext.CurrentContext.Test.Properties["UserName"].Count() > 0)
            {
                userName = TestContext.CurrentContext.Test.Properties["UserName"]?.First().ToString();
            }
            else
            {
                userName = ScenarioContext.Current["UserName"].ToString().ToUpper();
            }

            var user = clients.Users.First(x => x.User.ToUpper() == userName.ToUpper());
            DateTime transactionDate = DateTime.Today;

            var processRequest = new request.ProcessRequest
            {
                UserName = user.User,
                Password = user.Password,
                CorRequest = new request.CorRequest
                {
                    CorRequestType = "S",
                    CorVendorCode = dealerCode,
                    CorCustomerCode = fleetCode,
                    CorCommunityCode = clients.CommunityCode,
                    CorTransactionType = "P",
                    CorTransactionNumber = InvoiceNumber,
                    CorTransactionDate = transactionDate.ChangeDateFormat("yyyyMMdd"),
                    CorTransactionAmount = 2,
                    CorAuthorizationAmount = 2,
                    CorCurrencyCode = "USD",
                    CorPointOfSale = new request.CorPointOfSale(),
                    CorAccelerationTerms = new request.CorAccelerationTerms(),
                    CorReferences = new request.CorReferences(),
                    CorTaxes = new request.CorTaxes(),
                    CorSections = new request.CorSections()
                    {
                        CorSection = new System.Collections.Generic.List<request.CorSection> {
                        new request.CorSection()
                        {
                            CorSectionNumber = "1",
                            CorComments = new request.CorComments(),
                            CorLineDetails = new request.CorLineDetails()
                            {
                                CorLineDetail = new System.Collections.Generic.List<request.CorLineDetail>
                             {
                                new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = 1,
                                    CorLineDetailType = "P",
                                    CorLineDetailItem = "Parts",
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = 1,
                                    CorLineDetailUnitPrice = 200.00,
                                    CorLineDetailCorePrice = 0.00,
                                    CorLineDetailFET = 0,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                }
                              }
                            }
                        }
                        }
                    }

                }
            };

            using (var stringwriter = new request.Utf8StringWriter())
            {
                var serializer = new XmlSerializer(processRequest.GetType());
                serializer.Serialize(stringwriter, processRequest);
                var requestXML = stringwriter.ToString();

                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "text/xml");

                request.AddParameter("text/xml", requestXML, ParameterType.RequestBody);
                var client = new RestClient(clients.DMS);
                client.Timeout = -1;
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);

                using (StringReader reader = new StringReader(response.Content))
                {
                    var a = ((CorResponse)new XmlSerializer(typeof(CorResponse)).Deserialize(reader)).CorResponseStatusCode;

                    if (a == 3 || a == 2)
                    {
                        return true;
                    }

                    else return false;
                }
            }
        }

        internal bool SubmitInvoiceWithTransactionAmount(string InvoiceNumber, string dealerCode, string fleetCode , double transactionAmount , int quantity)
        {
            ClientConfiguration clients = ApplicationContext.GetInstance().ClientConfigurations.First(x => x.Client.ToLower() == CommonUtils.GetClientLower());
            string userName;
            string lineItemPrice = Convert.ToString(transactionAmount);
            double unitPrice = transactionAmount;
            if (lineItemPrice.Contains("-")) 
            { 
             lineItemPrice = Convert.ToString(transactionAmount).Replace("-", "");
             unitPrice = Convert.ToDouble(lineItemPrice);
            }
            if (TestContext.CurrentContext.Test.Properties["UserName"].Count() > 0)
            {
                userName = TestContext.CurrentContext.Test.Properties["UserName"]?.First().ToString();
            }
            else
            {
                userName = ScenarioContext.Current["UserName"].ToString().ToUpper();
            }

            var user = clients.Users.First(x => x.User.ToUpper() == userName.ToUpper());
            DateTime transactionDate = DateTime.Today;

            var processRequest = new request.ProcessRequest
            {
                UserName = user.User,
                Password = user.Password,
                CorRequest = new request.CorRequest
                {
                    CorRequestType = "S",
                    CorVendorCode = dealerCode,
                    CorCustomerCode = fleetCode,
                    CorCommunityCode = clients.CommunityCode,
                    CorTransactionType = "P",
                    CorTransactionNumber = InvoiceNumber,
                    CorTransactionDate = transactionDate.ChangeDateFormat("yyyyMMdd"),
                    CorTransactionAmount = transactionAmount,
                    CorAuthorizationAmount = transactionAmount,
                    CorCurrencyCode = "USD",
                    CorPointOfSale = new request.CorPointOfSale(),
                    CorAccelerationTerms = new request.CorAccelerationTerms(),
                    CorReferences = new request.CorReferences(),
                    CorTaxes = new request.CorTaxes(),
                    CorSections = new request.CorSections()
                    {
                        CorSection = new System.Collections.Generic.List<request.CorSection> {
                        new request.CorSection()
                        {
                            CorSectionNumber = "1",
                            CorComments = new request.CorComments(),
                            CorLineDetails = new request.CorLineDetails()
                            {
                                CorLineDetail = new System.Collections.Generic.List<request.CorLineDetail>
                             {
                                new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = 1,
                                    CorLineDetailType = "P",
                                    CorLineDetailItem = "Parts",
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = quantity,
                                    CorLineDetailUnitPrice = unitPrice,
                                    CorLineDetailCorePrice = 0.00,
                                    CorLineDetailFET = 0,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                }
                              }
                            }
                        }
                        }
                    }

                }
            };

            using (var stringwriter = new request.Utf8StringWriter())
            {
                var serializer = new XmlSerializer(processRequest.GetType());
                serializer.Serialize(stringwriter, processRequest);
                var requestXML = stringwriter.ToString();

                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "text/xml");

                request.AddParameter("text/xml", requestXML, ParameterType.RequestBody);
                var client = new RestClient(clients.DMS);
                client.Timeout = -1;
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);

                using (StringReader reader = new StringReader(response.Content))
                {
                    var a = ((CorResponse)new XmlSerializer(typeof(CorResponse)).Deserialize(reader)).CorResponseStatusCode;

                    if (a == 3 || a == 2)
                    {
                        return true;
                    }

                    else return false;
                }
            }
        }

        internal bool SubmitInvoiceWithTransactionAmountAndType(string InvoiceNumber, string dealerCode, string fleetCode, double transactionAmount, int quantity, string invoiceType)
        {
            ClientConfiguration clients = ApplicationContext.GetInstance().ClientConfigurations.First(x => x.Client.ToLower() == CommonUtils.GetClientLower());
            string userName;
            string lineItemPrice = Convert.ToString(transactionAmount);
            double unitPrice = transactionAmount;
            if (lineItemPrice.Contains("-"))
            {
                lineItemPrice = Convert.ToString(transactionAmount).Replace("-", "");
                unitPrice = Convert.ToDouble(lineItemPrice);
            }
            if (TestContext.CurrentContext.Test.Properties["UserName"].Count() > 0)
            {
                userName = TestContext.CurrentContext.Test.Properties["UserName"]?.First().ToString();
            }
            else
            {
                userName = ScenarioContext.Current["UserName"].ToString().ToUpper();
            }

            var user = clients.Users.First(x => x.User.ToUpper() == userName.ToUpper());
            DateTime transactionDate = DateTime.Today;

            var processRequest = new request.ProcessRequest
            {
                UserName = user.User,
                Password = user.Password,
                CorRequest = new request.CorRequest
                {
                    CorRequestType = "S",
                    CorVendorCode = dealerCode,
                    CorCustomerCode = fleetCode,
                    CorCommunityCode = clients.CommunityCode,
                    CorTransactionType = invoiceType,
                    CorTransactionNumber = InvoiceNumber,
                    CorTransactionDate = transactionDate.ChangeDateFormat("yyyyMMdd"),
                    CorTransactionAmount = transactionAmount,
                    CorAuthorizationAmount = transactionAmount,
                    CorCurrencyCode = "USD",
                    CorPointOfSale = new request.CorPointOfSale(),
                    CorAccelerationTerms = new request.CorAccelerationTerms(),
                    CorReferences = new request.CorReferences(),
                    CorTaxes = new request.CorTaxes(),
                    CorSections = new request.CorSections()
                    {
                        CorSection = new System.Collections.Generic.List<request.CorSection> {
                        new request.CorSection()
                        {
                            CorSectionNumber = "1",
                            CorComments = new request.CorComments(),
                            CorLineDetails = new request.CorLineDetails()
                            {
                                CorLineDetail = new System.Collections.Generic.List<request.CorLineDetail>
                             {
                                new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = 1,
                                    CorLineDetailType = "P",
                                    CorLineDetailItem = "Parts",
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = quantity,
                                    CorLineDetailUnitPrice = unitPrice,
                                    CorLineDetailCorePrice = 0.00,
                                    CorLineDetailFET = 0,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                }
                              }
                            }
                        }
                        }
                    }

                }
            };

            using (var stringwriter = new request.Utf8StringWriter())
            {
                var serializer = new XmlSerializer(processRequest.GetType());
                serializer.Serialize(stringwriter, processRequest);
                var requestXML = stringwriter.ToString();

                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "text/xml");

                request.AddParameter("text/xml", requestXML, ParameterType.RequestBody);
                var client = new RestClient(clients.DMS);
                client.Timeout = -1;
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);

                using (StringReader reader = new StringReader(response.Content))
                {
                    var a = ((CorResponse)new XmlSerializer(typeof(CorResponse)).Deserialize(reader)).CorResponseStatusCode;

                    if (a == 3 || a == 2)
                    {
                        return true;
                    }

                    else return false;
                }
            }
        }

        internal bool SubmitInvWithTransactionAmountAndDate(string InvoiceNumber, string dealerCode, string fleetCode, double transactionAmount, int days)
        {
            ClientConfiguration clients = ApplicationContext.GetInstance().ClientConfigurations.First(x => x.Client.ToLower() == CommonUtils.GetClientLower());
            string userName;
            string lineItemPrice = Convert.ToString(transactionAmount);
            double unitPrice = transactionAmount;
            if (lineItemPrice.Contains("-"))
            {
                lineItemPrice = Convert.ToString(transactionAmount).Replace("-", "");
                unitPrice = Convert.ToDouble(lineItemPrice);
            }
            if (TestContext.CurrentContext.Test.Properties["UserName"].Count() > 0)
            {
                userName = TestContext.CurrentContext.Test.Properties["UserName"]?.First().ToString();
            }
            else
            {
                userName = ScenarioContext.Current["UserName"].ToString().ToUpper();
            }

            var user = clients.Users.First(x => x.User.ToUpper() == userName.ToUpper());
            DateTime transactionDate = Convert.ToDateTime(CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(days)));

            var processRequest = new request.ProcessRequest
            {
                UserName = user.User,
                Password = user.Password,
                CorRequest = new request.CorRequest
                {
                    CorRequestType = "S",
                    CorVendorCode = dealerCode,
                    CorCustomerCode = fleetCode,
                    CorCommunityCode = clients.CommunityCode,
                    CorTransactionType = "P",
                    CorTransactionNumber = InvoiceNumber,
                    CorTransactionDate = transactionDate.ChangeDateFormat("yyyyMMdd"),
                    CorTransactionAmount = transactionAmount,
                    CorAuthorizationAmount = transactionAmount,
                    CorCurrencyCode = "USD",
                    CorPointOfSale = new request.CorPointOfSale(),
                    CorAccelerationTerms = new request.CorAccelerationTerms(),
                    CorReferences = new request.CorReferences(),
                    CorTaxes = new request.CorTaxes(),
                    CorSections = new request.CorSections()
                    {
                        CorSection = new System.Collections.Generic.List<request.CorSection> {
                        new request.CorSection()
                        {
                            CorSectionNumber = "1",
                            CorComments = new request.CorComments(),
                            CorLineDetails = new request.CorLineDetails()
                            {
                                CorLineDetail = new System.Collections.Generic.List<request.CorLineDetail>
                             {
                                new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = 1,
                                    CorLineDetailType = "P",
                                    CorLineDetailItem = "Parts",
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = 1,
                                    CorLineDetailUnitPrice = unitPrice,
                                    CorLineDetailCorePrice = 0.00,
                                    CorLineDetailFET = 0,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                }
                              }
                            }
                        }
                        }
                    }

                }
            };

            using (var stringwriter = new request.Utf8StringWriter())
            {
                var serializer = new XmlSerializer(processRequest.GetType());
                serializer.Serialize(stringwriter, processRequest);
                var requestXML = stringwriter.ToString();

                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "text/xml");

                request.AddParameter("text/xml", requestXML, ParameterType.RequestBody);
                var client = new RestClient(clients.DMS);
                client.Timeout = -1;
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);

                using (StringReader reader = new StringReader(response.Content))
                {
                    var a = ((CorResponse)new XmlSerializer(typeof(CorResponse)).Deserialize(reader)).CorResponseStatusCode;

                    if (a == 3 || a == 2)
                    {
                        return true;
                    }

                    else return false;
                }
            }
        }

        internal bool SubmitInvoiceWithAllNonPartItems(string InvoiceNumber, string dealerCode, string fleetCode, DateTime transactionDate)
        {
            ClientConfiguration clients = ApplicationContext.GetInstance().ClientConfigurations.First(x => x.Client.ToLower() == CommonUtils.GetClientLower());
            var userName = TestContext.CurrentContext.Test.Properties["UserName"]?.First().ToString();
            var user = clients.Users.First(x => x.User.ToUpper() == userName.ToUpper());

            var processRequest = new request.ProcessRequest
            {
                UserName = user.User,
                Password = user.Password,
                CorRequest = new request.CorRequest
                {
                    CorRequestID = 2365864354,
                    CorRequestType = "S",
                    CorVendorCode = dealerCode,
                    CorCustomerCode = fleetCode,
                    CorCommunityCode = "DTN",
                    CorTransactionType = "P",
                    CorTransactionNumber = InvoiceNumber,
                    CorTransactionDate = transactionDate.ChangeDateFormat("yyyyMMdd"),
                    CorPurchaseOrderNumber = "abc",
                    CorTransactionAmount = 257.00,
                    CorAuthorizationAmount = 00.00,
                    CorCurrencyCode = "USD",
                    CorPointOfSale = new request.CorPointOfSale(),
                    CorAccelerationTerms = new request.CorAccelerationTerms()
                    {
                        CorAccelerationProgramID = 237
                    },
                    CorReferences = new request.CorReferences(),
                    CorTaxes = new request.CorTaxes()
                    {
                        CorTax = new request.CorTax()
                        {
                            CorTaxType = "ST",
                            CorTaxAmount = 0,
                            CorTaxID = "001",
                        }
                    },

                    CorSections = new request.CorSections()
                    {
                        CorSection = new System.Collections.Generic.List<request.CorSection> {
                        new request.CorSection()
                        {
                            CorSectionNumber = "1",
                            CorComments = new request.CorComments(),
                            CorLineDetails = new request.CorLineDetails()
                            {
                                CorLineDetail = new System.Collections.Generic.List<request.CorLineDetail>
                             {
                               new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = 1,
                                    CorLineDetailType = "B",
                                    CorLineDetailItem = "Sublet Part",
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = 1,
                                    CorLineDetailUnitPrice = 10.00,
                                    CorLineDetailCorePrice = 5.00,
                                    CorLineDetailFET = 5,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                },
                                new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = 2,
                                    CorLineDetailType = "E",
                                    CorLineDetailItem = "Expense",
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = 1,
                                    CorLineDetailUnitPrice = 10.00,
                                    CorLineDetailCorePrice = 5.00,
                                    CorLineDetailFET = 5,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                },
                                new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = 3,
                                    CorLineDetailType = "F",
                                    CorLineDetailItem = "Frieght",
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = 1,
                                    CorLineDetailUnitPrice = 10.00,
                                    CorLineDetailCorePrice = 3.00,
                                    CorLineDetailFET = 5,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                },
                                new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = 4,
                                    CorLineDetailType = "G",
                                    CorLineDetailItem = "Fuel",
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = 1,
                                    CorLineDetailUnitPrice = 11.00,
                                    CorLineDetailCorePrice = 3.00,
                                    CorLineDetailFET = 5,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                },
                                  new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = 5,
                                    CorLineDetailType = "L",
                                    CorLineDetailItem = "Labor",
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = 1,
                                    CorLineDetailUnitPrice = 10.00,
                                    CorLineDetailCorePrice = 4.00,
                                    CorLineDetailFET = 5,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                },
                                   new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = 6,
                                    CorLineDetailType = "M",
                                    CorLineDetailItem = "Miscellaneous",
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = 1,
                                    CorLineDetailUnitPrice = 10.00,
                                    CorLineDetailCorePrice = 4.00,
                                    CorLineDetailFET = 5,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                },
                                new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = 7,
                                    CorLineDetailType = "N",
                                    CorLineDetailItem = "Environment",
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = 1,
                                    CorLineDetailUnitPrice = 5.00,
                                    CorLineDetailCorePrice = 10.00,
                                    CorLineDetailFET = 5,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                },
                                new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = 8,
                                    CorLineDetailType = "R",
                                    CorLineDetailItem = "Rental",
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = 1,
                                    CorLineDetailUnitPrice = 8.00,
                                    CorLineDetailCorePrice = 2.00,
                                    CorLineDetailFET = 5,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                },
                                new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = 9,
                                    CorLineDetailType = "S",
                                    CorLineDetailItem = "Shop suplies",
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = 1,
                                    CorLineDetailUnitPrice = 12.00,
                                    CorLineDetailCorePrice = 5.00,
                                    CorLineDetailFET = 5,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                },
                                new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = 10,
                                    CorLineDetailType = "T",
                                    CorLineDetailItem = "Tax",
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = 1,
                                    CorLineDetailUnitPrice = 7.00,
                                    CorLineDetailCorePrice = 3.00,
                                    CorLineDetailFET = 5,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                },
                                  new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = 11,
                                    CorLineDetailType = "U",
                                    CorLineDetailItem = "Sublet Labor",
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = 1,
                                    CorLineDetailUnitPrice = 20.00,
                                    CorLineDetailCorePrice = 5.00,
                                    CorLineDetailFET = 5,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                },
                                   new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = 12,
                                    CorLineDetailType = "V",
                                    CorLineDetailItem = "Variable",
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = 1,
                                    CorLineDetailUnitPrice = 10.00,
                                    CorLineDetailCorePrice = 5.00,
                                    CorLineDetailFET = 5,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                },
                                    new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = 13,
                                    CorLineDetailType = "X",
                                    CorLineDetailItem = "Fixed",
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = 1,
                                    CorLineDetailUnitPrice = 10.00,
                                    CorLineDetailCorePrice = 5.00,
                                    CorLineDetailFET = 5,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                }

                              }
                            }
                        }
                        }
                    }

                }
            };

            using (var stringwriter = new request.Utf8StringWriter())
            {
                var serializer = new XmlSerializer(processRequest.GetType());
                serializer.Serialize(stringwriter, processRequest);
                var requestXML = stringwriter.ToString();

                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "text/xml");

                request.AddParameter("text/xml", requestXML, ParameterType.RequestBody);
                var client = new RestClient(clients.DMS);
                client.Timeout = -1;
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);

                using (StringReader reader = new StringReader(response.Content))
                {
                    var a = ((CorResponse)new XmlSerializer(typeof(CorResponse)).Deserialize(reader)).CorResponseStatusCode;

                    if (a == 3 || a == 2)
                    {
                        return true;
                    }

                    else return false;
                }
            }
        }

        internal bool SubmitInvoiceWithNonPartItems(string InvoiceNumber, string dealerCode, string fleetCode, DateTime transactionDate)
        {
            ClientConfiguration clients = ApplicationContext.GetInstance().ClientConfigurations.First(x => x.Client.ToLower() == CommonUtils.GetClientLower());
            var userName = TestContext.CurrentContext.Test.Properties["UserName"]?.First().ToString();
            var user = clients.Users.First(x => x.User.ToUpper() == userName.ToUpper());

            var processRequest = new request.ProcessRequest
            {
                UserName = user.User,
                Password = user.Password,
                CorRequest = new request.CorRequest
                {
                    CorRequestID = 2365864354,
                    CorRequestType = "S",
                    CorVendorCode = dealerCode,
                    CorCustomerCode = fleetCode,
                    CorCommunityCode = "DTN",
                    CorTransactionType = "P",
                    CorTransactionNumber = InvoiceNumber,
                    CorTransactionDate = transactionDate.ChangeDateFormat("yyyyMMdd"),
                    CorPurchaseOrderNumber = "abc",
                    CorTransactionAmount = 148.00,
                    CorAuthorizationAmount = 00.00,
                    CorCurrencyCode = "USD",
                    CorPointOfSale = new request.CorPointOfSale(),
                    CorAccelerationTerms = new request.CorAccelerationTerms()
                    {
                        CorAccelerationProgramID = 237
                    },
                    CorReferences = new request.CorReferences(),
                    CorTaxes = new request.CorTaxes()
                    {
                        CorTax = new request.CorTax()
                        {
                            CorTaxType = "ST",
                            CorTaxAmount = 0,
                            CorTaxID = "001",
                        }
                    },

                    CorSections = new request.CorSections()
                    {
                        CorSection = new System.Collections.Generic.List<request.CorSection> {
                        new request.CorSection()
                        {
                            CorSectionNumber = "1",
                            CorComments = new request.CorComments(),
                            CorLineDetails = new request.CorLineDetails()
                            {
                                CorLineDetail = new System.Collections.Generic.List<request.CorLineDetail>
                             {

                                new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = 2,
                                    CorLineDetailType = "E",
                                    CorLineDetailItem = "Expense",
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = 1,
                                    CorLineDetailUnitPrice = 10.00,
                                    CorLineDetailCorePrice = 5.00,
                                    CorLineDetailFET = 5,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                },
                                new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = 4,
                                    CorLineDetailType = "G",
                                    CorLineDetailItem = "Fuel",
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = 1,
                                    CorLineDetailUnitPrice = 11.00,
                                    CorLineDetailCorePrice = 3.00,
                                    CorLineDetailFET = 5,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                },
                                   new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = 6,
                                    CorLineDetailType = "M",
                                    CorLineDetailItem = "Miscellaneous",
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = 1,
                                    CorLineDetailUnitPrice = 10.00,
                                    CorLineDetailCorePrice = 4.00,
                                    CorLineDetailFET = 5,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                },
                                new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = 7,
                                    CorLineDetailType = "N",
                                    CorLineDetailItem = "Environment",
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = 1,
                                    CorLineDetailUnitPrice = 5.00,
                                    CorLineDetailCorePrice = 10.00,
                                    CorLineDetailFET = 5,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                },
                                new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = 8,
                                    CorLineDetailType = "R",
                                    CorLineDetailItem = "Rental",
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = 1,
                                    CorLineDetailUnitPrice = 8.00,
                                    CorLineDetailCorePrice = 2.00,
                                    CorLineDetailFET = 5,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                },

                                new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = 10,
                                    CorLineDetailType = "T",
                                    CorLineDetailItem = "Tax",
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = 1,
                                    CorLineDetailUnitPrice = 7.00,
                                    CorLineDetailCorePrice = 3.00,
                                    CorLineDetailFET = 5,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                },
                                   new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = 12,
                                    CorLineDetailType = "V",
                                    CorLineDetailItem = "Variable",
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = 1,
                                    CorLineDetailUnitPrice = 10.00,
                                    CorLineDetailCorePrice = 5.00,
                                    CorLineDetailFET = 5,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                },
                                    new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = 13,
                                    CorLineDetailType = "X",
                                    CorLineDetailItem = "Fixed",
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = 1,
                                    CorLineDetailUnitPrice = 10.00,
                                    CorLineDetailCorePrice = 5.00,
                                    CorLineDetailFET = 5,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                }

                              }
                            }
                        }
                        }
                    }

                }
            };

            using (var stringwriter = new request.Utf8StringWriter())
            {
                var serializer = new XmlSerializer(processRequest.GetType());
                serializer.Serialize(stringwriter, processRequest);
                var requestXML = stringwriter.ToString();

                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "text/xml");

                request.AddParameter("text/xml", requestXML, ParameterType.RequestBody);
                var client = new RestClient(clients.DMS);
                client.Timeout = -1;
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);

                using (StringReader reader = new StringReader(response.Content))
                {
                    var a = ((CorResponse)new XmlSerializer(typeof(CorResponse)).Deserialize(reader)).CorResponseStatusCode;

                    if (a == 3 || a == 2)
                    {
                        return true;
                    }

                    else return false;
                }
            }
        }

        internal bool SubmitInvoiceWithLineItemType(string InvoiceNumber, string dealerCode, string fleetCode, DateTime transactionDate, string typeCode, string typeName)
        {
            ClientConfiguration clients = ApplicationContext.GetInstance().ClientConfigurations.First(x => x.Client.ToLower() == CommonUtils.GetClientLower());
            var userName = TestContext.CurrentContext.Test.Properties["UserName"]?.First().ToString();
            var user = clients.Users.First(x => x.User.ToUpper() == userName.ToUpper());

            var processRequest = new request.ProcessRequest
            {
                UserName = user.User,
                Password = user.Password,
                CorRequest = new request.CorRequest
                {
                    CorRequestID = 2365864354,
                    CorRequestType = "S",
                    CorVendorCode = dealerCode,
                    CorCustomerCode = fleetCode,
                    CorCommunityCode = "DTN",
                    CorTransactionType = "P",
                    CorTransactionNumber = InvoiceNumber,
                    CorTransactionDate = transactionDate.ChangeDateFormat("yyyyMMdd"),
                    CorPurchaseOrderNumber = "abc",
                    CorTransactionAmount = 20.00,
                    CorAuthorizationAmount = 00.00,
                    CorCurrencyCode = "USD",
                    CorPointOfSale = new request.CorPointOfSale(),
                    CorAccelerationTerms = new request.CorAccelerationTerms()
                    {
                        CorAccelerationProgramID = 237
                    },
                    CorReferences = new request.CorReferences(),
                    CorTaxes = new request.CorTaxes()
                    {
                        CorTax = new request.CorTax()
                        {
                            CorTaxType = "ST",
                            CorTaxAmount = 0,
                            CorTaxID = "001",
                        }
                    },

                    CorSections = new request.CorSections()
                    {
                        CorSection = new System.Collections.Generic.List<request.CorSection> {
                        new request.CorSection()
                        {
                            CorSectionNumber = "1",
                            CorComments = new request.CorComments(),
                            CorLineDetails = new request.CorLineDetails()
                            {
                                CorLineDetail = new System.Collections.Generic.List<request.CorLineDetail>
                             {
                                new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = 3,
                                    CorLineDetailType = typeCode,
                                    CorLineDetailItem = typeName,
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = 1,
                                    CorLineDetailUnitPrice = 10.00,
                                    CorLineDetailCorePrice = 5.00,
                                    CorLineDetailFET = 5,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                }
                              }
                            }
                        }
                        }
                    }

                }
            };

            using (var stringwriter = new request.Utf8StringWriter())
            {
                var serializer = new XmlSerializer(processRequest.GetType());
                serializer.Serialize(stringwriter, processRequest);
                var requestXML = stringwriter.ToString();

                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "text/xml");

                request.AddParameter("text/xml", requestXML, ParameterType.RequestBody);
                var client = new RestClient(clients.DMS);
                client.Timeout = -1;
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);

                using (StringReader reader = new StringReader(response.Content))
                {
                    var a = ((CorResponse)new XmlSerializer(typeof(CorResponse)).Deserialize(reader)).CorResponseStatusCode;

                    if (a == 3 || a == 2)
                    {
                        return true;
                    }

                    else return false;
                }
            }
        }

        internal bool SubmitInvoiceWithLineSubletItemTypes(string InvoiceNumber, string dealerCode, string fleetCode, DateTime transactionDate)
        {
            ClientConfiguration clients = ApplicationContext.GetInstance().ClientConfigurations.First(x => x.Client.ToLower() == CommonUtils.GetClientLower());
            var userName = TestContext.CurrentContext.Test.Properties["UserName"]?.First().ToString();
            var user = clients.Users.First(x => x.User.ToUpper() == userName.ToUpper());

            var processRequest = new request.ProcessRequest
            {
                UserName = user.User,
                Password = user.Password,
                CorRequest = new request.CorRequest
                {
                    CorRequestID = 2365864354,
                    CorRequestType = "S",
                    CorVendorCode = dealerCode,
                    CorCustomerCode = fleetCode,
                    CorCommunityCode = "DTN",
                    CorTransactionType = "P",
                    CorTransactionNumber = InvoiceNumber,
                    CorTransactionDate = transactionDate.ChangeDateFormat("yyyyMMdd"),
                    CorPurchaseOrderNumber = "abc",
                    CorTransactionAmount = 50.00,
                    CorAuthorizationAmount = 00.00,
                    CorCurrencyCode = "USD",
                    CorPointOfSale = new request.CorPointOfSale(),
                    CorAccelerationTerms = new request.CorAccelerationTerms()
                    {
                        CorAccelerationProgramID = 237
                    },
                    CorReferences = new request.CorReferences(),
                    CorTaxes = new request.CorTaxes()
                    {
                        CorTax = new request.CorTax()
                        {
                            CorTaxType = "ST",
                            CorTaxAmount = 0,
                            CorTaxID = "001",
                        }
                    },

                    CorSections = new request.CorSections()
                    {
                        CorSection = new System.Collections.Generic.List<request.CorSection> {
                        new request.CorSection()
                        {
                            CorSectionNumber = "1",
                            CorComments = new request.CorComments(),
                            CorLineDetails = new request.CorLineDetails()
                            {
                                CorLineDetail = new System.Collections.Generic.List<request.CorLineDetail>
                             {
                                new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = 1,
                                    CorLineDetailType = "B",
                                    CorLineDetailItem = "Sublet Part",
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = 1,
                                    CorLineDetailUnitPrice = 10.00,
                                    CorLineDetailCorePrice = 5.00,
                                    CorLineDetailFET = 5,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                },
                                new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = 2,
                                    CorLineDetailType = "U",
                                    CorLineDetailItem = "Sublet Labor",
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = 1,
                                    CorLineDetailUnitPrice = 20.00,
                                    CorLineDetailCorePrice = 5.00,
                                    CorLineDetailFET = 5,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                }
                              }
                            }
                        }
                        }
                    }

                }
            };

            using (var stringwriter = new request.Utf8StringWriter())
            {
                var serializer = new XmlSerializer(processRequest.GetType());
                serializer.Serialize(stringwriter, processRequest);
                var requestXML = stringwriter.ToString();

                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "text/xml");

                request.AddParameter("text/xml", requestXML, ParameterType.RequestBody);
                var client = new RestClient(clients.DMS);
                client.Timeout = -1;
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);

                using (StringReader reader = new StringReader(response.Content))
                {
                    var a = ((CorResponse)new XmlSerializer(typeof(CorResponse)).Deserialize(reader)).CorResponseStatusCode;

                    if (a == 3 || a == 2)
                    {
                        return true;
                    }

                    else return false;
                }
            }
        }

        internal bool SubmitInvoiceWithPartItems(string InvoiceNumber, string dealerCode, string fleetCode, DateTime transactionDate, string enginePart, string propPart, string vendorPart, string unrecPart)
        {
            ClientConfiguration clients = ApplicationContext.GetInstance().ClientConfigurations.First(x => x.Client.ToLower() == CommonUtils.GetClientLower());
            var userName = TestContext.CurrentContext.Test.Properties["UserName"]?.First().ToString();
            var user = clients.Users.First(x => x.User.ToUpper() == userName.ToUpper());

            var processRequest = new request.ProcessRequest
            {
                UserName = user.User,
                Password = user.Password,
                CorRequest = new request.CorRequest
                {
                    CorRequestID = 2365864354,
                    CorRequestType = "S",
                    CorVendorCode = dealerCode,
                    CorCustomerCode = fleetCode,
                    CorCommunityCode = "DTN",
                    CorTransactionType = "P",
                    CorTransactionNumber = InvoiceNumber,
                    CorTransactionDate = transactionDate.ChangeDateFormat("yyyyMMdd"),
                    CorPurchaseOrderNumber = "abc",
                    CorTransactionAmount = 96.00,
                    CorAuthorizationAmount = 00.00,
                    CorCurrencyCode = "USD",
                    CorPointOfSale = new request.CorPointOfSale(),
                    CorAccelerationTerms = new request.CorAccelerationTerms()
                    {
                        CorAccelerationProgramID = 237
                    },
                    CorReferences = new request.CorReferences(),
                    CorTaxes = new request.CorTaxes()
                    {
                        CorTax = new request.CorTax()
                        {
                            CorTaxType = "ST",
                            CorTaxAmount = 0,
                            CorTaxID = "001",
                        }
                    },

                    CorSections = new request.CorSections()
                    {
                        CorSection = new System.Collections.Generic.List<request.CorSection> {
                        new request.CorSection()
                        {
                            CorSectionNumber = "1",
                            CorComments = new request.CorComments(),
                            CorLineDetails = new request.CorLineDetails()
                            {
                                CorLineDetail = new System.Collections.Generic.List<request.CorLineDetail>
                             {
                               new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = 1,
                                    CorLineDetailType = "P",
                                    CorLineDetailItem = enginePart,
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = 1,
                                    CorLineDetailUnitPrice = 10.00,
                                    CorLineDetailCorePrice = 5.00,
                                    CorLineDetailFET = 5,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                },
                                new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = 2,
                                    CorLineDetailType = "P",
                                    CorLineDetailItem = vendorPart,
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = 1,
                                    CorLineDetailUnitPrice = 10.00,
                                    CorLineDetailCorePrice = 5.00,
                                    CorLineDetailFET = 5,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                },
                                new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = 3,
                                    CorLineDetailType = "P",
                                    CorLineDetailItem = propPart,
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = 1,
                                    CorLineDetailUnitPrice = 10.00,
                                    CorLineDetailCorePrice = 3.00,
                                    CorLineDetailFET = 5,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                },
                                new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = 4,
                                    CorLineDetailType = "P",
                                    CorLineDetailItem = unrecPart,
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = 1,
                                    CorLineDetailUnitPrice = 11.00,
                                    CorLineDetailCorePrice = 3.00,
                                    CorLineDetailFET = 5,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                },
                                  new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = 5,
                                    CorLineDetailType = "P",
                                    CorLineDetailItem = "152pJytesh",
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = 1,
                                    CorLineDetailUnitPrice = 10.00,
                                    CorLineDetailCorePrice = 4.00,
                                    CorLineDetailFET = 5,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                }
                              }
                            }
                        }
                        }
                    }

                }
            };

            using (var stringwriter = new request.Utf8StringWriter())
            {
                var serializer = new XmlSerializer(processRequest.GetType());
                serializer.Serialize(stringwriter, processRequest);
                var requestXML = stringwriter.ToString();

                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "text/xml");

                request.AddParameter("text/xml", requestXML, ParameterType.RequestBody);
                var client = new RestClient(clients.DMS);
                client.Timeout = -1;
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);

                using (StringReader reader = new StringReader(response.Content))
                {
                    var a = ((CorResponse)new XmlSerializer(typeof(CorResponse)).Deserialize(reader)).CorResponseStatusCode;

                    if (a == 3 || a == 2)
                    {
                        return true;
                    }

                    else return false;
                }
            }
        }

        internal bool SubmitInvoiceWithPartItemsRecognized(string InvoiceNumber, string dealerCode, string fleetCode, DateTime transactionDate, string enginePart, string propPart, string vendorPart)
        {
            ClientConfiguration clients = ApplicationContext.GetInstance().ClientConfigurations.First(x => x.Client.ToLower() == CommonUtils.GetClientLower());
            var userName = TestContext.CurrentContext.Test.Properties["UserName"]?.First().ToString();
            var user = clients.Users.First(x => x.User.ToUpper() == userName.ToUpper());

            var processRequest = new request.ProcessRequest
            {
                UserName = user.User,
                Password = user.Password,
                CorRequest = new request.CorRequest
                {
                    CorRequestID = 2365864354,
                    CorRequestType = "S",
                    CorVendorCode = dealerCode,
                    CorCustomerCode = fleetCode,
                    CorCommunityCode = "DTN",
                    CorTransactionType = "P",
                    CorTransactionNumber = InvoiceNumber,
                    CorTransactionDate = transactionDate.ChangeDateFormat("yyyyMMdd"),
                    CorPurchaseOrderNumber = "abc",
                    CorTransactionAmount = 58.00,
                    CorAuthorizationAmount = 00.00,
                    CorCurrencyCode = "USD",
                    CorPointOfSale = new request.CorPointOfSale(),
                    CorAccelerationTerms = new request.CorAccelerationTerms()
                    {
                        CorAccelerationProgramID = 237
                    },
                    CorReferences = new request.CorReferences(),
                    CorTaxes = new request.CorTaxes()
                    {
                        CorTax = new request.CorTax()
                        {
                            CorTaxType = "ST",
                            CorTaxAmount = 0,
                            CorTaxID = "001",
                        }
                    },

                    CorSections = new request.CorSections()
                    {
                        CorSection = new System.Collections.Generic.List<request.CorSection> {
                        new request.CorSection()
                        {
                            CorSectionNumber = "1",
                            CorComments = new request.CorComments(),
                            CorLineDetails = new request.CorLineDetails()
                            {
                                CorLineDetail = new System.Collections.Generic.List<request.CorLineDetail>
                             {
                               new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = 1,
                                    CorLineDetailType = "P",
                                    CorLineDetailItem = enginePart,
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = 1,
                                    CorLineDetailUnitPrice = 10.00,
                                    CorLineDetailCorePrice = 5.00,
                                    CorLineDetailFET = 5,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                },
                                new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = 2,
                                    CorLineDetailType = "P",
                                    CorLineDetailItem = vendorPart,
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = 1,
                                    CorLineDetailUnitPrice = 10.00,
                                    CorLineDetailCorePrice = 5.00,
                                    CorLineDetailFET = 5,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                },
                                new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = 3,
                                    CorLineDetailType = "P",
                                    CorLineDetailItem = propPart,
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = 1,
                                    CorLineDetailUnitPrice = 10.00,
                                    CorLineDetailCorePrice = 3.00,
                                    CorLineDetailFET = 5,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                }
                              }
                            }
                        }
                        }
                    }

                }
            };

            using (var stringwriter = new request.Utf8StringWriter())
            {
                var serializer = new XmlSerializer(processRequest.GetType());
                serializer.Serialize(stringwriter, processRequest);
                var requestXML = stringwriter.ToString();

                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "text/xml");

                request.AddParameter("text/xml", requestXML, ParameterType.RequestBody);
                var client = new RestClient(clients.DMS);
                client.Timeout = -1;
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);

                using (StringReader reader = new StringReader(response.Content))
                {
                    var a = ((CorResponse)new XmlSerializer(typeof(CorResponse)).Deserialize(reader)).CorResponseStatusCode;

                    if (a == 3 || a == 2)
                    {
                        return true;
                    }

                    else return false;
                }
            }
        }

        internal bool SubmitInvoiceWithAllLineItems(string InvoiceNumber, string dealerCode, string fleetCode, DateTime transactionDate, string enginePart, string propPart, string vendorPart, string unrecPart)
        {
            ClientConfiguration clients = ApplicationContext.GetInstance().ClientConfigurations.First(x => x.Client.ToLower() == CommonUtils.GetClientLower());
            var userName = TestContext.CurrentContext.Test.Properties["UserName"]?.First().ToString();
            var user = clients.Users.First(x => x.User.ToUpper() == userName.ToUpper());

            var processRequest = new request.ProcessRequest
            {
                UserName = user.User,
                Password = user.Password,
                CorRequest = new request.CorRequest
                {
                    CorRequestID = 2365864354,
                    CorRequestType = "S",
                    CorVendorCode = dealerCode,
                    CorCustomerCode = fleetCode,
                    CorCommunityCode = "DTN",
                    CorTransactionType = "P",
                    CorTransactionNumber = InvoiceNumber,
                    CorTransactionDate = transactionDate.ChangeDateFormat("yyyyMMdd"),
                    CorPurchaseOrderNumber = "abc",
                    CorTransactionAmount = 353.00,
                    CorAuthorizationAmount = 00.00,
                    CorCurrencyCode = "USD",
                    CorPointOfSale = new request.CorPointOfSale(),
                    CorAccelerationTerms = new request.CorAccelerationTerms()
                    {
                        CorAccelerationProgramID = 237
                    },
                    CorReferences = new request.CorReferences(),
                    CorTaxes = new request.CorTaxes()
                    {
                        CorTax = new request.CorTax()
                        {
                            CorTaxType = "ST",
                            CorTaxAmount = 0,
                            CorTaxID = "001",
                        }
                    },

                    CorSections = new request.CorSections()
                    {
                        CorSection = new System.Collections.Generic.List<request.CorSection> {

                         new request.CorSection()
                        {
                            CorSectionNumber = "1",
                            CorComments = new request.CorComments(),
                            CorLineDetails = new request.CorLineDetails()
                            {
                                CorLineDetail = new System.Collections.Generic.List<request.CorLineDetail>
                             {
                                new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = 1,
                                    CorLineDetailType = "B",
                                    CorLineDetailItem = "Sublet Part",
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = 1,
                                    CorLineDetailUnitPrice = 10.00,
                                    CorLineDetailCorePrice = 5.00,
                                    CorLineDetailFET = 5,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                },
                                new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = 2,
                                    CorLineDetailType = "E",
                                    CorLineDetailItem = "Expense",
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = 1,
                                    CorLineDetailUnitPrice = 10.00,
                                    CorLineDetailCorePrice = 5.00,
                                    CorLineDetailFET = 5,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                },
                                new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = 3,
                                    CorLineDetailType = "F",
                                    CorLineDetailItem = "Frieght",
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = 1,
                                    CorLineDetailUnitPrice = 10.00,
                                    CorLineDetailCorePrice = 3.00,
                                    CorLineDetailFET = 5,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                },
                                new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = 4,
                                    CorLineDetailType = "G",
                                    CorLineDetailItem = "Fuel",
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = 1,
                                    CorLineDetailUnitPrice = 11.00,
                                    CorLineDetailCorePrice = 3.00,
                                    CorLineDetailFET = 5,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                },
                                  new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = 5,
                                    CorLineDetailType = "L",
                                    CorLineDetailItem = "Labor",
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = 1,
                                    CorLineDetailUnitPrice = 10.00,
                                    CorLineDetailCorePrice = 4.00,
                                    CorLineDetailFET = 5,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                },
                                   new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = 6,
                                    CorLineDetailType = "M",
                                    CorLineDetailItem = "Miscellaneous",
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = 1,
                                    CorLineDetailUnitPrice = 10.00,
                                    CorLineDetailCorePrice = 4.00,
                                    CorLineDetailFET = 5,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                },
                                new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = 7,
                                    CorLineDetailType = "N",
                                    CorLineDetailItem = "Environment",
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = 1,
                                    CorLineDetailUnitPrice = 5.00,
                                    CorLineDetailCorePrice = 10.00,
                                    CorLineDetailFET = 5,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                },
                                new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = 8,
                                    CorLineDetailType = "R",
                                    CorLineDetailItem = "Rental",
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = 1,
                                    CorLineDetailUnitPrice = 8.00,
                                    CorLineDetailCorePrice = 2.00,
                                    CorLineDetailFET = 5,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                },
                                new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = 9,
                                    CorLineDetailType = "S",
                                    CorLineDetailItem = "Shop suplies",
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = 1,
                                    CorLineDetailUnitPrice = 12.00,
                                    CorLineDetailCorePrice = 5.00,
                                    CorLineDetailFET = 5,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                },
                                new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = 10,
                                    CorLineDetailType = "T",
                                    CorLineDetailItem = "Tax",
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = 1,
                                    CorLineDetailUnitPrice = 7.00,
                                    CorLineDetailCorePrice = 3.00,
                                    CorLineDetailFET = 5,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                },
                                  new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = 11,
                                    CorLineDetailType = "U",
                                    CorLineDetailItem = "Sublet Labor",
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = 1,
                                    CorLineDetailUnitPrice = 20.00,
                                    CorLineDetailCorePrice = 5.00,
                                    CorLineDetailFET = 5,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                },
                                   new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = 12,
                                    CorLineDetailType = "V",
                                    CorLineDetailItem = "Variable",
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = 1,
                                    CorLineDetailUnitPrice = 10.00,
                                    CorLineDetailCorePrice = 5.00,
                                    CorLineDetailFET = 5,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                },
                                    new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = 13,
                                    CorLineDetailType = "X",
                                    CorLineDetailItem = "Fixed",
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = 1,
                                    CorLineDetailUnitPrice = 10.00,
                                    CorLineDetailCorePrice = 5.00,
                                    CorLineDetailFET = 5,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                },
                               new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = 14,
                                    CorLineDetailType = "P",
                                    CorLineDetailItem = enginePart,
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = 1,
                                    CorLineDetailUnitPrice = 10.00,
                                    CorLineDetailCorePrice = 5.00,
                                    CorLineDetailFET = 5,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                },
                                new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = 15,
                                    CorLineDetailType = "P",
                                    CorLineDetailItem = vendorPart,
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = 1,
                                    CorLineDetailUnitPrice = 10.00,
                                    CorLineDetailCorePrice = 5.00,
                                    CorLineDetailFET = 5,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                },
                                new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = 16,
                                    CorLineDetailType = "P",
                                    CorLineDetailItem = propPart,
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = 1,
                                    CorLineDetailUnitPrice = 10.00,
                                    CorLineDetailCorePrice = 3.00,
                                    CorLineDetailFET = 5,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                },
                                new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = 17,
                                    CorLineDetailType = "P",
                                    CorLineDetailItem = unrecPart,
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = 1,
                                    CorLineDetailUnitPrice = 11.00,
                                    CorLineDetailCorePrice = 3.00,
                                    CorLineDetailFET = 5,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                },
                                  new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = 18,
                                    CorLineDetailType = "P",
                                    CorLineDetailItem = "152pJytesh",
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = 1,
                                    CorLineDetailUnitPrice = 10.00,
                                    CorLineDetailCorePrice = 4.00,
                                    CorLineDetailFET = 5,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                }
                        }

                              }
                            }
                        }
                    }

                }
            };

            using (var stringwriter = new request.Utf8StringWriter())
            {
                var serializer = new XmlSerializer(processRequest.GetType());
                serializer.Serialize(stringwriter, processRequest);
                var requestXML = stringwriter.ToString();

                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "text/xml");

                request.AddParameter("text/xml", requestXML, ParameterType.RequestBody);
                var client = new RestClient(clients.DMS);
                client.Timeout = -1;
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);

                using (StringReader reader = new StringReader(response.Content))
                {
                    var a = ((CorResponse)new XmlSerializer(typeof(CorResponse)).Deserialize(reader)).CorResponseStatusCode;

                    if (a == 3 || a == 2)
                    {
                        return true;
                    }

                    else return false;
                }
            }
        }

        internal bool CreateAuthorization(string InvoiceNumber, string dealerCode, string fleetCode, out string authCode)
        {
            ClientConfiguration clients = ApplicationContext.GetInstance().ClientConfigurations.First(x => x.Client.ToLower() == CommonUtils.GetClientLower());
            string userName;

            if (TestContext.CurrentContext.Test.Properties["UserName"].Count() > 0)
            {
                userName = TestContext.CurrentContext.Test.Properties["UserName"]?.First().ToString();
            }
            else
            {
                userName = ScenarioContext.Current["UserName"].ToString().ToUpper();
            }
            var user = clients.Users.First(x => x.User.ToUpper() == userName.ToUpper());
            DateTime transactionDate = DateTime.Today;
            var processRequest = new request.ProcessRequest
            {
                UserName = user.User,
                Password = user.Password,
                CorRequest = new request.CorRequest
                {
                    CorRequestType = "A",
                    CorVendorCode = dealerCode,
                    CorCustomerCode = fleetCode,
                    CorCommunityCode = clients.CommunityCode,
                    CorTransactionType = "P",
                    CorTransactionNumber = InvoiceNumber,
                    CorTransactionDate = transactionDate.ChangeDateFormat("yyyyMMdd"),
                    CorTransactionAmount = 2,
                    CorAuthorizationAmount = 72,
                    CorCurrencyCode = "USD",
                    CorPointOfSale = new request.CorPointOfSale(),
                    CorAccelerationTerms = new request.CorAccelerationTerms(),
                    CorReferences = new request.CorReferences(),
                    CorTaxes = new request.CorTaxes()
                    {
                        CorTax = new request.CorTax()
                        {
                            CorTaxType = "ST",
                            CorTaxAmount = 2,
                        }
                    },

                    CorSections = new request.CorSections()
                    {
                        CorSection = new System.Collections.Generic.List<request.CorSection> {
                            new request.CorSection()
                        {
                            CorSectionNumber = "1",
                            CorComments = new request.CorComments(),
                            CorLineDetails = new request.CorLineDetails()
                        } }

                    }

                }
            };

            using (var stringwriter = new request.Utf8StringWriter())
            {
                var serializer = new XmlSerializer(processRequest.GetType());
                serializer.Serialize(stringwriter, processRequest);
                var requestXML = stringwriter.ToString();

                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "text/xml");

                request.AddParameter("text/xml", requestXML, ParameterType.RequestBody);
                var client = new RestClient(clients.DMS);
                client.Timeout = -1;
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);

                using (StringReader reader = new StringReader(response.Content))
                {
                    CorResponse corResp = (CorResponse)new XmlSerializer(typeof(CorResponse)).Deserialize(reader);
                    var a = corResp.CorResponseStatusCode;
                    authCode = null;
                    if (a == 3 || a == 2)
                    {
                        authCode = corResp.CorAuthorizationCode;
                        return true;
                    }

                    else return false;
                }
            }
        }

        internal bool CreateAuthorizationWithTransactionAmount(string InvoiceNumber, string dealerCode, string fleetCode, out string authCode , int transactionAmount)
        {
            ClientConfiguration clients = ApplicationContext.GetInstance().ClientConfigurations.First(x => x.Client.ToLower() == CommonUtils.GetClientLower());
            string userName;

            if (TestContext.CurrentContext.Test.Properties["UserName"].Count() > 0)
            {
                userName = TestContext.CurrentContext.Test.Properties["UserName"]?.First().ToString();
            }
            else
            {
                userName = ScenarioContext.Current["UserName"].ToString().ToUpper();
            }
            var user = clients.Users.First(x => x.User.ToUpper() == userName.ToUpper());
            DateTime transactionDate = DateTime.Today;
            var processRequest = new request.ProcessRequest
            {
                UserName = user.User,
                Password = user.Password,
                CorRequest = new request.CorRequest
                {
                    CorRequestType = "A",
                    CorVendorCode = dealerCode,
                    CorCustomerCode = fleetCode,
                    CorCommunityCode = clients.CommunityCode,
                    CorTransactionType = "P",
                    CorTransactionNumber = InvoiceNumber,
                    CorTransactionDate = transactionDate.ChangeDateFormat("yyyyMMdd"),
                    CorTransactionAmount = transactionAmount,
                    CorAuthorizationAmount = transactionAmount,
                    CorCurrencyCode = "USD",
                    CorPointOfSale = new request.CorPointOfSale(),
                    CorAccelerationTerms = new request.CorAccelerationTerms(),
                    CorReferences = new request.CorReferences(),
                    CorTaxes = new request.CorTaxes()
                    {
                        CorTax = new request.CorTax()
                        {
                            CorTaxType = "ST",
                            CorTaxAmount = transactionAmount,
                        }
                    },

                    CorSections = new request.CorSections()
                    {
                        CorSection = new System.Collections.Generic.List<request.CorSection> {
                            new request.CorSection()
                        {
                            CorSectionNumber = "1",
                            CorComments = new request.CorComments(),
                            CorLineDetails = new request.CorLineDetails()
                        } }

                    }

                }
            };

            using (var stringwriter = new request.Utf8StringWriter())
            {
                var serializer = new XmlSerializer(processRequest.GetType());
                serializer.Serialize(stringwriter, processRequest);
                var requestXML = stringwriter.ToString();

                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "text/xml");

                request.AddParameter("text/xml", requestXML, ParameterType.RequestBody);
                var client = new RestClient(clients.DMS);
                client.Timeout = -1;
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);

                using (StringReader reader = new StringReader(response.Content))
                {
                    CorResponse corResp = (CorResponse)new XmlSerializer(typeof(CorResponse)).Deserialize(reader);
                    var a = corResp.CorResponseStatusCode;
                    authCode = null;
                    if (a == 3 || a == 2)
                    {
                        authCode = corResp.CorAuthorizationCode;
                        return true;
                    }

                    else return false;
                }
            }
        }

        internal bool SubmitInvoiceWithPPV(string InvoiceNumber, string dealer, string fleet)
        {
            ClientConfiguration clients = ApplicationContext.GetInstance().ClientConfigurations.First(x => x.Client.ToLower() == CommonUtils.GetClientLower());
            var userName = TestContext.CurrentContext.Test.Properties["UserName"]?.First().ToString();
            var user = clients.Users.First(x => x.User.ToUpper() == userName.ToUpper());

            var processRequest = new request.ProcessRequest
            {
                UserName = user.User,
                Password = user.Password,
                CorRequest = new request.CorRequest
                {
                    CorRequestType = "S",
                    CorVendorCode = dealer,
                    CorCustomerCode = fleet,
                    CorCommunityCode = "DTN",
                    CorTransactionType = "P",
                    CorTransactionNumber = InvoiceNumber,
                    CorTransactionDate = "20220610",
                    CorTransactionAmount = 2,
                    CorAuthorizationAmount = 72,
                    CorCurrencyCode = "USD",
                    CorPointOfSale = new request.CorPointOfSale(),
                    CorAccelerationTerms = new request.CorAccelerationTerms(),
                    CorReferences = new request.CorReferences()
                    {
                        CorReference = new request.CorReference()
                        {
                            CorReferenceType = "RR",
                            CorReferenceValue = "123",
                        }
                    },
                    CorTaxes = new request.CorTaxes()
                    {
                        CorTax = new request.CorTax()
                        {
                            CorTaxType = "ST",
                            CorTaxAmount = 2,
                        }
                    },

                    CorSections = new request.CorSections()
                    {
                        CorSection = new System.Collections.Generic.List<request.CorSection>
                        {
                         new request.CorSection()
                        {
                            CorSectionNumber = "1",
                            CorComments = new request.CorComments(),
                            CorLineDetails = new request.CorLineDetails()
                        }
                        }

                    }

                }
            };

            using (var stringwriter = new request.Utf8StringWriter())
            {
                var serializer = new XmlSerializer(processRequest.GetType());
                serializer.Serialize(stringwriter, processRequest);
                var requestXML = stringwriter.ToString();

                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "text/xml");

                request.AddParameter("text/xml", requestXML, ParameterType.RequestBody);
                var client = new RestClient(clients.DMS);
                client.Timeout = -1;
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);

                using (StringReader reader = new StringReader(response.Content))
                {
                    var a = ((CorResponse)new XmlSerializer(typeof(CorResponse)).Deserialize(reader)).CorResponseStatusCode;

                    if (a == 3 || a == 2)
                    {
                        return true;
                    }

                    else return false;
                }
            }
        }

        public string GetAuthorizationCode(string FleetCode, string DealerCode, string CommunityCode, string TransactionType, double TransactionAmount, double AuthorizationAmount, string CurrencyCode)
        {
            ClientConfiguration clients = ApplicationContext.GetInstance().ClientConfigurations.First(x => x.Client.ToLower() == CommonUtils.GetClientLower());
            var userName = TestContext.CurrentContext.Test.Properties["UserName"]?.First().ToString();
            var user = clients.Users.First(x => x.User.ToUpper() == userName.ToUpper());

            var processRequest = new request.ProcessRequest
            {
                UserName = user.User,
                Password = user.Password,
                CorRequest = new request.CorRequest
                {
                    CorRequestType = "T",
                    CorVendorCode = DealerCode,
                    CorCustomerCode = FleetCode,
                    CorCommunityCode = CommunityCode,
                    CorTransactionType = TransactionType,
                    CorTransactionNumber = GenerateRandomNo().ToString(),
                    CorTransactionDate = "20220101",
                    CorTransactionAmount = TransactionAmount,
                    CorAuthorizationAmount = AuthorizationAmount,
                    CorCurrencyCode = CurrencyCode,
                    CorPointOfSale = new request.CorPointOfSale(),
                    CorAccelerationTerms = new request.CorAccelerationTerms(),
                    CorReferences = new request.CorReferences(),
                    CorTaxes = new request.CorTaxes(),

                    CorSections = new request.CorSections()
                    {
                        CorSection = new System.Collections.Generic.List<request.CorSection>
                        {
                            new request.CorSection(){
                            CorSectionNumber = "1",
                            CorComments = new request.CorComments
                            {
                                CorComment = new request.CorComment
                                {
                                    CorSectionComment = "tést",
                                    CorSectionCommentType = "111",
                                    CorSectionCommentSequence = "1"
                                }
                            },

                            CorLineDetails = new request.CorLineDetails()
                            }

                        }
                    }

                }
            };

            using (var stringwriter = new request.Utf8StringWriter())
            {
                var serializer = new XmlSerializer(processRequest.GetType());
                serializer.Serialize(stringwriter, processRequest);
                var requestXML = stringwriter.ToString();

                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "text/xml");

                request.AddParameter("text/xml", requestXML, ParameterType.RequestBody);
                var client = new RestClient(clients.DMS);
                client.Timeout = -1;
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);

                using (StringReader reader = new StringReader(response.Content))
                {
                    return ((CorResponse)new XmlSerializer(typeof(CorResponse)).Deserialize(reader)).CorAuthorizationCode;
                }
            }
        }

        public int GenerateRandomNo()
        {
            int _min = 1;
            int _max = 9999999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }

        internal bool SubmitInvoiceMultipleSections(string InvoiceNumber, string dealerCode, string fleetCode, int numberOfSections,Part part)
        {
            ClientConfiguration clients = ApplicationContext.GetInstance().ClientConfigurations.First(x => x.Client.ToLower() == CommonUtils.GetClientLower());
            string userName;
            if (TestContext.CurrentContext.Test.Properties["UserName"].Count() > 0)
            {
                userName = TestContext.CurrentContext.Test.Properties["UserName"]?.First().ToString();
            }
            else
            {
                userName = ScenarioContext.Current["UserName"].ToString().ToUpper();
            }
            var user = clients.Users.First(x => x.User.ToUpper() == userName.ToUpper());
            DateTime transactionDate = DateTime.Today;

            var processRequest = new request.ProcessRequest
            {
                UserName = user.User,
                Password = user.Password,
                CorRequest = new request.CorRequest
                {
                    CorRequestType = "S",
                    CorVendorCode = dealerCode,
                    CorCustomerCode = fleetCode,
                    CorCommunityCode = "DTN",
                    CorTransactionType = "P",
                    CorTransactionNumber = InvoiceNumber,
                    CorTransactionDate = transactionDate.ChangeDateFormat("yyyyMMdd"),
                    CorPurchaseOrderNumber= InvoiceNumber,
                    CorTransactionAmount = numberOfSections*(part.CorePrice+part.UnitPrice),
                    CorAuthorizationAmount = numberOfSections * (part.CorePrice + part.UnitPrice),
                    CorCurrencyCode = "USD",
                    CorPointOfSale = new request.CorPointOfSale(),
                    CorAccelerationTerms = new request.CorAccelerationTerms(),
                    CorReferences = new request.CorReferences(),
                    CorTaxes = new request.CorTaxes(),
                    CorSections = new request.CorSections()
                    {
                        CorSection = new System.Collections.Generic.List<request.CorSection>()    
                    }

                }
            };

            for (int i = 1; i <= numberOfSections; i++)
            {

                processRequest.CorRequest.CorSections.CorSection.Add(

                    new request.CorSection()
                    {
                        CorSectionNumber = i.ToString(),
                        CorComments = new request.CorComments(),
                        CorLineDetails = new request.CorLineDetails()
                        {
                            CorLineDetail = new System.Collections.Generic.List<request.CorLineDetail>
                             {
                                new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = i,
                                    CorLineDetailType = "P",
                                    CorLineDetailItem = part.PartNumber,
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = 1,
                                    CorLineDetailUnitPrice = part.UnitPrice,
                                    CorLineDetailCorePrice = part.CorePrice,
                                    CorLineDetailFET = 0,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                }
                              }
                        }

                    }

                    );


            }
            using (var stringwriter = new request.Utf8StringWriter())
            {
                var serializer = new XmlSerializer(processRequest.GetType());
                serializer.Serialize(stringwriter, processRequest);
                var requestXML = stringwriter.ToString();

                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "text/xml");

                request.AddParameter("text/xml", requestXML, ParameterType.RequestBody);
                var client = new RestClient(clients.DMS);
                client.Timeout = -1;
                IRestResponse response = client.Execute(request);
               
                using (StringReader reader = new StringReader(response.Content))
                {
                    var a = ((CorResponse)new XmlSerializer(typeof(CorResponse)).Deserialize(reader)).CorResponseStatusCode;

                    if (a == 3 || a == 2)
                    {
                        return true;
                    }

                    else return false;
                }
            }
        }

        internal bool SubmitInvMultipleSectionsWithReferenceType(string InvoiceNumber, string dealerCode, string fleetCode, int numberOfSections, Part part, string rrValue)
        {
            ClientConfiguration clients = ApplicationContext.GetInstance().ClientConfigurations.First(x => x.Client.ToLower() == CommonUtils.GetClientLower());
            var userName = TestContext.CurrentContext.Test.Properties["UserName"]?.First().ToString();
            var user = clients.Users.First(x => x.User.ToUpper() == userName.ToUpper());
            DateTime transactionDate = DateTime.Today;

            var processRequest = new request.ProcessRequest
            {
                UserName = user.User,
                Password = user.Password,
                CorRequest = new request.CorRequest
                {
                    CorRequestType = "S",
                    CorVendorCode = dealerCode,
                    CorCustomerCode = fleetCode,
                    CorCommunityCode = "DTN",
                    CorTransactionType = "P",
                    CorTransactionNumber = InvoiceNumber,
                    CorTransactionDate = transactionDate.ChangeDateFormat("yyyyMMdd"),
                    CorPurchaseOrderNumber = InvoiceNumber,
                    CorTransactionAmount = numberOfSections * (part.CorePrice + part.UnitPrice),
                    CorAuthorizationAmount = numberOfSections * (part.CorePrice + part.UnitPrice),
                    CorCurrencyCode = "USD",
                    CorPointOfSale = new request.CorPointOfSale(),
                    CorAccelerationTerms = new request.CorAccelerationTerms(),
                    CorReferences = new request.CorReferences()
                    { 
                        CorReference = new request.CorReference()
                        { 
                            CorReferenceType = "RR",
                            CorReferenceValue = rrValue,
                        }
                    },
                    CorTaxes = new request.CorTaxes(),
                    CorSections = new request.CorSections()
                    {
                        CorSection = new System.Collections.Generic.List<request.CorSection>()
                    }

                }
            };

            for (int i = 1; i <= numberOfSections; i++)
            {

                processRequest.CorRequest.CorSections.CorSection.Add(

                    new request.CorSection()
                    {
                        CorSectionNumber = i.ToString(),
                        CorComments = new request.CorComments(),
                        CorLineDetails = new request.CorLineDetails()
                        {
                            CorLineDetail = new System.Collections.Generic.List<request.CorLineDetail>
                             {
                                new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = i,
                                    CorLineDetailType = "P",
                                    CorLineDetailItem = part.PartNumber,
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = 1,
                                    CorLineDetailUnitPrice = part.UnitPrice,
                                    CorLineDetailCorePrice = part.CorePrice,
                                    CorLineDetailFET = 0,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                }
                              }
                        }

                    }

                    );


            }
            using (var stringwriter = new request.Utf8StringWriter())
            {
                var serializer = new XmlSerializer(processRequest.GetType());
                serializer.Serialize(stringwriter, processRequest);
                var requestXML = stringwriter.ToString();

                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "text/xml");

                request.AddParameter("text/xml", requestXML, ParameterType.RequestBody);
                var client = new RestClient(clients.DMS);
                client.Timeout = -1;
                IRestResponse response = client.Execute(request);

                using (StringReader reader = new StringReader(response.Content))
                {
                    var a = ((CorResponse)new XmlSerializer(typeof(CorResponse)).Deserialize(reader)).CorResponseStatusCode;

                    if (a == 3 || a == 2)
                    {
                        return true;
                    }

                    else return false;
                }
            }
        }

        internal bool SubmitPOPOQMultipleSections(string InvoiceNumber, string dealerCode, string fleetCode, int numberOfSections,string RequestType, Part part)
        {
            ClientConfiguration clients = ApplicationContext.GetInstance().ClientConfigurations.First(x => x.Client.ToLower() == CommonUtils.GetClientLower());
            var userName = TestContext.CurrentContext.Test.Properties["UserName"]?.First().ToString();
            var user = clients.Users.First(x => x.User.ToUpper() == userName.ToUpper());
            DateTime transactionDate = DateTime.Today;

            var processRequest = new request.ProcessRequest
            {
                UserName = user.User,
                Password = user.Password,
                CorRequest = new request.CorRequest
                {
                    CorRequestType = RequestType,
                    CorVendorCode = dealerCode,
                    CorCustomerCode = fleetCode,
                    CorCommunityCode = "DTN",
                    CorTransactionType = "P",
                    CorTransactionNumber = InvoiceNumber,
                    CorTransactionDate = transactionDate.ChangeDateFormat("yyyyMMdd"),
                    CorTransactionAmount = numberOfSections * (part.CorePrice + part.UnitPrice),
                    CorAuthorizationAmount = numberOfSections * (part.CorePrice + part.UnitPrice),
                    CorCurrencyCode = "USD",
                    CorPointOfSale = new request.CorPointOfSale(),
                    CorAccelerationTerms = new request.CorAccelerationTerms(),
                    CorReferences = new request.CorReferences(),
                    CorTaxes = new request.CorTaxes(),
                    CorSections = new request.CorSections()
                    {
                        CorSection = new System.Collections.Generic.List<request.CorSection>()
                    }

                }
            };

            for (int i = 1; i <= numberOfSections; i++)
            {

                processRequest.CorRequest.CorSections.CorSection.Add(

                    new request.CorSection()
                    {
                        CorSectionNumber = i.ToString(),
                        CorComments = new request.CorComments(),
                        CorLineDetails = new request.CorLineDetails()
                        {
                            CorLineDetail = new System.Collections.Generic.List<request.CorLineDetail>
                             {
                                new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = i,
                                    CorLineDetailType = "P",
                                    CorLineDetailItem = part.PartNumber,
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = 1,
                                    CorLineDetailUnitPrice = part.UnitPrice,
                                    CorLineDetailCorePrice = part.CorePrice,
                                    CorLineDetailFET = 0,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                }
                              }
                        }

                    }

                    );


            }
            using (var stringwriter = new request.Utf8StringWriter())
            {
                var serializer = new XmlSerializer(processRequest.GetType());
                serializer.Serialize(stringwriter, processRequest);
                var requestXML = stringwriter.ToString();

                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "text/xml");

                request.AddParameter("text/xml", requestXML, ParameterType.RequestBody);
                var client = new RestClient(clients.DMS);
                client.Timeout = -1;
                IRestResponse response = client.Execute(request);

                using (StringReader reader = new StringReader(response.Content))
                {
                    var a = ((CorResponse)new XmlSerializer(typeof(CorResponse)).Deserialize(reader)).CorResponseStatusCode;

                    if (a == 3 || a == 2)
                    {
                        return true;
                    }

                    else return false;
                }
            }
        }

        internal bool SubmitInvoiceWithAuthCode(string InvoiceNumber, string dealerCode, string fleetCode, double transactionAmount, string authorizationCode)
        {
            ClientConfiguration clients = ApplicationContext.GetInstance().ClientConfigurations.First(x => x.Client.ToLower() == CommonUtils.GetClientLower());
            string userName;
            string lineItemPrice = Convert.ToString(transactionAmount);
            double unitPrice = transactionAmount;
            if (lineItemPrice.Contains("-"))
            {
                lineItemPrice = Convert.ToString(transactionAmount).Replace("-", "");
                unitPrice = Convert.ToDouble(lineItemPrice);
            }
            if (TestContext.CurrentContext.Test.Properties["UserName"].Count() > 0)
            {
                userName = TestContext.CurrentContext.Test.Properties["UserName"]?.First().ToString();
            }
            else
            {
                userName = ScenarioContext.Current["UserName"].ToString().ToUpper();
            }

            var user = clients.Users.First(x => x.User.ToUpper() == userName.ToUpper());
            DateTime transactionDate = DateTime.Today;

            var processRequest = new request.ProcessRequest
            {
                UserName = user.User,
                Password = user.Password,
                CorRequest = new request.CorRequest
                {
                    CorRequestType = "S",
                    CorVendorCode = dealerCode,
                    CorCustomerCode = fleetCode,
                    CorCommunityCode = clients.CommunityCode,
                    CorAuthorizationCode = authorizationCode,
                    CorTransactionType = "P",
                    CorTransactionNumber = InvoiceNumber,
                    CorTransactionDate = transactionDate.ChangeDateFormat("yyyyMMdd"),
                    CorTransactionAmount = transactionAmount,
                    CorAuthorizationAmount = transactionAmount,
                    CorCurrencyCode = "USD",
                    CorPointOfSale = new request.CorPointOfSale(),
                    CorAccelerationTerms = new request.CorAccelerationTerms(),
                    CorReferences = new request.CorReferences(),
                    CorTaxes = new request.CorTaxes(),
                    CorSections = new request.CorSections()
                    {
                        CorSection = new System.Collections.Generic.List<request.CorSection> {
                        new request.CorSection()
                        {
                            CorSectionNumber = "1",
                            CorComments = new request.CorComments(),
                            CorLineDetails = new request.CorLineDetails()
                            {
                                CorLineDetail = new System.Collections.Generic.List<request.CorLineDetail>
                             {
                                new request.CorLineDetail()
                                {
                                    CorLineDetailSequence = 1,
                                    CorLineDetailType = "P",
                                    CorLineDetailItem = "Parts",
                                    CorPartCategories = new request.CorPartCategories(),
                                    CorLineDetailQuantity = 1,
                                    CorLineDetailUnitPrice = unitPrice,
                                    CorLineDetailCorePrice = 0.00,
                                    CorLineDetailFET = 0,
                                    CorLineDetailNotes = new request.CorLineDetailNotes(),
                                    CorLineDetailUOM = "EA",
                                }
                              }
                            }
                        }
                        }
                    }

                }
            };

            using (var stringwriter = new request.Utf8StringWriter())
            {
                var serializer = new XmlSerializer(processRequest.GetType());
                serializer.Serialize(stringwriter, processRequest);
                var requestXML = stringwriter.ToString();

                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "text/xml");

                request.AddParameter("text/xml", requestXML, ParameterType.RequestBody);
                var client = new RestClient(clients.DMS);
                client.Timeout = -1;
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);

                using (StringReader reader = new StringReader(response.Content))
                {
                    var a = ((CorResponse)new XmlSerializer(typeof(CorResponse)).Deserialize(reader)).CorResponseStatusCode;

                    if (a == 3 || a == 2)
                    {
                        return true;
                    }

                    else return false;
                }
            }
        }
    }
}
