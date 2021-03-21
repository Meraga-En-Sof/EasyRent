using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyRent.Constants
{

    public class FileFormat
    {

        public static List<string> GetSupportedImageTypeExtensionsList()
        {
            List<string> Pictures = new List<string>()
        {
            ".JPG",".PNG",".Gif", ".Gif",".gif",".Png",".png",".Jpg",".jpg"
        };
            return Pictures;
        }
        public static List<string> GetSupportedVideoTypeExtensionsList()
        {
            List<string> Videos = new List<string>()
            {
                ".3GP",".MP4",".AVI",".3gp",".Mp4",".mp4",".Avi",".avi"
            };
            return Videos;
        }
        public static List<string> GetSupportedDocumentTypeExtensionsList()
        {
            List<string> Documents = new List<string>()
            {
                ".DOC",".DOCX",".PDF",".PDT",".XLMS",".PPT",".PPTX",".TXT", ".doc",".Doc",".Docx",".docx",".Pdf",".pdf",".Pdt",".xlsx",".xlxm",".XLSX",".XLSM",".pdt",".txt"
            };
            return Documents;
        }

        public static List<string> GetSupportedPowPointTypeExtensionsList()
        {
            List<string> Documents = new List<string>()
            {
                ".ppt",".pptm",".pptx",".PPT",".PPTM",".PPTX",".Ppt",".Pptm",".Pptx"
            };
            return Documents;
        }


        public static List<string> GetSupportedWordTypeExtensionsList()
        {
            List<string> Documents = new List<string>()
            {
                ".DOC",".DOCX", ".doc",".Doc",".Docx",".docx"
            };
            return Documents;
        }

        public static List<string> GetSupportedExcelTypeExtensionsList()
        {
            List<string> Documents = new List<string>()
            {
                ".XLMS",".xlsx",".xlxm",".XLSX",".XLSM"
            };
            return Documents;
        }

        public static List<string> GetSupportedPDFTypeExtensionsList()
        {
            List<string> Documents = new List<string>()
            {
                ".PDF",".Pdf",".pdf"
            };
            return Documents;
        }

        public static List<string> GetSupportedTypeExtensionsList()
        {
            List<string> All = new List<string>()
            {
                ".JPG",".PNG",".Gif",".DOC",".DOCX",".PDF",".PDT",".XLMS",".PPT",".PPTX",".TXT",".3GP",".MP4",".AVI" +
                ".Gif",".gif",".Png",".png",".Jpg",".jpg"+
                 ".doc",".Doc",".Docx",".docx",".Pdf",".pdf",".Pdt",".xlsx",".xlxm",".XLSX",".XLSM",".pdt",".txt"+
                 ".3gp",".Mp4",".mp4",".Avi",".avi",".pptx",".pptm"

            };


            return All;


        }

    }


}
