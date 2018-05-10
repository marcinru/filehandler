using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore.Internal;

namespace FileHandler2.Api.Helpers
{
    public static class XmlHelper
    {
        public static string GetTagsXml(List<string> inputTags)
        {
            StringBuilder tagsXmlBuilder = new StringBuilder();

            foreach (var tag in inputTags)
            {
                tagsXmlBuilder.AppendLine($"<{tag} />");
            }

            return tagsXmlBuilder.ToString();
        }

        public static string GetTagsCsv(string tags)
        {
            tags = tags.Replace(",",String.Empty);

            string[] tagsTab = tags.Split(
                '<',
                StringSplitOptions.None);

            for (int i = 0; i < tagsTab.Length; i++)
            {
                tagsTab[i] = tagsTab[i].Replace("/>", string.Empty).Trim();
            }

            return tagsTab.Where(s=>!string.IsNullOrWhiteSpace(s)).Join();
        }
    }
}