using BoDi;
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
        public StartupHook4Report(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        IObjectContainer _objectContainer;

        public IObjectContainer ObjectContainer
        {
            get
            {
                if (_objectContainer == null)
                {
                    throw new InvalidOperationException("Cannot access ObjectContainer until after the constructor has fully executed");
                }
                return _objectContainer;
            }
            set
            {
                _objectContainer = value;
            }
        }

        public ScenarioContext ScenarioContext
        {
            get
            {
                return ObjectContainer.Resolve<ScenarioContext>();
            }
        }

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
                ScenarioContext.ScenarioInfo.Title,
                ScenarioContext.ScenarioInfo.Tags);
        }

        [BeforeStep(Order = 200)]
        public void LogStepInfo()
        {
            TestScenarioDetailsManager
                .Instance
                .AddStepToCurrentScenario(ScenarioContext.StepContext.StepInfo.Text);
        }
    }
}
