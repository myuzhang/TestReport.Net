# TestReport.Net
The purpose of TestReport.Net is to collect test results, including details at test run time for developers/testers/managers to learn how is the product quality.

Currently there is only one project: TestReport.SpecFlow which is collecting test results and details for SpecFlow test cases.

## TestReport.SpecFlow
This project is to generate the report of SpecFlow test run and send the report via email. The report can show the total pass ratio, feature-based pass ratio and each test scenario context. If it is a portal UI testing and the test case fails, a screenshot will be captured in the test scenario context for that failed test step.
