using System;
using System.Collections;
using System.Collections.Generic;
using Mdl.WebApi.Services;
using NUnit.Framework;

namespace Mdl.WebApi.Tests;

public class MailValidatorTests
{
    [TestCaseSource(typeof(MailValidatorTestData))]
    public void CheckIsValidMailTest(string email, bool isValid)
    {
        var actual = MailValidator.IsValidMail(email);
        Assert.AreEqual(isValid, actual);
    }

    [Test]
    public void CheckIsValidMailNullTest()
    {
        string? mail = null;
        Assert.Throws<ArgumentException>(() => MailValidator.IsValidMail(mail));
    }
}

internal class MailValidatorTestData : IEnumerable<TestCaseData>
{
    public IEnumerator<TestCaseData> GetEnumerator()
    {
        yield return new TestCaseData("test@test.com", true);
        yield return new TestCaseData("123@test.com", true);
        yield return new TestCaseData("1@", false);
        yield return new TestCaseData("@", false);
        yield return new TestCaseData(string.Empty, false);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}