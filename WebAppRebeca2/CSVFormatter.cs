using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WebAppRebeca2.Models;

namespace WebAppRebeca2
{
    public class CSVFormatter : MediaTypeFormatter
    {
        public CSVFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/csv"));
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/csv"));

            SupportedEncodings.Add(new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
            SupportedEncodings.Add(Encoding.GetEncoding("iso-8859-1"));
        }
        public override bool CanWriteType(System.Type type)
        {
            bool Result;
            if (type == typeof(Products2))
            {
                Result = true;
            }
            else
            {
                TypeInfo enumerableType = typeof(IEnumerable<Products2>).GetTypeInfo();
                Result = enumerableType.IsAssignableFrom(type.GetTypeInfo());
            }
            return Result;
        }
        public override bool CanReadType(System.Type type)
        {
            bool Result;
            if (type == typeof(Products2))
            {
                Result = true;
            }
            else
            {
                TypeInfo enumerableType = typeof(IEnumerable<Products2>).GetTypeInfo();
                Result = enumerableType.IsAssignableFrom(type.GetTypeInfo());
            }
            return Result;
        }
        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext)
        {
            
            var ResultTask = Task.Factory.StartNew(() =>
            {
                //Encoding effectiveEncoding = SelectCharacterEncoding(content.Headers);
                Encoding effectiveEncoding = SelectCharacterEncoding(content.Headers);
                var writer = new StreamWriter(writeStream, effectiveEncoding);

                var products = value as IEnumerable<Products2>;
                if (products != null)
                {
                    foreach (var p in products)
                    {
                        writer.WriteLine(
                            $"{p.ProductID}, {p.Nombre}, {p.Precio}, {p.Existencias}");
                    }
                }
                else
                {
                    var p = value as Products2;
                    if (p == null)
                    {
                        throw new InvalidOperationException(
                            "No es posible realizar la serializacion");
                    }
                    writer.WriteLine(
                         $"{p.ProductID}, {p.Nombre}, {p.Precio}, {p.Existencias}");
                }
                writer.Flush();
            });
            return ResultTask;
        }

        public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
        {
            Encoding effectiveEncoding = SelectCharacterEncoding(content.Headers);
            var ResultTask = Task<object>.Factory.StartNew(() =>
            {
                List<Products2> Products = new List<Products2>();
                using (StreamReader reader = new StreamReader(readStream))
                {
                    string Line;
                    while ((Line = reader.ReadLine())!= null)
                    {
                        var values = Line.Split(new string[] { "," },
                            StringSplitOptions.RemoveEmptyEntries);

                        Products.Add(new Models.Products2()
                        {
                            ProductID = int.Parse(values[0]),
                            Nombre = values[1],
                            Precio = decimal.Parse(values[2]),
                            Existencias = short.Parse(values[3])
                        });
                    }
                }
                object Result;
                if (Products.Count ==1)
                {
                    Result = Products[0];
                }
                else
                {
                    Result = Products;
                }
                return Result;
            });
            return ResultTask;
        }
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id})",
                defaults: new { id = RouteParameter.Optional });
            config.Formatters.Add(new CSVFormatter());
        }
    }
}