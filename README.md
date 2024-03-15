# Introduction 
TODO: Web Automation Framework based on Selenium 4 designed for CorConnect UI 

# Getting Started
TODO: Guide users through the setup, in getting system ready to read/write code and execute on their own.
1.	Setup process
- [Clone Repository](https://dev.azure.com/Corcentric-Devops/Corconnect%20Automation)
- [Download and install Access Database Engine](https://www.microsoft.com/en-us/download/details.aspx?id=54920) 
- Open the directory where the project is cloned. Note: Remove {space} from Repo Folder cloned
- The following two folders inside the main project folder will be used for the setup
  - AutomationTesting_CorConnect
  - PageObject Utility
- Open the PageObject Utility folder
  - Launch the PageObject Utility solution file using the visual studio
  - Build the solution
  - Run the utility → It will generate the page objects json files in the newly create page elements folder
  - Copy page elements folder and paste it in your C:/ directory
- Create Empty directories for Allure Results
  - Create folders in your C:/ directory. C:\projects\default\results 
- Open the AutomationTesting_Corconnect Folder
  - Launch the AutomationTesting_Corconnect solution file using the visual studio
  - Add SpecFlow for Visual Studio extension from Manage Extensions under Extensions tab in Visual Studio 
  - Build the solution
  - Execute the test cases using Test Explorer from View Tab in Visual Studio
2.	Software dependencies
- Selenium - Version 4.1
- .NET - Version 6.0
- Visual Studio - Microsoft Visual Studio Community 2022 (64-bit) Recommended
- Microsoft Access Database Engine