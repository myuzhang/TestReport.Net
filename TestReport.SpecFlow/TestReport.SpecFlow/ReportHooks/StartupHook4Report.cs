using System;
using System.Configuration;
using System.IO;
using TechTalk.SpecFlow;
using TestReport.SpecFlow.MakeReport;

namespace TestReport.SpecFlow.ReportHooks
{
    [Binding]
    public sealed class StartupHook4Report
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks

        [BeforeTestRun(Order = 12)]
        public static void CreateTestResultsFolder()
        {
            string failedTestResultFolder = ConfigurationManager.AppSettings["testResultFolder"];
            failedTestResultFolder = $@"{failedTestResultFolder}\{DateTime.Now.ToString("yyyy-MM-dd HH_mm")}";
            if (!Directory.Exists(failedTestResultFolder))
            {
                Directory.CreateDirectory(failedTestResultFolder);
            }
            TestRunContext.SaveValue("TestResultFolder", failedTestResultFolder);
        }

        [BeforeScenario(Order = 200)]
        public void LogScenarioInfo()
        {
            TestScenarioDetailsManager
                .Instance
                .AddCurrentScenarioInfo(
                ScenarioContext.Current.ScenarioInfo.Title,
                ScenarioContext.Current.ScenarioInfo.Tags);
        }

        [BeforeStep(Order = 200)]
        public void LogStepInfo()
        {
            TestScenarioDetailsManager
                .Instance
                .AddStepToCurrentScenario(ScenarioContext.Current.StepContext.StepInfo.Text);
        }
    }
}
