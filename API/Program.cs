namespace API;

/// <summary>
/// Main class of the API
/// </summary>
public static class Program {
    /// <summary>
    /// Entry point
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args) {
        CreateHostBuilder(args).Build().Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder => {
            webBuilder.UseStartup<Startup>();
        });
}