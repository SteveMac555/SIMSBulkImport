﻿using System;
using System.Data;
using System.IO;
using NLog;

namespace SIMSBulkImport
{
    public class ImportFile
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private string _filePath;
        private DataSet _importDataSet;
        private FileType _importFileType;

        internal string SetImportFilePath
        {
            set
            {
                logger.Log(LogLevel.Debug,
                    "Trace:: SIMSBulkImport.ImportFile.SetImportFilePath(SET: " + value + ")");
                if (File.Exists(value))
                {
                    logger.Log(LogLevel.Debug, value);
                    logger.Log(LogLevel.Debug, "Import File is UNC path: " + IsUnc(value));
                    _filePath = value;
                }
                else
                {
                    logger.Log(LogLevel.Error, "File does not exist:" + value);
                }
            }
        }

        internal DataSet GetDataSet
        {
            get
            {
                logger.Log(LogLevel.Debug, "Trace:: SIMSBulkImport.ImportFile.GetDataSet(GET)");
                return _importDataSet;
            }
        }

        internal bool GetIsExcel
        {
            get
            {
                logger.Log(LogLevel.Debug, "Trace:: SIMSBulkImport.ImportFile.GetIsExcel(GET)");
                return (importFileType == FileType.Xls);
            }
        }

        private FileType importFileType
        {
            get
            {
                logger.Log(LogLevel.Debug, "Trace:: SIMSBulkImport.ImportFile.importFileType(GET)");
                string ext = Path.GetExtension(_filePath);
                logger.Log(LogLevel.Debug, "importFileType: " + ext);
                switch (ext)
                {
                    case ".csv":
                        return FileType.Csv;
                    case ".xml":
                        return FileType.Xml;
                    case ".xls":
                        return FileType.Xls;
                    case ".xlsx":
                        return FileType.Xls;
                    case ".txt":
                        return FileType.Csv;
                    default:
                        return FileType.Unknown;
                }
            }
        }

        internal void GetImportDataSet()
        {
            logger.Log(LogLevel.Debug, "Trace:: SIMSBulkImport.ImportFile.GetImportDataSet()");

            _importFileType = importFileType;

            switch (_importFileType)
            {
                case FileType.Csv:
                    var _importCsv = new ImportCsv();
                    _importCsv.SetFilePath = _filePath;
                    _importDataSet = _importCsv.GetDataSet;
                    Switcher.PreImportClass.SetMatchIgnoreFirstRow = true;
                    break;
                case FileType.Xml:
                    var _importXml = new ImportXml();
                    _importXml.SetFilePath = _filePath;
                    _importDataSet = _importXml.GetDataSet;
                    break;
                case FileType.Xls:
                    var _importExcel = new ImportExcel();
                    _importExcel.SetFilePath = _filePath;
                    _importDataSet = _importExcel.GetDataSet;
                    break;
                case FileType.Unknown:
                    break;
            }
        }

        public void Reset()
        {
            // TODO
            logger.Log(LogLevel.Debug, "Trace:: SIMSBulkImport.ImportFile.Reset()");
        }

        private enum FileType
        {
            Xml,
            Csv,
            Xls,
            Unknown
        };

        public bool IsUnc(string path)
        {
            return new Uri(path).IsUnc;
        }
    }
}