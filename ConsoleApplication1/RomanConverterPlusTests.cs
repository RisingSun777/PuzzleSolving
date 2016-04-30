using System;
using System.Collections.Generic;
using System.Linq;

// ROMAN CONVERTER - AUTHOR: NGUYEN QUOC THAI DUONG - EMAIL: THAIDUONGQUOCNGUYEN@GMAIL.COM

#region Implementation

public class RomanConverterHelper
{
    public Dictionary<int, string> ReferenceTable { get; private set; }

    public RomanConverterHelper()
    {
        ReferenceTable = new Dictionary<int, string>();

        ReferenceTable.Add(1, "I");
        ReferenceTable.Add(5, "V");
        ReferenceTable.Add(10, "X");
        ReferenceTable.Add(50, "L");
        ReferenceTable.Add(100, "C");
        ReferenceTable.Add(500, "D");
        ReferenceTable.Add(1000, "M");
    }

    public Tuple<int, int> GetRomanReferenceRangeWithinNumber(int input)
    {
        int lowerBound = ReferenceTable.Keys.Where(a => a <= input).Max();
        int upperBound = ReferenceTable.Keys.Where(a => a >= input).Min();

        return new Tuple<int, int>(lowerBound, upperBound);
    }

    public int NumberOfDigits(int input)
    {
        if (input < 0)
            throw new NotSupportedException();

        int ret = 0;

        do
        {
            ++ret;
        }
        while ((input = input / 10) > 0);
        
        return ret;
    }
}

public class RomanConverter
{
    private RomanConverterHelper romanConverterHelper;

    public static readonly int MAX_BOUND = 1000;
    public static readonly int MIN_BOUND = 1;

    public RomanConverter()
    {
        romanConverterHelper = new RomanConverterHelper();
    }

    private string ResultFromPrecedenceNumberToUpperBound(int input, Tuple<int, int> range)
    {
        int numOfDigitsForInput = romanConverterHelper.NumberOfDigits(input);
        int lowerBoundForDigits = 0;

        foreach (var item in romanConverterHelper.ReferenceTable)
        {
            if (romanConverterHelper.NumberOfDigits(item.Key) == numOfDigitsForInput)
            {
                lowerBoundForDigits = item.Key;
                break;
            }
        }

        int subtractResult = range.Item2 - input;

        if (subtractResult == lowerBoundForDigits)
            return romanConverterHelper.ReferenceTable[lowerBoundForDigits] + romanConverterHelper.ReferenceTable[range.Item2];
        else if (subtractResult < lowerBoundForDigits && subtractResult > 0)
            return romanConverterHelper.ReferenceTable[lowerBoundForDigits] + romanConverterHelper.ReferenceTable[range.Item2] + FromDecimal(input - (range.Item2 - lowerBoundForDigits));

        return null;
    }

    private string ResultFromReferenceTable(int input)
    {
        foreach (var keyValue in romanConverterHelper.ReferenceTable)
        {
            if (input == keyValue.Key)
                return keyValue.Value;
        }

        return null;
    }

    private string ResultForNormalCases(int input, Tuple<int, int> range)
    {
        string result = "";

        int subtractor = input;
        while (subtractor > range.Item1)
        {
            result += romanConverterHelper.ReferenceTable[range.Item1];
            subtractor -= range.Item1;
        }

        result += FromDecimal(subtractor);

        return result;
    }

    public string FromDecimal(int input)
    {
        if (input > RomanConverter.MAX_BOUND)
            throw new NotSupportedException();

        if (input < RomanConverter.MIN_BOUND)
            throw new NotSupportedException();

        string result = "";

        result = ResultFromReferenceTable(input);
        if (!string.IsNullOrEmpty(result))
            return result;

        var range = romanConverterHelper.GetRomanReferenceRangeWithinNumber(input);

        result = ResultFromPrecedenceNumberToUpperBound(input, new Tuple<int, int>(range.Item1, range.Item2));
        if (!string.IsNullOrEmpty(result))
            return result;

        return ResultForNormalCases(input, range);
    }
}

#endregion

#region Unit Testing

public static class UnitTesting
{
    public static void AlertTestSucceeded(string testName)
    {
        Console.WriteLine(string.Format("Testing for " + testName + " Ok.\r\n"));
    }

    public static void AlertTestFailed(string testName, string msg = null)
    {
        Console.WriteLine("Testing for " + testName + " failed.");

        if (msg != null)
            Console.WriteLine("Due to: " + msg);

        Console.WriteLine();
    }

    public static void AlertTestInitializing(string testName)
    {
        Console.WriteLine("Testing for {0}", testName);
    }

    public static string CheckForEqual<T>(T expected, T actual)
    {
        return 
            EqualityComparer<T>.Default.Equals(expected, actual) ? 
            string.Empty : 
            string.Format("Expected: {0}, actual: {1}", expected, actual);
    }

    public static void AssertAreEqual<T>(T expected, T actual, string messageIfFail = null)
    {
        var assertingResult = CheckForEqual(expected, actual);

        if (assertingResult != string.Empty)
            Console.WriteLine(string.Format("{0}{1}", messageIfFail ?? string.Empty, assertingResult));
    }

    public static void AssertNotNull<T>(T value, string messageIfFail = null) where T : class
    {
        var assertingResult = CheckForEqual(null, value);

        if (string.IsNullOrEmpty(assertingResult))
            Console.WriteLine(string.Format("{0}{1}", messageIfFail ?? string.Empty, value + " is null"));
    }
}

public class RomanConverterHelperTest
{
    private RomanConverterHelper romanConverterHelper;

    public RomanConverterHelperTest()
    {
        romanConverterHelper = new RomanConverterHelper();
    }

    public void RomanConverterHelper_WhenObjectIsCreatedSuccessfully_ReferenceDictionaryShouldContainCorrectData()
    {
        string testName = "RomanConverterHelper_WhenObjectIsCreatedSuccessfully_ReferenceDictionaryShouldContainCorrectData";

        UnitTesting.AlertTestInitializing(testName);

        UnitTesting.AssertAreEqual(7, romanConverterHelper.ReferenceTable.Count, "The size of reference table is incorrect. ");

        UnitTesting.AssertAreEqual("I", romanConverterHelper.ReferenceTable[1], "The corresponding values from reference table are incorrect. ");
        UnitTesting.AssertAreEqual("V", romanConverterHelper.ReferenceTable[5], "The corresponding values from reference table are incorrect. ");
        UnitTesting.AssertAreEqual("X", romanConverterHelper.ReferenceTable[10], "The corresponding values from reference table are incorrect. ");
        UnitTesting.AssertAreEqual("L", romanConverterHelper.ReferenceTable[50], "The corresponding values from reference table are incorrect. ");
        UnitTesting.AssertAreEqual("C", romanConverterHelper.ReferenceTable[100], "The corresponding values from reference table are incorrect. ");
        UnitTesting.AssertAreEqual("D", romanConverterHelper.ReferenceTable[500], "The corresponding values from reference table are incorrect. ");
        UnitTesting.AssertAreEqual("M", romanConverterHelper.ReferenceTable[1000], "The corresponding values from reference table are incorrect. ");

        UnitTesting.AlertTestSucceeded(testName);
    }

    public void NumberOfDigits_ValidInput_ReturnCorrectNumberOfDigits()
    {
        string testName = "NumberOfDigits_ValidInput_ReturnCorrectNumberOfDigits";

        UnitTesting.AlertTestInitializing(testName);

        UnitTesting.AssertAreEqual(1, romanConverterHelper.NumberOfDigits(2), "Number of digits is incorrect. ");
        UnitTesting.AssertAreEqual(2, romanConverterHelper.NumberOfDigits(22), "Number of digits is incorrect. ");
        UnitTesting.AssertAreEqual(1, romanConverterHelper.NumberOfDigits(0), "Number of digits is incorrect. ");

        UnitTesting.AlertTestSucceeded(testName);
    }

    public void NumberOfDigits_InputIsNegative_ThrowNotSupportedException()
    {
        string testName = "NumberOfDigits_InputIsNegative_ThrowNotSupportedException";

        UnitTesting.AlertTestInitializing(testName);

        try
        {
            romanConverterHelper.NumberOfDigits(-2);
            UnitTesting.AlertTestFailed(testName, "NotSupportedException not thrown with negative input.");
        }
        catch(NotSupportedException)
        {
            UnitTesting.AlertTestSucceeded(testName);
        }
    }

    public void GetRomanReferenceRangeWithinNumber_InputIsWithinARangeInReferenceTable_OutputCorrectRange()
    {
        string testName = "GetRomanReferenceRangeWithinNumber_InputIsWithinARangeInReferenceTable_OutputCorrectRange";

        UnitTesting.AlertTestInitializing(testName);

        Tuple<int, Tuple<int, int>>[] dataset = new Tuple<int, Tuple<int, int>>[] 
        {
            new Tuple<int, Tuple<int, int>>(3, new Tuple<int, int>(1, 5)),
            new Tuple<int, Tuple<int, int>>(8, new Tuple<int, int>(5, 10)),
            new Tuple<int, Tuple<int, int>>(20, new Tuple<int, int>(10, 50)),
            new Tuple<int, Tuple<int, int>>(80, new Tuple<int, int>(50, 100)),
            new Tuple<int, Tuple<int, int>>(300, new Tuple<int, int>(100, 500)),
            new Tuple<int, Tuple<int, int>>(800, new Tuple<int, int>(500, 1000)),
        };

        for (int i = 0; i < dataset.Length; ++i)
            UnitTesting.AssertAreEqual(dataset[i].Item2, romanConverterHelper.GetRomanReferenceRangeWithinNumber(dataset[i].Item1), "Range returned is incorrect.");

        UnitTesting.AlertTestSucceeded(testName);
    }

    public void GetRomanReferenceRangeWithinNumber_InputIsAValueInReferenceTable_OutputBoundaryWithSameValueFromReferenceTable()
    {
        string testName = "GetRomanReferenceRangeWithinNumber_InputIsAValueInReferenceTable_OutputBoundaryWithSameValueFromReferenceTable";

        UnitTesting.AlertTestInitializing(testName);

        Tuple<int, Tuple<int, int>> dataset = 
            new Tuple<int, Tuple<int, int>>(10, new Tuple<int, int>(10, 10));

        UnitTesting.AssertAreEqual(dataset.Item2, romanConverterHelper.GetRomanReferenceRangeWithinNumber(dataset.Item1), "Range returned is incorrect.");

        UnitTesting.AlertTestSucceeded(testName);
    }
}

public class RomanConverterTest
{
    private RomanConverter romanConverter;

    public RomanConverterTest()
    {
        romanConverter = new RomanConverter();
    }

    public void InputIsOutsideOfDefinedRange_ThrowNotSupportedException()
    {
        string testName = "InputIsOutsideOfDefinedRange_ThrowNotSupportedException";
        int[] inputs = new int[] { 0, -1, 1001 };

        UnitTesting.AlertTestInitializing(testName);

        foreach (var input in inputs)
        {
            try
            {
                string ret = romanConverter.FromDecimal(input);
                UnitTesting.AlertTestFailed(testName, string.Format("NotSupportedException not thrown with input {0}", input));
            }
            catch (NotSupportedException)
            {
                UnitTesting.AlertTestSucceeded(testName);
            }
        }
    }

    public void InputIsFromReferenceTable_OutputRomanLettersAccordingly(int input, string expected)
    {
        string testName = "InputIsFromReferenceTable_OutputRomanLettersAccordingly";

        UnitTesting.AlertTestInitializing(string.Format("{0} with input: {1}", testName, input));

        string ret = romanConverter.FromDecimal(input);

        UnitTesting.AssertAreEqual(expected, ret, "Converted value is incorrect. ");

        if (ret != expected)
        {
            UnitTesting.AlertTestFailed("InputIsFromReferenceTable_OutputRomanLettersAccordingly", string.Format("{0} is not converted to {1}", input, expected));
            return;
        }

        UnitTesting.AlertTestSucceeded(testName);
    }
}

#endregion

public class Test
{
    private static void TestRomanConverterHelper()
    {
        RomanConverterHelperTest test = new RomanConverterHelperTest();

        test.RomanConverterHelper_WhenObjectIsCreatedSuccessfully_ReferenceDictionaryShouldContainCorrectData();
        test.GetRomanReferenceRangeWithinNumber_InputIsWithinARangeInReferenceTable_OutputCorrectRange();
        test.GetRomanReferenceRangeWithinNumber_InputIsAValueInReferenceTable_OutputBoundaryWithSameValueFromReferenceTable();
        test.NumberOfDigits_ValidInput_ReturnCorrectNumberOfDigits();
        test.NumberOfDigits_InputIsNegative_ThrowNotSupportedException();
    }

    private static void TestRomanConverter()
    {
        RomanConverterTest tester = new RomanConverterTest();

        tester.InputIsOutsideOfDefinedRange_ThrowNotSupportedException();

        tester.InputIsFromReferenceTable_OutputRomanLettersAccordingly(1, "I");
        tester.InputIsFromReferenceTable_OutputRomanLettersAccordingly(5, "V");
        tester.InputIsFromReferenceTable_OutputRomanLettersAccordingly(10, "X");
        tester.InputIsFromReferenceTable_OutputRomanLettersAccordingly(2, "II");
        tester.InputIsFromReferenceTable_OutputRomanLettersAccordingly(3, "III");
        tester.InputIsFromReferenceTable_OutputRomanLettersAccordingly(6, "VI");
        tester.InputIsFromReferenceTable_OutputRomanLettersAccordingly(8, "VIII");
        tester.InputIsFromReferenceTable_OutputRomanLettersAccordingly(80, "LXXX");
        tester.InputIsFromReferenceTable_OutputRomanLettersAccordingly(438, "CDXXXVIII");

        tester.InputIsFromReferenceTable_OutputRomanLettersAccordingly(9, "IX");
        tester.InputIsFromReferenceTable_OutputRomanLettersAccordingly(19, "XIX");
        tester.InputIsFromReferenceTable_OutputRomanLettersAccordingly(49, "XLIX");
        tester.InputIsFromReferenceTable_OutputRomanLettersAccordingly(900, "CM");
        tester.InputIsFromReferenceTable_OutputRomanLettersAccordingly(999, "CMXCIX");
    }

    public static void Main()
    {
        TestRomanConverterHelper();
        TestRomanConverter();
    }
}