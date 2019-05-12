using System;
using System.Collections.Generic;
using System.Linq;
using MRP.Entities;
using MRP.Model;

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

        private static void TraverseTree(Component parent, ICollection<Component> components)
        {
            if (parent.Children == null || parent.Children.Count <= 0) return;
            foreach (var child in parent.Children)
            {
                components.Add(child);
                TraverseTree(child,components);
            }
        }

        public static void TraverseEntityComponents(Component component, Action<Component> action)
        {
            action(component);
            if (component.Children == null) return;
            foreach (var child in component.Children)
            {
                TraverseEntityComponents(child, action);
            }
        }

        public static void TraverseComponentTree(ComponentPlanning component, Action<ComponentPlanning> action)
        {
            action(component);
            if (component.Children == null) return;
            
                foreach (var child in component.Children)
                {
                    TraverseComponentTree(child, action);
                }
            
        }

        public static List<Component> GetChildrenComponents(Component component)
        {
            return component.Children.ToList();
        }

        public static Component FindComponentByName(List<Component> components, string name)
        {
            return components.FirstOrDefault(x => x.Name == name);
        }

    }
}
