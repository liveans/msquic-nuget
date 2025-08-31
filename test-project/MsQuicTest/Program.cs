using System.Net.Quic;

Console.WriteLine("=== MsQuic Native Library Test ===");
Console.WriteLine($"Runtime: {System.Runtime.InteropServices.RuntimeInformation.RuntimeIdentifier}");
Console.WriteLine($".NET Version: {Environment.Version}");
Console.WriteLine();

bool testPassed = false;
string failureReason = "";

try
{
    Console.WriteLine($"QuicConnection.IsSupported: {QuicConnection.IsSupported}");
    
    if (QuicConnection.IsSupported)
    {
        Console.WriteLine("✅ QUIC is supported! The MsQuic native library was loaded successfully.");
        
        // Try to get some additional info about QUIC support
        Console.WriteLine($"QuicListener.IsSupported: {QuicListener.IsSupported}");
        
        if (QuicListener.IsSupported)
        {
            Console.WriteLine("✅ QUIC classes are accessible and working");
            Console.WriteLine("✅ TEST_RESULT: SUCCESS");
            testPassed = true;
        }
        else
        {
            Console.WriteLine("❌ QuicListener is not supported");
            failureReason = "QuicListener not supported";
        }
    }
    else
    {
        Console.WriteLine("❌ QUIC is not supported. Check if the MsQuic native library is available.");
        failureReason = "QUIC not supported";
    }
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Error testing QUIC support: {ex.Message}");
    Console.WriteLine($"Exception Type: {ex.GetType().Name}");
    if (ex.InnerException != null)
    {
        Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
    }
    failureReason = $"Exception: {ex.Message}";
}

Console.WriteLine();
Console.WriteLine("=== Native Library Search Paths ===");
try 
{
    var searchPaths = AppContext.GetData("NATIVE_DLL_SEARCH_DIRECTORIES");
    if (searchPaths != null)
    {
        Console.WriteLine($"Native DLL Search Directories: {searchPaths}");
    }
    
    Console.WriteLine($"Current Directory: {Environment.CurrentDirectory}");
    Console.WriteLine($"BaseDirectory: {AppDomain.CurrentDomain.BaseDirectory}");
}
catch (Exception ex)
{
    Console.WriteLine($"Error getting search paths: {ex.Message}");
}

Console.WriteLine();
if (testPassed)
{
    Console.WriteLine("🎉 All tests passed! MsQuic NuGet package is working correctly.");
    Environment.Exit(0);
}
else
{
    Console.WriteLine($"💥 TEST_RESULT: FAILED - {failureReason}");
    Environment.Exit(1);
}
