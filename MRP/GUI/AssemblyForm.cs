using System;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;
using MRP.Entities;
using Telerik.WinControls.UI;

namespace MRP.GUI
{
    public partial class AssemblyForm : RadForm
    {
        private int _selectedSpecificationId;

        public AssemblyForm()
        {
            InitializeComponent();
            LoadAssemblies();
        }

        private void LoadAssemblies()
        {
            using (var db = new DataContext())
            {
                var assemblies = db.Specifications.ToList();
                if (assemblies.Count > 0)
                {
                    radListView1.DataSource = assemblies;
                    radListView1.DisplayMember = "Name";
                    radListView1.ValueMember = "Id";
                }
            }
        }

        private void ClearAssemblies()
        {
            radTreeView1.DataSource = null;
        }

        private void radListView1_SelectedItemChanged(object sender, EventArgs e)
        {
            var selectedItem = (ListViewItemEventArgs) e;
            var assemblyObject = selectedItem.Item.Value;
            if (assemblyObject == null) return;

            if (assemblyObject is int id)
            {
                _selectedSpecificationId = id;
            }else if (assemblyObject is Entities.Specification specification)
            {
                _selectedSpecificationId = specification.Id;
            }
            
            CreateTree();
        }

        private void CreateTree()
        {
            using (var db = new DataContext())
            {
                var treeComponent = db.Components
                    .Include(x => x.Childrens.Select(y => y.Childrens))
                    .FirstOrDefault(x => x.Id == _selectedSpecificationId);

                var node = new RadTreeNode {Value = null, Expanded = true, Text = "Root"};
                radTreeView1.Nodes.Add(node);
                CreateLeafs(node, treeComponent);
            }
        }


        private void CreateLeafs(RadTreeNode parentNode, Component component)
        {
            var componentNode = new RadTreeNode(component.Name) {Value = component, Expanded = true};
            parentNode.Nodes.Add(componentNode);
            if (component.Childrens != null)
                foreach (var children in component.Childrens)
                    CreateLeafs(componentNode, children);
        }

        private void radTreeView1_SelectedNodeChanged(object sender, RadTreeViewEventArgs e)
        {
        }

        private void radButton3_Click(object sender, EventArgs e)
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
                var parent = db.Components.Include(x=>x.Childrens).FirstOrDefault(x => x.Id == component.Id);
                if (parent != null)
                {
                    var newComponent = new Component {Name = e.ComponentName, SpecificationId = _selectedSpecificationId, Unit = "шт"};
                    parent.Childrens.Add(newComponent);
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

        private void radButton4_Click(object sender, EventArgs e)
        {
            if (radTreeView1.SelectedNode?.Value == null) return;
            var component = (Component)radTreeView1.SelectedNode.Value;
            using (var db = new DataContext())
            {
                var parent = db.Components.Include(x => x.Childrens).FirstOrDefault(x => x.Id == component.Id);
                if (parent != null)
                {
                    db.Components.Remove(parent);
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

        private void radButton1_Click(object sender, EventArgs e)
        {
            var addAssemblyForm = new AddAssemblyForm();
            addAssemblyForm.OnAddNewAssembly +=OnAddNewAssembly;
            addAssemblyForm.Show();
        }

        private void OnAddNewAssembly(object sender, AddAssemblyEventArgs e)
        {
            AddNewAssembly(e.AssemblyName);
            ClearAssemblies();
            LoadAssemblies();
        }

        private void AddNewAssembly(string name)
        {
            using (var db = new DataContext())
            {
                var rootComponent = new Component
                {
                    Name = name,
                    Unit = "шт"
                };

                var firstSpecification = new Entities.Specification
                {
                    Name = name,
                    StartComponent = rootComponent
                };

                db.Specifications.Add(firstSpecification);
                db.SaveChanges();
            }
        }
    }
}