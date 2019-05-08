using System.Collections.Generic;
using MRP.Core;

namespace MRP.Model
{
    internal class Manager
    {
        private ComponentPlanning _componentA;
        private ComponentPlanning _componentB;
        private ComponentPlanning _componentC;
        private ComponentPlanning _componentD;


        public void LoadDataFromDb()
        {
            _componentA = new ComponentPlanning("Душевая кабина");
            _componentB = new ComponentPlanning("Каркас со стенками");
            _componentC = new ComponentPlanning("Ручной душ");
            _componentD = new ComponentPlanning("Смеситель");

            _componentA.IsMain = true;

            _componentB.IsMain = false;
            _componentB.ComponentParent = _componentA;

            _componentC.IsMain = false;
            _componentC.ComponentParent = _componentA;

            _componentD.IsMain = false;
            _componentD.ComponentParent = _componentC;

            InitMainPlan(_componentA);
            InitStore(_componentA, _componentB, _componentC, _componentD);
            InitLeadTime(_componentA, _componentB, _componentC, _componentD);
            InitLotSize(_componentA, _componentB, _componentC, _componentD);
        }

        private void InitLotSize(params ComponentPlanning[] components)
        {
            var loadLotSize = "SELECT Объём_партии.Обозначение FROM Объём_партии;";
            var table = DbAccess.ExecuteDataTable(loadLotSize);

            for (var i = 0; i < 4; i++)
                components[i].LotSize = table.Rows[i][0].ToString();
        }

        private void InitLeadTime(params ComponentPlanning[] components)
        {
            var loadLeadTime = "SELECT Время_выполнения_заказа.Время FROM Время_выполнения_заказа;";
            var table = DbAccess.ExecuteDataTable(loadLeadTime);

            for (var i = 0; i < 4; i++)
                components[i].LeadTime = int.Parse(table.Rows[i][0].ToString());
        }

        private void InitStore(params ComponentPlanning[] components)
        {
            var loadStoreData = "SELECT Склад.Кол_компонента FROM Склад;";
            var table = DbAccess.ExecuteDataTable(loadStoreData);
            for (var i = 0; i < 4; i++)
                components[i].StartAvailableBalance = int.Parse(table.Rows[i][0].ToString());
        }


        private void InitMainPlan(ComponentPlanning componentA)
        {
            var mainPlanQuery =
                "SELECT Глав_производ_план.Неделя1, Глав_производ_план.Неделя2, Глав_производ_план.Неделя3, Глав_производ_план.Неделя4, "
                + "Глав_производ_план.Неделя5, Глав_производ_план.Неделя6, Глав_производ_план.Неделя7, Глав_производ_план.Неделя8, Глав_производ_план.Неделя9 "
                + "FROM Глав_производ_план WHERE (((Глав_производ_план.ID_компонента)=1));";
            var table = DbAccess.ExecuteDataTable(mainPlanQuery);

            for (var i = 0; i < 9; i++) componentA.Weeks[i].GrossRequirements = int.Parse(table.Rows[0][i].ToString());
        }


        public void AnalyseData()
        {
            _componentA.MakeCalculation();
            SetupGrossRequirements(_componentB);
            _componentB.MakeCalculation();
            SetupGrossRequirements(_componentC);
            _componentC.MakeCalculation();
            SetupGrossRequirements(_componentD);
            _componentD.MakeCalculation();
        }

        public List<ComponentReport> MakeReport()
        {
            var comps = new List<ComponentPlanning> {_componentA, _componentB, _componentC, _componentD};
            var report = new Report();
            var results = report.GetReport(comps);

            return results;
        }

        private void SetupGrossRequirements(ComponentPlanning component)
        {
            var parent = component.ComponentParent;
            for (var i = 0; i < 9; i++) component.Weeks[i].GrossRequirements = parent.Weeks[i].PlannedOrderReleases;
        }
    }
}