using System.Collections.Generic;
using System.Data;

namespace MRP.Model
{
    class Report
    {

        public List<ComponentReport> GetReport(List<ComponentPlanning> components)
        {
            var results = new List<ComponentReport>();


            foreach(ComponentPlanning component in components)
            {

                var report = new ComponentReport
                {
                    Results = GetResultsInTable(component),
                    AvailableBalance = component.StartAvailableBalance.ToString(),
                    LeadTime = component.LeadTime.ToString(),
                    NameComponent = component.NameComponent,
                    LotSize = component.LotSize
                };

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
                workRow[i] = component.Weeks[i].PlannedOrderReleases.ToString();
            }

            table.Rows.Add(workRow);
        }

        private void AddBalanceRow(ComponentPlanning component, DataTable table)
        {
            DataRow workRow = table.NewRow();

            for (int i = 0; i < 9; i++)
            {
                workRow[i] = component.Weeks[i].AvailableBalance.ToString();
            }

            table.Rows.Add(workRow);
        }

        private void AddGrossRow(ComponentPlanning component, DataTable table)
        {

            DataRow workRow = table.NewRow();

            for(int i=0;i<9;i++)
            {
                workRow[i] = component.Weeks[i].GrossRequirements.ToString();
            }

            table.Rows.Add(workRow);
        }

        private void AddPlannedReceiptsRow(ComponentPlanning component, DataTable table)
        {

            DataRow workRow = table.NewRow();

            for (int i = 0; i < 9; i++)
            {
                workRow[i] = component.Weeks[i].PlannedOrderReceipts.ToString();
            }

            table.Rows.Add(workRow);
        }

    }
}
