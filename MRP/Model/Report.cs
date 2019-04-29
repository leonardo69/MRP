using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Kurswork.Model;

namespace Kurswork.Model
{
    class Report
    {

        public List<ComponentReport> GetReport(List<ComponentPlanning> components)
        {
            List<ComponentReport> results = new List<ComponentReport>();


            foreach(ComponentPlanning component in components)
            {

                ComponentReport report = new ComponentReport();
                report.Results = GetResultsInTable(component);
                report.AvaibleBalance = component.startAvailableBalance.ToString();
                report.Leadtime = component.leadTime.ToString();
                report.NameComponent = component.nameComponent;
                report.LotSize = component.lotSize;

                results.Add(report);
            }

            
            return results;

        }

        private DataTable GetResultsInTable(ComponentPlanning component)
        {
            DataTable componentTable = new DataTable();
            for (int i = 0; i < 9; i++)
                componentTable.Columns.Add("Неделя " + (i + 1).ToString());

            AddGrossRow(component, componentTable);
            AddPlannedReceiptsRow(component, componentTable);
            AddBalanceRow(component, componentTable);
            AddPlannedRelease(component, componentTable);
            return componentTable;

        }

        private void AddPlannedRelease(ComponentPlanning component, DataTable table)
        {
            DataRow workRow = table.NewRow();

            for (int i = 0; i < 9; i++)
            {
                workRow[i] = component.weeks[i].plannedOrderReleases.ToString();
            }

            table.Rows.Add(workRow);
        }

        private void AddBalanceRow(ComponentPlanning component, DataTable table)
        {
            DataRow workRow = table.NewRow();

            for (int i = 0; i < 9; i++)
            {
                workRow[i] = component.weeks[i].availableBalance.ToString();
            }

            table.Rows.Add(workRow);
        }

        private void AddGrossRow(ComponentPlanning component, DataTable table)
        {

            DataRow workRow = table.NewRow();

            for(int i=0;i<9;i++)
            {
                workRow[i] = component.weeks[i].grossRequirements.ToString();
            }

            table.Rows.Add(workRow);
        }

        private void AddPlannedReceiptsRow(ComponentPlanning component, DataTable table)
        {

            DataRow workRow = table.NewRow();

            for (int i = 0; i < 9; i++)
            {
                workRow[i] = component.weeks[i].plannedOrderReceipts.ToString();
            }

            table.Rows.Add(workRow);
        }

    }
}
