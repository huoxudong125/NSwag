//-----------------------------------------------------------------------
// <copyright file="WebApiToSwaggerGeneratorSettings.cs" company="NSwag">
//     Copyright (c) Rico Suter. All rights reserved.
// </copyright>
// <license>https://github.com/NSwag/NSwag/blob/master/LICENSE.md</license>
// <author>Rico Suter, mail@rsuter.com</author>
//-----------------------------------------------------------------------

using NJsonSchema;

namespace NSwag.CodeGeneration.SwaggerGenerators.WebApi
{
    /// <summary>Settings for the <see cref="WebApiAssemblyToSwaggerGenerator"/>.</summary>
    public class WebApiToSwaggerGeneratorSettings : JsonSchemaGeneratorSettings
    {
        /// <summary>Webs the API assembly to swagger generator settings.</summary>
        public WebApiToSwaggerGeneratorSettings()
        {
            DefaultUrlTemplate = "api/{controller}/{action}/{id}";
        }

        /// <summary>Gets or sets the default Web API URL template.</summary>
        public string DefaultUrlTemplate { get; set; }
    }
}