# TestReport.Net
The purpose of TestReport.Net is to collect test results, including details at test run time for developers/testers/managers to learn how is the product quality.

Currently there is only one project: TestReport.SpecFlow which is collecting test results and details for SpecFlow test cases.

## TestReport.SpecFlow
This project is to generate the report of SpecFlow test run and send the report via email. The report can show the total pass ratio, feature-based pass ratio and each test scenario context. If it is a portal UI testing and the test case fails, a screenshot will be captured in the test scenario context for that failed test step.

### How to setup test report by using TestReport.SpecFlow
1. Add the package to your SepcFlow project by NuGet @https://www.nuget.org/packages/TestReport.SpecFlow
2. As the NuGet package is installed, the following configuration will be added to the App.config of the specflow project and you can update the value accroding to your requirements:

```
  <specFlow>
    <stepAssemblies>
      <stepAssembly assembly="TestReport.SpecFlow" />
    </stepAssemblies>
  </specFlow>

  <appSettings>
    <add key="testResultFolder" value="c:\TestResults"/>
    <!--Report Email settings-->
    <add key="sendEmailReport" value="true" />
    <add key="stmpHost" value="your.smpt.host" />
    <add key="stmpPort" value="587OrElse" />
    <add key="stmpSSL" value="trueOrFault" />
    <add key="stmpUser" value="user" />
    <add key="stmpPass" value="password" />
    <add key="stmpFrom" value="anyName@your.domain" />
    <add key="stmpTo" value="anyone@your.domain;anyGroup@your.domain" />
  </appSettings>
```
  
### Note of appSettings
* testResultFolder -- define the test report folder that you want to store the result
* sendEmailReport -- if it is true, the report will be sent out via email, otherwise no report being sent out
* stmpHost/stmpPort/stmpSSL/stmpUser/stmpPass -- the smtp service parameters that you need to set
* stmpFrom/stmpTo -- the mail you want to sent from and to. Use ; to separate different mail recipient
