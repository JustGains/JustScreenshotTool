namespace JustScreenshotTool.Models
{
    public class ScreenshotRequest
    {
        /// <summary>
        /// URL to take a screenshot of
        /// </summary>
        public required string Url { get; set; }

        /// <summary>
        /// Wait time after page load in milliseconds (default: 3000ms)
        /// </summary>
        public int WaitTime { get; set; } = 3000;

        /// <summary>
        /// X coordinate to start screenshot from
        /// </summary>
        public int X { get; set; } = 0;

        /// <summary>
        /// Y coordinate to start screenshot from
        /// </summary>
        public int Y { get; set; } = 0;

        /// <summary>
        /// Width of the screenshot in pixels
        /// </summary>
        public int Width { get; set; } = 1280;

        /// <summary>
        /// Height of the screenshot in pixels
        /// </summary>
        public int Height { get; set; } = 800;

        /// <summary>
        /// Scale factor for the screenshot (default: 1.0)
        /// </summary>
        public float Scale { get; set; } = 1.0f;

        /// <summary>
        /// Image format (png, jpeg)
        /// </summary>
        public string Format { get; set; } = "png";
    }
}
