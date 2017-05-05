/**
 * 
 * Code from the book "CLR via C# 4.0, Jeffrey Richter"  
 *
 **/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public static class Program
{
    public static void Main()
    {
        ValueTypePerfTest();
        ReferenceTypePerfTest();
    }

    private static void ValueTypePerfTest()
    {
        const Int32 count = 10000000;

        using (new OperationTimer("List<Int32>"))
        {
            List<Int32> l = new List<Int32>(count);
            for (Int32 n = 0; n < count; n++)
            {
                l.Add(n);
                Int32 x = l[n];
            }
            l = null;  // Make sure this gets GC'd
        }

        using (new OperationTimer("ArrayList of Int32"))
        {
            ArrayList a = new ArrayList();
            for (Int32 n = 0; n < count; n++)
            {
                a.Add(n);
                Int32 x = (Int32)a[n];
            }
            a = null;  // Make sure this gets GC'd
        }
    }

    static void ReferenceTypePerfTest()
    {
        const Int32 count = 10000000;

        using (new OperationTimer("List<String>"))
        {
            List<String> l = new List<String>();
            for (Int32 n = 0; n < count; n++)
            {
                l.Add("X");
                String x = l[n];
            }
            l = null;  // Make sure this gets GC'd
        }

        using (new OperationTimer("ArrayList of String"))
        {
            ArrayList a = new ArrayList();
            for (Int32 n = 0; n < count; n++)
            {
                a.Add("X");
                String x = (String)a[n];
            }
            a = null;  // Make sure this gets GC'd
        }
    }
}

// This is useful for doing operation performance timing.
internal sealed class OperationTimer : IDisposable
{
    private Int64 m_startTime;
    private String m_text;
    private Int32 m_collectionCount;

    public OperationTimer(String text)
    {
        PrepareForOperation();

        m_text = text;
        m_collectionCount = GC.CollectionCount(0);

        // This should be the last statement in this 
        // method to keep timing as accurate as possible
        m_startTime = Stopwatch.GetTimestamp();
    }

    public void Dispose()
    {
        Console.WriteLine("{0,6:###.00} seconds (GCs={1,3}) {2}",
           (Stopwatch.GetTimestamp() - m_startTime) /
              (Double)Stopwatch.Frequency,
           GC.CollectionCount(0) - m_collectionCount, m_text);
    }

    private static void PrepareForOperation()
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();
    }
}
