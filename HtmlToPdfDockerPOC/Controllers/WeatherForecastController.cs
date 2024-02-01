

using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using SkiaSharp;
using System.IO;





namespace HtmlToPdfDockerPOC.Controllers
{
    [ApiController]
    [Route("test")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly IRazorViewEngine _razorViewEngine;
        private readonly ITempDataProvider _tempDataProvider;
        private readonly IServiceProvider _serviceProvider;
        private readonly IWebHostEnvironment _environment;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IRazorViewEngine razorViewEngine, ITempDataProvider tempDataProvider, IServiceProvider serviceProvider, IWebHostEnvironment environment)
        {
            _logger = logger;
            _razorViewEngine = razorViewEngine;
            _tempDataProvider = tempDataProvider;
            _serviceProvider = serviceProvider;
            _environment = environment;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<FileStreamResult> Get()
        {
            try
            {
                var qouteSerliezed = @"{""Status"":1,""ZipCode"":"""",""Project"":{""Name"":""project1"",""Limit"":0.00,""Address"":null,""City"":null,""State"":null,""FramingDate"":""0001-01-01T00:00:00"",""SalesOrders"":[],""ProjectCompanies"":[],""Qoutes"":[],""Id"":""019a6f34-348b-4b6e-9d92-6c3473933388"",""CreationDate"":""2021-07-16T10:12:58.169793"",""ModificationDate"":null,""CreatedBy"":""Anonymous"",""ModifiedBy"":null,""SerialNumber"":1},""ExpireDate"":""2023-04-06T00:00:00"",""FromWarehouse"":null,""Customer"":{""Id"":""d9315195-5f1f-41d3-9963-f8bc4a48b1a8"",""Name"":""Daniel Limon"",""Email"":""mglil@steelnetwork.com"",""CompanyId"":""00d9929e-0f23-4ff6-9024-0011be489ccf"",""CompanyName"":""HD Supply - White Cap - San Antonio"",""CompanyTypeId"":""22e19760-c191-446a-9f80-836544693ca9"",""CompanyTypeName"":""Distributor""},""ShippingCustomer"":null,""ShippingAddress"":null,""ShippingDate"":null,""AppliedShippingOptionId"":null,""Shipping"":0.00,""ShippingNotes"":null,""ItemsTotal"":0.00,""DiscountValue"":0.00,""SubTotal"":0.00,""Tax"":0.00,""Total"":0.00,""Items"":[{""Product"":{""Id"":""904b3b57-b542-4c49-8745-0022eb9e116b"",""TsnNumber"":0,""Name"":""14.447in 68mil 50ksi G60*"",""Number"":483,""Nature"":""Simple"",""MeasurmentUnitId"":""0f6b10ba-98fa-4863-9861-4f37a2adb5b1"",""MeasurmentUnit"":""Foot"",""Measurement"":1,""ERPFreeShipping"":false,""StoreFreeShipping"":false,""ERPFullContainer"":false,""StoreFullContainer"":false,""LengthInInches"":12.00000000480000,""WidthInInches"":14.4470,""HeightInInches"":0.0000,""WeightInLbs"":3.6730,""Size"":1.0000,""FitInPallet"":null,""Type"":2},""SubProductItems"":null,""AttributesValues"":[{""Id"":""585d9911-268c-4560-8985-994692dad270"",""Value"":null}],""RowNumber"":2,""Notes"":"""",""Quantity"":1,""ProductPackage"":null,""DefaultProductPackage"":null,""Value"":0.00,""ValueInFeet"":{""Feet"":0,""Inches"":0,""UpperFraction"":0,""LowerFraction"":1000},""IsBrokenBox"":false,""UnitPrice"":0.09,""BrokenBoxFees"":null,""IsBrokenBoxFeesApplied"":false,""Discount"":0.00,""DiscountType"":1,""ExtraFees"":0.00,""DiscountValue"":0.000000,""SubTotal"":0.0000,""Total"":0.000000,""ShippedItems"":null,""Id"":""f5f76b69-f348-47a6-991e-82ffa62f7c45"",""CreationDate"":""2024-01-11T21:17:11.2614419"",""ModificationDate"":null,""CreatedBy"":""admin@tsn.com"",""ModifiedBy"":null,""SerialNumber"":3053},{""Product"":{""Id"":""284c19af-0c73-40c2-9376-000ba2d61949"",""TsnNumber"":4321,""Name"":""12.664in 68mil 50ksi G60"",""Number"":499,""Nature"":""Simple"",""MeasurmentUnitId"":""0f6b10ba-98fa-4863-9861-4f37a2adb5b1"",""MeasurmentUnit"":""Foot"",""Measurement"":1,""ERPFreeShipping"":false,""StoreFreeShipping"":false,""ERPFullContainer"":false,""StoreFullContainer"":false,""LengthInInches"":19.20000000768000,""WidthInInches"":12.6640,""HeightInInches"":3.6000,""WeightInLbs"":2.5000,""Size"":1.6000,""FitInPallet"":null,""Type"":2},""SubProductItems"":null,""AttributesValues"":[{""Id"":""585d9911-268c-4560-8985-994692dad270"",""Value"":""""}],""RowNumber"":1,""Notes"":"""",""Quantity"":1,""ProductPackage"":null,""DefaultProductPackage"":{""Id"":""95cd5632-8b31-6a0e-413a-7693d9df2620"",""Type"":""Box"",""Name"":""Box-8 in x 6 in x 4 in-10.00 Units"",""WeightInLbs"":25.400000,""Quantity"":10.00,""TotalUnitsQuantity"":10.00,""PackageSize"":8.0000,""PackageLengthInInches"":8.0000,""PackageWidthInInches"":4.0000,""PackageHeightInInches"":4.0000},""Value"":0.00,""ValueInFeet"":{""Feet"":0,""Inches"":0,""UpperFraction"":0,""LowerFraction"":1000},""IsBrokenBox"":false,""UnitPrice"":0.23,""BrokenBoxFees"":null,""IsBrokenBoxFeesApplied"":false,""Discount"":0.00,""DiscountType"":1,""ExtraFees"":0.00,""DiscountValue"":0.000000,""SubTotal"":0.0000,""Total"":0.000000,""ShippedItems"":null,""Id"":""6b079fe2-f1cb-4c4d-ac13-e4d50104cb20"",""CreationDate"":""2024-01-11T21:17:11.2613769"",""ModificationDate"":null,""CreatedBy"":""admin@tsn.com"",""ModifiedBy"":null,""SerialNumber"":3052},{""Product"":{""Id"":""f5b11684-6a62-5ff6-6a94-00a142f9ae9e"",""TsnNumber"":989898,""Name"":""362SG250-54,50ksi   "",""Number"":73,""Nature"":""Simple"",""MeasurmentUnitId"":""0f6b10ba-98fa-4863-9861-4f37a2adb5b1"",""MeasurmentUnit"":""Foot"",""Measurement"":1,""ERPFreeShipping"":false,""StoreFreeShipping"":false,""ERPFullContainer"":false,""StoreFullContainer"":false,""LengthInInches"":12.00000000480000,""WidthInInches"":10.1960,""HeightInInches"":0.0000,""WeightInLbs"":1.9619,""Size"":1.0000,""FitInPallet"":null,""Type"":2},""SubProductItems"":null,""AttributesValues"":[{""Id"":""585d9911-268c-4560-8985-994692dad270"",""Value"":null},{""Id"":""830e0e99-af3a-467e-b037-c10a52cc2a54"",""Value"":null}],""RowNumber"":3,""Notes"":"""",""Quantity"":1,""ProductPackage"":null,""DefaultProductPackage"":null,""Value"":0.00,""ValueInFeet"":{""Feet"":0,""Inches"":0,""UpperFraction"":0,""LowerFraction"":1000},""IsBrokenBox"":false,""UnitPrice"":1.32,""BrokenBoxFees"":null,""IsBrokenBoxFeesApplied"":false,""Discount"":0.00,""DiscountType"":1,""ExtraFees"":0.00,""DiscountValue"":0.000000,""SubTotal"":0.0000,""Total"":0.000000,""ShippedItems"":null,""Id"":""528c9446-ffb2-4d00-9b10-f636e038c2bf"",""CreationDate"":""2024-01-11T21:17:11.2614514"",""ModificationDate"":null,""CreatedBy"":""admin@tsn.com"",""ModifiedBy"":null,""SerialNumber"":3051}],""BaseOrderItems"":[{""Product"":{""Id"":""904b3b57-b542-4c49-8745-0022eb9e116b"",""TsnNumber"":0,""Name"":""14.447in 68mil 50ksi G60*"",""Number"":483,""Nature"":""Simple"",""MeasurmentUnitId"":""0f6b10ba-98fa-4863-9861-4f37a2adb5b1"",""MeasurmentUnit"":""Foot"",""Measurement"":1,""ERPFreeShipping"":false,""StoreFreeShipping"":false,""ERPFullContainer"":false,""StoreFullContainer"":false,""LengthInInches"":12.00000000480000,""WidthInInches"":14.4470,""HeightInInches"":0.0000,""WeightInLbs"":3.6730,""Size"":1.0000,""FitInPallet"":null,""Type"":2},""SubProductItems"":null,""AttributesValues"":[{""Id"":""585d9911-268c-4560-8985-994692dad270"",""Value"":null}],""RowNumber"":2,""Notes"":"""",""Quantity"":1,""ProductPackage"":null,""DefaultProductPackage"":null,""Value"":0.00,""ValueInFeet"":{""Feet"":0,""Inches"":0,""UpperFraction"":0,""LowerFraction"":1000},""IsBrokenBox"":false,""UnitPrice"":0.09,""BrokenBoxFees"":null,""IsBrokenBoxFeesApplied"":false,""Discount"":0.00,""DiscountType"":1,""ExtraFees"":0.00,""DiscountValue"":0.000000,""SubTotal"":0.0000,""Total"":0.000000,""ShippedItems"":null,""Id"":""f5f76b69-f348-47a6-991e-82ffa62f7c45"",""CreationDate"":""2024-01-11T21:17:11.2614419"",""ModificationDate"":null,""CreatedBy"":""admin@tsn.com"",""ModifiedBy"":null,""SerialNumber"":3053},{""Product"":{""Id"":""284c19af-0c73-40c2-9376-000ba2d61949"",""TsnNumber"":4321,""Name"":""12.664in 68mil 50ksi G60"",""Number"":499,""Nature"":""Simple"",""MeasurmentUnitId"":""0f6b10ba-98fa-4863-9861-4f37a2adb5b1"",""MeasurmentUnit"":""Foot"",""Measurement"":1,""ERPFreeShipping"":false,""StoreFreeShipping"":false,""ERPFullContainer"":false,""StoreFullContainer"":false,""LengthInInches"":19.20000000768000,""WidthInInches"":12.6640,""HeightInInches"":3.6000,""WeightInLbs"":2.5000,""Size"":1.6000,""FitInPallet"":null,""Type"":2},""SubProductItems"":null,""AttributesValues"":[{""Id"":""585d9911-268c-4560-8985-994692dad270"",""Value"":""""}],""RowNumber"":1,""Notes"":"""",""Quantity"":1,""ProductPackage"":null,""DefaultProductPackage"":{""Id"":""95cd5632-8b31-6a0e-413a-7693d9df2620"",""Type"":""Box"",""Name"":""Box-8 in x 6 in x 4 in-10.00 Units"",""WeightInLbs"":25.400000,""Quantity"":10.00,""TotalUnitsQuantity"":10.00,""PackageSize"":8.0000,""PackageLengthInInches"":8.0000,""PackageWidthInInches"":4.0000,""PackageHeightInInches"":4.0000},""Value"":0.00,""ValueInFeet"":{""Feet"":0,""Inches"":0,""UpperFraction"":0,""LowerFraction"":1000},""IsBrokenBox"":false,""UnitPrice"":0.23,""BrokenBoxFees"":null,""IsBrokenBoxFeesApplied"":false,""Discount"":0.00,""DiscountType"":1,""ExtraFees"":0.00,""DiscountValue"":0.000000,""SubTotal"":0.0000,""Total"":0.000000,""ShippedItems"":null,""Id"":""6b079fe2-f1cb-4c4d-ac13-e4d50104cb20"",""CreationDate"":""2024-01-11T21:17:11.2613769"",""ModificationDate"":null,""CreatedBy"":""admin@tsn.com"",""ModifiedBy"":null,""SerialNumber"":3052},{""Product"":{""Id"":""f5b11684-6a62-5ff6-6a94-00a142f9ae9e"",""TsnNumber"":989898,""Name"":""362SG250-54,50ksi   "",""Number"":73,""Nature"":""Simple"",""MeasurmentUnitId"":""0f6b10ba-98fa-4863-9861-4f37a2adb5b1"",""MeasurmentUnit"":""Foot"",""Measurement"":1,""ERPFreeShipping"":false,""StoreFreeShipping"":false,""ERPFullContainer"":false,""StoreFullContainer"":false,""LengthInInches"":12.00000000480000,""WidthInInches"":10.1960,""HeightInInches"":0.0000,""WeightInLbs"":1.9619,""Size"":1.0000,""FitInPallet"":null,""Type"":2},""SubProductItems"":null,""AttributesValues"":[{""Id"":""585d9911-268c-4560-8985-994692dad270"",""Value"":null},{""Id"":""830e0e99-af3a-467e-b037-c10a52cc2a54"",""Value"":null}],""RowNumber"":3,""Notes"":"""",""Quantity"":1,""ProductPackage"":null,""DefaultProductPackage"":null,""Value"":0.00,""ValueInFeet"":{""Feet"":0,""Inches"":0,""UpperFraction"":0,""LowerFraction"":1000},""IsBrokenBox"":false,""UnitPrice"":1.32,""BrokenBoxFees"":null,""IsBrokenBoxFeesApplied"":false,""Discount"":0.00,""DiscountType"":1,""ExtraFees"":0.00,""DiscountValue"":0.000000,""SubTotal"":0.0000,""Total"":0.000000,""ShippedItems"":null,""Id"":""528c9446-ffb2-4d00-9b10-f636e038c2bf"",""CreationDate"":""2024-01-11T21:17:11.2614514"",""ModificationDate"":null,""CreatedBy"":""admin@tsn.com"",""ModifiedBy"":null,""SerialNumber"":3051}],""ShippingOptions"":[],""SpecialDeliveryOptions"":null,""Id"":""2d68192b-0a4b-4637-8f69-72e610613262"",""CreationDate"":""2022-04-06T16:43:26.990485"",""ModificationDate"":""2024-01-11T21:17:11.2614655"",""CreatedBy"":""ahernandez@steelnetwork.com"",""ModifiedBy"":""admin@tsn.com"",""SerialNumber"":1002}";
                var quote = Newtonsoft.Json.JsonConvert.DeserializeObject<Quote>(qouteSerliezed);
                var viewPath = "Views/SendQuoteToContact.cshtml";
                var html = await RenderToStringAsync(null, viewPath, quote);


                var renderer = new ChromePdfRenderer();

                // Create a PDF from a HTML string
                var pdf = renderer.RenderHtmlAsPdf(html).Stream;

                // Create a MemoryStream to save the PDF
            
                return new FileStreamResult(pdf, "application/pdf");
            }
            
            
            
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                throw new FieldAccessException(ex.Message);
            }
            throw new FieldAccessException("");

        }


       
        //private static string GeneratePdf()
        //{
        //    //GeneratedBarcode barcode = BarcodeWriter.CreateBarcode("1023", BarcodeWriterEncoding.UPCA);
        //    //barcode.ResizeTo(400, 120);
        //    //barcode.AddBarcodeValueTextBelowBarcode();
        //    //barcode.ChangeBarCodeColor(IronSoftware.Drawing.Color.Black);
        //    //barcode.SetMargins(10);
        //    //var stream = barcode.ToStream();
        //    //stream.Position = 0;
        //    //byte[] bytes = new byte[stream.Length];

        //    //// Read the stream into the byte array
        //    //stream.Read(bytes, 0, bytes.Length);

        //    //// Convert the byte array to a Base64 string
        //    //string base64String = Convert.ToBase64String(bytes);

        //    //return base64String;
        //}

        //public async Task<byte[]> GetPDF(string html)
        //{
        //    using MemoryStream stream = new MemoryStream();
        //    using PdfWriter writer = new PdfWriter(stream);
        //    using (iText.Kernel.Pdf.PdfDocument pdf = new iText.Kernel.Pdf.PdfDocument(writer))
        //    {
        //        ConverterProperties properties = new ConverterProperties();
        //        HtmlConverter.ConvertToPdf(html, pdf, properties);
        //    }

        //    return stream.ToArray();
        //}

        public async Task<string> RenderToStringAsync(string executingFilePath, string viewPath, object model)
        
        {
            var httpContext = new DefaultHttpContext { RequestServices = _serviceProvider };
            var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());
            using (var sw = new StringWriter())
            {
                var viewResult = _razorViewEngine.GetView(executingFilePath, viewPath, false);
                if (viewResult.View == null)
                {
                    throw new ArgumentNullException($"{viewPath} does not match any available view");
                }
                var viewDictionary =
                    new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                    {
                        Model = model
                    };
                var viewContext = new ViewContext(
                    actionContext,
                    viewResult.View,
                    viewDictionary,
                    new TempDataDictionary(actionContext.HttpContext, _tempDataProvider),
                    sw,
                    new HtmlHelperOptions()
                );
                await viewResult.View.RenderAsync(viewContext);
                return sw.ToString();
            }
        }
    }

    
}
