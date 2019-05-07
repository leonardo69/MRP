using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MRP.Entities;

namespace MRP.Core
{
    public static class Utils
    {
        public static List<Component> GetTreeComponents(Component parent)
        {
            var components = new List<Component> {parent};
            TraverseTree(parent, components);
            return components;
        }

        private static void TraverseTree(Component parent, List<Component> components)
        {
            if (parent.Children == null || parent.Children.Count <= 0) return;
            foreach (var child in parent.Children)
            {
                components.Add(child);
                TraverseTree(child,components);
            }
        }
    }
}
