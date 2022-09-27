using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Media3D;
using System.Windows.Media;

namespace TreeListView
{
    public class TreeListViewItem : TreeViewItem
    {
        static TreeListViewItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TreeListViewItem),
                new FrameworkPropertyMetadata(typeof(TreeListViewItem)));
        }

        public int Depth
        {
            get 
            {
                var parent = ParentItem;
                return parent == null ? 0 : parent.Depth + 1;
            }
        }

        protected TreeListView ParentTreeListView
        {
            get
            {
                ItemsControl parent = ItemsControlFromItemContainer(this);
                while (parent != null)
                {
                    if (parent is TreeListView tv)
                        return tv;

                    parent = ItemsControlFromItemContainer(parent);
                }

                return null;
            }
        }

        protected TreeListViewItem ParentItem
        {
            get
            {
                var itemsCtrl = ItemsControlFromItemContainer(this);
                return itemsCtrl as TreeListViewItem;
            }
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new TreeListViewItem();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is TreeListViewItem;
        }
    }
}