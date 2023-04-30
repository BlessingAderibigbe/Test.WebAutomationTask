using NUnit.Framework;

namespace Test.WebAutomationTask
{
    public static class EnvironmentData
    {
        public static string baseUrl { get; } = TestContext.Parameters["baseUrl"];
    }
}
