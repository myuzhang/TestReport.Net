using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
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
            failedTestResultFolder = $@"{failedTestResultFolder}\{DateTime.Now.ToString("yyyy-MM-dd hh_mm")}";
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
