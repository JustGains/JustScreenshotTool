namespace JustScreenshotTool
{
    //https://localhost:7023/Screenshot?url=http%3A%2F%2Flocalhost%3A3000%2Fimage-gen%2Fexercise-og%2F%3FexerciseCode%3DCABLE.CLOSE.GRIP.FRONT.LAT.PULLDOWN&waitTime=6000&x=0&y=0&width=1200&height=630&scale=2&format=png

    public class Program
    {
        public static void Main ( string[] args )
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Add CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseCors("AllowAll");

            app.MapControllers();

            app.Run();
        }
    }
}
