This app is a basic demo of an ecommerce application and it's built with C# and .Net Core Web API.
Make sure you have an update Microsoft Visual Studio to run this application.

update the connection string and the jwt details by clicking on the manage user secrets on the "ProductApi_PLL" library.


e.g:

 "JwtSettings": {
   "validIssuer": "ProductAPI",
   "validAudience": "https://localhost:7258",
   "expires": 54,
   "Secret": ""
 },
 "ConnectionStrings": {
   "sqlConnection": ""
 }

 To use the app:

 Make sure it's running, create a seller account, and log in by setting your bearer.

 Create product, update product, delete, get single product, get all products.


 Note: only an authenticated seller can manage these operations.

