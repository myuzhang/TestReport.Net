using System.Collections.Generic;
using System.Text;
using TestReport.SpecFlow.ReportModels;

namespace TestReport.SpecFlow.EmailReport
{
    public class TestResultMailBody
    {
        private string _mailBody = @"<html>
<head>
<title>Test Result</title>
<style>th { background-color: #4CAF50; color: white; }</style>
</head>
<body>
<p>Hi there,</p>
<p>Below is the test result summary, please find the test details in attachment.</p>
<p>
Total  Test Run: TotalRun<br>
Passed Test Run: PassRun<br>
Failed Test Run: FailRun<br>
</p>
<table style=""width:100%"">
  <tr>
    <th>Features</th>
    <th>Passed Test Cases</th> 
    <th>Failed Test Cases</th>
  </tr>
  TableContents
</table>
<p>Thanks,<br>QA</p>
</body>
</html>";

        public string Body => _mailBody;

        public TestResultMailBody AppendTestResultDetails(Dictionary<string, TestResultPerFeature> testResults)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var result in testResults)
            {
                string tr = $@"
  <tr>
    <td>{result.Key}</td>
    <td>{result.Value.Pass}</td> 
    <td>{result.Value.Fail}</td>
  </tr>";
                builder.Append(tr);
            }
            _mailBody = _mailBody.Replace("TableContents", builder.ToString());
            return this;
        }

        public TestResultMailBody AppendTestResultSummary(int passedTests, int failedTests)
        {
            int total = passedTests + failedTests;
            _mailBody = _mailBody.Replace("TotalRun", total.ToString())
                .Replace("PassRun", $"{passedTests} ({passedTests * 100 / total}%)")
                .Replace("FailRun", $"{failedTests} ({failedTests * 100 / total}%)");
            return this;
        }
    }
}
