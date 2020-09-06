
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Description;


namespace ClinicWeb
{
    public class RemoveVerbsFilter:IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            //var paths = new OpenApiPaths();
            //foreach (var path in swaggerDoc.Paths)
            //{
            //    paths.Add(path.Key.Replace("v{version}", swaggerDoc.Info.Version), path.Value);
            //}
            //swaggerDoc.Paths = paths;
          
                 swaggerDoc.Components.Schemas.Remove("Day");
            swaggerDoc.Components.Schemas.Remove("Bills");
            swaggerDoc.Components.Schemas.Remove("Clinic");
            swaggerDoc.Components.Schemas.Remove("Doctor");
        }


    }
}
