using System;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;
using MRP.Core;
using MRP.Entities;
using Telerik.WinControls.UI;

namespace MRP.GUI
{
    public partial class AssemblyForm : RadForm
    {
        private int _selectedSpecificationId;
        private int _selectedRootComponentId;

        public AssemblyForm()
        {
            InitializeComponent();
            LoadAssemblies();
        }

        private void LoadAssemblies()
        {
            using (var db = new DataContext())
            {
                var assemblies = db.Specifications.Include(x => x.StartComponent).ToList();
                if (assemblies.Count > 0)
                    foreach (var x in assemblies)
                    {
                        listView1.Items.Add(new ListViewItem {
                            Text = x.Name,
                            Tag = new SelectedAssembly
                            {
                                AssemblyId = x.Id,
                                RootComponentId = x.StartComponent.Id
                            }
                        });
                    }
            }
        }

        private void ClearAssemblies()
        {
            listView1.Clear();
        }

        private void CreateTree()
        {
            using (var db = new DataContext())
            {
                var treeComponent = db.Components
                    .Include(x => x.Children.Select(
                        y => y.Children.Select(
                            z => z.Children.Select(
                                e => e.Children.Select(p => p.Children)))
                    ))
                    .FirstOrDefault(x => x.Id == _selectedRootComponentId);

                var node = new RadTreeNode {Value = null, Expanded = true, Text = "Root"};
                radTreeView1.Nodes.Add(node);
                CreateLeafs(node, treeComponent);
            }
        }

        private void CreateLeafs(RadTreeNode parentNode, Component component)
        {
            var componentNode = new RadTreeNode(component.Name) {Value = component, Expanded = true};
            parentNode.Nodes.Add(componentNode);
            if (component.Children != null)
                foreach (var children in component.Children)
                    CreateLeafs(componentNode, children);
        }

        private void AddComponent_Click(object sender, EventArgs e)
        {
            if (radTreeView1.SelectedNode?.Value == null) return;

            var addComponentForm = new AddComponentForm();
            addComponentForm.OnAddNewComponent += OnAddNewComponent;
            addComponentForm.Show();
        }

        private void OnAddNewComponent(object sender, AddComponentEventArgs e)
        {
            var component = (Component) radTreeView1.SelectedNode.Value;
            using (var db = new DataContext())
            {
                var parent = db.Components.Include(x => x.Children).FirstOrDefault(x => x.Id == component.Id);
                if (parent != null)
                {
                    var newComponent = new Component
                        {Name = e.ComponentName, AssemblyId = _selectedSpecificationId, Unit = "шт"};
                    parent.Children.Add(newComponent);
                    db.SaveChanges();
                    ClearTree();
                    CreateTree();
                }
                else
                {
                    MessageBox.Show(@"Компонент не найден");
                }
            }
        }

        private void ClearTree()
        {
            radTreeView1.DataSource = null;
            radTreeView1.Nodes.Clear();
        }

        private void DeleteComponent_Click(object sender, EventArgs e)
        {
            var component = (Component) radTreeView1.SelectedNode?.Value;
            if (component?.Parent == null) return;
            using (var db = new DataContext())
            {
                var parent = db.Components.Include(x => x.Children).FirstOrDefault(x => x.Id == component.Id);
                if (parent != null)
                {
                    var treeComponents = Utils.GetTreeComponents(parent);
                    treeComponents.Sort((a, b) => a.Id > b.Id ? 1 : -1);
                    db.Components.RemoveRange(treeComponents);
                    db.SaveChanges();
                    ClearTree();
                    CreateTree();
                }
                else
                {
                    MessageBox.Show(@"Компонент не найден");
                }
            }
        }


        private void DeleteAssembly_Click(object sender, EventArgs e)
        {
            var addAssemblyForm = new AddAssemblyForm();
            addAssemblyForm.OnAddNewAssembly += (sender1, e1) =>
            {
                AddNewAssembly(e1.AssemblyName);
                ClearAssemblies();
                LoadAssemblies();
                ClearTree();
            };
            addAssemblyForm.Show();
        }

        private void AddNewAssembly(string name)
        {
            using (var db = new DataContext())
            {
                var specification = new Assembly
                {
                    Name = name
                };

                db.Specifications.Add(specification);
                db.SaveChanges();

                var rootComponent = new Component
                {
                    Name = name,
                    Unit = "шт",
                    AssemblyId = specification.Id
                };

                specification.StartComponent = rootComponent;

                db.Entry(specification).State = EntityState.Modified;
                db.SaveChanges();

                var mainPlan = new MainPlan {Assembly = specification};

                db.MainPlans.Add(mainPlan);
                db.SaveChanges();
            }
        }


        private void Assembly_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count <= 0) return;

            var selectedAssembly = (SelectedAssembly) listView1.SelectedItems[0].Tag;
            _selectedSpecificationId = selectedAssembly.AssemblyId;
            _selectedRootComponentId = selectedAssembly.RootComponentId;
            ClearTree();
            CreateTree();
        }
    }

    public class SelectedAssembly
    {
        public int RootComponentId { get; set; }
        public int AssemblyId { get; set; }
    }
}