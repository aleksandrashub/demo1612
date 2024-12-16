using Avalonia.Controls;
using Metsys.Bson;
using Microsoft.EntityFrameworkCore;
using MsBox.Avalonia;
using shubenok1212.Models;
using System.Collections.Generic;
using System.Linq;

namespace shubenok1212
{
    public partial class MainWindow : Window
    {
        List<string> manufs = Helper.context.Manufs.Select(x => x.Name).ToList();
        List<Prod> prods = Helper.context.Prods.Include(x => x.IdManufNavigation).Include(x => x.IdActivNavigation).ToList();
        public MainWindow()
        {
            InitializeComponent();
            List<string> manufs = new List<string>();
            manufs.Add("Все элементы");
            List<string> mans = Helper.context.Manufs.Select(x => x.Name).ToList();
            manufs.AddRange(mans);
            manufCb.ItemsSource = manufs;
            update();
        }

        public void update()
        {

            var list = Helper.context.Prods.Include(x => x.IdManufNavigation).Include(x => x.IdActivNavigation).ToList();

            if (manufCb.SelectedItem != null && manufCb.SelectedItem != "Все элементы")
            {
                var man = manufCb.SelectedItem as string;
                Manuf manuf = Helper.context.Manufs.Where(x => x.Name == man).First();
                list = prods.Where(x => x.IdManuf == manuf.IdManuf).ToList();

            }


            switch (costCb.SelectedIndex)
            {
                case 0:
                    list = list.OrderByDescending(x => x.Cost).ToList();
                    break;

                case 1:
                    list = list.OrderBy(x => x.Cost).ToList();
                    break;
                default: break;
            }


            string searchText = textbox.Text ?? "";
            int count = searchText.Split(' ').Length;
            string[] values = new string[count];
            values = searchText.Split(new char[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);

            foreach (string value in values)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    list = list.Where(x => x.Name.ToLower().Contains(value.ToLower()) || x.Descr.ToLower().Contains(value.ToLower())).ToList();
                }
                else
                {
                    continue;
                }

            }
            listbox.ItemsSource = list;


        }

        private void TextBox_KeyUp(object? sender, Avalonia.Input.KeyEventArgs e)
        {
            update();
        }

        private void sort_SelectionChanged(object? sender, Avalonia.Controls.SelectionChangedEventArgs e)
        {
            update();
        }

        private void filter_SelectionChanged(object? sender, Avalonia.Controls.SelectionChangedEventArgs e)
        {
            update();
        }

        private void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            AddNew add = new AddNew();
            add.Show();
            this.Close();


        }

        private void delButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            int ind = (int)((sender as Button)!).Tag!;
            Prod prod = Helper.context.Prods.Where(x => x.IdProd == ind).First();
            if (Helper.context.Sales.Where(x => x.IdProd == ind).Count() > 0)
            {
                var ms = MessageBoxManager.GetMessageBoxStandard("Ошибка", "Нельзя удалить данный товар, сначала удалите заказы с ним");
                ms.ShowAsync();
            }
            else
            {
                prod.IdDops.Clear();
                Helper.context.SaveChanges();
                Helper.context.Prods.Remove(prod);
                Helper.context.SaveChanges();
                update();

            }


        }

        private void editButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            int ind = (int)((sender as Button)!).Tag!;
            Prod prod = Helper.context.Prods.Where(x => x.IdProd == ind).First();
            AddNew addnew = new AddNew(prod!);
             
        }
    }



}
