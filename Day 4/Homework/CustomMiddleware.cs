using System.Text.Json;
namespace Day_4;

public class RequestCultureMiddleware
{
    private readonly RequestDelegate _next;

    public RequestCultureMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var headers = new Dictionary<string, string>();
        foreach (var item in context.Request.Headers)
        {
            headers.Add(item.Key, item.Value.ToString());
        }

        var reader = new StreamReader(context.Request.Body);
        var body = await reader.ReadToEndAsync();
        
        var requestData = new
        {
            Scheme = context.Request.Scheme,
            Host = context.Request.Host.ToString(),
            Path = context.Request.Path.ToString(),
            QueryString = context.Request.QueryString.ToString(),
            Body = body,
            Headers = headers
        };

        using (StreamWriter writer = File.AppendText("file.txt"))
        {
            var data = JsonSerializer.Serialize(requestData);
            await writer.WriteLineAsync(data);
            // await writer.WriteLineAsync("Hello, Anyone there?");
        };
        // try
        // {
        //     await context.Response.WriteAsync("Hello world");

        // }
        // catch
        // {

        // }
        // finally
        // {
        await _next(context);
        // }

        // Call the next delegate/middleware in the pipeline.
    }
}

public static class RequestCultureMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestCulture(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestCultureMiddleware>();
    }
}