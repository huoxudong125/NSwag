﻿//-----------------------------------------------------------------------
// <copyright file="MainWindowModel.cs" company="NSwag">
//     Copyright (c) Rico Suter. All rights reserved.
// </copyright>
// <license>https://github.com/NSwag/NSwag/blob/master/LICENSE.md</license>
// <author>Rico Suter, mail@rsuter.com</author>
//-----------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MyToolkit.Storage;
using NJsonSchema.CodeGeneration.CSharp;
using NSwag;
using NSwag.CodeGeneration.ClientGenerators;
using NSwag.Commands;

namespace NSwagStudio.ViewModels.ClientGenerators
{
    public class CSharpClientGeneratorViewModel : ViewModelBase
    {
        private string _clientCode;
        private SwaggerToCSharpCommand _command = new SwaggerToCSharpCommand();

        public bool ShowSettings
        {
            get { return ApplicationSettings.GetSetting("CSharpClientGeneratorViewModel.ShowSettings", true); }
            set { ApplicationSettings.SetSetting("CSharpClientGeneratorViewModel.ShowSettings", value); }
        }

        /// <summary>Gets the settings.</summary>
        public SwaggerToCSharpCommand Command
        {
            get { return _command; }
            set
            {
                if (Set(ref _command, value))
                    RaiseAllPropertiesChanged();
            }
        }

        /// <summary>Gets the list of operation modes. </summary>
        public OperationGenerationMode[] OperationGenerationModes
        {
            get
            {
                return Enum.GetNames(typeof(OperationGenerationMode))
                    .Select(t => (OperationGenerationMode)Enum.Parse(typeof(OperationGenerationMode), t))
                    .ToArray();
            }
        }

        /// <summary>Gets the list of date time types. </summary>
        public CSharpDateTimeType[] DateTimeTypes
        {
            get
            {
                return Enum.GetNames(typeof(CSharpDateTimeType))
                    .Select(t => (CSharpDateTimeType)Enum.Parse(typeof(CSharpDateTimeType), t))
                    .ToArray();
            }
        }

        /// <summary>Gets or sets the namespace usages (comma separated). </summary>
        public string AdditionalNamespaceUsages
        {
            get
            {
                return Command.AdditionalNamespaceUsages != null ? string.Join(",", Command.AdditionalNamespaceUsages) : "";
            }
            set
            {
                if (value != null)
                    Command.AdditionalNamespaceUsages = value.Split(',').Select(n => n.Trim()).ToArray();
                else
                    Command.AdditionalNamespaceUsages = new string[] { };
                RaisePropertyChanged(() => AdditionalNamespaceUsages);
            }
        }

        /// <summary>Gets or sets the client code. </summary>
        public string ClientCode
        {
            get { return _clientCode; }
            set { Set(ref _clientCode, value); }
        }

        public Task GenerateClientAsync(string swaggerData)
        {
            return RunTaskAsync(async () =>
            {
                var code = string.Empty;
                await Task.Run(async () =>
                {
                    if (!string.IsNullOrEmpty(swaggerData))
                    {
                        Command.Input = swaggerData;
                        code = await Command.RunAsync();
                        Command.Input = null;
                    }
                });

                ClientCode = code;
            });
        }

        public override void HandleException(Exception exception)
        {
            MessageBox.Show(exception.Message);
        }
    }
}
