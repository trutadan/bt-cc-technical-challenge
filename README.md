# bt-cc-technical-challenge
 Technical Challenge for CC by BT

 Write a solution in c# that can generate a one-time password. 
 The input should be the following two pieces information: User Id and Date time. Every generated password should be valid for up to 30 seconds.
 You are free to use a Web, API, Console or Class Library project in order to accomplish the requirement.
 
 The file that specifically handles the requirement to generate a one-time password in C# can be found at backend/Service/OTPLoginService.cs.

 However, if you are interested in exploring the entire project, including the implementation of the one-time password generation in a login scenario, you can check the rest.
 
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

To run the React app, follow these steps:

1. Change into the project directory:
`cd .\frontend\`

2. Install the project dependencies:
`npm install`

3. Start the frontend development server:
`npm run dev`

The frontend server should now be running by default on `http://127.0.0.1:5173/`.

**_NOTE:_**
If you want to modify the IP address of the backend, you can update the DEV_BACKEND_API_URL constant from the `constants.tsx` file, to reflect the desired IP address and port of your backend server.
