This app is a basic demo of an ecommerce application.


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

 Make sure it's running, create a seller account and login by setting your bearer.

 Create product, update product , delete, get singe product, get all products.


 Note only an authenticated seller can manage this operatins.

