# Costs-Preview
MVC .net Web Application with contrived example demonstrating a clients annual cost to pay employees with benefits package.

Restore Nuget packages/Clean/Rebuild Solution

Make sure the connectionStrings satisfy the needs of the local computer running the application. 
1. Update Web.Config in Costs.Preview.UI 
2. Update App.Config in Costs.Preview.Data 
3. Update App.Config in Costs.Preview.Business.LogicTests

From Package Manager Console 
1. Enable-Migrations 
2. Add-Migration Initialize 
3. Update-Database
