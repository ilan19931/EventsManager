using EventsManager.Windows.Pop_Ups;
using EventsManagerLogic.Classes;
using EventsManagerLogic.WindowsHelpers;
using EventsManagerLogic.WindowsHelpers.AdminPanel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EventsManager.Windows.Admin_Panel
{
    /// <summary>
    /// Interaction logic for CategoriesPage.xaml
    /// </summary>
    public partial class CategoriesPage : Page
    {

        ObservableCollection<Category> categories;
        ObservableCollection<Category> subCategories;
        AddCategoriesWindowHelper windowHelper = new AddCategoriesWindowHelper();

        public CategoriesPage()
        {
            InitializeComponent();

            fetchCategories();
        }

        private void fetchCategories()
        {
            categories = windowHelper.GetCategories();

            listBoxCategories.ItemsSource = null;

            listBoxCategories.DisplayMemberPath = "Title";
            listBoxCategories.SelectedValuePath = "Id";
            listBoxCategories.ItemsSource = categories;
        }
        private void fetchSubCategories(int i_Val)
        {
            textSubCategories.Text = $"Sub-Categories ( {((Category)listBoxCategories.SelectedItem).Title} )";
            subCategories = windowHelper.GetCategories(i_Val);

            listBoxSubCategories.ItemsSource = null;

            listBoxSubCategories.DisplayMemberPath = "Title";
            listBoxSubCategories.SelectedValuePath = "Id";
            listBoxSubCategories.ItemsSource = subCategories;
            
        }

        private void listBoxCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBoxCategories.SelectedValue != null)
            {
                int categoryId = (int)listBoxCategories.SelectedValue;

                fetchSubCategories(categoryId);
            }
        }

        private void buttonAddCategory_Click(object sender, RoutedEventArgs e)
        {
            addNewCategory();
        }

        private void addNewCategory(int i_CategoryId = -1)
        {
            AddCategoryPopUpWindow newCategoryPopUp = new AddCategoryPopUpWindow();
            newCategoryPopUp.ShowDialog();

            if(newCategoryPopUp.IsSubmit == true && !string.IsNullOrEmpty(newCategoryPopUp.textCategory.Text))
            {
                windowHelper.AddNewCategory(newCategoryPopUp.textCategory.Text, i_CategoryId);

                if(i_CategoryId == -1)
                {
                    fetchCategories();
                }
                else
                {
                    fetchSubCategories(i_CategoryId);
                }
            }
        }

        private void buttonAddSubCategory_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxCategories.SelectedItem != null)
            {
                addNewCategory((int)listBoxCategories.SelectedValue);
            }
        }

        private void buttonEditCategory_Click(object sender, RoutedEventArgs e)
        {
            editCategory();
        }

        private void editCategory(int i_ParentId = -1)
        {
            string query = null;
            int categoryId = 0;

            if(listBoxSubCategories.SelectedItem != null)
            {
                string categoryTitle = ((Category)listBoxSubCategories.SelectedItem).Title;
                EditCategoryPopUpWindow editCategoryPopUp = new EditCategoryPopUpWindow(categoryTitle);
                editCategoryPopUp.ShowDialog();

                if (editCategoryPopUp.IsSubmit == true)
                {
                    string newTitle = editCategoryPopUp.textCategory.Text;

                    categoryId = (int)listBoxSubCategories.SelectedValue;
                    windowHelper.EditCategory(categoryId, newTitle);

                    int parentId = (int)listBoxCategories.SelectedValue;
                    fetchSubCategories(parentId);
                }
            }
            else if (listBoxCategories.SelectedItem != null)
            {
                string categoryTitle = ((Category)listBoxCategories.SelectedItem).Title;
                EditCategoryPopUpWindow editCategoryPopUp = new EditCategoryPopUpWindow(categoryTitle);
                editCategoryPopUp.ShowDialog();

                if (editCategoryPopUp.IsSubmit == true)
                {
                    string newTitle = editCategoryPopUp.textCategory.Text;
                    categoryId = (int)listBoxCategories.SelectedValue;
                    windowHelper.EditCategory(categoryId, newTitle);

                    fetchCategories();
                }
            }
        }

        private void buttonEditSubCategory_Click(object sender, RoutedEventArgs e)
        {
            editCategory();
        }

        private void buttonDeleteCategory_Click(object sender, RoutedEventArgs e)
        {
            this.deleteCategory();
            listBoxSubCategories.ItemsSource = null;
        }

        private void buttonDeleteSubCategory_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxSubCategories.SelectedItem != null)
            {
                this.deleteCategory();
            }
        }


        private void deleteCategory()
        {
            // delete category
            if(listBoxCategories.SelectedItem != null && listBoxSubCategories.SelectedItem == null)
            {
                int categoryId = (int)listBoxCategories.SelectedValue;
                windowHelper.DeleteCategory(categoryId);

                fetchCategories();
            }
            // delete sub-category
            else if(listBoxSubCategories.SelectedItem != null && listBoxCategories.SelectedItem != null)
            {
                int categoryId = (int)listBoxSubCategories.SelectedValue;
                int parentId = (int)listBoxCategories.SelectedValue;
                windowHelper.DeleteCategory(categoryId);

                listBoxSubCategories.SelectedItem = null;
                fetchSubCategories(parentId);
            }
        }
    }
}
