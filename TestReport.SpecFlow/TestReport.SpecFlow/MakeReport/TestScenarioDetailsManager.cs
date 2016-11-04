using System.Collections.Generic;
using System.Linq;
using TestReport.SpecFlow.ReportModels;

namespace TestReport.SpecFlow.MakeReport
{
    public class TestScenarioDetailsManager
    {
        private static TestScenarioDetailsManager _instance;

        private readonly Dictionary<int, TestScenarioDetails> _allTestScenarios = new Dictionary<int, TestScenarioDetails>();

        private int _testId;

        private TestScenarioDetails _currenTestScenarioDetails;

        private TestScenarioDetailsManager()
        {
        }

        public Dictionary<int, TestScenarioDetails> AllTestScenarios => _allTestScenarios;

        public int PassedTestRun => _allTestScenarios.Count(t => t.Value.TestPass);

        public int FailededTestRun => _allTestScenarios.Count(t => !t.Value.TestPass);

        public int AllTestRun => _allTestScenarios.Count;

        public static TestScenarioDetailsManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TestScenarioDetailsManager();
                }
                return _instance;
            }
        }

        public void AddCurrentScenarioInfo(string title, string[] tags)
        {
            _currenTestScenarioDetails = new TestScenarioDetails
            {
                Id = ++_testId,
                Title = title
            };
            _currenTestScenarioDetails.Tags.AddRange(tags);
        }

        public void AddStepToCurrentScenario(string step)
        {
            _currenTestScenarioDetails.Steps.Add(step);
        }

        public void StoreCurrentScenarioInfo(bool testPassResult, string errorMessage = null)
        {
            _currenTestScenarioDetails.TestPass = testPassResult;
            _currenTestScenarioDetails.ErrorMessage = errorMessage;
            _allTestScenarios.Add(_currenTestScenarioDetails.Id, _currenTestScenarioDetails);
        }

        public void SaveScreenShotFile(string filePath)
        {
            _currenTestScenarioDetails.ScreenShotFile = filePath;
        }

        public Dictionary<string, TestResultPerFeature> GetTestResult()
        {
            var results = new Dictionary<string, TestResultPerFeature>();

            foreach (var testDetail in _allTestScenarios)
            {
                if (testDetail.Value.Tags == null || testDetail.Value.Tags.Count.Equals(0))
                {
                    continue;
                }
                foreach (string tag in testDetail.Value.Tags)
                {
                    if (!results.ContainsKey(tag))
                    {
                        var testResult = new TestResultPerFeature
                        {
                            Feature = tag
                        };
                        results.Add(tag, testResult);
                    }

                    if (testDetail.Value.TestPass)
                    {
                        results[tag].Pass++;
                    }
                    else
                    {
                        results[tag].Fail++;
                    }
                }
            }

            return results;
        }
    }
}
