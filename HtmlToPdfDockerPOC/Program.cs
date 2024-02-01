



namespace HtmlToPdfDockerPOC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddControllersWithViews();
            License.LicenseKey = "IRONSUITE.MGLIL7.GMAIL.COM.27823-50EB3D6AA7-DIJCE-CAPGTWS2MUYY-7ROAAFIEG2MR-3QEDUDHJRFEG-BVCKRA5MCJZP-IJLF6TSJIEEW-4P5AMMH47FAP-JJRDKV-TNBJODUR7Y6LUA-DEPLOYMENT.TRIAL-YJAYN4.TRIAL.EXPIRES.01.MAR.2024";
            builder.Services.AddRazorPages();
            builder.Services.AddLogging();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            app.UseStaticFiles();
            app.Run();

           
        }
    }
}
