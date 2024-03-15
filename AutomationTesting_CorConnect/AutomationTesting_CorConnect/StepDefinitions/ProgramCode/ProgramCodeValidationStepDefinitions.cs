using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.PageObjects;
using AutomationTesting_CorConnect.PageObjects.DealerLocations;
using AutomationTesting_CorConnect.PageObjects.DraftStatementReport;
using AutomationTesting_CorConnect.PageObjects.Enrollment;
using AutomationTesting_CorConnect.PageObjects.FleetLocations;
using AutomationTesting_CorConnect.PageObjects.GPDraftStatements;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.DraftStatementReport;
using AutomationTesting_CorConnect.Utils.FleetLookup;
using AutomationTesting_CorConnect.Utils.GPDraftStatements;
using AutomationTesting_CorConnect.Utils.ManageUsers;
using NUnit.Framework;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace AutomationTesting_CorConnect.StepDefinitions.ProgramCode
{
    [Binding]
    internal class ProgramCodeValidationStepDefinitions : DriverBuilderClass
    {
        DealerLocationsPage DLPPage;
        GPDraftStatementsPage GDSpage;
        FleetLocationsPage FLPPage;
        DraftStatementReportPage DSRPage;
        EnrollmentPage EPage;

        [Given(@"Program CodeToken is ""([^""]*)"" for Program Code and ""([^""]*)"" for Subcommunity")]
        public void GivenProgramCodeTokenIsForProgramCodeAndForForSubcommunity(string programCodeToken, string subcommunityToken)
        {
            if (programCodeToken == "Active")
            {
                CommonUtils.ToggleValidateProgramCode(true);
            }
            else if (programCodeToken == "InActive")
            {
                CommonUtils.ToggleValidateProgramCode(false);

            }

            if (subcommunityToken == "Active")
            {
                CommonUtils.ToggleEnablePrgmCodeAsgnOnSubCommunity(true);
            }
            else if (subcommunityToken == "InActive")
            {
                CommonUtils.ToggleEnablePrgmCodeAsgnOnSubCommunity(false);

            }

        }

        [When(@"User is on ""([^""]*)"" page and populate grid for ""([^""]*)""")]
        public void WhenUserIsOnPageAndPopulateGridFor(string pageName, string locationLevel)
        {
            var locationCode = "";
            string EPDate = "";
            string entity = "";
            if (locationLevel == "Subcommunity")
            {
                if (pageName == "Dealer Locations")
                {
                    locationCode = FleetLookupUtils.GetSubCommunityAccountCodeforDealer();
                }
                else if (pageName == "GP Draft Statements")
                {
                    LocationDetails getLocationdetails = GPDraftStatementsUtil.GetLocationDetailsforSubCom();
                    entity = getLocationdetails.locationCode;
                    EPDate = getLocationdetails.Date;
                }
                else if (pageName == "Draft Statement Report")
                {
                    LocationDetails getLocationdetails = DraftStatementReportUtil.GetLocationDetailsforSubcom();
                    entity = getLocationdetails.locationCode;
                    EPDate = getLocationdetails.Date;
                }
                else
                {
                    locationCode = FleetLookupUtils.GetSubCommunityAccountCodeforFleet();

                }
            }
            else if (locationLevel == "Entity Level")
            {
                if (pageName == "Dealer Locations")
                {
                    locationCode = FleetLookupUtils.GetEntityLevelAccountCodeforDealer();
                }

                else if (pageName == "GP Draft Statements")
                {
                    LocationDetails getLocationdetails = GPDraftStatementsUtil.GetLocationDetails();
                    entity = getLocationdetails.locationCode;
                    EPDate = getLocationdetails.Date;
                }
                else if (pageName == "Draft Statement Report")
                {
                    LocationDetails getLocationdetails = DraftStatementReportUtil.GetLocationDetails();
                    entity = getLocationdetails.locationCode;
                    EPDate = getLocationdetails.Date;

                }
                else
                {
                    locationCode = FleetLookupUtils.GetEntityLevelAccountCodeforFleet();
                }

            }
            else if (locationLevel == "Enrollment Entity")
            {

                if (pageName == "Dealer Locations")
                {
                    locationCode = "11383-5";
                }

                else
                {
                    locationCode = "ProgramCode";
                }
            }

            switch (pageName)
            {
                case "Fleet Locations":
                    FLPPage = new FleetLocationsPage(driver);
                    FLPPage.LoadDataOnGridwithCode(locationCode);
                    break;
                case "Dealer Locations":
                    DLPPage = new DealerLocationsPage(driver);
                    DLPPage.LoadDataOnGrid();
                    DLPPage.FilterTable("Account Code", locationCode);
                    break;
                case "GP Draft Statements":
                    GDSpage = new GPDraftStatementsPage(driver);
                    if (locationLevel == "Subcommunity")
                    {
                        GDSpage.LoaddataonGrid(EPDate, EPDate, entity);
                    }
                    else if (locationLevel == "Entity Level")
                    {
                        GDSpage.LoaddataonGrid(EPDate, EPDate, entity);
                    }
                    break;
                case "Draft Statement Report":
                    DSRPage = new DraftStatementReportPage(driver);
                    if (locationLevel == "Subcommunity")
                    {
                        DSRPage.SearchAndSelectValueAfterOpen(FieldNames.CompanyName, entity);
                        DSRPage.PopulateGrid(EPDate, EPDate);
                    }
                    else if (locationLevel == "Entity Level")
                    {
                        DSRPage.SearchAndSelectValueAfterOpen(FieldNames.CompanyName, entity);
                        DSRPage.PopulateGrid(EPDate, EPDate);
                    }
                    break;
                case "Enrollment":
                    EPage = new EnrollmentPage(driver);
                    EPage.LoadDataOnGrid(locationCode);
                    break;
            }
        }


        [Then(@"Column Program Code value is ""([^""]*)"" for Page ""([^""]*)""")]
        public void ThenColumnProgramCodeValueIsForPage(string PCValue, string pageName)
        {
            if (PCValue == "null")
            {
                switch (pageName)
                {
                    case "Fleet Locations":
                        if (FLPPage.IsAnyDataOnGrid())
                        {
                            FLPPage = new FleetLocationsPage(driver);
                            Assert.AreEqual(" ", FLPPage.GetFirstRowData(TableHeaders.ProgramCode));
                        }
                        break;
                    case "Dealer Locations":
                        if (DLPPage.IsAnyDataOnGrid())
                        {
                            DLPPage = new DealerLocationsPage(driver);
                            Assert.AreEqual(" ", DLPPage.GetFirstRowData(TableHeaders.ProgramCode));
                        }
                        break;
                    case "GP Draft Statements":
                        if (GDSpage.IsAnyDataOnGrid())
                        {
                            GDSpage = new GPDraftStatementsPage(driver);
                            Assert.AreEqual(" ", GDSpage.GetFirstRowData(TableHeaders.ProgramCode));
                        }
                        break;
                    case "Draft Statement Report":
                        if (DSRPage.IsAnyDataOnGrid())
                        {
                            DSRPage = new DraftStatementReportPage(driver);
                            Assert.AreEqual(" ", DSRPage.GetFirstRowData(TableHeaders.ProgramCode));
                        }
                        break;
                    case "Enrollment":
                        EPage = new EnrollmentPage(driver);
                        Assert.AreEqual(" ", EPage.GetFirstRowDataPopUpPage(TableHeaders.Program));
                        break;
                }
            }
            if (PCValue == "PC Values")
            {
                switch (pageName)
                {
                    case "Fleet Locations":
                        if (FLPPage.IsAnyDataOnGrid())
                        {
                            FLPPage = new FleetLocationsPage(driver);
                            Assert.NotNull(FLPPage.GetFirstRowData(TableHeaders.ProgramCode));
                        }
                        break;
                    case "Dealer Locations":
                        if (DLPPage.IsAnyDataOnGrid())
                        {
                            DLPPage = new DealerLocationsPage(driver);
                            Assert.NotNull(DLPPage.GetFirstRowData(TableHeaders.ProgramCode));
                        }
                        break;
                    case "GP Draft Statements":
                        if (GDSpage.IsAnyDataOnGrid())
                        {
                            GDSpage = new GPDraftStatementsPage(driver);
                            Assert.NotNull(GDSpage.GetFirstRowData(TableHeaders.ProgramCode));
                        }
                        break;
                    case "Draft Statement Report":
                        if (DSRPage.IsAnyDataOnGrid())
                        {
                            DSRPage = new DraftStatementReportPage(driver);
                            Assert.NotNull(DSRPage.GetFirstRowData(TableHeaders.ProgramCode));
                        }
                        break;
                    case "Enrollment":
                        EPage = new EnrollmentPage(driver);
                        Assert.NotNull(EPage.GetFirstRowDataPopUpPage(TableHeaders.ProgramCode));
                        break;
                }
            }
            else if (PCValue == "Entity level PC values")
            {
                List<string> PCValues = FleetLookupUtils.GetEntityLevelPCValueforFleet();

                switch (pageName)
                {
                    case "Fleet Locations":
                        if (FLPPage.IsAnyDataOnGrid())
                        {
                            FLPPage = new FleetLocationsPage(driver);
                            Assert.IsTrue(FLPPage.VerifyFilterDataOnGridByHeader("Program Code", string.Join(",", PCValues.ToArray())));
                        }
                        break;
                    case "Dealer Locations":
                        if (DLPPage.IsAnyDataOnGrid())
                        {
                            DLPPage = new DealerLocationsPage(driver);
                            PCValues = FleetLookupUtils.GetEntityLevelPCValueforDealer();
                            string dealerInvoiceNum = DLPPage.GetFirstRowData(TableHeaders.ProgramCode);
                            Assert.AreEqual(string.Join(",", PCValues.ToArray()), dealerInvoiceNum);
                        }
                        break;
                    case "GP Draft Statements":
                        if (GDSpage.IsAnyDataOnGrid())
                        {
                            GDSpage = new GPDraftStatementsPage(driver);
                            Assert.IsTrue(GDSpage.VerifyFilterDataOnGridByHeader("Program Code", string.Join(",", PCValues.ToArray())));
                        }
                        break;
                    case "Draft Statement Report":
                        if (DSRPage.IsAnyDataOnGrid())
                        {
                            DSRPage = new DraftStatementReportPage(driver);
                            Assert.NotNull(DSRPage.GetFirstRowData(TableHeaders.ProgramCode));
                        }
                        break;
                    case "Enrollment":
                        EPage = new EnrollmentPage(driver);
                        Assert.AreEqual(string.Join(" ,", PCValues.ToArray()), EPage.GetFirstRowDataPopUpPage(TableHeaders.Program));
                        break;
                }
            }

            else if (PCValue == "Enrollment PC Value")
            {
                PCValue = FleetLookupUtils.GetEntityLevelPCValueforFleetEnroll();
                switch (pageName)
                {
                    case "Fleet Locations":
                        if (FLPPage.IsAnyDataOnGrid())
                        {
                            FLPPage = new FleetLocationsPage(driver);
                            Assert.IsTrue(FLPPage.VerifyFilterDataOnGridByHeader("Program Code", PCValue));
                        }
                        break;
                    case "Dealer Locations":
                        if (DLPPage.IsAnyDataOnGrid())
                        {
                            DLPPage = new DealerLocationsPage(driver);
                            PCValue = FleetLookupUtils.GetEnrollmentPCValueforDealer();
                            Assert.IsTrue(DLPPage.VerifyFilterDataOnGridByHeader("Program Code", PCValue));
                        }
                        break;
                    case "GP Draft Statements":
                        if (GDSpage.IsAnyDataOnGrid())
                        {
                            GDSpage = new GPDraftStatementsPage(driver);
                            Assert.NotNull(GDSpage.GetFirstRowData(TableHeaders.ProgramCode));
                        }
                        break;
                    case "Draft Statement Report":
                        if (DSRPage.IsAnyDataOnGrid())
                        {
                            DSRPage = new DraftStatementReportPage(driver);
                            Assert.NotNull(DSRPage.GetFirstRowData(TableHeaders.ProgramCode));
                        }
                        break;
                    case "Enrollment":
                        EPage = new EnrollmentPage(driver);
                        Assert.AreEqual(PCValue, EPage.GetFirstRowDataPopUpPage(TableHeaders.Program));
                        break;
                }
            }

        }
    }
}
