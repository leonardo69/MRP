using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using MRP.Core;
using MRP.Entities;

namespace MRP.Model
{
    public class Manager
    {

        private List<ComponentPlanning> components = new List<ComponentPlanning>();
        private Component _rootComponentEntity;


        public void LoadAssemblyStructureFromDb(int assemblyId)
        {
            using (var db = new DataContext())
            {
                var allComponents = db.Components.Include(x => x.Children.Select(
                    y => y.Children.Select(
                        z => z.Children.Select(
                            e => e.Children.Select(p => p.Children)))
                )).Where(x => x.AssemblyId == assemblyId).ToList();

                _rootComponentEntity = allComponents.FirstOrDefault(x => x.Parent == null);


                Utils.TraverseEntityComponents(_rootComponentEntity, component =>
                {
                    if (!component.ExecutionTime.HasValue || !component.CountInStore.HasValue ||
                        string.IsNullOrEmpty(component.Volume))
                    {
                        throw new Exception("Проверьте компонент: "+component.Name);
                    }


                    var componentPlanning = new ComponentPlanning(component.Name)
                    {
                        IsMain = component.Parent == null,
                        LeadTime = component.ExecutionTime ?? 0,
                        LotSize = component.Volume,
                        ComponentName = component.Name,
                        StartAvailableBalance = component.CountInStore ?? 0
                    };
                    components.Add(componentPlanning);
                });

                //add links
                foreach (var component in allComponents)
                {
                    if (component.Parent == null) continue;

                    var parentName = component.Parent.Name;
                    var currentComponent = components.FirstOrDefault(x => x.ComponentName == component.Name);
                    var parentComponent = components.FirstOrDefault(x => x.ComponentName == parentName);
                    if (currentComponent != null)
                    {
                        currentComponent.Parent = parentComponent;
                    }
                    else
                    {
                        MessageBox.Show(@"Component not found");
                    }
                }

                var rootComponent = components.FirstOrDefault(x => x.Parent == null);

                InitAssemblyPlan(rootComponent, assemblyId);
            }
        }

        private void InitAssemblyPlan(ComponentPlanning rootComponent, int assemblyId)
        {
            using (var db = new DataContext())
            {
                var plan = db.MainPlans.Include(x => x.Assembly).FirstOrDefault(x => x.Assembly.Id == assemblyId);
                if (plan == null) return;

                rootComponent.Weeks[0].GrossRequirements = plan.Week1;
                rootComponent.Weeks[1].GrossRequirements = plan.Week2;
                rootComponent.Weeks[2].GrossRequirements = plan.Week3;
                rootComponent.Weeks[3].GrossRequirements = plan.Week4;
                rootComponent.Weeks[4].GrossRequirements = plan.Week5;
                rootComponent.Weeks[5].GrossRequirements = plan.Week6;
                rootComponent.Weeks[6].GrossRequirements = plan.Week7;
                rootComponent.Weeks[7].GrossRequirements = plan.Week8;
                rootComponent.Weeks[8].GrossRequirements = plan.Week9;
            }
        }

        public void AnalyseData()
        {

            var rootComponent = components.FirstOrDefault(x => x.Parent == null);
            
            if (rootComponent == null)
            {
                Debug.WriteLine("AnalyseData. Root component (ComponentPlanning) is null ");
            }
            else
            {
                rootComponent.MakeCalculation();
            }

            Utils.TraverseEntityComponents(_rootComponentEntity, currentComponent =>
            {
                if (currentComponent.Parent == null)
                {
                    Debug.WriteLine("Component parent is null");
                    return;
                }

                var componentPlanning = components.FirstOrDefault(x => x.ComponentName == currentComponent.Name);
                if (componentPlanning != null)
                {
                    componentPlanning.SetupGrossRequirements();
                    componentPlanning.MakeCalculation();
                }
                else
                {
                    Debug.WriteLine("AnalyseData.Component Planning for calculation not found");
                }
            });
   
        }

        public List<ComponentReport> MakeReport()
        {
            var report = new Report();
            var results = report.GetReport(components);

            return results;
        }

    
    }
}