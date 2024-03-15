using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.PageObjects.DealerLocations;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.AccountMaintenance;
using AutomationTesting_CorConnect.Utils.DealerLocations;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace AutomationTesting_CorConnect.StepDefinitions.DealerLocations
{
    [Binding]
    internal class DealerLocationsStepDefinition : DriverBuilderClass
    {
        DealerLocationsPage DLPage;

        [When(@"User drills down record for ""([^""]*)"" Location")]
        public void WhenUserDrillsDownRecordForLocation(string sellerEntity)
        {
            DLPage = new DealerLocationsPage(driver);
            if (sellerEntity == "Seller")
            {
                sellerEntity = DealerLocationsUtil.GetInActiveCorCentricCodeforDealer();
                CommonUtils.GetDealerCode();
            }
            DLPage.LoadDataOnGrid(sellerEntity);
            Assert.IsTrue(DLPage.IsNestedGridOpen());
        }

        [Then(@"User with Matching subcommunity for ""([^""]*)"" should be displayed")]
        public void ThenUserWithMatchingSubcommunityForShouldBeDisplayed(string corCentricCode)
        {
            DLPage = new DealerLocationsPage(driver);
            int entityID = CommonUtils.GetEntityId(corCentricCode);
            string subCommunity = CommonUtils.GetSubcommunityforEntity(corCentricCode);
            List<string> Users = DealerLocationsUtil.GetActiveUsersListforDealerEntity(entityID, subCommunity);
            for (int i = 0; i < Users.Count; i++)
            {
                DLPage.FilterNestedTable(TableHeaders.UserName, Users[i]);
                Assert.IsTrue(DLPage.VerifyFilterDataOnGridByNestedHeader(TableHeaders.UserName, Users[i]));

            }
        }

        [Then(@"User with Non-Matching subcommunity for ""([^""]*)"" should not be displayed")]
        public void ThenUserWithNon_MatchingSubcommunityForShouldNotBeDisplayed(string corCentricCode)
        {
            DLPage = new DealerLocationsPage(driver);
            int entityID = CommonUtils.GetEntityId(corCentricCode);
            string subCommunity = CommonUtils.GetSubcommunityforEntity(corCentricCode);
            List<string> Users = DealerLocationsUtil.GetActiveUsersListforDealerEntity(entityID, subCommunity);
            for (int i = 0; i < Users.Count; i++)
            {
                DLPage.FilterNestedTable(TableHeaders.UserName, Users[i]);
                Assert.IsFalse(DLPage.VerifyFilterDataOnGridByNestedHeader(TableHeaders.UserName, Users[i]));
            }
        }

        [Then(@"Empty Grid should be displayed")]
        public void ThenEmptyGridShouldBeDisplayed()
        {
            DLPage = new DealerLocationsPage(driver);
            Assert.IsFalse(DLPage.IsAnyDataOnNestedGrid());
        }

        [Then(@"""([^""]*)"" users should not be displayed")]
        public void ThenUsersShouldNotBeDisplayed(string entityState)
        {
            DLPage = new DealerLocationsPage(driver);
            switch (entityState)
            {
                case "Inactive":
                    List<string> Users = DealerLocationsUtil.GetInActiveUsersListforDealerEntity();
                    for (int i = 0; i < Users.Count; i++)
                    {
                        Assert.IsFalse(DLPage.VerifyFilterDataOnGridByNestedHeader(TableHeaders.UserName, Users[i]));
                    }
                    break;
                case "Suspended":
                    string corCentriccode = CommonUtils.GetSuspendedDealerCode();
                    Assert.IsFalse(DLPage.VerifyFilterDataOnGridByNestedHeader(TableHeaders.UserName, corCentriccode));
                    break;
            }
        }

    }
}
