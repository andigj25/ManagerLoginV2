using OpenQA.Selenium;
using System.IO;
using System.Text.Json;

public static class CookieHelper_V2
{

    // Method to save a cookie as a file
    public static void SaveCookieToFile(IWebDriver driver, string fileName)
    {
        // Get the current set of cookies from the driver
        var cookies = driver.Manage().Cookies.AllCookies;

        // Serialize the cookies to JSON format
        var json = JsonSerializer.Serialize(cookies);

        // Write the JSON data to a file
        var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
        File.WriteAllText(filePath, json);
    }

    // Method to load a cookie from a file
    public static void LoadCookieFromFile(IWebDriver driver, string fileName)
    {
        // Read the JSON data from the file
        var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
        var json = File.ReadAllText(filePath);

        // Deserialize the cookies from JSON format
        var cookies = JsonSerializer.Deserialize<List<Cookie>>(json);

        // Add the cookies to the driver
        foreach (var cookie in cookies)
        {
            driver.Manage().Cookies.AddCookie(cookie);
        }
    }
}
