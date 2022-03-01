# Bank_Simulation
Simulating Bank accounts, transactions and logging

To create Windows service open a command prompt in administrator mode and use the below command:
`sc create <name of service you want to create> binPath= <path of executable of your app>`

For Example:
`sc create WindowsServiceDemo binPath= "C:\Project\WindowsServiceDemo.exe"`

Our service is now created.


Right-click on service and click on start. So our Web API is running on URL http://localhost:5000. 

For Documentation and testing go to http://localhost:5000/swagger in a browser and you will see the response