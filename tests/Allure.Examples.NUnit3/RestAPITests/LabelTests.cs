namespace Allure.Examples.NUnit3.RestAPITests;

[AllureNUnit]
[AllureLabel("layer", "rest")]
[AllureFeature("Labels API")]
class LabelTests
{
    Random random;

    [SetUp]
    [AllureBefore("Setup API connection")]
    public void Setup()
    {
        random = new Random();
    }

    [TearDown]
    [AllureAfter("Dispose API connection")]
    public void Teardown() { }

    [Test]
    [AllureName("Create new label via API")]
    [AllureTag("smoke")]
    public void NewLabelTest()
    {
        PostNewLabel("hello");
        AssertLabel("hello");
    }

    [Test]
    [AllureName("Delete label via API")]
    [AllureTag("regress")]
    public void DeleteLabelTest()
    {
        PostNewLabel("hello");
        DeleteLabel("hello");
        AssertNoLabel("hello");
    }

    [AllureStep("When I create new label with title {title} via API")]
    public void PostNewLabel(string title)
    {
        Step("POST /repos/:owner/:repo/labels");
    }

    [AllureStep("And I delete label with title {title} via API")]
    public void DeleteLabel(string title)
    {
        var labelId = FindLabelByTitle(title);
        Step($"DELETE /repos/:owner/:repo/labels/{labelId}");
    }

    [AllureStep("Then I should see label with title {title} via api")]
    public void AssertLabel(string title)
    {
        var labelId = FindLabelByTitle(title);
        Step($"GET /repos/:owner/:repo/labels/{labelId}");
    }

    [AllureStep("Then I should not see label with title {title} via api")]
    public void AssertNoLabel(string title)
    {
        _ = FindLabelByTitle(title);
    }

    int FindLabelByTitle(string title)
    {
        Step("GET /repos/:owner/:repo/labels?text=" + title);
        return random.Next(1000);
    }
}