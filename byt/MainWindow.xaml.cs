using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace TwojaAplikacja
{
    public partial class MainWindow : Window
    {
        private List<Category> categories;
        private List<Subcategory> subcategories;
        private List<Element> elements;

        public MainWindow()
        {
            InitializeComponent();
            InitializeData();
        }

        private void InitializeData()
        {
            // Inicjalizacja przykładowych danych (kategorie, podkategorie, elementy)
            categories = new List<Category>
            {
                new Category { Name = "Samochody" },
                new Category { Name = "Motocykle" },
            };

            subcategories = new List<Subcategory>
            {
                new Subcategory { Name = "Osobowe", ParentCategory = categories[0] },
                new Subcategory { Name = "Ciezarowe", ParentCategory = categories[0] },
                new Subcategory { Name = "Sportowe", ParentCategory = categories[1] },
            };

            elements = new List<Element>
            {
                new Element { Name = "Audi", Subcategory = subcategories[0] },
                new Element { Name = "BMW", Subcategory = subcategories[0] },
                new Element { Name = "Mercedes", Subcategory = subcategories[0] },
                new Element { Name = "Opel", Subcategory = subcategories[0] },
                new Element { Name = "Man", Subcategory = subcategories[1] },
                new Element { Name = "Star", Subcategory = subcategories[1] },
                new Element { Name = "Iveco", Subcategory = subcategories[1] },
                new Element { Name = "Yamaha", Subcategory = subcategories[2] },
                new Element { Name = "Suzuki", Subcategory = subcategories[2] },
                new Element { Name = "Honda", Subcategory = subcategories[2] },
                new Element { Name = "Kawasaki", Subcategory = subcategories[2] },
            };

            // Wypełnienie list widoków
            CategoryListView.ItemsSource = categories;
        }

        private void CategoryListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Obsługa wyboru kategorii
            SubcategoryListView.ItemsSource = null;
            ElementListView.ItemsSource = null;
            DetailsPanel.Visibility = Visibility.Collapsed;

            if (CategoryListView.SelectedItem != null)
            {
                Category selectedCategory = (Category)CategoryListView.SelectedItem;
                SubcategoryListView.ItemsSource = GetSubcategories(selectedCategory);
            }
        }

        private void SubcategoryListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Obsługa wyboru podkategorii
            ElementListView.ItemsSource = null;
            DetailsPanel.Visibility = Visibility.Collapsed;

            if (SubcategoryListView.SelectedItem != null)
            {
                Subcategory selectedSubcategory = (Subcategory)SubcategoryListView.SelectedItem;
                ElementListView.ItemsSource = GetElements(selectedSubcategory);
            }
        }

        private void ElementListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Obsługa wyboru elementu
            DetailsPanel.Visibility = Visibility.Visible;

            if (ElementListView.SelectedItem != null)
            {
                Element selectedElement = (Element)ElementListView.SelectedItem;
                DetailsText.Text = $"Nazwa: {selectedElement.Name}\nOpis: {selectedElement.Description}";
            }
        }

        private List<Subcategory> GetSubcategories(Category category)
        {
            // Pobierz podkategorie dla wybranej kategorii
            List<Subcategory> result = new List<Subcategory>();
            foreach (Subcategory subcategory in subcategories)
            {
                if (subcategory.ParentCategory == category)
                {
                    result.Add(subcategory);
                }
            }
            return result;
        }

        private List<Element> GetElements(Subcategory subcategory)
        {
            // Pobierz elementy dla wybranej podkategorii
            List<Element> result = new List<Element>();
            foreach (Element element in elements)
            {
                if (element.Subcategory == subcategory)
                {
                    result.Add(element);
                }
            }
            return result;
        }
    }

    public class Category
    {
        public string Name { get; set; }
    }

    public class Subcategory
    {
        public string Name { get; set; }
        public Category ParentCategory { get; set; }
    }

    public class Element
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Subcategory Subcategory { get; set; }
    }
}
