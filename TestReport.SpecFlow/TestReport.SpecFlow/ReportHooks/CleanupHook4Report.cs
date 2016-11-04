using System;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using TechTalk.SpecFlow;
using TestReport.SpecFlow.EmailReport;
using TestReport.SpecFlow.MakeReport;

namespace TestReport.SpecFlow.ReportHooks
{
    [Binding]
    public sealed class CleanupHook4Report
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks

        [AfterTestRun(Order = 500)]
        public static void GenerateTestResultReportInHtml()
        {
            string testResultFolder = TestRunContext.GetValue<string>("TestResultFolder");
            string htmlFile =
                $@"{testResultFolder}\TestReport.html";
            var report = new TestResultReportInHtmlManager(htmlFile, TestScenarioDetailsManager.Instance.AllTestScenarios);
            report.GenerateReport();
        }

        [AfterTestRun(Order = 501)]
        public static void PublishTestResultByEmail()
        {
            try
            {
                if (Convert.ToBoolean(ConfigurationManager.AppSettings["sendEmailReport"]))
                {
                    string testResultFolder = TestRunContext.GetValue<string>("TestResultFolder");
                    SendEmailManager.Instance.SendEmail(testResultFolder);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Email sending meets some errors: {0}", e.Message);
            }
        }

        [AfterScenario(Order = 1)]
        public void TakeScreenShotIfFailed()
        {
            if (ScenarioContext.Current.TestError == null) return;

            string testResultFolder = TestRunContext.GetValue<string>("TestResultFolder");
            string screenShotFileName = $@"{ScenarioContext.Current.ScenarioInfo.Title}_{DateTime.Now.ToString("yyyyMMdd_hhmmss")}.jpg";
            string screenShotFile = $@"{testResultFolder}\{screenShotFileName}";

            // Please refer to: http://stackoverflow.com/questions/1163761/capture-screenshot-of-active-window
            Rectangle bounds = Screen.GetBounds(Point.Empty);
            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
                }
                bitmap.Save(screenShotFile, ImageFormat.Jpeg);
            }

            TestScenarioDetailsManager
                .Instance
                .SaveScreenShotFile(screenShotFileName);
        }

        [AfterScenario(Order = 200)]
        public void StoreScenarioInfo()
        {
            bool testPassResult = ScenarioContext.Current.TestError == null;
            string errorMessage = null;
            if (!testPassResult)
            {
                errorMessage = ScenarioContext.Current.TestError.Message;
            }

            TestScenarioDetailsManager
                .Instance
                .StoreCurrentScenarioInfo(testPassResult, errorMessage);
        }
    }
}
