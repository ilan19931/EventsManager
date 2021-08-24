using EventsManagerLogic.Classes;
using EventsManagerLogic.Events;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsManagerLogic.Helpers
{
    public class EventsFactory
    {
        public static Event CreateEvent(DataRow data)
        {
            Event newEvent = null;

            switch((EEventMode) data["mode"])
            {
                case EEventMode.Regular:
                    newEvent = new RegularEvent(data);
                    break;

                case EEventMode.Important:
                    newEvent = new ImportantEvent(data);
                    break;

                case EEventMode.Issue:
                    newEvent = new IssueEvent(data);
                    break;

                case EEventMode.Task:
                    newEvent = new TaskEvent(data);
                    break;
            }

            return newEvent;

        }
        /*
        public static Event CreateEvent(int i_Id, string i_Title, string i_Details, int i_Mode, int i_Category, string i_DateCreated, int i_GroupId, int i_IsClosed = 0)
        {
            Event newEvent = null;
            EEventMode eEventMode = (EEventMode)i_Mode;

            switch (eEventMode)
            {
                case EEventMode.Regular:
                    newEvent = new RegularEvent(i_Id, i_Title, i_Details, i_Mode, i_Category, i_DateCreated, i_GroupId, i_IsClosed = 0);
                    break;

                case EEventMode.Important:
                    newEvent = new ImportantEvent(i_Id, i_Title, i_Details, i_Mode, i_Category, i_DateCreated, i_GroupId, i_IsClosed = 0);
                    break;

                case EEventMode.Issue:
                    newEvent = new IssueEvent(i_Id, i_Title, i_Details, i_Mode, i_Category, i_DateCreated, i_GroupId, i_IsClosed = 0);
                    break;

                case EEventMode.Task:
                    newEvent = new TaskEvent(i_Id, i_Title, i_Details, i_Mode, i_Category, i_DateCreated, i_GroupId, i_IsClosed = 0);
                    break;
            }

            return newEvent;

        }
        */

        public static DataTable CreateEventTable(string i_Title, string i_Details, int i_Mode, int i_Category, DateTime i_DateCreated)
        {
            // Create a new DataTable titled 'Names.'
            DataTable eventTable = new DataTable("event");

            // Add three column objects to the table.
            DataColumn titleColumn = new DataColumn();
            titleColumn.DataType = System.Type.GetType("System.String");
            titleColumn.ColumnName = "title";
            eventTable.Columns.Add(titleColumn);

            DataColumn detailsColumn = new DataColumn();
            detailsColumn.DataType = System.Type.GetType("System.String");
            detailsColumn.ColumnName = "details";
            eventTable.Columns.Add(detailsColumn);

            DataColumn modeColumn = new DataColumn();
            modeColumn.DataType = System.Type.GetType("System.Int32");
            modeColumn.ColumnName = "mode";
            eventTable.Columns.Add(modeColumn);

            DataColumn categoryColumn = new DataColumn();
            categoryColumn.DataType = System.Type.GetType("System.Int32");
            categoryColumn.ColumnName = "category";
            eventTable.Columns.Add(categoryColumn);

            DataColumn dateCreatedColumn = new DataColumn();
            dateCreatedColumn.DataType = System.Type.GetType("System.String");
            dateCreatedColumn.ColumnName = "dateCreated";
            eventTable.Columns.Add(dateCreatedColumn);

            // Return the new DataTable.
            return eventTable;
        }
    }
}
