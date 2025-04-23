# JustScreenshotTool

A simple, containerized web API for taking high-quality screenshots of web pages or specific sections of pages.

## Quick Example

```bash
# Capture a screenshot of Google's homepage
GET http://localhost:8080/Screenshot?url=https://www.google.com

# Capture a specific section with custom dimensions and wait time
GET http://localhost:8080/Screenshot?url=https://www.example.com&waitTime=5000&x=100&y=200&width=800&height=600&scale=2&format=png
```

## Features

- Take screenshots of entire web pages or specific sections
- Control the wait time after page load
- Specify the starting position (X, Y coordinates)
- Customize screenshot dimensions (width, height)
- Adjust scale factor for higher resolution images
- Choose between PNG and JPEG formats
- Docker-ready for easy deployment

## Technologies

- ASP.NET Core 9.0
- PuppeteerSharp for browser emulation
- Docker containerization

## Usage

### API Endpoints

#### Take a Screenshot

```
GET /Screenshot
```

Query parameters:

- `url` (required): The URL to take a screenshot of
- `waitTime` (optional): Wait time after page load in milliseconds (default: 3000ms)
- `x` (optional): X coordinate to start screenshot from (default: 0)
- `y` (optional): Y coordinate to start screenshot from (default: 0)
- `width` (optional): Width of the screenshot in pixels (default: 1280)
- `height` (optional): Height of the screenshot in pixels (default: 800)
- `scale` (optional): Scale factor for the screenshot (default: 1)
- `format` (optional): Image format - "png" or "jpeg" (default: "png")

Example:

```
GET /Screenshot?url=https://www.example.com&waitTime=3000&x=0&y=0&width=1280&height=800&scale=1&format=png
```

Parameters:

- `url` (required): The URL to take a screenshot of
- `waitTime` (optional): Wait time after page load in milliseconds (default: 3000ms)
- `x` (optional): X coordinate to start screenshot from (default: 0)
- `y` (optional): Y coordinate to start screenshot from (default: 0)
- `width` (optional): Width of the screenshot in pixels (default: 1280)
- `height` (optional): Height of the screenshot in pixels (default: 800)
- `scale` (optional): Scale factor for the screenshot (default: 1)
- `format` (optional): Image format - "png" or "jpeg" (default: "png")

Response:

The API returns the image file directly with the appropriate content type.

## Running Locally

```bash
dotnet run --project JustScreenshotTool
```

## Docker

Build the Docker image:

```bash
docker build -t justscreenshottool .
```

Run the container:

```bash
docker run -p 8080:80 justscreenshottool
```

Then access the API at `http://localhost:8080/Screenshot`