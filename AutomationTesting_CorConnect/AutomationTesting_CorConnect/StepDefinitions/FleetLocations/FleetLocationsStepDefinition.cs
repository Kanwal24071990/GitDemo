using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.PageObjects;
using AutomationTesting_CorConnect.PageObjects.DealerLocations;
using AutomationTesting_CorConnect.PageObjects.FleetLocations;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.DealerLocations;
using AutomationTesting_CorConnect.Utils.FleetLocations;
using AutomationTesting_CorConnect.Utils.FleetLocationSalesSummary;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace AutomationTesting_CorConnect.StepDefinitions.FleetLocations
{
    [Binding]
    internal class FleetLocationsStepDefinition : DriverBuilderClass
    {
        FleetLocationsPage Page;

        [When(@"User drills down record for Buyer ""([^""]*)"" Location")]
        public void WhenUserDrillsDownRecordForBuyerLocation(string buyerCode)
        {
            if (buyerCode == "Buyer")
            {
                buyerCode = FleetLocationsUtil.GetInActiveCorCentricCodeforFleet();
            }
            Page = new FleetLocationsPage(driver);
            Page.LoadDataOnGrid(buyerCode);
            Assert.IsTrue(Page.IsNestedGridOpen());
        }

        [Then(@"Users with Matching subcommunity for Buyer ""([^""]*)"" should be displayed")]
        public void ThenUsersWithMatchingSubcommunityForBuyerShouldBeDisplayed(string buyerCode)
        {
            Page = new FleetLocationsPage(driver);
            int entityID = CommonUtils.GetEntityId(buyerCode);
            string subCommunity = CommonUtils.GetSubcommunityforEntity(buyerCode);
            List<string> Users = FleetLocationsUtil.GetActiveUsersListforFleetEntity(entityID, subCommunity);
            for (int i = 0; i < Users.Count; i++)
            {
                Page.FilterNestedTable(TableHeaders.UserName, Users[i]);
                Assert.IsTrue(Page.VerifyFilterDataOnGridByNestedHeader(TableHeaders.UserName, Users[i]));
            }
        }

        [Then(@"User with Non-Matching subcommunity for Buyer ""([^""]*)"" should not be displayed")]
        public void ThenUserWithNon_MatchingSubcommunityForBuyerShouldNotBeDisplayed(string buyerCode)
        {
            Page = new FleetLocationsPage(driver);
            int entityID = CommonUtils.GetEntityId(buyerCode);
            string subCommunity = CommonUtils.GetSubcommunityforEntity(buyerCode);
            List<string> Users = FleetLocationsUtil.GetActiveUsersListforFleetEntity(entityID, subCommunity);
            for (int i = 0; i < Users.Count; i++)
            {
                Page.FilterNestedTable(TableHeaders.UserName, Users[i]);
                Assert.IsFalse(Page.VerifyFilterDataOnGridByNestedHeader(TableHeaders.UserName, Users[i]));
            }
        }

        [Then(@"Empty Grid should be displayed for Buyer Location")]
        public void ThenEmptyGridShouldBeDisplayedForBuyerLocation()
        {
            Page = new FleetLocationsPage(driver);
            Assert.IsFalse(Page.IsAnyDataOnNestedGrid());
        }

        [Then(@"""([^""]*)"" users should not be displayed for Buyer Location")]
        public void ThenUsersShouldNotBeDisplayedForBuyerLocation(string entityState)
        {
            Page = new FleetLocationsPage(driver);
            switch (entityState)
            {
                case "Inactive":
                    List<string> Users = FleetLocationsUtil.GetInActiveUsersListforFleetEntity();
                    for (int i = 0; i < Users.Count; i++)
                    {
                        Assert.IsFalse(Page.VerifyFilterDataOnGridByNestedHeader(TableHeaders.UserName, Users[i]));
                    }
                    break;
                case "Suspended":
                    string buyerCode = CommonUtils.GetSuspendedFleetCode();
                    Assert.IsFalse(Page.VerifyFilterDataOnGridByNestedHeader(TableHeaders.UserName, buyerCode));
                    break;
            }
        }
    }
}
