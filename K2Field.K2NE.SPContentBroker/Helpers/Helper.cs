﻿using System;
using System.Collections.Generic;
using System.Text;
using SourceCode.SmartObjects.Services.ServiceSDK.Objects;
using SourceCode.SmartObjects.Services.ServiceSDK.Types;
using System.Text.RegularExpressions;

namespace K2Field.K2NE.SPContentBroker.Helpers
{
    public class Helper
    {

        /// <summary>
        /// Creates a system name from the given name. 
        /// 
        /// This would remove spaces and other fancy characters.
        /// </summary>
        /// <param name="name">Name to change</param>
        /// <returns>The system name</returns>
        public static string MakeSystemName(string name)
        {
            return name.Replace(" ", "");
        }


        /// <summary>
        /// Method that adds a space before a captical letter, this makes CamelCasing more readable.
        /// Examples:
        /// CamelCasing => Camel Casing
        /// This isSparta => This is Sparta
        /// </summary>
        /// <param name="name">A camelcased name.</param>
        /// <returns></returns>
        public static string AddSpaceBeforeCaptialLetter(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return string.Empty;
            }
            StringBuilder newText = new StringBuilder(name.Length + 10);
            newText.Append(name[0]);
            for (int i = 1; i < name.Length; i++)
            {
                if (char.IsUpper(name[i]) && name[i - 1] != ' ')
                {
                    if (i + 1 < name.Length && !char.IsUpper(name[i + 1]))
                    {
                        newText.Append(' ');
                    }
                    if (i - 1 > 0 && !char.IsUpper(name[i - 1]) && newText[newText.Length - 1] != ' ')
                    {
                        newText.Append(' ');
                    }
                }
                newText.Append(name[i]);
            }
            return newText.ToString();
        }

        /// <summary>
        /// See <see cref="CreateSpecificProperty"/>.
        /// This method however simply uses AddSpaceBeforeCapitalLetter(name) for it's displayname.
        /// </summary>
        public static Property CreateProperty(string name, SoType type, string description)
        {
            return CreateSpecificProperty(name, AddSpaceBeforeCaptialLetter(name), description, type);
        }

        public static Property CreateProperty(string name, string description, SoType type)
        {
            return CreateSpecificProperty(name, AddSpaceBeforeCaptialLetter(name), description, type);
        }


        /// <summary>
        /// Create an instance of a Service Object property
        /// This method allows you to set the displayname specifically.
        /// </summary>
        /// <param name="name">DisplayName of the property</param>
        /// <param name="displayName">The displayname to use.</param>
        /// <param name="description">A short description.</param>
        /// <param name="type">SMO Type of the property.</param>
        /// <returns>The property</returns>
        public static Property CreateSpecificProperty(string name, string displayName, string description, SoType type)
        {
            Property property = new Property
            {
                Name = name,
                SoType = type,
                Type = MapHelper.GetTypeBySoType(type),
                MetaData = new MetaData(displayName, description)
            };
            return property;

        }

        /// <summary>
        /// Creates a service object method with the given name, description and Methodtype
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="methodType"></param>
        /// <returns></returns>
        public static Method CreateMethod(string name, string description, MethodType methodType)
        {
            Method m = new Method
            {
                Name = name,
                Type = methodType,
                MetaData = new MetaData(AddSpaceBeforeCaptialLetter(name), description)
            };
            return m;
        }

        /// <summary>
        /// Create a service object with a name and description.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public static ServiceObject CreateServiceObject(string name, string description)
        {
            return CreateServiceObject(name, AddSpaceBeforeCaptialLetter(name), description);
        }

        /// <summary>
        /// Create a service object with a name and description.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="displayName"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public static ServiceObject CreateServiceObject(string name, string displayName, string description)
        {
            ServiceObject so = new ServiceObject();
            so.Name = name;
            so.MetaData = new MetaData(displayName, description);
            so.Active = true;
            return so;
        }

        /// <summary>
        /// Check if special characters exist in ZoneName
        /// </summary>
        /// <param name="zoneName">Name of a zone</param>
        /// <returns></returns>
        public static bool SpecialCharactersExist(string zoneName)
        {
            Regex pattern = new Regex(@"^[a-zA-Z0-9]*$");
            return pattern.IsMatch(zoneName);
        }



        public static void AddSiteURLParameter(Method method)
        {
            MethodParameter methodParameter = new MethodParameter(Constants.InternalProperties.SiteUrl);
            methodParameter.MetaData.DisplayName = AddSpaceBeforeCaptialLetter(Constants.InternalProperties.SiteUrl);
            methodParameter.Type = "System.String";
            methodParameter.SoType = SoType.Text;
            methodParameter.IsRequired = true;
            method.MethodParameters.Create(methodParameter);
        }

        public static void AddStringParameter(Method method, string name, bool isRequired = true)
        {
            MethodParameter methodParameter = new MethodParameter(name);
            methodParameter.MetaData.DisplayName = AddSpaceBeforeCaptialLetter(name);
            methodParameter.Type = "System.String";
            methodParameter.SoType = SoType.Text;
            methodParameter.IsRequired = isRequired;
            method.MethodParameters.Create(methodParameter);
        }

    }
}
