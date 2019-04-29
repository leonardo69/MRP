using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using System.Data;

namespace Kurswork.Model
{
    class Manager
    {

        ComponentPlanning componentA;
        ComponentPlanning componentB;
        ComponentPlanning componentC;
        ComponentPlanning componentD;


        public void LoadDataFromDB()
        {
            componentA = new ComponentPlanning("Душевая кабина");
            componentB = new ComponentPlanning("Каркас со стенками");
            componentC = new ComponentPlanning("Ручной душ");
            componentD = new ComponentPlanning("Смеситель");

            componentA.IsMain = true;
            
            componentB.IsMain = false;
            componentB.ComponentParent = componentA;

            componentC.IsMain = false;
            componentC.ComponentParent = componentA;

            componentD.IsMain = false;
            componentD.ComponentParent = componentC;

            InitMainPlan(componentA);
            InitStore(componentA, componentB, componentC, componentD);
            InitLeadTime(componentA, componentB, componentC, componentD);
            InitLotSize(componentA, componentB, componentC, componentD);



        }

        private void InitLotSize(params ComponentPlanning[] components)
        {
            string loadLotSize = "SELECT Объём_партии.Обозначение FROM Объём_партии;";
            DataTable table = DBAccess.ExecuteDataTable(loadLotSize);

            for (int i = 0; i < 4;i++ )
                components[i].lotSize = table.Rows[i][0].ToString();
            
        }

        private void InitLeadTime(params ComponentPlanning[] components)
        {
            string loadLeadTime = "SELECT Время_выполнения_заказа.Время FROM Время_выполнения_заказа;";
            DataTable table = DBAccess.ExecuteDataTable(loadLeadTime);

            for (int i = 0; i < 4; i++)
                components[i].leadTime = int.Parse(table.Rows[i][0].ToString());

        }

        private void InitStore(params ComponentPlanning[] components)
        {
            string loadStoreData = "SELECT Склад.Кол_компонента FROM Склад;";
            DataTable table = DBAccess.ExecuteDataTable(loadStoreData);
            for (int i = 0; i < 4; i++)
                components[i].startAvailableBalance = int.Parse(table.Rows[i][0].ToString());
        }

        
        private void InitMainPlan(ComponentPlanning componentA)
        {
            string mainPlanQuery = "SELECT Глав_производ_план.Неделя1, Глав_производ_план.Неделя2, Глав_производ_план.Неделя3, Глав_производ_план.Неделя4, "
            + "Глав_производ_план.Неделя5, Глав_производ_план.Неделя6, Глав_производ_план.Неделя7, Глав_производ_план.Неделя8, Глав_производ_план.Неделя9 "
            + "FROM Глав_производ_план WHERE (((Глав_производ_план.ID_компонента)=1));";
            DataTable table = DBAccess.ExecuteDataTable(mainPlanQuery);

            for (int i = 0; i < 9; i++)
            {
                componentA.weeks[i].grossRequirements = int.Parse(table.Rows[0][i].ToString());
            }

        }

        


        public void AnalyseData()
        {
            
            componentA.MakeCalculation();
            SetupGrossRequirements(componentB);
            componentB.MakeCalculation();
            SetupGrossRequirements(componentC);
            componentC.MakeCalculation();
            SetupGrossRequirements(componentD);
            componentD.MakeCalculation();

           
        }

        public List<ComponentReport> MakeReport()
        {

            List<ComponentPlanning> comps  = new List<ComponentPlanning>() {componentA, componentB, componentC, componentD};
            Report report = new Report();
            List<ComponentReport> results = report.GetReport(comps);
            
            return results;

        }

        private void SetupGrossRequirements(ComponentPlanning component)
        {
            ComponentPlanning parent = component.ComponentParent;
            for(int i=0;i<9; i++)
            {
                component.weeks[i].grossRequirements = parent.weeks[i].plannedOrderReleases;
            }
        }

    }
}
