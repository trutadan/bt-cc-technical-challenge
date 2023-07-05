# bt-cc-technical-challenge
 Technical Challenge for CC by BT

 Write a solution in c# that can generate a one-time password. 
 The input should be the following two pieces information: User Id and Date time. Every generated password should be valid for up to 30 seconds.
 You are free to use a Web, API, Console or Class Library project in order to accomplish the requirement.

# Short demo
![](https://github.com/trutadan/bt-cc-technical-challenge/blob/main/demo.gif)

# Backend

To open the ASP.NET Core REST API app in Visual Studio, follow these steps:

1. Open Visual Studio.
2. Click on "Open a project or solution."
3. Navigate to the project directory and select the solution file (.sln) for the app(found in the 'backend' folder).
4. Click "Open" to open the project in Visual Studio.

**_NOTE:_**
Make sure to update the 'DatabaseConnection' connection string in the `appsettings.json` file to match your PostgreSQL database configuration.
If you want, you can as well change the Secure key used for JWT token generation and validation or the Secret key used for OTP login functionality.

# Frontend

Change into the project directory:
`cd .\frontend\`

Install the project dependencies:
`npm install`

Start the frontend development server:
`npm run dev`

The frontend server should now be running by default on `http://127.0.0.1:5173/`.
