# Backend/API Data
SATTSInventory folder : contains the API/Backend code. These apis are developed in ASP.NET WBI with .net framework 4.7.2.
BackEnd_OR_API_Journals.txt file : contains the approximate journal of the time required to develop the APIs.


# Fronend/Angular Data
SadguruAmritTulayaTShop folder : contain the UI code. The UI has been developed using Angular 8, Bootstrap 4.5
FrontEnd_OR_ Angular_Journals.txt file : contains the approximate journal of the time required to develop the UI.

# Instructions to Run the Code
## BackEnd Code Setup
1. Open the solution from folder "SATTSInventory" in VS 2019
2. Update the connecting string in web.config.
3. Open the Package Manager Console and Execute the below Steps to create the required tables in the database.
   i. Enable-Migrations
   ii. Add-Migration Initial
   iii. Update-Database -Verbose
4. Verify the database to have the newly Created Beverage table in it.
5. Run the code locally or deploy it on server.
6. Verify if the application by browsing to http://{hostip}:{portnumber}/api/beverages. hostpip is where the application is hosted may to localhost or server ip address. portnumber on which the application is running.

## Front End Code Setup
1. Open the folder "SadguruAmritTulayaTShop" using Visual Studio Code
2. Update the API property present in "SadguruAmritTulayaTShop\src\app\shared\api\beverage-store.service.ts" with address of the api you have hosted that is "http://{hostip}:{portnumber}/api/beverages".
3. Once Done then we can run command "ng serve -o" from Visual Studio code Terminal.
4. Verify it by browsing to http://{hostip}:{portnumber}. By Default it opens to http://localhost:4200. We can also specify port if we want it to open at any specific port by executing command "ng serve --port {portnumber} -o".


Thank You,
Sandeep
