# storefront

To get started with running this sample you will need to, within Visual Studio, bring up the properties for the Storefront.Dbup project.  On the Debug tab pass a command line parameter in the format of `"Server=<yourServerUrl>;Database=Storefront;User Id=<yourUsername>;Password=<yourPassword>"` Note: include parentheses.  To get the schema and seed data to run simply debug the Dbup project.

Next you should place the same connection string in the appsettings.json that resides in Storefront.web.  This ensures the connection to your newly created database.
