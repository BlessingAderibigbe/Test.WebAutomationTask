Project Name: ShoppingCart

Pre-Requisites for Project Execution
	Visual Studio 2019
	Latest Chrome Driver Version 112.0.5615.4900

Automation Framework Design Approach

IDE & Language
	Visual Studio 2019 & C#

Automation Tool
	Selenium WebDriver

Project Type
	BDD-SpecFlow

DataDriven
	Scenario 
	

Reports
	Extent Reports

Browsers
	Chrome Driver

Test Scenarios
	Scenario : User can add items to a cart and remove the lowest price item 
	

 Brief Description of Framework Approach
	Reports are created using ExtentReports and ScreenShots is used to capture failed Scenarios.
	In Project Solution
		Runsettings file included to show that test can be run in different environments.
		Locators are defined in ShoppingCartPage
		NuGet packages needs to be restored before test run
		Contains a Local Run Settings which must be selected before any test run. This can be done by navigating to Test and then Configure Run Settings.

BDDFramework (SpecFlow Project)
	Feature Files
	StepDefinitions Files
	Pages Files
	SetUp Files