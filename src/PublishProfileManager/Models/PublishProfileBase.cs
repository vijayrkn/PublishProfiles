using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using Microsoft.Build.Evaluation;

namespace PublishProfileManager.Models
{
    public abstract class PublishProfileBase
    {
        public override string ToString()
        {
            string msDeployPublishXmlContents = null;
            using (MemoryStream stream = new MemoryStream())
            {
                using (XmlTextWriter xmlWriter = new XmlTextWriter(stream, Encoding.UTF8) { Formatting = Formatting.Indented })
                {
                    xmlWriter.WriteStartDocument();
                    xmlWriter.WriteStartElement("Project");
                    xmlWriter.WriteAttributeString("ToolsVersion", "4.0");
                    xmlWriter.WriteAttributeString("xmlns", "http://schemas.microsoft.com/developer/msbuild/2003");
                    xmlWriter.WriteStartElement("PropertyGroup");
                    foreach (PropertyInfo pi in GetPropertyInfo())
                    {
                        if (pi.GetValue(this, null) != null)
                        {
                            xmlWriter.WriteElementString(pi.Name, pi.GetValue(this, null).ToString());
                        }
                    }
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndDocument();
                    xmlWriter.Flush();
                    stream.Position = 0;
                    // TODO: Logic to handle itemgroups
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        msDeployPublishXmlContents = reader.ReadToEnd();
                    }
                }
            }

            return msDeployPublishXmlContents;
        }

        public void LoadModel(string pubxmlContents)
        {
            using (XmlTextReader reader = new XmlTextReader(new StringReader(pubxmlContents)))
            {
                Project project = new Project(reader);
                foreach (PropertyInfo pi in GetPropertyInfo())
                {
                    ProjectProperty msbuildProperty = project.AllEvaluatedProperties.FirstOrDefault(prop => string.Equals(prop.Name, pi.Name, StringComparison.OrdinalIgnoreCase) && !prop.IsEnvironmentProperty);
                    if (msbuildProperty != null)
                    {
                        object propertyValue = msbuildProperty.EvaluatedValue;
                        if (pi.PropertyType == typeof(bool))
                        {
                            propertyValue = Convert.ChangeType(propertyValue, TypeCode.Boolean);
                        }

                        pi.SetValue(this, propertyValue);
                    }
                }
            }
        }

        private IOrderedEnumerable<PropertyInfo> GetPropertyInfo()
        {
            Type t = GetType();
            return 
                t.GetProperties()
               .Where(prop => prop.PropertyType == typeof(string) || prop.PropertyType == typeof(bool))
               .OrderBy(p => p.GetCustomAttributes(typeof(DisplayAttribute), true)
               .Cast<DisplayAttribute>()
               .Select(a => a.Order)
               .FirstOrDefault());
        }

        public IReadOnlyList<KeyValuePair<string, object>> GetProfileProperties()
        {
            List<KeyValuePair<string, object>> profileProperties = new List<KeyValuePair<string, object>>();
            foreach (var pi in GetPropertyInfo())
            {
                if (pi.GetValue(this, null) != null)
                {
                    profileProperties.Add(new KeyValuePair<string, object>(pi.Name, pi.GetValue(this, null)));
                }
            }

            return profileProperties;
        }
    }
}