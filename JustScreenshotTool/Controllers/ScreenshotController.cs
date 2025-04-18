using Microsoft.AspNetCore.Mvc;

using PuppeteerSharp;
using PuppeteerSharp.Media;

namespace JustScreenshotTool.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScreenshotController : ControllerBase
    {
        private readonly ILogger<ScreenshotController> _logger;

        public ScreenshotController ( ILogger<ScreenshotController> logger )
        {
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> TakeScreenshot (
            [FromQuery] string url,
            [FromQuery] int waitTime = 3000,
            [FromQuery] int x = 0,
            [FromQuery] int y = 0,
            [FromQuery] int width = 1280,
            [FromQuery] int height = 800,
            [FromQuery] int scale = 1,
            [FromQuery] string format = "png" )
        {
            if ( format == "jpg" )
                format = "jpeg";

            try
            {
                // Validate URL
                if ( string.IsNullOrEmpty(url) || !IsValidUrl(url) )
                {
                    return BadRequest("Invalid URL provided");
                }

                // Validate dimensions
                if ( width <= 0 || height <= 0 || x < 0 || y < 0 )
                {
                    return BadRequest("Invalid dimensions provided");
                }

                // Validate scale
                if ( scale <= 0 )
                {
                    return BadRequest("Invalid scale provided");
                }

                // Validate format
                if ( format != "png" && format != "jpeg" )
                {
                    return BadRequest("Format must be 'png' or 'jpeg'");
                }

                // Download the browser if it doesn't exist
                var browserFetcherOptions = new BrowserFetcherOptions();
                var browserFetcher = new BrowserFetcher(browserFetcherOptions);
                await browserFetcher.DownloadAsync();

                await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions
                {
                    Headless = true,
                    Args = new string[] { "--no-sandbox", "--disable-setuid-sandbox" } // Required for running in Docker
                });

                await using var page = await browser.NewPageAsync();

                // Set viewport with scale factor
                await page.SetViewportAsync(new ViewPortOptions
                {
                    Width = width,
                    Height = height,
                    DeviceScaleFactor = scale
                });

                // Navigate to URL
                await page.GoToAsync(url, new NavigationOptions
                {
                    WaitUntil = new[] { WaitUntilNavigation.Load, WaitUntilNavigation.DOMContentLoaded }
                });

                // Wait additional time if specified
                if ( waitTime > 0 )
                {
                    await Task.Delay(waitTime);
                }

                // Take screenshot of the specified area
                var screenshotOptions = new ScreenshotOptions
                {
                    Type = format == "png" ? ScreenshotType.Png : ScreenshotType.Jpeg,
                    Clip = new Clip
                    {
                        X = x,
                        Y = y,
                        Width = width,
                        Height = height,
                        Scale = scale
                    }
                };

                if ( format == "jpeg" )
                {
                    screenshotOptions.Quality = 90;
                }

                var screenshotData = await page.ScreenshotDataAsync(screenshotOptions);

                // Return the image
                return File(screenshotData, format == "png" ? "image/png" : "image/jpeg");
            }
            catch ( Exception ex )
            {
                _logger.LogError(ex, "Error taking screenshot");
                return StatusCode(500, $"Error taking screenshot: {ex.Message}");
            }
        }

        private bool IsValidUrl ( string url )
        {
            return Uri.TryCreate(url, UriKind.Absolute, out Uri? uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}
