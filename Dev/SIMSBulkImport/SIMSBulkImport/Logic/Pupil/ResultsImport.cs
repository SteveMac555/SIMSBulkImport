﻿/*
 * Developer : Matt Smith (matt@matt40k.co.uk)
 * All code (c) Matthew Smith all rights reserved
 */

using System;
using System.Data;

namespace Matt40k.SIMSBulkImport.Pupil
{
    public class ResultsImport
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private DataTable pupilTable;

        public ResultsImport()
        {
            CreateResultTable();
        }

        private void CreateResultTable()
        {
            logger.Log(NLog.LogLevel.Debug, "Generating Pupil Result table");

            pupilTable = new DataTable("Pupil Import Results");
            pupilTable.Columns.Add(new DataColumn("Surname", typeof(string)));
            pupilTable.Columns.Add(new DataColumn("Forename", typeof(string)));
            pupilTable.Columns.Add(new DataColumn("Gender", typeof(string)));
            pupilTable.Columns.Add(new DataColumn("Admission_Number", typeof(string)));
            pupilTable.Columns.Add(new DataColumn("DOB", typeof(string)));
            pupilTable.Columns.Add(new DataColumn("Year", typeof(string)));
            pupilTable.Columns.Add(new DataColumn("Registration", typeof(string)));
            pupilTable.Columns.Add(new DataColumn("House", typeof(string)));
            pupilTable.Columns.Add(new DataColumn("PersonID", typeof(string)));
            pupilTable.Columns.Add(new DataColumn("Result", typeof(string)));
            pupilTable.Columns.Add(new DataColumn("Item", typeof(string)));
            pupilTable.Columns.Add(new DataColumn("Value", typeof(string)));
            pupilTable.Columns.Add(new DataColumn("Notes", typeof(string)));
        }

        public bool AddToResultsTable(string surname, string forename, string personID, string result, string item, string value, string notes)
        {
            try
            {
                DataRow newrow = pupilTable.NewRow();
                newrow["Surname"] = surname;
                newrow["Forename"] = forename;
                newrow["Gender"] = "";
                newrow["Admission_Number"] = "";
                newrow["DOB"] = "";
                newrow["Year"] = "";
                newrow["Registration"] = "";
                newrow["House"] = "";
                newrow["PersonID"] = personID;
                newrow["Result"] = result;
                newrow["Item"] = item;
                newrow["Value"] = value;
                newrow["Notes"] = notes;
                pupilTable.Rows.Add(newrow);
            }
            catch (Exception AddToResultsTable_Exception)
            {
                logger.Log(NLog.LogLevel.Error, AddToResultsTable_Exception);
                return false;
            }
            return true;
        }

        public DataTable GetPupilResultsTable
        {
            get
            {
                return pupilTable;
            }
        }
    }
}
