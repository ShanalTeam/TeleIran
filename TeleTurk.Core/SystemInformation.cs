using Microsoft.Win32;
using System.Diagnostics;

public static class SystemInformation
{
    public static string GetFullPCName() => GetManufacturer() + " " + GetModel();

    public static string GetManufacturer() => processWmic("computersystem get manufacturer");
    public static string GetModel() => processWmic("computersystem get model");

    public static string GetOSName()
	{
		try
		{
			RegistryKey rk = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Microsoft\Windows NT\CurrentVersion");
			if (rk == null) return "";
			return (string)rk.GetValue("ProductName");
		}
		catch { return "Unknown"; }
	}

    #region Private methods
    
    static string processWmic(string args)
    {
        var p = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                FileName = "wmic",
                Arguments = args,
                CreateNoWindow = true
            }
        };
        p.Start();
        string output = p.StandardOutput.ReadToEnd();
        p.WaitForExit();
        return output.Split('\n')[1].Trim();
    }

    #endregion
}
