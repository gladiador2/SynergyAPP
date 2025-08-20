using Microsoft.Maui.Controls;
using System.Linq;

namespace CBA_app.Views.GIO
{
    public class AlternatingColorTemplateSelector : DataTemplateSelector
    {
        public DataTemplate EvenTemplate { get; set; }
        public DataTemplate OddTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var listView = container as ListView;
            var index = listView.ItemsSource.Cast<object>().ToList().IndexOf(item);
            return index % 2 == 0 ? EvenTemplate : OddTemplate;
        }
    }
}


